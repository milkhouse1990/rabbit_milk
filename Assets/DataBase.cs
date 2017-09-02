using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DataBase : MonoBehaviour {
    public Texture2D[] tachie;

    private int itemperscr = 10;
    private int delay = 0;
    private string[] labelname= {"Characters","Enemies","Endings" };
    private string[] item;
    private bool[][] b_item;
    public Texture2D vector;
    private string[][] info;
    private int pos = 0;
    private int labelpos = 0;
    private int cospos = 0;
    private int dispos = 0;
    private int labels;
    private bool b_info=true;

    public GameObject List;
    private GameObject pages;

    // Use this for initialization
    void Start () {
        labels = labelname.Length;
        item = new string[labels];
        item[0] = "milk酱\n公主\n3\n4\n5\n6\n7\n8\n9\n10\n11";
        item[1]= "1\n2";
        item[2] = "Happy Ending\n最速Clear\n地狱Ending";

        b_item = new bool[labels][];
        string path = "Save\\database.txt";
        if (!File.Exists(path))
        {
            //File.Create(path);
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < labels; i++)
            {
                sw.Write("0");
                for (int j = 1; j < item[i].Length; j++)
                    sw.Write(" 0");
                sw.Write("\r\n");
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        string[] commands = File.ReadAllLines(path);
        if (commands.Length != labels)
            Debug.Log("savefile wrong");
        for (int i=0;i<labels;i++)
        {
            //l_item[i] = item[i].Length;
            b_item[i] = new bool[item[i].Length];
            string[] temp_string=commands[i].Split(' ');

            for (int j=0; j < temp_string.Length; j++)
                if (temp_string[j] == "1")
                    b_item[i][j] = true;
                else
                    b_item[i][j] = false;
        }
        
        info = new string[labels][];
        info[0] = new string[]
        {
            "种族不明\n本作主人公。",
            "兔耳族\n王国的公主殿下。",
            "3"
        };
        info[1] = new string[]
        {
            "1",
            "2",
            "3"
        };
        info[2] = new string[]
        {
            "触发条件：按照正常流程攻略\n牛奶酱救回了公主，打跑了坏人，王国又恢复了和平。",
            "触发条件：生日Party上在和对话后攻击她\n牛奶酱提前识破了坏人的阴谋，阻止了一切的发生。",
            "触发条件：Miss100次\n不管怎样都过不了关，于是死后在地狱和???过上了幸福的生活。"
        };

        //message lock manage
        string[] messages;
        
            
        for (int i=0;i<item.Length;i++)
        {
            messages = item[i].Split('\n');
            item[i] = "";
            for (int j=0;j<messages.Length;j++)
            {
                if (!b_item[i][j])
                    messages[j] = "???";
                item[i] += messages[j]+"\n";
            }                 
        }

        {
            pages= Instantiate(List);
            pages.GetComponent<List>().Init(item[labelpos]);
            //temp = Instantiate(List);
            //temp.GetComponent<List>().Init("1\n2\n3\n4");
            /*
            pages[i].GetComponent<List>().Init(new string[]
            {
            "1",
            "2",
            "3"
            });*///pages[i].GetComponent<List>().items[0] = "0";
        }


    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("db.update");
        if (CrossPlatformInputManager.GetButtonDown("down")||
            (CrossPlatformInputManager.GetButton("down") && delay==0))
        {
            delay = 10;
            pos++;
            if (pos - dispos == itemperscr)
                dispos++;
            if (pos == item[labelpos].Length)
            {
                pos = 0;
                dispos = 0;
            }               
        }
        if (CrossPlatformInputManager.GetButtonDown("up") || 
            (CrossPlatformInputManager.GetButton("up") && delay==0))
        {
            delay = 10;
            pos--;
            if (pos < dispos)
                dispos--;
            if (pos == -1)
            {
                //pos = item[labelpos].Length - 1;
                Debug.Log(pos);
                dispos = pos - itemperscr+1;
            }               
        }
        if (CrossPlatformInputManager.GetButtonDown("L"))
        {
            pos = 0;
            dispos = 0;
            labelpos--;
            if (labelpos == -1)
                labelpos = labels-1;
            pages.GetComponent<List>().Init(item[labelpos]);
        }
        if (CrossPlatformInputManager.GetButtonDown("R"))
        {
            pos = 0;
            dispos = 0;
            labelpos++;
            if (labelpos == labels)
                labelpos = 0;
            pages.GetComponent<List>().Init(item[labelpos]);
        }
        if (CrossPlatformInputManager.GetButtonDown("A"))
            b_info = !b_info;
        if (CrossPlatformInputManager.GetButtonDown("X"))
            cospos++;           
    }
    void FixedUpdate()
    {
        if (delay > 0)
            delay--;
    }
    void OnGUI()
    {
        string dis;
        //list
        for (int i=dispos;i<dispos+itemperscr;i++)
            if (i<item[labelpos].Length)
            {              
                //string dis;
                
                //Debug.Log(dis);
                //GUI.Label(new Rect(0, 0, 640, 20), dis);
                //GUI.Label(new Rect(100, 100+(i - dispos) * 20, 640, 20), dis);
            }
        //GUI.Label(new Rect(80, 100 + (pos - dispos) * 20, 20, 20), vector);
        //help
        GUI.Label(new Rect(1280 - 400, 100, 400, 20), "↑↓项目选择 LR标签选择 A文字表示/非表示 X服装变更");
        //info
        /*
        if (b_info)
        {
            Debug.Log(pos);
            if (b_item[labelpos][pos])
                //Debug.Log(info[0][0]);
                dis = info[labelpos][pos];
            else
                dis = "???????";
            GUI.Label(new Rect(640, 360, 640, 360), dis);
        }*/
    }
}
