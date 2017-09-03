using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
    private string[] info;
    private int[] pos = { 0, 0, 0};
    private int[] dispos = { 0, 0, 0 };
    private int labelpos = 0;
    private int cospos = 0;
    private int labels;
    private bool b_info=true;

    public List list;
    private List page;

    // Use this for initialization
    void Start () {
        labels = labelname.Length;
        item = new string[labels];

        item[1]= "1\n2";
        item[2] = "Happy Ending\n最速Clear\n地狱Ending";

        //string raw="milk酱\n公主\n233\n4\n5\n6\n7\n8\n9\n10\n11";

        b_item = new bool[labels][];

        FileStream fs;
        string mes;

        string path = "Text\\mnu.txt";

        string[] readins= File.ReadAllLines(path);
        string raw = "";
        int k = 0;
        foreach (string readin in readins)
        {
            k++;
            if (readins.Length > k)
                raw += readin + "\n";
            else
                raw += readin;
        }
            
        path= "Text\\temp.txt";
        fs = new FileStream(path, FileMode.OpenOrCreate);
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(raw);
        sw.Flush();
        sw.Close();
        fs.Close();

        fs = new FileStream(path, FileMode.OpenOrCreate);
        byte[] bytes = new byte[fs.Length];
        fs.Read(bytes, 0, (int)fs.Length);
        
        path = "Text\\mnu.bin";
        FileStream fsout = new FileStream(path, FileMode.OpenOrCreate);
        fsout.Write(bytes, 0, (int)bytes.Length);
        fsout.Flush();
        fsout.Close();
        fs.Close();

        if (File.Exists(path))
        {
            fs = new FileStream(path, FileMode.Open);
            byte[] bytes2 = new byte[fs.Length];
            fs.Read(bytes2, 0, (int)fs.Length);
            fs.Close();
            Decoder d = Encoding.UTF8.GetDecoder();
            char[] chars=new char[bytes2.Length];
            d.GetChars(bytes2, 0, bytes2.Length, chars, 0);
            Debug.Log(chars[0]);
            item[0] = new string(chars);

        }
        else
            Debug.Log("game file lost: " + path);
 


        path = "Save\\database.txt";
        
        if (!File.Exists(path))
        {
            //File.Create(path);
            fs = new FileStream(path, FileMode.Create);
            sw = new StreamWriter(fs);
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
        
        info = new string[labels];
        info[0] = "种族不明 本作主人公。\n兔耳族 王国的公主殿下。\n3\n\n\n\n\n\n\n\n";
        info[1] = "1\n2\n3";
        info[2] = "触发条件：按照正常流程攻略 牛奶酱救回了公主，打跑了坏人，王国又恢复了和平。\n触发条件：生日Party上在和对话后攻击她 牛奶酱提前识破了坏人的阴谋，阻止了一切的发生。\n触发条件：Miss100次 不管怎样都过不了关，于是死后在地狱和???过上了幸福的生活。";

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
                item[i] += messages[j];
                if (j!=messages.Length-1)
                    item[i]+="\n";
            }

            messages = info[i].Split('\n');
            info[i] = "";
            for (int j = 0; j < messages.Length; j++)
            {
                if (!b_item[i][j])
                    messages[j] = "???????";
                info[i] += messages[j];
                if (j != messages.Length - 1)
                    info[i] += "\n";
            }
        }

        {
            page = Instantiate<List>(list);
            page.Init(vector);
            page.InitText(item[labelpos],info[labelpos]);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetButtonDown("L"))
        {
            pos[labelpos] = page.GetFocus();
            dispos[labelpos] = page.GetScroll();
            labelpos--;
            if (labelpos == -1)
                labelpos = labels-1;
            page.InitText(item[labelpos],info[labelpos]);
            page.SetFocus(pos[labelpos]);
            page.SetScroll(dispos[labelpos]);
        }
        if (CrossPlatformInputManager.GetButtonDown("R"))
        {
            pos[labelpos] = page.GetFocus();
            dispos[labelpos] = page.GetScroll();
            labelpos++;
            if (labelpos == labels)
                labelpos = 0;
            page.InitText(item[labelpos],info[labelpos]);
            page.SetFocus(pos[labelpos]);
            page.SetScroll(dispos[labelpos]);
        }
        if (CrossPlatformInputManager.GetButtonDown("A"))
            page.SetBInfo(!page.GetBInfo());
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
        string dis="<color=black>"+ "↑↓项目选择 LR标签选择 A文字表示/非表示 X服装变更"+"</color>";
                            
        //help
        GUI.Label(new Rect(1280 - 400, 100, 400, 20), dis);

    }
}
