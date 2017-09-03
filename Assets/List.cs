using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class List : MonoBehaviour {
    
    private int item = 0;
    private int delay = 0;
    private string pre_items;
    private string pre_infos;
    private string[] items;
    private string[] infos;
    private Texture2D vector;
    private bool b_info=true;

    public Rect pos;
    public int vspace;
    

    private int itemperscr = 10;
    private int dispos = 0;

    public void Init(Texture2D p_vector)
    {
        vector = p_vector;
    }
    public void InitText(string p_pre_items, string p_pre_infos)
    {
        pre_items = p_pre_items;
        items = pre_items.Split('\n');

        pre_infos = p_pre_infos;
        infos = pre_infos.Split('\n');
    }
    // Use this for initialization
    void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
       
        {
            if (CrossPlatformInputManager.GetButtonDown("down") ||
            (CrossPlatformInputManager.GetButton("down") && delay == 0))
            {
                
                delay = 10;
                item++;
                if (item - dispos == itemperscr)
                    dispos++;
                if (item == items.Length)
                {
                    item = 0;
                    dispos = 0;
                }
            }
            if (CrossPlatformInputManager.GetButtonDown("up") ||
                (CrossPlatformInputManager.GetButton("up") && delay == 0))
            {
                delay = 10;
                item--;
                if (item < dispos)
                    dispos--;
                if (item == -1)
                {
                    item = items.Length - 1;
                    dispos = item - itemperscr + 1;
                }
            }
        }
        
    }
    void FixedUpdate()
    {
        if (delay > 0)
            delay--;
    }
    void OnGUI()
    {
        //text
        string dis = "";
        //for (int i = 0; i < m_item; i++)
        //GUI.Label(pos, list_text);
        for (int i = dispos; i < Mathf.Min(dispos + itemperscr, items.Length); i++)
        {
            if (i == item)
                dis = "<color=magenta>" + items[i] + "</color>";
            else
                dis = "<color=black>" + items[i] + "</color>";
            GUI.Label(new Rect(pos.x, pos.y + (i - dispos) * vspace, pos.width, pos.width), dis);
        }

        //vector
        GUI.Label(new Rect(pos.x-20, pos.y + (item - dispos) * vspace, 20, 20), vector);

        //info
        if (b_info)
        {
            dis = "<color=black>" + infos[item] + "</color>";
            GUI.Label(new Rect(640, 360, 640, 360), dis);
        }
    }

    public int GetFocus()
    {
        return item;
    }
    public void SetFocus(int p_focus)
    {
        item = p_focus;
    }
    public int GetScroll()
    {
        return dispos;
    }
    public void SetScroll(int p_scroll)
    {
        dispos = p_scroll;
    }
    public bool GetBInfo()
    {
        return b_info;
    }
    public void SetBInfo(bool p_b_info)
    {
        b_info = p_b_info;
    }
}
