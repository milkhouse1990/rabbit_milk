using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp_gauge : MonoBehaviour {
    //private Status c_status;
    public Texture2D life;
    public Texture2D tank;

    public Texture2D icon_milk;
    private int mhp;

	// Use this for initialization
	void Start () {
        GameObject milk = GameObject.Find("milk");
    }
	
	// Update is called once per frame
	void Update () {
    }
    void OnGUI()
    {
        int hp = 0;
        GameObject milk=GameObject.Find("milk");
        if (milk!=null)
        {
            hp = milk.GetComponent<Status>().GetHp();
            mhp = milk.GetComponent<Status>().hpmax;
        }
        
        float scale = (float)hp / (float)mhp;
        //GUI.DrawTextureWithTexCoords(new Rect(0.5f*64, 260, 32, mhp*8), hpg,new Rect(0,0,0.25f,1));
        GUI.DrawTextureWithTexCoords(new Rect(0, 720-128, 128, 128), tank, new Rect(0, 0, 1, 0.25f));
        GUI.DrawTextureWithTexCoords(new Rect(0, 720 - 128 - 8 * mhp, 128, 8 * mhp), tank, new Rect(0, 0.25f, 1, mhp / 64.0f));
        GUI.DrawTextureWithTexCoords(new Rect(0, 720 - 128 - 8 * mhp - 8 * 2, 128, 8 * 2), tank, new Rect(0, 0.75f, 1, 2 / 64.0f));
        GUI.DrawTextureWithTexCoords(new Rect(8,720-128-8*hp,5*8, 8*hp),life,new Rect(0,0,1,hp/32.0f));

        //icon
        GUI.DrawTexture(new Rect(0, 720 - 196, 128, 196), icon_milk);

    }
}
