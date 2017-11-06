using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
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

    private Rect view_range;
    // Use this for initialization
    void Start()
    {
        view_range = new Rect(0, 0, 20, 11.25f);
        GameObject[] invisible_doorxs = GameObject.FindGameObjectsWithTag("InvisibleDoorx");
        l_door = invisible_doorxs.Length;
        if (l_door != 0)
            scroll_door = new int[l_door];
        else
            scroll_door = new int[1] { 20 };
        l_door = scroll_door.Length;
        foreach (GameObject invisible in invisible_doorxs)
            scroll_door[i++] = (int)invisible.transform.position.x;

        //对scroll_door里的元素进行升序排序
        for (i = 0; i < l_door; i++)
            for (int j = i; j < l_door; j++)
                if (scroll_door[i] > scroll_door[j])
                {
                    int temp = scroll_door[j];
                    scroll_door[j] = scroll_door[i];
                    scroll_door[i] = temp;
                }

        i = 0;
        view_range = new Rect(view_range.x, view_range.y, scroll_door[i] - view_range.x, view_range.height);
        left_border = view_range.x + 10;
        right_border = view_range.x + view_range.width - 10;
        target = GameObject.Find("milk").transform;
        if (target != null)
        {
            while (target.position.x > right_border + 10)
            {
                i++;
                view_range = new Rect(view_range.x + view_range.width, view_range.y, scroll_door[i] - view_range.x - view_range.width, view_range.height);
                left_border = view_range.x + 10;
                right_border = view_range.x + view_range.width - 10;

            }
            if (i > 0)
                Instantiate(airwall, new Vector3(view_range.x - 1, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            if (target != null)
            {
                if (target.position.x < left_border)
                    transform.position = new Vector3(left_border, target.position.y, -20);
                else if (target.position.x > right_border)
                    transform.position = new Vector3(right_border, target.position.y, -20);
                else
                    transform.position = new Vector3(target.position.x, target.position.y, -20);

                if (target.position.y > top_border)
                    transform.position = new Vector3(transform.position.x, top_border, -20);
                else if (target.position.y < bottom_border)
                    transform.position = new Vector3(transform.position.x, bottom_border, -20);
                else
                    transform.position = new Vector3(transform.position.x, target.position.y, -20);
                if (i == l_door - 1)
                {
                    //Instantiate(warning, new Vector3(0, 0, 0), Quaternion.identity);
                    i++;
                }
                else
                {
                    if (i < l_door - 1)
                    {
                        if (target.position.x > view_range.x + view_range.width)
                        {
                            i++;
                            view_range = new Rect(view_range.x + view_range.width, view_range.y, scroll_door[i] - view_range.x - view_range.width, view_range.height);
                            left_border = view_range.x + 10;
                            right_border = view_range.x + view_range.width - 10;

                            Instantiate(airwall, target.position - new Vector3(1, 0, 0), Quaternion.identity);
                            b_moving = true;
                            target.gameObject.GetComponent<Platformer2DUserControl>().enabled = false;
                            target.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, target.gameObject.GetComponent<Rigidbody2D>().velocity.y, 0);
                        }
                    }

                }
            }
        }




    }
    public bool GetMoving()
    {
        return b_moving;
    }
}
