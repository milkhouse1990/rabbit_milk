using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {
    public int hpmax;
    private int hp;
    public int atk;
    public int def;
    public Transform milk_die;

    public bool b_autorecover = false;
    private int autorecover_timer = 0;
	// Use this for initialization
	void Start () {
        hp = hpmax;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        //autorecover

        if (b_autorecover)
        {
            autorecover_timer++;
            if (autorecover_timer > 30)
            {
                autorecover_timer = 0;
                hp++;

                if (hp > hpmax)
                    hp = hpmax;
            }
        }
        else
            autorecover_timer = 0;
        
    }

    public void GetDamage(Status teki)
    {
        int damage=teki.atk - def;
        if (damage < 0)
            damage = 0;
        hp -= damage;
        if (hp < 0)
            hp = 0;
        if (hp == 0)
        {
            if (tag == "Player")
            {
                //GetComponent<SpriteRenderer>().enabled = false;
                Instantiate(milk_die, transform.position, Quaternion.identity);
            }
                
            
            GameObject.Destroy(gameObject);
        }
            
    }
    public int GetHp()
    {
        return hp;
    }
}
