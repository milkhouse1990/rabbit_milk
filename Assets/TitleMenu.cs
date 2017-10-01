using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ReadList))]
public class TitleMenu : MonoBehaviour {
    public List list;
    public Texture2D vector;

    public Rect list_pos;
    public Rect info_pos;
    public Rect title_pos;

    private List page;

    private string info;

    // Use this for initialization
    void Start() {
        string binid = "@MENU0000";

        string path = "Text\\mnu.bin";

        string[] temp = GetComponent<ReadList>().Read(path, binid);

        page = Instantiate<List>(list);
        page.Init(list_pos, info_pos, vector);
        page.InitText(temp[0], temp[1]);
    }

    // Update is called once per frame
    void Update() {
        if (CrossPlatformInputManager.GetButtonDown("A"))
        {
            switch (page.GetFocus())
            {
                case 0:
                    SceneManager.LoadScene("Party_demo");
                    break;
                case 1:
                    SceneManager.LoadScene("Highway_demo");
                    break;
                case 2:
                    SceneManager.LoadScene("database");
                    break;
                case 4:
                    Application.Quit();
                    break;
            }

        }


    }
    void OnGUI()
    {
        GUI.Label(title_pos, "兔耳魔女牛奶酱 Ver. 201709");
        page.Display();
    }


}
