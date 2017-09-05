using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ReadList : MonoBehaviour {
    public string[] Read(string path, string binid, int times=1)
    {
        string[] res = new string[2*times];//return {item, info}
        if (File.Exists(path))
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            byte[] labelr = new byte[9];
            byte[] lenr = new byte[4];
            Decoder d = Encoding.UTF8.GetDecoder();

            for (int i=0;i<times;)
            {
                //read head
                fs.Read(labelr, 0, labelr.Length);
                fs.Read(lenr, 0, lenr.Length);
                //decode label

                char[] label_chars = new char[labelr.Length];
                d.GetChars(labelr, 0, labelr.Length, label_chars, 0);
                string label_str = new string(label_chars);
                //decode length
                int len_int = bytesToInt(lenr, 0);
                //check label
                if ((i==0)&(label_str == binid)|i>0)
                {
                    //read raw
                    byte[] bytes2 = new byte[len_int - 1];
                    fs.Read(bytes2, 0, (int)bytes2.Length);
                    int len_char = d.GetCharCount(bytes2, 0, bytes2.Length, true);
                    char[] content = new char[len_char];
                    d.GetChars(bytes2, 0, bytes2.Length, content, 0);
                    res[i*2] = new string(content);
                    //Debug.Log(res[i*2]);

                    //split into items and infos
                    int split_point = res[i*2].IndexOf("\n\n");
                    res[i*2+1] = res[i*2].Substring(split_point);
                    res[i*2] = res[i*2].Substring(0, split_point);

                    fs.Seek(1, SeekOrigin.Current);
                    i++;
                }
                else
                {
                    fs.Seek(len_int, SeekOrigin.Current);
                    if (fs.Position == fs.Length - 1)
                        Debug.Log("Wrong BinID.");
                }
                
            }

            fs.Close();
        }
        else
            Debug.Log("game file lost: " + path);

        return res;
    }
    public static int bytesToInt(byte[] src, int offset)
    {
        int value;
        value = (int)(((src[offset] & 0xFF) << 24)
                | ((src[offset + 1] & 0xFF) << 16)
                | ((src[offset + 2] & 0xFF) << 8)
                | (src[offset + 3] & 0xFF));
        return value;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
