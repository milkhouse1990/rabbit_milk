using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_attack_enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//GetComponent<Rigidbody2D> ().velocity = new Vector3 (1, 0, 0);
	}
	
	
    
	
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "weapon")
            GetComponent<Status>().GetDamage(other.GetComponent<Status>());
    }
    
}
