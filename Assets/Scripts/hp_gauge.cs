using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
public class hp_gauge : MonoBehaviour {
    private Status c_status;
    public Texture2D hpg;
    void Awake()
    {
        c_status = GetComponent<Status>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }
    void OnGUI()
    {
        int hp = c_status.GetHp();
        int mhp = c_status.hpmax;
        float scale = (float)hp / (float)mhp;
        GUI.DrawTextureWithTexCoords(new Rect(0.5f*64, 260, 32, mhp*8), hpg,new Rect(0,0,0.25f,1));
        GUI.DrawTextureWithTexCoords(new Rect(0.5f*64, 260+hpg.height*(1-scale), 32, scale*hpg.height), hpg, new Rect(0.25f, 0, 0.25f, scale));

    }
}
