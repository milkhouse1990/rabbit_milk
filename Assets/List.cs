using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class List : MonoBehaviour {
    private bool ready = false;

    private int item = 0;
    private int delay = 0;
    private string pre_items;
    private string[] items;
    public Rect pos;
    public int vspace;
    private int itemperscr = 10;
    private int dispos = 0;

    public void Init(string p_pre_items)
    {
        pre_items = p_pre_items;
        items = pre_items.Split('\n');
        ready = true;
    }
    // Use this for initialization
    void Start()
    {
        Debug.Log("list.start");
        //Init("1\n2");
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("list.update");
        if (ready)
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
        string dis = "";
        //for (int i = 0; i < m_item; i++)
        //GUI.Label(pos, list_text);
        for (int i = dispos; i < Mathf.Min(dispos + itemperscr, items.Length); i++)
        {
            if (i == item)
                dis = "<color=magenta>" + items[i] + "</color>";
            else
                dis = items[i];
            GUI.Label(new Rect(pos.x, pos.y + (i - dispos) * vspace, pos.width, pos.width), dis);
        }
        
    }
}
