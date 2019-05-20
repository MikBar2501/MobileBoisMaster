using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class SaveingManager : MonoBehaviour
{
    public static SaveingManager main;

    public static PlayerData data;

    void Awake()
    {
        main = this;
    }

    private void Start()
    {
        Load();
    }

    public void DeleteData()
    {
        data = new PlayerData();
        data.points = 0;
        data.size = 0;
    }

    public byte[] GetDataBytes()
    {
        data = new PlayerData();

        CaveGenerator.main.Save(data);
        PlayerAccel.main.Save(data);

        return DataToByte(data);
    }

    public void LoadDataBytes(byte[] bytes)
    {
        data = ByteToData(bytes);

        CaveGenerator.main.Load(data);
        PlayerAccel.main.Load(data);
    }

    public void Load()
    {
        byte[] bytes = new byte[0]; //Tutaj zczytaj bytesy z google services

        if(bytes.Length > 0)
            LoadDataBytes(bytes);
        else
        {
            CaveGenerator.main.Load(data);
            PlayerAccel.main.Load(data);
        }
    }

    public void Save()
    {
        if (PlayerAccel.lose)
        {
            print("LOSE");
            DeleteData();
            return;
        }

        byte[] bytes = GetDataBytes();

        //Tutaj zapisz bytesy na google services
    }

    byte[] DataToByte(PlayerData data)
    {
        BinaryFormatter bf = new BinaryFormatter();

        byte[] bytes;
        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream())
        {
            formatter.Serialize(stream, data);
            bytes = stream.ToArray();
        }

        return bytes;
    }

    PlayerData ByteToData(byte[] bytes)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream(bytes))
        {
            return (PlayerData)formatter.Deserialize(stream);
        }
    }

}
