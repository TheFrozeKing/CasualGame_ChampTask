using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFrameHandler : MonoBehaviour
{
    [SerializeField] private Transform _framePrefab;
    public void SpawnFrames(List<Transform> abilities)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for(int i = 0; i < abilities.Count; i++)
        {
            Transform newFrame = Instantiate(_framePrefab, transform);
            newFrame.transform.localRotation = Quaternion.identity;
            Transform newAbility = Instantiate(abilities[i], newFrame);

            newAbility.transform.localPosition = Vector3.zero;
            newAbility.transform.localRotation = Quaternion.identity;

        }
    }
}
