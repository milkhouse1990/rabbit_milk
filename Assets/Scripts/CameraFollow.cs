using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float left_border;
    public float right_border;
    public float top_border;
    public float bottom_border;
    public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (target.position.x < left_border)
            transform.position = new Vector3(left_border, target.position.y, -10);
        else if (target.position.x>right_border)
            transform.position = new Vector3(right_border, target.position.y, -10);
        else
            transform.position = new Vector3(target.position.x, target.position.y, -10);

        if (target.position.y > top_border)
            transform.position = new Vector3(transform.position.x, top_border, -10);
        else if (target.position.y < bottom_border)
            transform.position = new Vector3(transform.position.x, bottom_border, -10);
        else
            transform.position = new Vector3(transform.position.x, target.position.y, -10);


    }
}
