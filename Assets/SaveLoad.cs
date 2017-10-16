using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.GetInt("SaveFlag") == 1)
            GetComponent<Text>().text = "SAVE";
        else
            GetComponent<Text>().text = "LOAD";
    }
}
