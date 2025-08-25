using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCamera : MonoBehaviour
{
    [SerializeField] private float _speed;
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(horizontal, vertical, 0) * Time.deltaTime * _speed;
    }
}
