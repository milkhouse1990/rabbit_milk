using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour {
	public Transform brick;
	// Use this for initialization
	void Start () {
		Instantiate(brick, new Vector3(0, 0, 0), Quaternion.identity);
		Instantiate(brick, new Vector3(0, 1, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
