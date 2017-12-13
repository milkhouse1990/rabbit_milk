using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    Grid grid;
    int focus = 1;
    int focus_tile = 0;
    Texture2D[] tilelist;
    Texture2D[] WallTileTextures;
    string[] tilename = { "npc0", "enemy" };
    string[] WallTileNames;

    public void OnEnable()
    {
        grid = (Grid)target;

        //wall tiles
        Object[] WallTiles = Resources.LoadAll("Prefabs\\Walls");// as GameObject[];
        Debug.Log(WallTiles.Length);
        int l_WallTiles = WallTiles.Length;
        WallTileTextures = new Texture2D[l_WallTiles];
        WallTileNames = new string[l_WallTiles];
        int i = 0;
        foreach (Object tile in WallTiles)
        {
            GameObject gotile = tile as GameObject;
            WallTileTextures[i] = gotile.GetComponent<SpriteRenderer>().sprite.texture;
            WallTileNames[i] = gotile.name;
            i++;
        }



        tilelist = new Texture2D[2];
        GameObject pre = Resources.Load("Prefabs\\Enemies\\" + "npc0") as GameObject;
        Texture2D texture = pre.GetComponent<SpriteRenderer>().sprite.texture;
        tilelist[0] = texture;
        pre = Resources.Load("Prefabs\\Enemies\\" + "enemy") as GameObject;
        texture = pre.GetComponent<SpriteRenderer>().sprite.texture;
        tilelist[1] = texture;

        SceneView.onSceneGUIDelegate = GridUpdate;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid Width");
        grid.width = EditorGUILayout.FloatField(grid.width, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid Height");
        grid.height = EditorGUILayout.FloatField(grid.height, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Scene Name");
        grid.scenename = EditorGUILayout.TextField(grid.scenename, GUILayout.Width(100));
        GUILayout.EndHorizontal();

        SceneView.RepaintAll();
    }
    void GridUpdate(SceneView sceneview)
    {
        Handles.BeginGUI();
        GUILayout.BeginArea(new Rect(0, 0, 1000, 640));
        // GUILayout.BeginHorizontal();

        string[] toolbar_text = { "Wall", "Enemy" };
        focus = GUILayout.Toolbar(focus, toolbar_text, GUILayout.Width(100));
        if (focus == 0)
            focus_tile = GUILayout.Toolbar(focus_tile, WallTileTextures, GUILayout.Width(64 * WallTileTextures.Length), GUILayout.Height(64));
        else
            focus_tile = GUILayout.Toolbar(focus_tile, tilelist, GUILayout.Width(128), GUILayout.Height(64));
        // GUILayout.EndHorizontal();

        GUILayout.EndArea();
        Handles.EndGUI();

        Event e = Event.current;

        Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePos = r.origin;

        if (e.isKey)
        {
            switch (e.character)
            {
                case 'a':
                    string name = WallTileNames[focus_tile];
                    string catecory = "Walls";
                    GameObject pre = Resources.Load("Prefabs\\" + catecory + "\\" + name, typeof(GameObject)) as GameObject;
                    pre = Instantiate(pre);
                    pre.name = name;

                    Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.width) * grid.width + grid.width / 2, Mathf.Floor(mousePos.y / grid.height) * grid.height + grid.height / 2, 0);
                    pre.transform.position = aligned;

                    break;
                //save the map
                case 's':
                    Debug.Log("save map start!");

                    string path = "Level";

                    //check the directory
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    path += "/" + grid.scenename + ".lv";

                    LevelInfo levelinfo = new LevelInfo();

                    GameObject[] AllGameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
                    foreach (GameObject go in AllGameObjects)
                    {
                        if (go.name == "grid" || go.name == "Main Camera")
                            continue;
                        if (go.transform.parent != null)
                            continue;
                        LevelItem li = new LevelItem();
                        li.name = go.name;
                        li.x = go.transform.position.x;
                        li.y = go.transform.position.y;
                        levelinfo.items.Add(li);
                    }

                    XmlSaver xs = new XmlSaver();
                    string datastring = xs.SerializeObject(levelinfo, typeof(LevelInfo));
                    xs.CreateXML(path, datastring);

                    Debug.Log("save map success!");
                    break;
            }
        }
    }
}
