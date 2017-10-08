using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListCursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int current = GetComponentInParent<FarmItemTool>().GetFocus();
        Vector3 worldpos = Camera.main.ScreenToWorldPoint(new Vector3(1280/2-550 + current * 64, 720/2-260, 100));
        transform.position = worldpos;
    }
}
