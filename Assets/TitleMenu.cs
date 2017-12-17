using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{

    private List page;
    //private GameObject title_menu;

    private string info;

    public GameObject data_canvas;
    private GameObject farm_menu;
    private bool pause = false;

    public ListTool list_tool;
    private ListTool main_menu;
    public Rect list_pos;
    public Rect info_pos;

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.DeleteAll();

        farm_menu = Instantiate(data_canvas);
        farm_menu.GetComponentInChildren<GameDataManager>().save_flag = false;
        farm_menu.SetActive(false);

        //list
        string binid = "MENU0000";
        ReadList rl = new ReadList(binid);

        main_menu = Instantiate(list_tool, transform);
        main_menu.SetListPos(list_pos);
        main_menu.SetInfoPos(info_pos);
        main_menu.GetComponent<ListTool>().InitText(rl);
        main_menu.SetInfoAlign(TextAnchor.MiddleCenter);
    }

    // Update is called once per frame
    void Update()
    {
        //deactive
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
            //operation
            if (CrossPlatformInputManager.GetButtonDown("A"))
            {
                switch (main_menu.GetComponent<ListTool>().GetFocus())
                {
                    case 0:
                        if (Input.GetButton("L"))
                            SceneManager.LoadScene("School");
                        else
                        {
                            DataInit();
                            SceneManager.LoadScene("0Castle0Party");
                        }
                        break;
                    case 2:
                        SceneManager.LoadScene("database");
                        break;
                    case 1:
                        pause = true;
                        farm_menu.SetActive(true);
                        main_menu.gameObject.SetActive(false);
                        break;
                    case 4:
                        PlayerPrefs.DeleteAll();
                        Application.Quit();
                        break;
                    case 3:
                        SceneManager.LoadScene("Milkhouse");
                        break;
                }

            }
        }

    }
    void DataInit()
    {
        //crystal
        PlayerPrefs.SetInt("Crystal", 0);
        //fairy
        PlayerPrefs.SetInt("Fairy", 0);
    }
}
