using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MakeBin : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //read .txt
        string path = "Text\\mnu.txt";
        string[] readins = File.ReadAllLines(path);

        //open .bin
        path = "Text\\mnu.bin";
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

        string raw = "";
        
        //int len = 0;
        string label = "";
        foreach (string readin in readins)
        {
            if (readin!="")
                if (readin[0] == '@')
                {
                if (label != "")
                    WriteBlock(fs, label, raw);
                label = readin;
                //len = 0;
                raw = "";
                continue;
                }
            //len += readin.Length + 1;
            //if (readins.Length > k)
            raw += readin + "\n";
            //else
            //raw += readin;
        }
        WriteBlock(fs, label, raw);

        fs.Flush();
        fs.Close();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void WriteBlock(FileStream fs, string label, string raw)
    {
        //encode
        Encoder e = Encoding.UTF8.GetEncoder();
        int len = e.GetByteCount(raw.ToCharArray(), 0, raw.Length, true);
        byte[] bytes = new byte[label.Length+4 + len];
        //Debug.Log(raw.Length);

        e.GetBytes(label.ToCharArray(), 0, label.Length, bytes, 0, true);
        byte[] bytes_temp = intToBytes(len);
        for (int i = 0; i < 4; i++)
            bytes[i+label.Length] = bytes_temp[i];
        e.GetBytes(raw.ToCharArray(), 0, raw.Length, bytes, label.Length+4, true);
        //write
        fs.Write(bytes, 0, (int)bytes.Length);
    }
    public static byte[] intToBytes(int value)
    {
        byte[] src = new byte[4];
        src[0] = (byte)((value >> 24) & 0xFF);
        src[1] = (byte)((value >> 16) & 0xFF);
        src[2] = (byte)((value >> 8) & 0xFF);
        src[3] = (byte)(value & 0xFF);
        return src;
    }
}
