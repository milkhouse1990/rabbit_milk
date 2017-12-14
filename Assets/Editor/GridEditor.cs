using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    Grid grid;
    int focus;
    int[] focus_tile;
    Texture2D[][] TileTextures = new Texture2D[2][];
    string[][] TileNames = new string[2][];
    string[] catecory;

    public void OnEnable()
    {
        grid = (Grid)target;

        // toolbar init
        focus = 0;
        focus_tile = new int[2] { 0, 0 };
        catecory = new string[2] { "Wall", "Enemy" };
        // load tiles
        for (int i = 0; i < 2; i++)
        {
            Object[] WallTiles = Resources.LoadAll("Prefabs\\" + catecory[i]);// as GameObject[];
            int l_WallTiles = WallTiles.Length;
            TileTextures[i] = new Texture2D[l_WallTiles];
            TileNames[i] = new string[l_WallTiles];
            int j = 0;
            foreach (Object tile in WallTiles)
            {
                GameObject gotile = tile as GameObject;
                TileTextures[i][j] = gotile.GetComponent<SpriteRenderer>().sprite.texture;
                TileNames[i][j] = gotile.name;
                j++;
            }
        }

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

        focus = GUILayout.Toolbar(focus, catecory, GUILayout.Width(64 * catecory.Length));

        focus_tile[focus] = GUILayout.Toolbar(focus_tile[focus], TileTextures[focus], GUILayout.Width(64 * TileTextures[focus].Length), GUILayout.Height(64));
        // GUILayout.EndHorizontal();
        GUILayout.Label("press a to add, s to save,\nwhen the scene window is activated.");

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
                    string name = TileNames[focus][focus_tile[focus]];
                    Object loaded = Resources.Load("Prefabs\\" + catecory[focus] + "\\" + name, typeof(GameObject));
                    GameObject pre = PrefabUtility.InstantiatePrefab(loaded) as GameObject;
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
                        li.tag = go.tag;
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
