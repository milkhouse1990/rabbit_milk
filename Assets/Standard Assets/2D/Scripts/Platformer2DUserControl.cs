using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private const int FPS = 60;
        
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool act=true;
        private bool change = false;
        private bool change_finish = false;

        //for changing gui
        public Texture2D change_up;
        public Texture2D change_down;
        public Texture2D change_left;
        public Texture2D change_right;
        private int guialarm = 0;
        private int guioffset = 0;
        //private Vector3 screenpos;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }
        

        private void Update()
        {
            if (act)
            {
                //changing check
                if (CrossPlatformInputManager.GetButtonDown("X"))
                    guioffset = 0;
                if (CrossPlatformInputManager.GetButton("X"))
                {
                    if (CrossPlatformInputManager.GetButton("down"))
                        change_finish = true;
                    else if (change_finish)
                        change = false;
                    else
                    {
                        change = true;
                        guialarm = 0;
                        
                    }
                        
                }
                else
                {
                    change = false;
                    change_finish = false;
                }

                if (change)
                {
                    Time.timeScale = 0;
                    
                    if (CrossPlatformInputManager.GetButtonDown("up"))
                        {
                            m_Character.CostumeChange(1);
                            change_finish = true;
                        }
                }
                else
                {
                    Time.timeScale = 1;
                    if (CrossPlatformInputManager.GetButtonDown("X"))
                    {
                        m_Character.CostumeChange(0);
                        change_finish = true;
                    }
                        
                    if (CrossPlatformInputManager.GetButtonDown("Y"))
                    {
                        m_Character.Shoot();
                    }
                    if (CrossPlatformInputManager.GetButtonDown("A"))
                        m_Character.CostumeChange(5);
                    if (!m_Jump)
                    {
                        // Read the jump input in Update so button presses aren't missed.
                        m_Jump = CrossPlatformInputManager.GetButtonDown("B");
                    }
                }              
            }
            

       }


        private void FixedUpdate()
        {
            if (act)
            {
                // Read the inputs.
                bool crouch = CrossPlatformInputManager.GetButton("down");
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                if (!change)
                // Pass all parameters to the character control script.
                    m_Character.Move(h, crouch, m_Jump);
                m_Jump = false;
            }        
        }


        private void OnTriggerStay2D(Collider2D other)
        {

            switch (other.tag)
            {
                case "clothes":
                    if (CrossPlatformInputManager.GetButtonDown("A"))
                    {
                        m_Character.CostumeChange(0);
                        Destroy(other.gameObject);
                    }
                    break;
                case "npc":
                    if (!change)
                    {
                        if (CrossPlatformInputManager.GetButtonDown("up"))
                        {
                            GetComponent<AvgEngine>().Open(other.name);
                            GetComponent<AvgEngine>().enabled = true;
                            
                            GetComponent<AvgEngineInput>().enabled = true;
                            
                            enabled = false;

                        }
                    }
                    break;

            }

        }

        private void OnGUI()
        {
            if (change)
            {
                if (guioffset<96)
                    guioffset+=8;
                guialarm++;
                if (guialarm > FPS / 3)
                {
                    guialarm = 1;
                    //guistatus = !guistatus;
                }
                Vector3 screenpos = Camera.main.WorldToScreenPoint(transform.position);
                GUI.Label(new Rect(screenpos.x-64,screenpos.y-64-guioffset, 128, 128), change_up);
                GUI.Label(new Rect(screenpos.x - 64, screenpos.y -64+ guioffset, 128, 128), change_down);
                GUI.Label(new Rect(screenpos.x - 64 - guioffset, screenpos.y - 64, 128, 128), change_left);
                GUI.Label(new Rect(screenpos.x - 64 + guioffset, screenpos.y - 64, 128, 128),change_right);
                
            }
            
                
        }
    }
}
