using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HintPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _hintObject;
    private Coroutine _hintCoroutine;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _hintCoroutine = StartCoroutine(ShowHint());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(_hintCoroutine);
        _hintObject.SetActive(false);
    }

    public IEnumerator ShowHint()
    {
        yield return new WaitForSeconds(2);
        _hintObject.SetActive(true);
    }


}
