using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap : MonoBehaviour {
    public int xTiles;
    public int zTiles;
    public List <Tile> tileList;
    public GameObject obj;
	// Use this for initialization
	void Start () {
        tileList = new List<Tile>();
        SpawnTileMap();
        FixCameraPosition();

    }
	
    public void SpawnTile(Vector3 pos) {
        GameObject cleiton = (GameObject) Instantiate(obj, pos,obj.transform.rotation);
        tileList.Add(cleiton.GetComponent<Tile>());
        cleiton.transform.SetParent(this.gameObject.transform);
        cleiton.transform.name = "Tile(" + pos.x + "," + pos.z+")";
    }

    public void SpawnTileMap() {
        for (int i = 0; i < xTiles; ++i) {
            for (int j = 0; j < zTiles; ++j) {
                SpawnTile(new Vector3(i, 0, j));
            }
        }

    }

    public void FixCameraPosition() {
        Camera.main.transform.position = tileList[tileList.Count - 1].gameObject.transform.position;
        Vector3 vect = Camera.main.transform.position;
        vect.y = 7;
        vect.x += xTiles * 0.75f;
        vect.z += zTiles * 0.75f;
        Camera.main.transform.position = vect;
        Camera.main.transform.LookAt(tileList[0].gameObject.transform);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
