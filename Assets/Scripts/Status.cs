using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {
    public int hpmax;
    private int hp;
    public int atk;
    public int def;
	// Use this for initialization
	void Start () {
        hp = hpmax;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetDamage(Status teki)
    {
        hp -= teki.atk - def;
        if (hp < 0)
            hp = 0;
        if (hp == 0)
            GameObject.Destroy(gameObject);
    }
    public int GetHp()
    {
        return hp;
    }
}
