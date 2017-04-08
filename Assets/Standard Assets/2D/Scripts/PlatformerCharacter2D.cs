using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        public Transform bullet;
        public Transform clothes;

        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private bool m_Crouch;
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private int costume=0;
        private bool double_jump = false;
        private bool m_clothes = false;

        private int invincible = 0;
        
        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
            //if (act)if (!change)
            //milk status update
            if (invincible > 0)
                invincible--;
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    double_jump = false;
                }
                    
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


        


        public void Move(float move, bool crouch, bool jump)
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
                //检测如果趴下前方是否有足够空间

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
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));//跳跃问题仍然需要解决
                }
                else if (!m_Grounded && jump &&!double_jump)
                {
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                    double_jump = true;
                }
            
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
            if (cos==0)
            {
                if (costume > 0 && costume < 6)
                {
                    if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                    {

                    }
                    else
                    {
                        costume = 0;
                        //change("rabbit");
                        

                        //speed = 8;
                        //motion = 0;
                        //mchange("rabbit", 0, 1, 0, tile, 2 * tile);
                    }

                }
            }
            else if (cos==5)
            {
                if (m_Crouch)
                {
                    costume = 5;
                    //speed = 16;
                    //motion = 0;
                    //mchange("milk_bunny", 0, 1, 0, tile, 0.5 * tile);
                    Instantiate(clothes, transform.position, transform.rotation);                  
                }
            }
        }

        public void Shoot()
        {
            if (costume==0)
            {                   
                    Transform new_bullet = Instantiate(bullet, transform.position, transform.rotation);
                    new_bullet.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * 10, 0);
                    new_bullet.gameObject.transform.localScale = transform.localScale;
                
            }
        }
    }
}
