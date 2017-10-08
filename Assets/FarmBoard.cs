using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(FarmMenu))]
public class FarmBoard : MonoBehaviour {
    private bool pause = false;
    private bool m_waitnpc = false;
    private PlatformerCharacter2D milk;
    public Texture2D waitnpc;
    private GameObject farm_menu;
	// Use this for initialization
	void Start() {
        farm_menu = GameObject.Find("Farm_Canvas");
        farm_menu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (m_waitnpc)
        {
            if (pause)
            {
                if (CrossPlatformInputManager.GetButtonDown("B"))
                    {
                        milk.GetComponent<Platformer2DUserControl>().SetPause(false);
                    pause = false;
                    farm_menu.SetActive(false);
                    }
               
            }
            else
            {
                if (CrossPlatformInputManager.GetButtonDown("up"))
                {
                    milk.GetComponent<Platformer2DUserControl>().SetPause(true);
                    pause=true;
                    farm_menu.SetActive(true);
                }
            }
        }            
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        switch (other.gameObject.tag)
        {
            case "Player":
                milk = other.GetComponentInParent<PlatformerCharacter2D>();
                m_waitnpc = true;
                break;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log(other.name);
        switch (other.gameObject.tag)
        {
            case "Player":
                milk = null;
                m_waitnpc = false;
                break;
        }
    }
    void OnGUI()
    {
        if (m_waitnpc)
            if (milk.GetGround())
            {
                Vector3 screenpos = Camera.main.WorldToScreenPoint(milk.transform.position);
                GUI.Label(new Rect(screenpos.x - 24, screenpos.y - 128, 64, 64), waitnpc);
            }
                
    }
}
