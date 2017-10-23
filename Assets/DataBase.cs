using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class DataBase : MonoBehaviour {
    public Texture2D[] tachie;

    private int itemperscr = 10;
    private int delay = 0;
    private string[] labelname= {"Characters","Enemies","Endings" };
    private string[][] item;
    private string binid;
    private bool[][] b_item;
    public Texture2D vector;
    private string[][] info;
    private int[] pos = { 0, 0, 0};
    private int[] dispos = { 0, 0, 0 };
    private int labelpos = 0;
    private int cospos = 0;
    private int labels;
    private bool b_info=true;
    ReadList[] rls;

    public List list;
    private List page;

    public Rect list_pos;
    public Rect info_pos;

    private ListTool db_list;
    public ListTool listtool;

    // Use this for initialization
    void Start () {
        labels = labelname.Length;
        item = new string[labels][];
        info = new string[labels][];

        b_item = new bool[labels][];

        string path = "Text\\mnu.bin";
        FileStream fs;

        rls = new ReadList[labels];
        for (int i = 0;i<labels;i++)
        {
            int j = i + 1;
            binid = "MENU000" + j.ToString();
            rls[i] = new ReadList(binid);

            item[i] = rls[i].items;
            info[i] = rls[i].infos;
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
            string[] messages = item[i];
            int mes_len = messages.Length;
            if (b_item[i].Length != mes_len)
                Debug.Log("savedata wrong.");
            
            for (int j=0;j<mes_len;j++)
            {
                if (!b_item[i][j])
                {
                    item[i][j] = "???";
                    info[i][j]= "???????";
                }       
            }
        }

        {
            db_list = Instantiate(listtool, transform);
            db_list.SetListPos(list_pos);
            db_list.SetInfoPos(info_pos);
            db_list.InitText(rls[labelpos]);
        }
    }
    
    
	
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetButtonDown("left"))
        {
            pos[labelpos] = db_list.GetFocus();
            //dispos[labelpos] = page.GetScroll();
            labelpos--;
            if (labelpos == -1)
                labelpos = labels-1;
            db_list.SetFocus(pos[labelpos]);
            db_list.InitText(rls[labelpos]);
            
            //page.SetScroll(dispos[labelpos]);
        }
        if (CrossPlatformInputManager.GetButtonDown("right"))
        {
            pos[labelpos] = db_list.GetFocus();
            //dispos[labelpos] = page.GetScroll();
            labelpos++;
            if (labelpos == labels)
                labelpos = 0;
            db_list.SetFocus(pos[labelpos]);
            db_list.InitText(rls[labelpos]);
            
            //page.SetScroll(dispos[labelpos]);
        }
        if (CrossPlatformInputManager.GetButtonDown("A"))
            //page.SetBInfo(!page.GetBInfo());
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
