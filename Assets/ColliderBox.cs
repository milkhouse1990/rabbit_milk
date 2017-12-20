﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBox : MonoBehaviour
{
    public Rect size;// from origin
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool CheckIn(Vector3 position)
    {
        Rect4 collider = new Rect4(size);
        collider = new Rect4(transform.position.y + collider.up, transform.position.y + collider.down, transform.position.x + collider.left, transform.position.x + collider.right);
        if (position.x >= collider.left)
            if (position.x <= collider.right)
                if (position.y <= collider.up)
                    if (position.y >= collider.down)
                        return true;
        return false;
    }
}
