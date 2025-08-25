using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour, IPointerClickHandler
{
    private AudioSource _source;
    private void Awake()
    {
        _source = GameObject.Find("/Button Sound").GetComponent<AudioSource>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _source.Play();
    }

}
