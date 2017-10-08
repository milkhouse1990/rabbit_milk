using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {
    private int hp,mhp,mp,mmp;
    private Status c_Status;
    private GameObject milk;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Awaken()
    {
        milk = GameObject.Find("milk");
        //if (milk)
            
    }

    void OnGUI()
    {
        
        //time
        System.DateTime now = System.DateTime.Now;
        string dis = now.ToString();
        GUI.Label(new Rect(1000, 0, 720, 720), "<color=red>" + dis + "</color>");
        

        //hpmp
        GameObject milk = GameObject.Find("milk");
        if (milk != null)
        {
            if (milk.GetComponent<AvgEngine>().enabled)
                return;
            c_Status = milk.GetComponent<Status>();
            hp = c_Status.GetHp();
            mhp = c_Status.hpmax;
            mp = c_Status.GetMp();
            mmp = c_Status.mpmax;
        }

        GUI.Label(new Rect(50, 50, 200, 20), "HP "+hp.ToString() + "/" + mhp.ToString());
        GUI.Label(new Rect(50, 70, 200, 20), "MP " + mp.ToString() + "/" + mmp.ToString());
    }
}
