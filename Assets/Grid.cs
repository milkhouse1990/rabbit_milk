using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public float width = 1;
    public float height = 1;
    public string scenename = "scene";
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDrawGizmos()
    {
        Vector3 pos = Camera.current.transform.position;
        for (float y = pos.y - 800.0f; y < pos.y + 800.0f; y += height)
        {
            Gizmos.DrawLine(new Vector3(-1000000.0f, Mathf.Floor(y / height) * height, 0.0f), new Vector3(1000000.0f, Mathf.Floor(y / height) * height, 0.0f));
        }
        for (float x = pos.x - 1200.0f; x < pos.x + 1200.0f; x += width)
        {
            Gizmos.DrawLine(new Vector3(Mathf.Floor(x / width) * width, -1000000.0f, 0.0f), new Vector3(Mathf.Floor(x / width) * width, 1000000.0f, 0.0f));
        }
    }
}