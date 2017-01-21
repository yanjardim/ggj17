using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridEditorWindow : EditorWindow {

    private int xTiles, zTiles;
    private List<Tile> tileList;
    private Transform parent;
    private GameObject gridPrefab;

    private GUIStyle style;

    [MenuItem("Tools/Grid Editor")]
    static void InitWindow()
    {
        // Get existing open window or if none, make a new one:
        GridEditorWindow window = (GridEditorWindow)EditorWindow.GetWindow(typeof(GridEditorWindow));
        window.Show();
    }


    public void OnGUI()
    {

        GUILayout.Space(5);

        EditorGUILayout.LabelField("Grid Editor", style);

        GUILayout.Space(15);

        #region XZ

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("X", GUILayout.Width(20));
        xTiles = EditorGUILayout.IntField(xTiles);


        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Z", GUILayout.Width(20));       
        zTiles = EditorGUILayout.IntField(zTiles);

        EditorGUILayout.EndHorizontal();
        #endregion

        #region Grid Prefab
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Grid Prefab", GUILayout.Width(70));
        gridPrefab = (GameObject)EditorGUILayout.ObjectField(gridPrefab, typeof(GameObject), true);

        EditorGUILayout.EndHorizontal();

        #endregion

        #region Parent
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Parent", GUILayout.Width(70));
        parent = (Transform)EditorGUILayout.ObjectField(parent, typeof(Transform), true);
        EditorGUILayout.EndHorizontal();
        #endregion

        GUILayout.Space(5);
        if (GUILayout.Button("Build Grid"))
        {
            SpawnTileMap();
        }
        GUILayout.Space(5);
        //GUILayout.Button("Put objects in Grid");
        if (GUILayout.Button("Fix Camera Position"))
        {
            FixCameraPosition();
        }
        GUILayout.Space(5);
        if (GUILayout.Button("Clear Tiles"))
        {
            ClearTiles();
        }


    }

    public void SpawnTile(Vector3 pos)
    {
        GameObject cleiton = (GameObject)Instantiate(gridPrefab, pos, gridPrefab.transform.rotation);
        tileList.Add(cleiton.GetComponent<Tile>());
        cleiton.transform.SetParent(parent);
        cleiton.transform.name = "Tile(" + pos.x + "," + pos.z + ")";
    }

    public void SpawnTileMap()
    {
        for (int i = 0; i < xTiles; ++i)
        {
            for (int j = 0; j < zTiles; ++j)
            {
                SpawnTile(new Vector3(i, 0, j));
            }
        }

    }

    public void FixCameraPosition()
    {
        Camera.main.transform.position = tileList[tileList.Count - 1].gameObject.transform.position;
        Vector3 vect = Camera.main.transform.position;
        vect.y = 7;
        vect.x += xTiles * 0.75f;
        vect.z += zTiles * 0.75f;
        Camera.main.transform.position = vect;
        Camera.main.transform.LookAt(tileList[0].gameObject.transform);
    }


    public void ClearTiles()
    {
        foreach(GameObject a in GameObject.FindGameObjectsWithTag("Tile"))
        {
            DestroyImmediate(a);
        }
        tileList.Clear();
    }

    

    private void OnEnable()
    {
        tileList = new List<Tile>();
        style = new GUIStyle();
        style.fontSize = 20;
    }

    

}
