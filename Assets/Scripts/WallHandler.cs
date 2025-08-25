using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallHandler : MonoBehaviour
{
    [SerializeField] private Material[] _wallMaterials;
    [SerializeField] private List<MeshRenderer> _wallRenderers;
    private void Start()
    {
        WallHandler[] wallHandlers = FindObjectsOfType<WallHandler>();
        if (wallHandlers.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void ChangeWallMaterial(Toggle toggle)
    {
        if (!toggle.isOn)
        {
            return;
        }
        int index = toggle.transform.parent.GetSiblingIndex();
        foreach (MeshRenderer renderer in _wallRenderers)
        {
            renderer.material = _wallMaterials[index];
        }
    }

}
