using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserDataHandler : MonoBehaviour
{
    public UserData Data;
    
    private void Awake()
    {
        UserDataHandler[] userDataHandlers = FindObjectsOfType<UserDataHandler>();
        if(userDataHandlers.Length > 1)
        {
            Destroy(this);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        

        if (!Directory.Exists("Resources"))
        {
            Directory.CreateDirectory("Resources");
        }

        Data = new();

        try
        {
            LoadData();
        }
        catch
        {
            SaveData();
        }
    }

    public void SaveData()
    {
        DataHandler.WriteXml(Data, nameof(UserData));
    }

    public void LoadData()
    {
        Data = DataHandler.ReadXml<UserData>(nameof(UserData));
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

}
