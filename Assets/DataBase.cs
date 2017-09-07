using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ReadList))]
public class DataBase : MonoBehaviour {
    public Texture2D[] tachie;

    private int itemperscr = 10;
    private int delay = 0;
    private string[] labelname= {"Characters","Enemies","Endings" };
    private string[] item;
    private string binid= "@MENU0001";
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

    public Rect list_pos;
    public Rect info_pos;

    // Use this for initialization
    void Start () {
        labels = labelname.Length;
        item = new string[labels];
        info = new string[labels];

        b_item = new bool[labels][];

        string path = "Text\\mnu.bin";
        FileStream fs;

        string[] temp = GetComponent<ReadList>().Read(path, binid, labels);
        for (int i = 0;i<labels;i++)
        {
            item[i] = temp[i * 2];
            info[i] = temp[i * 2 + 1];
        }
           
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
            string[] temp_string=commands[i].Split(' ');
            b_item[i] = new bool[temp_string.Length];

            for (int j=0; j < temp_string.Length; j++)
                if (temp_string[j] == "1")
                    b_item[i][j] = true;
                else
                    b_item[i][j] = false;
        }
        
        //message lock manage    
            
        for (int i=0;i<item.Length;i++)
        {           
            string[] messages = item[i].Split('\n');
            int mes_len = messages.Length;
            if (b_item[i].Length != mes_len)
                Debug.Log("savedata wrong.");
            item[i] = "";
            for (int j=0;j<mes_len;j++)
            {
                if (!b_item[i][j])
                    messages[j] = "???";
                item[i] += messages[j];
                if (j!=mes_len-1)
                    item[i]+="\n";
            }

            for (int j = mes_len; j >0; j--)
            {
                int point = info[i].LastIndexOf("\n\n");
                if (b_item[i][j-1])
                    messages[j-1]= info[i].Substring(point + 2);
                else
                    messages[j-1] = "???????";
                info[i] = info[i].Substring(0, point);
                //Debug.Log(infos[items.Length - 1 - i]);
            }
            
            info[i] = "\n\n";
            for (int j = 0; j < mes_len; j++)
            {
                
                info[i] += messages[j];
                if (j != mes_len - 1)
                    info[i] += "\n\n";
            }
        }

        {
            page = Instantiate<List>(list);
            
            page.Init(list_pos,info_pos,vector);
            page.InitText(item[labelpos],info[labelpos]);
        }
    }
    
    
	
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetButtonDown("left"))
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
        if (CrossPlatformInputManager.GetButtonDown("right"))
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
        if (CrossPlatformInputManager.GetButtonDown("B"))
            SceneManager.LoadScene("Title");
    }
    void FixedUpdate()
    {
        if (delay > 0)
            delay--;
    }
    void OnGUI()
    {
        string dis="<color=black>"+ "↑↓项目选择 ←→标签选择 A文字显示/隐藏 B返回标题"+"</color>";
                            
        //help
        GUI.Label(new Rect(640, 100, 400, 20), dis);

        page.Display();

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
