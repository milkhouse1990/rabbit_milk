using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour {
    public string plotno;
	// Use this for initialization
	void Start () {
        string binid = "PLOT" + plotno;
        GameObject milk = GameObject.Find("milk");
        milk.GetComponent<Platformer2DUserControl>().EnterAVGMode(binid);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
