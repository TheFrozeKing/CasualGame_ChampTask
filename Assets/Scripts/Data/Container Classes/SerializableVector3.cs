using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableVector3
{
    public float X;
    public float Y; 
    public float Z;

    public static SerializableVector3 ConvertFrom(Vector3 from)
    {
        return new SerializableVector3() { X = from.x, Y = from.y, Z = from.z };
    }

    public Vector3 ConvertTo()
    {
        return new Vector3(X, Y, Z);
    }
}
