using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLeft : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        transform.position -= new Vector3(speed, 0, 0);
        if (transform.position.x < -10)
            transform.position += new Vector3(40f, 0, 0);
    }
}
