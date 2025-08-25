using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDrag : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 20;
    [SerializeField] private LayerMask _quadLayer;

    [field:SerializeField] public int AbilityId { get; private set; }

    public static event Action<int> DragStarted;
    public static event Action DragEnded;

    private Camera _camera => Camera.main;
    private Ray _ray => _camera.ScreenPointToRay(Input.mousePosition);

    private void OnMouseDown()
    {
        StopAllCoroutines();

        DragStarted?.Invoke(AbilityId);
    }
    private void OnMouseDrag()
    {
        if (!Physics.Raycast(_ray, out RaycastHit hit, 500, _quadLayer))
        {
            return;
        }
        if(hit.collider.gameObject.name != "Quad To Hit")
        {
            return;
        }
        transform.position = hit.point;
    }

    private void OnMouseUp()
    {
        StartCoroutine(MoveBack());
        DragEnded?.Invoke();
    }


    private IEnumerator MoveBack()
    {
        while(transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, _moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
