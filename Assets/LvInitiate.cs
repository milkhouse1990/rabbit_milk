using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvInitiate : MonoBehaviour
{
    private string scenename;
    // Use this for initialization
    void OnEnable()
    {
        scenename = "0Castle1Outside";
        // Load Map
        LoadMap(scenename);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LoadMap(string MapName)
    {
        XmlSaver xs = new XmlSaver();
        string path = "Level/" + MapName + ".lv";
        if (xs.hasFile(path))
        {
            string datastring = xs.LoadXML(path);
            LevelInfo levelinfo = xs.DeserializeObject(datastring, typeof(LevelInfo)) as LevelInfo;

            Camera.main.GetComponent<CameraFollow>().CameraMode = 0;
            Camera.main.GetComponent<CameraFollow>().Rooms = levelinfo.Rooms;

            foreach (LevelItem li in levelinfo.items)
            {
                string tag = li.tag;
                string name = li.name;
                float x = li.x + 1;
                float y = li.y;

                GameObject pre = Resources.Load("Prefabs\\" + tag + "\\" + name, typeof(GameObject)) as GameObject;
                if (!pre)
                    Debug.Log("tile " + name + " load failed.");

                pre = Instantiate(pre, new Vector3(x, y, 0), Quaternion.identity);
                pre.name = name;
            }


        }
    }
    public void ReloadMap(string MapName)
    {
        GameObject[] AllGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in AllGameObjects)
        {
            if (go == gameObject)
                continue;
            if (go.name == "grid" || go.name == "Main Camera" || go.name == "scenario")
                continue;
            if (go.transform.parent != null)
                continue;
            GameObject.Destroy(go);
        }
        LoadMap(MapName);
    }
}
