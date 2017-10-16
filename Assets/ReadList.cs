using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ReadList {
    public string item;
    public string info;
    public ReadList(string binid)
    {      
        TextAsset ta = Resources.Load("Text\\"+binid) as TextAsset;
        if (ta!=null)
        {              
                    //get content
                    item = ta.text;

                    //split into items and infos
                    int split_point = item.IndexOf("\r\n\r\n");
            if (split_point == -1)
                Debug.Log("cant find \r\n\r\n in " + binid);
          
                    info = item.Substring(split_point);
                    item = item.Substring(0, split_point);
    
        }
        else
            Debug.Log("game file lost: " + binid);
    }
}
