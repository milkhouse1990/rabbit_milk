using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ReadList : MonoBehaviour {
    public string[] Read(string binid)
    {
        string[] res = new string[2];
       
        TextAsset ta = Resources.Load("Text\\"+binid) as TextAsset;
        if (ta!=null)
        {              
                    //get content
                    res[0] = ta.text;

                    //split into items and infos
                    int split_point = res[0].IndexOf("\r\n\r\n");
                    res[1] = res[0].Substring(split_point);
                    res[0] = res[0].Substring(0, split_point);
    
        }
        else
            Debug.Log("game file lost: " + binid);
    
        return res;
    }
}
