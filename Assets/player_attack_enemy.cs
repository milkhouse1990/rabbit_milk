using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_attack_enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//GetComponent<Rigidbody2D> ().velocity = new Vector3 (1, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {/*
		if (loi->x<xview || loi->x> xview + xscreen)
		{
			loi = milk_listobj.erase(loi);
			continue;
		}

		//enemy_damage_check
		for (lei = enemy_list.begin(); lei != enemy_list.end();)
		{
			if (lei->id< 100)
			if (lei->damage_check(*loi))
			{
				lei->nHp -= 1;
				if (lei->nHp == 0)
					lei = enemy_list.erase(lei);
				else
					lei++;
			}
			else
				lei++;
			else
				lei++;

		}

		loi++;

	}*/
    
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy(gameObject);
    }
    
}
