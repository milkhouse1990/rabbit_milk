using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class fairy
{
    public int lv;
    public int[] next;
    public fairy()
    {
        lv = 5;
        next = new int[]{ 1, 1, 1, 1, 1, 1, 2, 2, 2, 2 } ;
    }
}
public class FairySystem : MonoBehaviour {
    //list
    public ListTool list_tool;
    private ListTool fairy_list;
    public Rect list_pos;
    public Rect info_pos;
    public Rect crystal_pos;

    private int[] lvs = new int[2 * 2] { 0, 0, 0, 0 };
    private int crystal=0;
    private int equip = -1;
    private int feed = 0;
    private string[] info;
    private fairy[] fys;

    private ReadList rl;

    public Text text;
    private Text[] fairy_lv_bar;
    private Text[] fairy_lv_status;
    private Text crystal_amount;

    public GameObject diag;
    private GameObject feed_confirm;
    private bool pause=false;
    // Use this for initialization
    void Awake () {
        string binid = "MENU0004";
        rl = new ReadList(binid);
        int a_fairy = rl.items.Length;
        info = new string[a_fairy];
        for(int i=0;i<a_fairy;i++)
            info[i] = rl.infos[i];

        fys = new fairy[a_fairy];

        fairy_list = Instantiate(list_tool, transform);
        fairy_list.SetListPos(list_pos);
        fairy_list.SetInfoPos(info_pos);

        fairy_lv_bar = new Text[a_fairy];
        fairy_lv_status = new Text[a_fairy];
        for (int i = 0; i < a_fairy; i++)
        {
            fairy_lv_bar[i] = Instantiate(text, transform);
            fairy_lv_bar[i].name = "lv_bar_" + i.ToString();
            TextSetPos(fairy_lv_bar[i], new Rect(list_pos.x + 100, list_pos.y + i * 30, list_pos.width, list_pos.height));
            fairy_lv_bar[i].text = lv_bar(i);

            fairy_lv_status[i] = Instantiate(text, transform);
            fairy_lv_status[i].name = "lv_status_" + i.ToString();
            TextSetPos(fairy_lv_status[i], new Rect(list_pos.x + 300, list_pos.y + i * 30, list_pos.width, list_pos.height));
            fys[i] = new fairy();
            FairyUpdate(i);
        }          

        crystal_amount = Instantiate(text, transform);
        crystal_amount.name = "CrystalAmount";
        TextSetPos(crystal_amount, crystal_pos);
        //crystal = PlayerPrefs.GetInt("Crystal", 0);
        CrystalUpdate();

        feed_confirm = Instantiate(diag,transform);
        feed_confirm.SetActive(false);
    }
	void OnEnable()
    {
        CrystalUpdate();
    }
	// Update is called once per frame
	void Update () {
        int current = fairy_list.GetFocus();
        if (!pause)
        {
            
            if (CrossPlatformInputManager.GetButtonDown("left"))
                if (lvs[current * 2] > -5)
                {
                    lvs[current * 2]--;
                    
                    fairy_lv_bar[current].text = lv_bar(current);
                }
            if (CrossPlatformInputManager.GetButtonDown("right"))
                if (lvs[current * 2] + lvs[current * 2 + 1] < fys[current].lv)
                {
                    lvs[current * 2]++;
                    FairyLvABUpdate(current);
                    fairy_lv_bar[current].text = lv_bar(current);
                }
            if (CrossPlatformInputManager.GetButtonDown("Y"))
                if (lvs[current * 2] + lvs[current * 2 + 1] < fys[current].lv)
                {
                    lvs[current * 2 + 1]++;
                    FairyLvABUpdate(current);
                    fairy_lv_bar[current].text = lv_bar(current);
                }
            if (CrossPlatformInputManager.GetButtonDown("A"))
                if (lvs[current * 2 + 1] > -5)
                {
                    lvs[current * 2 + 1]--;
                    FairyLvABUpdate(current);
                    fairy_lv_bar[current].text = lv_bar(current);
                }
            if (CrossPlatformInputManager.GetButtonDown("X"))
                if (equip == current)
                    equip = -1;
                else
                    equip = current;
            if (CrossPlatformInputManager.GetButtonDown("B"))
            {
                if (fys[current].lv < 10)
                    if (crystal >= fys[current].next[fys[current].lv])
                    {
                        pause = true;
                        feed_confirm.SetActive(true);

                        
                    }

            }
        }
        else
        {
            if (CrossPlatformInputManager.GetButtonDown("A"))
            {
                crystal -= fys[current].next[fys[current].lv];
                fys[current].lv++;
                PlayerPrefs.SetInt("Crystal", crystal);
                CrystalUpdate();
                FairyUpdate(current);

                pause = false;
                feed_confirm.SetActive(false);
            }
            if (CrossPlatformInputManager.GetButtonDown("B"))
            {
                pause = false;
                feed_confirm.SetActive(false);
            }
        }
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
    public int GetLvB(int fairyno)
    {
        return lvs[fairyno * 2+1];
    }
    public void CrystalUpdate()
    {
        crystal = PlayerPrefs.GetInt("Crystal", 0);
        crystal_amount.text="Crystal: " + crystal.ToString();
        //Debug.Log("update");
    }
    private void FairyUpdate(int i)
    {
        fairy_lv_status[i].text = "Lv " + fys[i].lv + " NEXT ";
        if (fys[i].lv < 10)
            fairy_lv_status[i].text += fys[i].next[fys[i].lv];
        else if (fys[i].lv == 10)
            fairy_lv_status[i].text += "MAX";
        else
            Debug.Log("fairy " + i.ToString() + " lv over.");

        FairyLvABUpdate(i);
    }
    private void FairyLvABUpdate(int i)
    {
        rl.infos[i] = info[i] + "(" + lvs[i * 2].ToString() + " + " + lvs[i * 2 + 1].ToString() + ") / " + fys[i].lv.ToString();
        fairy_list.GetComponent<ListTool>().InitText(rl);
    }
    private void TextSetPos(Text txt,Rect pos)
    {
        RectTransform rt = txt.GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, pos.x, pos.width);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, pos.y, pos.height);
    }
}
