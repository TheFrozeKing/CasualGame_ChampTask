using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectDeterminator
{
    public static ObjectType DetermineType(GameObject obj)
    {
        return (ObjectType)Enum.Parse(typeof(ObjectType), obj.name);
    }
}
