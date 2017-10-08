using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class FarmItemTool : MonoBehaviour {
    private int[] bag = new int[50];
    private int current = 0;
    private string[] names;

    private GameObject ch_item_name, ch_cursor;
    private Transform[] ch_item_box;
    // Use this for initialization
    void Start () {
        
    }
	
    void Awaken()
    {
        //ch_item_name = GameObject.Find("item_name");
        //ch_cursor = GameObject.Find("cursor");
    }
	// Update is called once per frame
	void Update () {
        
        if (CrossPlatformInputManager.GetButtonDown("left"))
        {
            current--;
            if (current < 0)
                current = 1;
        }
            
        if (CrossPlatformInputManager.GetButtonDown("right"))
        {
            current++;
            if (current == 2)
                current = 0;
        }
            


        
        
    }
    public int GetFocus()
    {
        return current;
    }
}
