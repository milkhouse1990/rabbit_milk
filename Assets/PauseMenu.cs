using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PauseMenu : MonoBehaviour {
    public List list;
    public Texture2D vector;

    public Rect list_pos;
    public Rect info_pos;
    public Rect title_pos;
    public Texture2D bg;

    private List page;
    

    private string[] basic_text;

    private int[] lvs=new int[2 * 2] { 0, 0, 0, 0 };

    private int current = 0;
    private int equip = 0;
    private int feed = 0;
    private bool moon = true;
    // Use this for initialization
    void Start () {
        string binid;
        if (moon)
            binid = "MENU0004";
        else
            binid = "MENU0004";

        ReadList rl = new ReadList(binid);
        
        page = Instantiate<List>(list);
        page.Init(list_pos, info_pos, vector);
        page.InitText(rl.item, rl.info);
    }
	
	// Update is called once per frame
	void Update () {
        page.enabled = true;
		if (CrossPlatformInputManager.GetButtonDown("START"))
        {
            GetComponent<Platformer2DUserControl>().SetPause(false);
            page.enabled = false;
            enabled = false;

            GetComponent<Status>().SetHPMax(16 + lvs[0]);
        }
        current = page.GetFocus();
        if (CrossPlatformInputManager.GetButtonDown("left"))
            if (lvs[page.GetFocus() * 2] > -5)
                lvs[page.GetFocus() * 2]--;
        if (CrossPlatformInputManager.GetButtonDown("right"))
            if (lvs[page.GetFocus() * 2] + lvs[page.GetFocus() * 2 + 1] < 5)
                lvs[page.GetFocus() * 2]++;
        if (CrossPlatformInputManager.GetButtonDown("Y"))
            if (lvs[page.GetFocus() * 2] +lvs[page.GetFocus()*2+1]<5)
                lvs[page.GetFocus() * 2+1]++;
        if (CrossPlatformInputManager.GetButtonDown("A"))
            if (lvs[page.GetFocus() * 2 + 1] >-5)
                lvs[page.GetFocus() * 2+1]--;
        if (CrossPlatformInputManager.GetButtonDown("X"))
            if (equip == page.GetFocus())
                equip = -1;
            else
                equip = page.GetFocus();
        if (CrossPlatformInputManager.GetButtonDown("B"))
            feed = page.GetFocus();

    }
    void OnGUI()
    {
        //bg
        GUI.DrawTextureWithTexCoords(new Rect(100, 100, 1080, 520), bg, new Rect(1, 1, 1, 1));

        page.Display();
        if (!moon)
        {
            GUI.Label(title_pos, "<color=red>时间停止中</color>");
            
            GUI.Label(new Rect(info_pos.x, info_pos.y + 20, info_pos.width, info_pos.height), "<color=red>现在等级 " + lvs[current * 2] + " + " + lvs[current * 2 + 1] + " / 5</color>");
            if (equip > -1)
                GUI.Label(new Rect(list_pos.x - 60, list_pos.y + 20 * equip, list_pos.width, list_pos.height), "<color=red>装备中</color>");
            GUI.Label(new Rect(list_pos.x - 100, list_pos.y + 20 * feed, list_pos.width, list_pos.height), "<color=red>培养中</color>");

            //elf level status bar
            for (int i = 0; i < 2; i++)
            {
                string lv_info = "";
                int lva = lvs[i * 2];
                int lvb = lvs[i * 2 + 1];
                //effect A
                if (lva < 0)
                {
                    lv_info = "<color=red><";
                    for (int j = 0; j > lva; j--)
                        lv_info += "-";
                    lv_info += "</color>";
                }
                else
                {
                    lv_info = "<color=black>";
                    for (int j = 0; j < lvs[i * 2]; j++)
                        lv_info += "-";
                    lv_info += ">";
                    lv_info += "</color>";
                }
                //empty
                lv_info += "<color=black>";
                for (int j = 0; j < 10 - (lva > 0 ? lva : 0) - (lvb > 0 ? lvb : 0); j++)
                    lv_info += "-";
                lv_info += "</color>";
                //effect B
                if (lvb < 0)
                {
                    lv_info += "<color=red>";
                    for (int j = 0; j > lvb; j--)
                        lv_info += "-";
                    lv_info += "></color>";
                }
                else
                {
                    lv_info += "<color=black>";
                    lv_info += "<";
                    for (int j = 0; j < lvb; j++)
                        lv_info += "-";
                    lv_info += "</color>";
                }

                GUI.Label(new Rect(list_pos.x + 100 + (lva < 0 ? lva * 4 : 0), list_pos.y + 20 * i, list_pos.width, list_pos.height), "<color=black>" + lv_info + "</color>");
            }
        }
        else
        {
            
        }

        
    }
}
