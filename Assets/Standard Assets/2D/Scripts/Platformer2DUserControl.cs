using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool act=true;
        private bool change = false;
        private bool change_finish = false;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }
        

        private void Update()
        {
            if (act)
            {
                //changing check
                if (CrossPlatformInputManager.GetButton("X"))
                {
                    if (CrossPlatformInputManager.GetButton("down"))
                        change_finish = true;
                    else if (change_finish)
                        change = false;
                    else
                        change = true;
                }
                else
                {
                    change = false;
                    change_finish = false;
                }

                if (change)
                {
                    if (CrossPlatformInputManager.GetButtonDown("up"))
                        {
                            m_Character.CostumeChange(1);
                            change_finish = true;
                        }
                }
                else
                {
                    if (CrossPlatformInputManager.GetButtonDown("X"))
                        m_Character.CostumeChange(0);
                    if (CrossPlatformInputManager.GetButtonDown("Y"))
                    {
                        m_Character.Shoot();
                    }
                    if (!change)
                    {
                        if (CrossPlatformInputManager.GetButtonDown("A"))
                            m_Character.CostumeChange(5);
                    }
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
    }
}
