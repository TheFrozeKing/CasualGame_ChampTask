using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    [SerializeField] private List<int> _connectableAbilityIds;
    [SerializeField] private GameObject _visual;
    private Collider _collider;
    private Collision _currentCollision;
    private Car _master;
    private Camera _camera => Camera.main;
    private bool _canConnect = true;
    private int _lastDraggedAbilityId;

    private void Awake()
    {
        _master = transform.parent.GetComponent<Car>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        transform.LookAt(_camera.transform);
        AbilityDrag.DragStarted += OnDragStarted;
        AbilityDrag.DragEnded += OnDragEnded;
    }

    private void OnDestroy()
    {
        AbilityDrag.DragStarted -= OnDragStarted;
        AbilityDrag.DragEnded -= OnDragEnded;
    }

    public void OnDragStarted(int id)
    {
        if (!_canConnect)
        {
            return;
        }
        if (!_connectableAbilityIds.Contains(id))
        {
            _visual.SetActive(false);
            _collider.enabled = false;
        }
        _lastDraggedAbilityId = id;
    }

    public void OnDragEnded()
    {
        _visual.SetActive(true);
        _collider.enabled=true;
    }
    private void OnCollisionStay(Collision collision)
    {
        _currentCollision = collision;        
    }
    private void OnCollisionExit(Collision collision)
    {
        _currentCollision = null;
    }

    public void CheckIfShouldExist(List<int> abilityIds)
    {
        bool shouldExist = false;
        foreach(int connectable in _connectableAbilityIds)
        {
            if (abilityIds.Contains(connectable))
            {
                shouldExist = true;
                break;
            }
        }
        if (!shouldExist)
        {
            _master.RemoveFromSnapPoints(this);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && _currentCollision != null)
        {
            _master.SpawnAbility(_lastDraggedAbilityId, transform);
            _master.RemoveFromSnapPoints(this);
            Destroy(_currentCollision.gameObject);
            Destroy(gameObject);
        }
    }


}
