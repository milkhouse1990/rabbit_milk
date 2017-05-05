using System;
using UnityEngine;

//namespace UnityStandardAssets._2D
//{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        public Transform bullet;
        public Transform clothes;

        [SerializeField] private float m_MaxSpeed = 8f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheckL;    // A position marking where to check if the player is grounded.
        private Transform m_GroundCheckR;
        const float k_GroundedRadius = .01f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private bool m_GroundedL;
        private bool m_GroundedR;
        private bool m_Crouch;
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        public int costume;//0 majo 1 maid 2 idol 3 mermaid 4 hime 5 dress 6 bunny 7 bathtowel
        private bool double_jump = false;
        private bool m_clothes = false;

        private int invincible = 0;
        
        private void Awake()
        {
            // Setting up references.
            m_GroundCheckL = transform.Find("GroundCheckL");
            m_GroundCheckR = transform.Find("GroundCheckR");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
        if (invincible % 2 == 0)
            GetComponent<SpriteRenderer>().enabled = true;
        else
            GetComponent<SpriteRenderer>().enabled = false;

    }


        private void FixedUpdate()
        {
        // status update
        if (invincible > 0)
        
            invincible--;
        
            

        m_Grounded = false;
            m_GroundedL = false;
            m_GroundedR = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheckL.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_GroundedL = true;       
            }
            colliders = Physics2D.OverlapCircleAll(m_GroundCheckR.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_GroundedR = true;
             }
            if (m_GroundedL||m_GroundedR)
                {
                    m_Grounded = true;
                    double_jump = false;
                }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
            /*
            colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(100, 100), 0);
            for (int i=0;i<colliders.Length;i++)
            {
                if (colliders[i].gameObject.name == "clothes")
                    m_clothes = true;
            }*/
        }


        


        public void Move(float move, bool crouch, bool jump, bool jump_realise)
        {
            
            
                if (costume == 5)
                    crouch = false;
                // If crouching, check to see if the character can stand up
                if (!crouch && m_Anim.GetBool("Crouch"))
                {
                    // If the character has a ceiling preventing them from standing up, keep them crouching
                    if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                    {
                        crouch = true;
                    }
                }                

                // Set whether or not the character is crouching in the animator
                m_Anim.SetBool("Crouch", crouch);
                m_Crouch = crouch;

                //only control the player if grounded or airControl is turned on
                if (m_Grounded || m_AirControl)
                {
                    // Reduce the speed if crouching by the crouchSpeed multiplier
                    move = (crouch ? move * m_CrouchSpeed : move);

                    // The Speed animator parameter is set to the absolute value of the horizontal input.
                    m_Anim.SetFloat("Speed", Mathf.Abs(move));
            /*
            if (move > 0)
                move = 1;
            else if (move < 0)
                move = -1;
                */
                    // Move the character
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                    // If the input is moving the player right and the player is facing left...
                    if (move > 0 && !m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }
                    // Otherwise if the input is moving the player left and the player is facing right...
                    else if (move < 0 && m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }
                }
                // If the player should jump...
                if (m_Grounded && jump && m_Anim.GetBool("Ground"))
                {
                    // Add a vertical force to the player.
                    m_Grounded = false;
                    m_Anim.SetBool("Ground", false);
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 12);
            }
                else if (!m_Grounded && jump &&!double_jump)
                {
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 12);
                    double_jump = true;
                }
        if (jump_realise && m_Rigidbody2D.velocity.y > 0)
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        
        public void CostumeChange(int cos)
        {
            costume = cos;
            m_Anim.SetInteger("Costume", cos);
            switch (cos)
            {
                case 0:
                    m_MaxSpeed = 10f;
                    break;
                case 6:
                    if (m_Crouch)
                    {
                        m_MaxSpeed = 15f;
                        //speed = 16;
                        //motion = 0;

                        Instantiate(clothes, transform.position, transform.rotation);
                    }
                    else
                    {
                        costume = 0;
                        m_Anim.SetInteger("Costume", 0);
                    }
                    break;
            }
        }

        public void Shoot()
        {
            if (costume==0)
            {
                
                    Transform new_bullet = Instantiate(bullet, new Vector3(transform.position.x+transform.localScale.x,transform.position.y,0), transform.rotation);
                    new_bullet.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * 10, 0);
                    new_bullet.gameObject.transform.localScale = transform.localScale;
                
            }
        }

        public int GetCostume()
        {
            return costume;
        }
        public bool GetGround()
        {
            return m_Grounded;
        }
    public void Backward(float deltax)
    {
        if (deltax > 0)
            transform.position=new Vector3(transform.position.x - 2,transform.position.y,0);
        else
            transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
        invincible = 60;
    }
    public int GetInvincible()
    {
        return invincible;
    }
    }
//}
