using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectHelper : EditorWindow {

    private bool select;
    private string text;

    public Tile hoveredTile, selectedTile;
    public LayerMask mask;

    public GameObject obj;
    public Transform parent;
    private Color oldColor;
    private Material material;

    private float rotX, rotY, rotZ;

    private Tile lastTile;

    public void OnEnable()
    {
        mask = 1 << LayerMask.NameToLayer("Grid");
        hoveredTile = null;
        selectedTile = null;
        select = false;
        //if (oldColor != null)
        if(material != null)
        oldColor = material.color;
            
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

        if (select)
        {
            
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
 
            Event e = Event.current;
            Ray ray;
            RaycastHit hit;
            ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            { // aqui quando colide       
                
                if (e.type == EventType.MouseDown && e.button == 0)
                { //se foi clicado
                    if (selectedTile != null)
                    { //se já tem um selectedTile anterior
                        var tempMaterial1 = new Material(selectedTile.GetComponent<Renderer>().sharedMaterial);
                        tempMaterial1.color = oldColor;
                        selectedTile.GetComponent<Renderer>().sharedMaterial = tempMaterial1;

                        //selectedTile.GetComponent<Renderer>().material.color = (Color.white); //muda a cor do antigo para branco
                    }

                    var tempMaterial2 = new Material(hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial);
                    tempMaterial2.color = Color.red;
                    hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial = tempMaterial2;

                    

                    //hit.collider.gameObject.GetComponent<Renderer>().material.color = (Color.red); //mudo a cor do atual para vermelho
                    selectedTile = hit.collider.gameObject.GetComponent<Tile>(); //seta o novo selectedTile

                    if (obj != null && hit.collider.GetComponent<Tile>().obj == null)
                    {
                        Vector3 pos = selectedTile.gameObject.transform.position;

                        GameObject aux = Instantiate(obj, 
                            new Vector3(pos.x, pos.y, pos.z), 
                            obj.transform.rotation) as GameObject;

                        aux.transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);

                        /*aux.transform.position = new Vector3(aux.transform.position.x,
                            aux.transform.position.y + obj.GetComponent<Renderer>().bounds.max.y, 
                            aux.transform.position.z);*/
                        selectedTile.obj = aux;
                        lastTile = hit.collider.GetComponent<Tile>();
                        if(parent != null)
                        {
                            aux.transform.SetParent(parent);
                        }
                    }
                }
                else if (hoveredTile != selectedTile && hoveredTile != null && hit.collider.gameObject.GetComponent<Tile>() != hoveredTile)
                { //se não houve clique

                    var tempMaterial3 = new Material(hoveredTile.GetComponent<Renderer>().sharedMaterial);
                    tempMaterial3.color = oldColor;
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
                tempMaterial5.color = oldColor;
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
            tempMaterial6.color = oldColor;
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
        
        if (GUILayout.Button(text, GUILayout.Height(80)))
        {
            if (select) select = false;
            else select = true;
        }
        GUILayout.Space(25);

        GUI.color = Color.white;

        #region Object
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Object", GUILayout.Width(70));
        obj = (GameObject)EditorGUILayout.ObjectField(obj, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Parent
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Parent", GUILayout.Width(70));
        parent = (Transform)EditorGUILayout.ObjectField(parent, typeof(Transform), true);
        EditorGUILayout.EndHorizontal();
        #endregion


        #region Material
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Material", GUILayout.Width(70));
        material = (Material)EditorGUILayout.ObjectField(material, typeof(Material), true);
        EditorGUILayout.EndHorizontal();
        #endregion

        EditorGUILayout.ColorField(oldColor);

        GUILayout.Space(25);

        #region Rotation X
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("X Rotation", GUILayout.Width(80));
        rotX = EditorGUILayout.FloatField(rotX);

        EditorGUILayout.EndHorizontal();
        #endregion

        #region Rotation Y
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Y Rotation", GUILayout.Width(80));
        rotY = EditorGUILayout.FloatField(rotY);

        EditorGUILayout.EndHorizontal();
        #endregion


        #region Rotation Z
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Z Rotation", GUILayout.Width(80));
        rotZ = EditorGUILayout.FloatField(rotZ);

        EditorGUILayout.EndHorizontal();
        #endregion

        if(lastTile != null)
        {
            GUI.color = Color.yellow;
            if (GUILayout.Button("Undo", GUILayout.Height(40)))
            {
                DestroyImmediate(lastTile.obj);
                lastTile.obj = null;
                lastTile = null;
            }
        }

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
