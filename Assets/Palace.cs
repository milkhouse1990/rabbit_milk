using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palace : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "weapon")
        {
            GameObject milk = GameObject.Find("milk");
            string[] plot = {
            "say 0 你在做什么？",
            "say 1 那个…",
            "say 0 就算你是主角，也不能在王宫里胡闹吧。",
            "say 1 说的是呢，哎嘿嘿…",
            "say 0 这是给你的惩罚。",
            "hp-999"};
            milk.GetComponent<Platformer2DUserControl>().EnterAVGMode(plot);
        }
}
}
