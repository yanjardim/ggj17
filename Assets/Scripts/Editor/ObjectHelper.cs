using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectHelper : EditorWindow {

    private bool select;
    private string text;

    public Tile hoveredTile, selectedTile;
    public LayerMask mask;

    public void OnEnable()
    {
        mask = 1 << LayerMask.NameToLayer("Grid");
        hoveredTile = null;
        selectedTile = null;
        select = false;
    }

    [MenuItem("Tools/Object Helper")]
    static void InitWindow()
    {
        // Get existing open window or if none, make a new one:
        ObjectHelper window = (ObjectHelper)EditorWindow.GetWindow(typeof(ObjectHelper));
        window.Show();
    }


    void OnSceneGUI(SceneView sceneView)
    {

        // Do your drawing here using Handles.
        Handles.BeginGUI();
        Debug.Log("ASD");
        if (select)
        {
            Event e = Event.current;
            Ray ray;
            RaycastHit hit;
            ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            { // aqui quando colide
                if (e.type == EventType.MouseDown && e.button == 1)
                { //se foi clicado
                    if (selectedTile != null)
                    { //se já tem um selectedTile anterior
                        var tempMaterial1 = new Material(selectedTile.GetComponent<Renderer>().sharedMaterial);
                        tempMaterial1.color = Color.white;
                        selectedTile.GetComponent<Renderer>().sharedMaterial = tempMaterial1;

                        //selectedTile.GetComponent<Renderer>().material.color = (Color.white); //muda a cor do antigo para branco
                    }

                    var tempMaterial2 = new Material(hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial);
                    tempMaterial2.color = Color.red;
                    hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial = tempMaterial2;

                    //hit.collider.gameObject.GetComponent<Renderer>().material.color = (Color.red); //mudo a cor do atual para vermelho
                    selectedTile = hit.collider.gameObject.GetComponent<Tile>(); //seta o novo selectedTile
                }
                else if (hoveredTile != selectedTile && hoveredTile != null && hit.collider.gameObject.GetComponent<Tile>() != hoveredTile)
                { //se não houve clique

                    var tempMaterial3 = new Material(hoveredTile.GetComponent<Renderer>().sharedMaterial);
                    tempMaterial3.color = Color.white;
                    hoveredTile.GetComponent<Renderer>().sharedMaterial = tempMaterial3;

                    //hoveredTile.GetComponent<Renderer>().material.color = (Color.white);
                }
                if (hit.collider.gameObject.GetComponent<Tile>() != selectedTile)
                {
                    var tempMaterial4 = new Material(hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial);
                    tempMaterial4.color = Color.cyan;
                    hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial = tempMaterial4;


                    //hit.collider.gameObject.GetComponent<Renderer>().material.color = (Color.cyan);
                    hoveredTile = hit.collider.gameObject.GetComponent<Tile>(); //seta hovered
                }



            }
            else if (hoveredTile != null && hoveredTile != selectedTile)
            {
                var tempMaterial5 = new Material(hoveredTile.GetComponent<Renderer>().sharedMaterial);
                tempMaterial5.color = Color.white;
                hoveredTile.GetComponent<Renderer>().sharedMaterial = tempMaterial5;

                //hoveredTile.GetComponent<Renderer>().material.color = (Color.white);
            }


            GUI.color = Color.white;

            #region Hovered Tile
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Hovered", GUILayout.Width(70));
            hoveredTile = (Tile)EditorGUILayout.ObjectField(hoveredTile, typeof(Tile), true);
            EditorGUILayout.EndHorizontal();
            #endregion

            #region Selected Tile
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Selected", GUILayout.Width(70));
            selectedTile = (Tile)EditorGUILayout.ObjectField(selectedTile, typeof(Tile), true);
            EditorGUILayout.EndHorizontal();
            #endregion

        }

        else if (selectedTile != null)
        {
            var tempMaterial6 = new Material(selectedTile.GetComponent<Renderer>().sharedMaterial);
            tempMaterial6.color = Color.white;
            selectedTile.GetComponent<Renderer>().sharedMaterial = tempMaterial6;

            selectedTile = null;
        }


        

        // Do your drawing here using GUI.
        Handles.EndGUI();
    }


    public void OnGUI()
    {
        GUILayout.Space(10);
        if (select)
        {
            GUI.color = Color.green;
            text = "Helper ON!";
        }
        else if (!select) {
            GUI.color = Color.red;
            text = "Helper OFF!";
        }

        if (GUILayout.Button(text))
        {
            if (select) select = false;
            else select = true;
        }
        GUILayout.Space(50);


        
    }


    void OnFocus()
    {
        // Remove delegate listener if it has previously
        // been assigned.
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
        // Add (or re-add) the delegate.
        SceneView.onSceneGUIDelegate += this.OnSceneGUI;
    }

    void OnDestroy()
    {
        // When the window is destroyed, remove the delegate
        // so that it will no longer do any drawing.
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    }


}
