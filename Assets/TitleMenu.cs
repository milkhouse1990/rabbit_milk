using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour {

    private List page;
    //private GameObject title_menu;

    private string info;

    private GameObject farm_menu;
    private bool pause = false;

    public ListTool list_tool;
    private ListTool main_menu;
    public Rect list_pos;
    public Rect info_pos;

    // Use this for initialization
    void Start() {
        PlayerPrefs.DeleteAll();

        farm_menu = GameObject.Find("DataCanvas");
        farm_menu.SetActive(false);

        string binid = "MENU0000";

        ReadList rl = new ReadList(binid); 

        //Debug.Log(temp[0]);

        main_menu = Instantiate(list_tool,transform);
        main_menu.SetListPos(list_pos);
        main_menu.SetInfoPos(info_pos);
        main_menu.GetComponent<ListTool>().InitText(rl);
    }

    // Update is called once per frame
    void Update() {
        if (pause)
        {
            if (CrossPlatformInputManager.GetButtonDown("B"))
            {
                pause = false;
                farm_menu.SetActive(false);
                main_menu.gameObject.SetActive(true);
            }

        }
        else
        {
            if (CrossPlatformInputManager.GetButtonDown("A"))
            {
                switch (main_menu.GetComponent<ListTool>().GetFocus())
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
                    case 3:
                        pause = true;
                        PlayerPrefs.SetInt("SaveFlag", 0);
                        farm_menu.SetActive(true);
                        main_menu.gameObject.SetActive(false);
                        break;
                    case 4:
                        PlayerPrefs.DeleteAll();
                        Application.Quit();
                        break;
                }

            }
        }

    }
    void OnGUI()
    {
        //GUI.Label(title_pos, "兔耳魔女牛奶酱 Ver. 201709");
        //page.Display();
    }


}
