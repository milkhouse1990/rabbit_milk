using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics2DM : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    private Vector3 gravity = new Vector3(0, -30, 0);
    private ColliderBox mcb;
    private Rect ImageSize = new Rect(-1, -1.5f, 2, 3);
    public bool mGrounded = false;
    void Awake()
    {
        mcb = GetComponent<ColliderBox>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.Log("physics2dm");
        velocity += gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        // check ground
        ColliderBox[] cbs = FindObjectsOfType<ColliderBox>() as ColliderBox[];
        foreach (ColliderBox cb in cbs)
        {
            if (cb.transform == transform)
                continue;

            Vector3 LeftFoot = transform.position + new Vector3(-mcb.size.width / 2, -ImageSize.height / 2, 0);
            if (cb.CheckIn(LeftFoot))
            {
                transform.position = new Vector3(transform.position.x, cb.transform.position.y + cb.size.height / 2, transform.position.z);
                transform.position += new Vector3(0, ImageSize.height / 2, 0);
                velocity = Vector3.zero;
                mGrounded = true;
                break;
            }
            else
            {
                Vector3 RightFoot = transform.position + new Vector3(mcb.size.width / 2, -ImageSize.height / 2, 0);
                if (cb.CheckIn(RightFoot))
                {
                    transform.position = new Vector3(transform.position.x, cb.transform.position.y + cb.size.height / 2, transform.position.z);
                    transform.position += new Vector3(0, ImageSize.height / 2, 0);
                    velocity = Vector3.zero;
                    mGrounded = true;
                    break;
                }
            }
        }
    }

}
