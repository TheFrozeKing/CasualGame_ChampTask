using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletedPanel;
    private void Start()
    {
        EnabledEventSender[] levelPanels = FindObjectsOfType<EnabledEventSender>(true);
        foreach(EnabledEventSender levelPanel in levelPanels)
        {
            if(levelPanel.name == "Level Completed Panel")
            {
                _levelCompletedPanel = levelPanel.gameObject;
                return;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Car>())
        {
            return;
        }
        collision.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        _levelCompletedPanel.SetActive(true);
        _levelCompletedPanel.transform.parent.Find("Ability Panel").gameObject.SetActive(false);
    }
}
