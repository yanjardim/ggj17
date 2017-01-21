using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTile : MonoBehaviour
{
    public Tile hoveredTile, selectedTile;
    public LayerMask mask;
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
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask)) { // aqui quando colide
            if (Input.GetMouseButtonDown(0)) { //se foi clicado
                if (selectedTile != null) { //se já tem um selectedTile anterior
                    selectedTile.GetComponent<Renderer>().material.color = (Color.white); //muda a cor do antigo para branco
                }
                hit.collider.gameObject.GetComponent<Renderer>().material.color = (Color.red); //mudo a cor do atual para vermelho
                selectedTile = hit.collider.gameObject.GetComponent<Tile>(); //seta o novo selectedTile
            }
            else if (hoveredTile != selectedTile && hoveredTile != null && hit.collider.gameObject.GetComponent<Tile>() != hoveredTile ) { //se não houve clique
     
                hoveredTile.GetComponent<Renderer>().material.color = (Color.white); 
            }
            if(hit.collider.gameObject.GetComponent<Tile>() != selectedTile ) { 
            hit.collider.gameObject.GetComponent<Renderer>().material.color = (Color.cyan);
            hoveredTile = hit.collider.gameObject.GetComponent<Tile>(); //seta hovered
            }
            


        }
        else if (hoveredTile != null && hoveredTile != selectedTile){
            hoveredTile.GetComponent<Renderer>().material.color = (Color.white);
        }


    }
}
