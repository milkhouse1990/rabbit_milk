using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//GameData,储存数据的类，把需要储存的数据定义在GameData之内就行//
public class GameData
{
    //密钥,用于防止拷贝存档//
    public string key;

    //下面是添加需要储存的内容//
    public string Position;
    public float MusicVolume;
    public GameData()
    {
        Position = "Player";
        MusicVolume = 0.6f;
    }
}
//管理数据储存的类//
public class GameDataManager : MonoBehaviour
{
    private string dataFileName = "save";//存档文件的名称,自己定//
    private XmlSaver xs = new XmlSaver();
    private RectTransform[] go_datainfo;

    public GameData gameData;

    public RectTransform datainfo;
    void Start()
    {/*
        go_datainfo = new Transform[3];
        for (int i = 0; i < 2; i++)
        {
            go_datainfo[i]=Instantiate(datainfo, transform);
            go_datainfo[i].localPosition = new Vector2(85, 220-220*i);
        }
        */
           
    }
    public void Awake()
    {
        go_datainfo = new RectTransform[3];
        for (int i = 0; i < 3; i++)
        {
            go_datainfo[i] = Instantiate(datainfo, transform);
            go_datainfo[i].localPosition = new Vector2(85, 220 - 220 * i);
        }

        gameData = new GameData();

        //设定密钥，根据具体平台设定//
        gameData.key = SystemInfo.deviceUniqueIdentifier;
        for (int i=0;i<3;i++)
        Check(i);
    }
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("A"))
        {
            int current = GetComponent<ListTool>().GetFocus();
            if (PlayerPrefs.GetInt("SaveFlag") == 1)
            {
                Save(current);
                Check(current);
            }
            else
            {
                Load(current);
            }
        }
    }


    //存档时调用的函数//
    public void Save(int current)
    {
        string gameDataFile = GetDataPath() + "/" + dataFileName+current.ToString()+".sav";
        gameData.Position = SceneManager.GetActiveScene().name;
        string dataString = xs.SerializeObject(gameData, typeof(GameData));
        xs.CreateXML(gameDataFile, dataString);
    }

    //读档时调用的函数//
    public void Load(int current)
    {
        string gameDataFile = GetDataPath() + "/" + dataFileName + current.ToString() + ".sav";
        if (xs.hasFile(gameDataFile))
        {
            string dataString = xs.LoadXML(gameDataFile);
            GameData gameDataFromXML = xs.DeserializeObject(dataString, typeof(GameData)) as GameData;

            //是合法存档//
            if (gameDataFromXML.key == gameData.key)
            {
                gameData = gameDataFromXML;
                SceneManager.LoadScene(gameData.Position);
            }
            //是非法拷贝存档//
            else
            {
                Debug.Log("save file broken");
                //留空：游戏启动后数据清零，存档后作弊档被自动覆盖//
            }
        }

    }
    public void Check(int current)
    {
        string gameDataFile = GetDataPath() + "/" + dataFileName + current.ToString() + ".sav";
        if (xs.hasFile(gameDataFile))
        {
            string dataString = xs.LoadXML(gameDataFile);
            GameData gameDataFromXML = xs.DeserializeObject(dataString, typeof(GameData)) as GameData;

            //是合法存档//
            if (gameDataFromXML.key == gameData.key)
            {
                gameData = gameDataFromXML;
                go_datainfo[current].GetComponent<Text>().text = gameData.Position;
            }
            //是非法拷贝存档//
            else
            {
                go_datainfo[current].GetComponent<Text>().text = "BAD DATA";
                //留空：游戏启动后数据清零，存档后作弊档被自动覆盖//
            }
        }
        else
            go_datainfo[current].GetComponent<Text>().text = "NO DATA";

    }

    //获取路径//
    private static string GetDataPath()
    {
        //return Application.dataPath;
        return "Save";
    }
}