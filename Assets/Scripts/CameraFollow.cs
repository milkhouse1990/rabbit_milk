using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private float left_border;
    private float right_border;
    public float top_border;
    public float bottom_border;
    private int[] scroll_door;
    private int l_door;
    private int i = 0;
    private bool b_moving = false;
    private Transform target;
    public Transform airwall;
    public Transform warning;
	// Use this for initialization
	void Start () {
        GameObject[] invisible_doorxs=GameObject.FindGameObjectsWithTag("InvisibleDoorx");
        l_door = invisible_doorxs.Length;
        scroll_door = new int[l_door];
        foreach (GameObject invisible in invisible_doorxs)
            scroll_door[i++] = (int)invisible.transform.position.x;
        for (i=0;i<l_door;i++)
            for (int j=i;j<l_door;j++)
                if (scroll_door[i]>scroll_door[j])
                {
                    int temp = scroll_door[j];
                    scroll_door[j] = scroll_door[i];
                    scroll_door[i] = temp;
                }
        i = 0;
        left_border = 10;
        right_border = scroll_door[i]-10;
        target = GameObject.Find("milk").transform;
        if (target!=null)
        {
            while (target.position.x > scroll_door[i])
            {
                left_border = scroll_door[i] + 10;
                i++;
                right_border = scroll_door[i] - 10;
                
            }
            if (i>0)
            Instantiate(airwall, new Vector3(scroll_door[i-1],0,0) - new Vector3(1, 0, 0), Quaternion.identity);
        }
        Debug.Log(i);
	}
	
	// Update is called once per frame
	void Update () {
        if (b_moving)
        {
            if (transform.position.x >= left_border)
            {
                b_moving = false;
                target.gameObject.GetComponent<Platformer2DUserControl>().enabled = true;
            }

            else
                transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        }
        else
        {
            if (target!=null)
            {
                if (target.position.x < left_border)
                    transform.position = new Vector3(left_border, target.position.y, -10);
                else if (target.position.x > right_border)
                    transform.position = new Vector3(right_border, target.position.y, -10);
                else
                    transform.position = new Vector3(target.position.x, target.position.y, -10);

                if (target.position.y > top_border)
                    transform.position = new Vector3(transform.position.x, top_border, -10);
                else if (target.position.y < bottom_border)
                    transform.position = new Vector3(transform.position.x, bottom_border, -10);
                else
                    transform.position = new Vector3(transform.position.x, target.position.y, -10);
                if (i == scroll_door.Length - 1)
                {
                    //Instantiate(warning, new Vector3(0, 0, 0), Quaternion.identity);
                    i++;
                }
                else
                {
                    if (i < scroll_door.Length - 1)
                    {
                        if (target.position.x > scroll_door[i])
                        {
                            left_border = scroll_door[i] + 10;
                            i++;
                            right_border = scroll_door[i] - 10;
                            Instantiate(airwall, target.position - new Vector3(1, 0, 0), Quaternion.identity);
                            b_moving = true;
                            target.gameObject.GetComponent<Platformer2DUserControl>().enabled = false;
                            target.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, target.gameObject.GetComponent<Rigidbody2D>().velocity.y, 0);
                        }
                    }

                }
            }     
        }
        Debug.Log(right_border);
     
        


    }
    public bool GetMoving()
    {
        return b_moving;
    }
}
