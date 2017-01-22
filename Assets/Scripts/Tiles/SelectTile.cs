using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTile : MonoBehaviour
{
    public Tile hoveredTile, selectedTile;
    public LayerMask mask;
    public Color oldColor;

    public GameObject antennaMiddle;
    // Use this for initialization
    void Start()
    {
        mask = 1 << LayerMask.NameToLayer("Grid");
        hoveredTile = null;
        selectedTile = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.canSelect)
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            { // aqui quando colide

                if (Input.GetMouseButtonDown(0))
                { //se foi clicado
                    
                    selectedTile = hit.collider.gameObject.GetComponent<Tile>(); //seta o novo selectedTile

                    /*if (selectedTile.obj == null)
                    {
                        selectedTile.gameObject.GetComponent<Renderer>().material.color = oldColor;
                        selectedTile = null;
                    }*/
                    
                    if (antennaMiddle != null)
                    {
                        if (GameManager.instance.putAntenna)
                        {
                            if (antennaMiddle != null && selectedTile != null && selectedTile.obj == null)
                            {
                                GameObject aux = (GameObject)Instantiate(antennaMiddle, selectedTile.transform.position, antennaMiddle.transform.rotation);
                                selectedTile.obj = aux;

                                selectedTile.gameObject.GetComponent<Renderer>().material.color = oldColor;
                                GameManager.instance.putAntenna = false;
                                GameManager.instance.canSelect = false;
                            }
                            else if(antennaMiddle != null && selectedTile != null && selectedTile.obj != null && ((1 << selectedTile.obj.layer) & LayerMask.NameToLayer("Antenna")) == 0)
                            {
                                Destroy(selectedTile.obj);
                                selectedTile.obj = null;
                                GameManager.instance.Cancel();
                            }
                        }
                        else if(selectedTile != null)
                        {
                            selectedTile.gameObject.GetComponent<Renderer>().material.color = oldColor;
                            GameManager.instance.putAntenna = false;
                            GameManager.instance.canSelect = false;
                        }

                        if (GameManager.instance.rotateAntenna && selectedTile != null &&selectedTile.obj != null)
                        {
                            if (((1 << selectedTile.obj.layer) & LayerMask.NameToLayer("Antenna")) == 0 && selectedTile.obj.transform.GetChild(0).GetComponent<Antenna>().active)
                            {

                                GameManager.instance.selected = selectedTile;
                                GameManager.instance.gapField.value = selectedTile.obj.transform.GetChild(0).GetComponent<Antenna>().gap;
                                GameManager.instance.angleField.value = selectedTile.obj.transform.GetChild(0).GetComponent<Antenna>().angle;
                                GameManager.instance.canSelect = false;


                            }
                        }
                        
                    }
                                   
                }
                else if (hoveredTile != selectedTile && hoveredTile != null && hit.collider.gameObject.GetComponent<Tile>() != hoveredTile)
                { //se não houve clique

                    hoveredTile.GetComponent<Renderer>().material.color = oldColor;
                }
                if (hit.collider.gameObject.GetComponent<Tile>() != selectedTile)
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = (Color.black);
                    hoveredTile = hit.collider.gameObject.GetComponent<Tile>(); //seta hovered
                }



            }
            else if (hoveredTile != null && hoveredTile != selectedTile)
            {
                hoveredTile.GetComponent<Renderer>().material.color = oldColor;
            }
        }


    }
}
