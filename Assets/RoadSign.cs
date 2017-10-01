using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSign : MonoBehaviour {
    private Vector3 pos;
    private string[] label = { "PRINCESS CASTLE", "PUZZLING FOREST" };

    public float[] x, y;
    public float width,height;
	// Use this for initialization
	void Start () {
        pos=Camera.main.WorldToScreenPoint(transform.position);
    for (int i=0;i<2;i++)
        label[i] = "<color=magenta>To\n" + label[i]+"</color>";

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        for (int i=0;i<2;i++)
        GUI.Label(new Rect(pos.x + x[i], pos.y + y[i], width,height), label[i]);

    }
}
