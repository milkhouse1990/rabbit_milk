using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using UnityEngine;


//GameData,储存数据的类，把需要储存的数据定义在GameData之内就行//
public class GameData
{
    //密钥,用于防止拷贝存档//
    public string key;

    //下面是添加需要储存的内容//
    public string PlayerName;
    public float MusicVolume;
    public GameData()
    {
        PlayerName = "Player";
        MusicVolume = 0.6f;
    }
}
//管理数据储存的类//
public class GameDataManager : MonoBehaviour
{
    private string dataFileName = "save1.sav";//存档文件的名称,自己定//
    private XmlSaver xs = new XmlSaver();

    public GameData gameData;

    public void Awake()
    {
        gameData = new GameData();

        //设定密钥，根据具体平台设定//
        gameData.key = SystemInfo.deviceUniqueIdentifier;
        Load();
    }

    //存档时调用的函数//
    public void Save()
    {
        string gameDataFile = GetDataPath() + "/" + dataFileName;
        string dataString = xs.SerializeObject(gameData, typeof(GameData));
        xs.CreateXML(gameDataFile, dataString);
    }

    //读档时调用的函数//
    public void Load()
    {
        string gameDataFile = GetDataPath() + "/" + dataFileName;
        if (xs.hasFile(gameDataFile))
        {
            string dataString = xs.LoadXML(gameDataFile);
            GameData gameDataFromXML = xs.DeserializeObject(dataString, typeof(GameData)) as GameData;

            //是合法存档//
            if (gameDataFromXML.key == gameData.key)
            {
                gameData = gameDataFromXML;
            }
            //是非法拷贝存档//
            else
            {
                //留空：游戏启动后数据清零，存档后作弊档被自动覆盖//
            }
        }
        else
        {
            if (gameData != null)
                Save();
        }
    }

    //获取路径//
    private static string GetDataPath()
    {
        //return Application.dataPath;
        return "Save";
    }
}