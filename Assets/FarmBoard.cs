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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (waitnpc)
        if (CrossPlatformInputManager.GetButtonDown("up"))
        {
                milk.GetComponent<Platformer2DUserControl>().SetPause(true);
                enabled = false;
            GetComponent<FarmMenu>().enabled = true;
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
