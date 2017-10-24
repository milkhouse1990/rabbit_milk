using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWood : MonoBehaviour {
    public Vector2 falling_velocity;
    public Vector2 highest_position;
    public Vector2 lowest_position;
	// Use this for initialization
	void Start () {
        transform.position = highest_position;
        GetComponent<Rigidbody2D>().velocity = falling_velocity;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = falling_velocity;
        if (transform.position.y < lowest_position.y)
            transform.position = highest_position;
	}
}
