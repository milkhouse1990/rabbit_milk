using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("get");
        switch (other.gameObject.tag)
        {
            case "weapon":
                GetComponent<Status>().GetDamage(other.gameObject.GetComponent<Status>());
                Destroy(other.gameObject,0.1f);
                break;
            
        }
    }
}
