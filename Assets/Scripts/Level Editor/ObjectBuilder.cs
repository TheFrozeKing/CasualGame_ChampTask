using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectBuilder : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectPrefabs;
    [SerializeField] private Transform _finishPrefab;
    private Transform _finish;

    private Stack<GameObject> _objects = new();
    private Camera _camera => Camera.main;
    private Ray _ray => _camera.ScreenPointToRay(Input.mousePosition);

    private int _prefabIndex = -1;
    private bool _isOffset = false;
    private List<int> _nonWalkableIds = new() { 1, 9, 5, 7, 11, 14, 6, 8 };
    private enum Mode
    {
        Editor,
        PlayMode,
    }

    [SerializeField] private Mode _mode;
    private void Update()
    {
        if(_mode != Mode.Editor)
        {
            return;
        }

        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
        {
            Undo();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Rotate();
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Place();
        }

    }

    private void Place()
    {
        if(!Physics.Raycast(_ray, out RaycastHit hit, 5000))
        {
            return;
        }
        if(hit.collider.gameObject.name != "Raycast Quad")
        {
            return;
        }
        
        GameObject newObject = Instantiate(_objectPrefabs[_prefabIndex], hit.point, Quaternion.identity, null);
        newObject.name = _objectPrefabs[_prefabIndex].name;
        _objects.Push(newObject);

        //PlaceFinishLine(newObject.transform, _prefabIndex, out bool hasPlaced);
        if (_isOffset)
        {
            newObject.transform.position += new Vector3(0, 0, 4);
        }
    }

    public void Place(List<ObjectData> objects)
    {
        int count = _objects.Count;
        for(int i = 0; i < count; i++)
        {
            Destroy(_objects.Pop());
        }


        bool placedFinish = false;
        foreach(var obj in objects) 
        {
            GameObject newObject = Instantiate(_objectPrefabs[obj.ObjectType], obj.Position.ConvertTo(), Quaternion.Euler(obj.Rotation.ConvertTo()), null);
            newObject.transform.localScale = obj.Scale.ConvertTo();
            newObject.name = _objectPrefabs[obj.ObjectType].name;
            _objects.Push(newObject);
            if (!placedFinish)
            {
                PlaceFinishLine(newObject.transform, obj.ObjectType, out placedFinish);
            }
        }

        ReverseStack(ref _objects);
    }

    public void PlaceFinishLine(Transform placedObject, int id, out bool hasPlaced)
    {
        if (_nonWalkableIds.Contains(id))
        {
            hasPlaced = false;
            return;
        }
        if (_finish)
        {
            Destroy(_finish.gameObject);
        }
        _finish = Instantiate(_finishPrefab, null);

        hasPlaced = true;
        _finish.position = placedObject.GetChild(0).position;  

    }

    public List<GameObject> GetAllObjects()
    {
        // Finish placement
        Stack<GameObject> copyStack = new(_objects);
        ReverseStack(ref copyStack);
        bool hasPlaced = false;
        while (!hasPlaced)
        {
            GameObject objectToCheck = copyStack.Pop();
            PlaceFinishLine(objectToCheck.transform, (int)ObjectDeterminator.DetermineType(objectToCheck), out hasPlaced);
            if (hasPlaced)
            {
                break;
            }
        }

        // Object Return
        List<GameObject> data = new(_objects);
        return data;
    }

    public void ChooseObject(int index)
    {
        _prefabIndex = index;
    }

    public void SetIsOffset(bool isOffset)
    {
        _isOffset = isOffset;
    }

    public void Rotate()
    {
        try
        {
            Transform lastObject = _objects.Peek().transform;
            lastObject.rotation = Quaternion.Euler(lastObject.rotation.eulerAngles + new Vector3(0,0,45));
        }
        catch
        {

        }
    }

    public void Undo()
    {
        try
        {
            Destroy(_objects.Pop());
        }
        catch
        {

        }
    }

    public void ReverseStack<T>(ref Stack<T> toReverse)
    {
        Stack<T> tempStack = new Stack<T>();
        while(toReverse.Count > 0)
        {
            tempStack.Push(toReverse.Pop());
        }
        toReverse = tempStack;
        /*Stack<GameObject> tempStack = new();
        while(_objects.Count > 0)
        {
            tempStack.Push(_objects.Pop());
        }
        _objects = tempStack;*/
    }
}
