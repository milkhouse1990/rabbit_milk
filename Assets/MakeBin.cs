using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MakeBin : MonoBehaviour
{
    public string MakeWorld;
    // Use this for initialization
    void Start()
    {
        string world = "x";
        string scene = "x";
        string cut = "x";
        string filename = "xxx";
        //read .txt
        string path = "Scenario\\world" + MakeWorld + ".script";
        string[] readins = File.ReadAllLines(path);

        path = "Assets\\Resources\\Text\\PLOT" + world + scene + cut + ".txt";
        StreamWriter sw = new StreamWriter(path);

        foreach (string readin in readins)
        {
            //comment
            if (readin.Length > 2)
                if (readin[0] == '/' && readin[1] == '/')
                    continue;
            {
                //split by \s
                string[] commands = readin.Split(' ');
                //execute commands
                switch (commands[0])
                {
                    //file name
                    case "world":
                        world = commands[1];
                        break;
                    case "scene":
                        scene = commands[1];
                        break;
                    case "cut":
                        sw.Flush();
                        sw.Close();
                        if (filename != "xxx")
                            Debug.Log(filename + " success!");
                        cut = commands[1];
                        //open or create .txt
                        filename = "PLOT" + world + scene + cut + ".txt";
                        path = "Assets\\Resources\\Text\\" + filename;
                        sw = new StreamWriter(path);
                        break;
                    case "npccut":
                        sw.Flush();
                        sw.Close();
                        if (filename != "xxx")
                            Debug.Log(filename + " success!");
                        cut = commands[1];
                        filename = "NPC" + world + scene + cut + ".txt";
                        path = "Assets\\Resources\\Text\\" + filename;
                        sw = new StreamWriter(path);
                        break;
                    case "":
                        break;
                    //file contents
                    default:
                        if (isNpcName(commands[0]) || isCommand(commands[0]))
                            sw.WriteLine(readin);
                        else
                            Debug.Log("cannot understand this command: " + readin)
;
                        break;
                }
            }
        }
        sw.Flush();
        sw.Close();
        Debug.Log(filename + " success!");
        File.Delete("Assets\\Resources\\Text\\PLOTxxx.txt");
    }

    public bool isNpcName(string name)
    {
        string[] npcnames = { "皇家妹抖", "公主殿下", "牛奶酱", "草莓汁", "皇家妹抖？", "Drop", "？？？", "果酱亲", "台下", "声音A", "声音B", "果酱P" };
        foreach (string npcname in npcnames)
            if (npcname == name)
                return true;
        return false;
    }
    public bool isCommand(string name)
    {
        string[] commands = { "create", "downstairs", "EndingFastest", "charamove", "boss", "add", "plot", "gotoscene" };
        foreach (string npcname in commands)
            if (npcname == name)
                return true;
        return false;
    }
}
