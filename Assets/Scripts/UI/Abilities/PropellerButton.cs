using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PropellerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Propeller _propeller;
    public void OnPointerDown(PointerEventData eventData)
    {
        _propeller.Use();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _propeller.StopUsing();
    }

    private void OnEnable()
    {
        _propeller = FindObjectOfType<Propeller>();
    }


}
