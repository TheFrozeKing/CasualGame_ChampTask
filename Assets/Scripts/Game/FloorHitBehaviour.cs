using UnityEngine;

public class FloorHitBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _levelFailedPanel;
    [SerializeField] private GameObject _abilityPanel;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.GetComponent<Car>())
        {
            return;
        }
        _levelFailedPanel.SetActive(true);
        _abilityPanel.SetActive(false);
    }
}
