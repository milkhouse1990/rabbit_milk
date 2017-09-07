using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(ReadList))]
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
    // Use this for initialization
    void Start () {
        string binid = "@MENU0004";

        string path = "Text\\mnu.bin";

        basic_text = GetComponent<ReadList>().Read(path, binid);

        page = Instantiate<List>(list);
        page.Init(list_pos, info_pos, vector);
        page.InitText(basic_text[0], basic_text[1]);
    }
	
	// Update is called once per frame
	void Update () {
        page.GetComponent<List>().enabled = true;
		if (CrossPlatformInputManager.GetButtonDown("START"))
        {
            GetComponent<Platformer2DUserControl>().SetPause(false);
            page.GetComponent<List>().enabled = false;
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
        GUI.Label(title_pos, "<color=red>时间停止中</color>");
        GUI.DrawTextureWithTexCoords(new Rect(100, 100, 1080, 520),bg,new Rect(1,1,1,1));
        page.Display();
        GUI.Label(new Rect(info_pos.x,info_pos.y+20,info_pos.width,info_pos.height), "<color=red>现在等级 "+lvs[current*2]+" + "+lvs[current*2+1]+" / 5</color>");
        if (equip>-1)
            GUI.Label(new Rect(list_pos.x-60, list_pos.y + 20*equip, list_pos.width, list_pos.height), "<color=red>装备中</color>");
        GUI.Label(new Rect(list_pos.x-100, list_pos.y + 20*feed, list_pos.width, list_pos.height), "<color=red>培养中</color>");

        
    }
}
