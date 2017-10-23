using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ReadList {
    public string[] items;
    public string[] infos;
    public ReadList(string binid)
    {      
        TextAsset ta = Resources.Load("Text\\"+binid) as TextAsset;
        if (ta!=null)
        {
            //get content
            string text = ta.text;

            //split into items and infos
            int split_point = text.IndexOf("\r\n\r\n");
            if (split_point == -1)
                Debug.Log("cant find \r\n\r\n in " + binid);
            items = text.Substring(0, split_point).Split('\n');

            infos = new string[items.Length];
            string temp = text.Substring(split_point);
            for (int i = 0; i < items.Length; i++)
            {

                int point = temp.LastIndexOf("\r\n\r\n");
                infos[items.Length - 1 - i] = temp.Substring(point + 4);
                temp = temp.Substring(0, point);
                //Debug.Log(infos[items.Length - 1 - i]);
            }
        }
        else
            Debug.Log("game file lost: " + binid);
    }
}
