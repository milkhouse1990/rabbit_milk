using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class FairySystem : MonoBehaviour {
    //list
    public ListTool list_tool;
    private ListTool fairy_list;
    public Rect list_pos;
    public Rect info_pos;

    private int[] lvs = new int[2 * 2] { 0, 0, 0, 0 };
    private int equip = -1;
    private int feed = 0;
    private string[] info;

    private ReadList rl;

    public Text text;
    private Text[] fairy_lv_bar;
    // Use this for initialization
    void Start () {
        string binid = "MENU0004";
        rl = new ReadList(binid);
        int a_fairy = rl.items.Length;
        info = new string[a_fairy];
        for(int i=0;i<a_fairy;i++)
            info[i] = rl.infos[i];

        fairy_list = Instantiate(list_tool, transform);
        fairy_list.SetListPos(list_pos);
        fairy_list.SetInfoPos(info_pos);
        for (int i = 0; i < a_fairy; i++)
            rl.infos[i] =info[i]+ "("+lvs[i * 2].ToString() + " + " + lvs[i * 2 + 1].ToString() + ") / 5";
        fairy_list.GetComponent<ListTool>().InitText(rl);

        fairy_lv_bar = new Text[a_fairy];
        for (int i = 0; i < a_fairy; i++)
        {
            fairy_lv_bar[i] = Instantiate(text, transform);
            RectTransform rt = fairy_lv_bar[i].GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, list_pos.x + 100, list_pos.width);
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, list_pos.y + i * 30, list_pos.height);
            fairy_lv_bar[i].text = lv_bar(i);
        }            
    }
	
	// Update is called once per frame
	void Update () {
        int current = fairy_list.GetFocus();
        if (CrossPlatformInputManager.GetButtonDown("left"))
            if (lvs[current * 2] > -5)
            {
                lvs[current * 2]--;
                rl.infos[current] = info[current] + "("+lvs[current * 2].ToString() + " + " + lvs[current * 2 + 1].ToString() + ") / 5";
                fairy_list.GetComponent<ListTool>().InitText(rl);
                fairy_lv_bar[current].text = lv_bar(current);
            }             
        if (CrossPlatformInputManager.GetButtonDown("right"))
            if (lvs[current * 2] + lvs[current * 2 + 1] < 5)
            {
                lvs[current * 2]++;
                rl.infos[current] = info[current] + "(" + lvs[current * 2].ToString() + " + " + lvs[current * 2 + 1].ToString() + ") / 5";
                fairy_list.GetComponent<ListTool>().InitText(rl);
                fairy_lv_bar[current].text = lv_bar(current);
            }              
        if (CrossPlatformInputManager.GetButtonDown("Y"))
            if (lvs[current * 2] + lvs[current * 2 + 1] < 5)
            {
                lvs[current * 2 + 1]++;
                rl.infos[current] = info[current] + "(" + lvs[current * 2].ToString() + " + " + lvs[current * 2 + 1].ToString() + ") / 5";
                fairy_list.GetComponent<ListTool>().InitText(rl);
                fairy_lv_bar[current].text = lv_bar(current);
            }
        if (CrossPlatformInputManager.GetButtonDown("A"))
            if (lvs[current * 2 + 1] > -5)
            {
                lvs[current * 2 + 1]--;
                rl.infos[current] = info[current] + "(" + lvs[current * 2].ToString() + " + " + lvs[current * 2 + 1].ToString() + ") / 5";
                fairy_list.GetComponent<ListTool>().InitText(rl);
                fairy_lv_bar[current].text = lv_bar(current);
            }               
        if (CrossPlatformInputManager.GetButtonDown("X"))
            if (equip == current)
                equip = -1;
            else
                equip = current;
        if (CrossPlatformInputManager.GetButtonDown("B"))
            feed = current;
    }
    private string lv_bar(int i)
    {
        string lv_info = "";
        int lva = lvs[i * 2];
        int lvb = lvs[i * 2 + 1];
        //effect A
        //space
        for (int j = -5; j < lva && j<0; j++)
            lv_info += " ";
        if (lva < 0)
        {
            lv_info += "<color=red><";
            for (int j = 0; j > lva; j--)
                lv_info += "-";
            lv_info += "</color>";
        }
        else
        {
            lv_info += "<color=black>";
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

        return lv_info;
    }
    public int GetEquip()
    {
        return equip;
    }
    public int GetLvA(int fairyno)
    {
        return lvs[fairyno * 2];
    }
}
