using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

//namespace UnityStandardAssets._2D
//{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private const int FPS = 60;
        
        private PlatformerCharacter2D m_Character;
        private Status m_Status; 
        private bool m_Jump;
        private bool b_Jump;
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

        private bool m_waitnpc=false;
        public Texture2D waitnpc;
        private string[] npcplot;
    
    public Transform warning;

        private int d = 0;

        private GameObject teki;
        private bool b_damage;


    private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            m_Status = GetComponent<Status>();
        }
        

        private void Update()
        {              
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
                    if (CrossPlatformInputManager.GetButtonDown("up"))
                    {
                        if (m_waitnpc)
                        {
                            EnterAVGMode(npcplot);
                            
                         }
                    }
                        
                        if (CrossPlatformInputManager.GetButtonDown("X"))
                    {
                        int cos = m_Character.GetCostume();
                        if (cos>0 && cos<6)
                        {
                            m_Character.CostumeChange(0);
                            change_finish = true;
                        }
                            
                    }
                        
                    if (CrossPlatformInputManager.GetButtonDown("Y"))
                    {
                        m_Character.Shoot();
                    }
                    if (CrossPlatformInputManager.GetButtonDown("A"))
                        m_Character.CostumeChange(6);
                    if (!m_Jump)
                    {
                        // Read the jump input in Update so button presses aren't missed.
                        m_Jump = CrossPlatformInputManager.GetButtonDown("B");
                    }
            if (!b_Jump)
                b_Jump = CrossPlatformInputManager.GetButtonUp("B");
                }
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

                //damage
                if (b_damage)
            {
                if (m_Character.GetInvincible() == 0)
                {
                    m_Status.GetDamage(teki.GetComponent<Status>());
                    m_Character.Backward(teki.transform.position.x - transform.position.x);
                }
            }
            
            

       }


        private void FixedUpdate()
        {
            //if (act)
            //{
                // Read the inputs.
                bool crouch = CrossPlatformInputManager.GetButton("down");
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                if (!change)
                // Pass all parameters to the character control script.
                    m_Character.Move(h, crouch, m_Jump,b_Jump);
                m_Jump = false;
        b_Jump = false;
           // }
            
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
        //Debug.Log(other.name);
        switch (other.tag)
            {
                case "enemy":
                    b_damage = true;
                    teki = other.gameObject;
                    
                    Destroy(other.gameObject);
                break;
                case "npc":
                    m_waitnpc = true;
                    npcplot = other.GetComponent<Npc>().plot;
                    break;
            }
            switch(other.name)
            {
                case "onsei":
                //Debug.Log(m_Status.b_autorecover);
                m_Status.b_autorecover = true;
                //Debug.Log(m_Status.b_autorecover);
                break;
                case "ev_bathtowel":
                    m_Character.CostumeChange(7);
                    break;
                case "ev_majo":
                    m_Character.CostumeChange(0);
                    break;
            case "ev_warning":
                
                Instantiate(warning, new Vector3(0, 0, 0), Quaternion.identity);
                break;
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
                
              }
        }
        
        void OnTriggerExit2D(Collider2D other)
        {
        switch (other.tag)
        {
            case "enemy":
                b_damage = false;
                teki = null;
                break;
        }
        
            m_waitnpc = false;
            m_Status.b_autorecover = false;
        }
        void OnCollisionEnter2D(Collision2D other)
        {
            switch (other.gameObject.tag)
            {
                case "enemy":
                    b_damage = true;
                    teki = other.gameObject;

                    break;              
            }
        }
    void OnCollisionExit2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "enemy":
                b_damage = false;
                teki = null;
                break;
        }
    }

        void OnGUI()
        {
            Vector3 screenpos = Camera.main.WorldToScreenPoint(transform.position);
            if (change)
            {
                if (guioffset < 96)
                    guioffset += 8;
                guialarm++;
                if (guialarm > FPS / 3)
                {
                    guialarm = 1;
                    //guistatus = !guistatus;
                }
                
                GUI.Label(new Rect(screenpos.x - 64, screenpos.y - 32 - guioffset, 128, 128), change_up);
                GUI.Label(new Rect(screenpos.x - 64, screenpos.y - 32 + guioffset, 128, 128), change_down);
                GUI.Label(new Rect(screenpos.x - 64 - guioffset, screenpos.y - 32, 128, 128), change_left);
                GUI.Label(new Rect(screenpos.x - 64 + guioffset, screenpos.y - 32, 128, 128), change_right);

            }
            else if (m_waitnpc)
                if (m_Character.GetGround())
                    GUI.Label(new Rect(screenpos.x-32, screenpos.y-64 , 64, 64), waitnpc);         
        }

        public void EnterAVGMode(string[] plot)
    {
        GetComponent<AvgEngine>().Open(plot);
        GetComponent<AvgEngine>().enabled = true;
        GetComponent<AvgEngineInput>().enabled = true;
        enabled = false;
        //GetComponent<hp_gauge>().enabled = false;

        m_Character.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        m_Character.Move(0, false, false, true);
    }
        
    }
//}
