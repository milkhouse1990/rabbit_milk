using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoPlot : MonoBehaviour {
    public string plotno;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            string binid = "PLOT" + plotno;
            other.transform.parent.GetComponent<Platformer2DUserControl>().EnterAVGMode(binid);
            GameObject.Destroy(gameObject);
        }
    }
}
