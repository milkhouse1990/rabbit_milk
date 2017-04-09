using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMap : MonoBehaviour {

    public string title;
    public Transform wall;

    // Use this for initialization
    void Start () {

        string path = "Map\\" + title + ".txt";
        string[] raw = File.ReadAllLines(path);

        for(int i=0;i<raw.Length;i++)
        {
            
            string[] row = raw[i].Split(' ');
            for(int j=0;j<row.Length;j++)
            {
                int temp;
                int.TryParse(row[j], out temp);
                if ((temp==1)?true:false)
                {
                    Instantiate(wall, new Vector3(j, raw.Length-i-1, 0), transform.rotation);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
