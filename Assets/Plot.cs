using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour {
    public string[] plot;
	// Use this for initialization
	void Start () {
        GameObject milk = GameObject.Find("milk");
        milk.GetComponent<Platformer2DUserControl>().EnterAVGMode(plot);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
