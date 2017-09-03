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
    private string[] binid= {"[MENU0001]","[MENU0002]","[MENU0003]" };
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

        b_item = new bool[labels][];

        //read .txt
        string path = "Text\\mnu.txt";
        string[] readins= File.ReadAllLines(path);

        //open .bin
        path = "Text\\mnu.bin";
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

        string raw = "";
        int k = 0;
        int len=0;
        string label = "";
        foreach (string readin in readins)
        {
            //k++;
            if (readin[0] == '[')
            {
                if (label!="")
                    WriteBlock(fs,label,len,raw);
                label = readin;
                len = 0;
                raw = "";
                continue;
            }            
            len += readin.Length+1;
            //if (readins.Length > k)
                raw += readin + "\n";
            //else
                //raw += readin;
        }
        WriteBlock(fs, label, len, raw);

        fs.Flush();
        fs.Close();

        if (File.Exists(path))
        {
            fs = new FileStream(path, FileMode.Open);
            byte[] labelr = new byte[10];
            byte[] lenr = new byte[4];
            Decoder d = Encoding.UTF8.GetDecoder();
            do
            {
                
                //read head
                fs.Read(labelr, 0, labelr.Length);
                fs.Read(lenr, 0, lenr.Length);
                //decode label
                
                char[] label_chars = new char[labelr.Length];
                d.GetChars(labelr, 0, labelr.Length, label_chars, 0);
                string label_str = new string(label_chars);
                //decode length
                int len_int = bytesToInt(lenr, 0);
                //check label
                if (label_str == binid[k])
                {
                    byte[] bytes2 = new byte[len_int - 1];
                    fs.Read(bytes2, 0, (int)bytes2.Length);
                    char[] content = new char[bytes2.Length];
                    d.GetChars(bytes2, 0, bytes2.Length, content, 0);
                    item[k] = new string(content);
                    k++;
                    fs.Seek(1, SeekOrigin.Current);
                }
                else
                {
                    fs.Seek(len_int, SeekOrigin.Current);
                    if (fs.Position == fs.Length - 1)
                        Debug.Log("Wrong BinID.");

                }
            }
            while (k<labels);

            fs.Close();          

        }
        else
            Debug.Log("game file lost: " + path);
 


        path = "Save\\database.txt";
        
        if (!File.Exists(path))
        {
            //File.Create(path);
            fs = new FileStream(path, FileMode.Create);
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
    private void WriteBlock(FileStream fs,string label,int len,string raw)
    {
        //encode
        Encoder e = Encoding.UTF8.GetEncoder();
        byte[] bytes = new byte[14 + e.GetByteCount(raw.ToCharArray(),0,raw.Length,true)];
        //Debug.Log(raw.Length);

        e.GetBytes(label.ToCharArray(), 0, label.Length, bytes, 0, true);
        byte[] bytes_temp = intToBytes(len);
        for (int i = 10; i < 14; i++)
            bytes[i] = bytes_temp[i - 10];
        e.GetBytes(raw.ToCharArray(), 0, raw.Length, bytes, 14, true);
        //write
        fs.Write(bytes, 0, (int)bytes.Length);
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

    public static byte[] intToBytes(int value)
    {
        byte[] src = new byte[4];
        src[0] = (byte)((value >> 24) & 0xFF);
        src[1] = (byte)((value >> 16) & 0xFF);
        src[2] = (byte)((value >> 8) & 0xFF);
        src[3] = (byte)(value & 0xFF);
        return src;
    }
    public static int bytesToInt(byte[] src, int offset)
    {
        int value;
        value = (int)(((src[offset] & 0xFF) << 24)
                | ((src[offset + 1] & 0xFF) << 16)
                | ((src[offset + 2] & 0xFF) << 8)
                | (src[offset + 3] & 0xFF));
        return value;
    }
}
