using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSign : MonoBehaviour {
    private Vector3 pos;
    private string[] label=new string[2];

    public float[] x, y;
    public float width,height;
    public GameObject exit;
    public GameObject milk;
	// Use this for initialization
	void Start () {
        GameObject exit_left = Instantiate(exit, new Vector3(2, 2,0), Quaternion.identity);
        GameObject exit_right = Instantiate(exit, new Vector3(16, 2,0), Quaternion.identity);
        GameObject player = Instantiate(milk, new Vector3(10, 2, 0), Quaternion.identity);
        player.name = "milk";

        string last_scene = PlayerPrefs.GetString("LastScene", null);
        switch (last_scene)
        {
            case "0Castle1Outside":
                label[0] = "PRINCESS CASTLE";
                label[1] = "PUZZLING FOREST";
                exit_left.GetComponent<GotoScene>().scenename = "0Castle1Outside";
                exit_right.GetComponent<GotoScene>().scenename = "1Forest";
                player.transform.position = new Vector3(4, 2, 0);
                break;
        }
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
