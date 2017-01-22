using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {
    private const int limit = 10;
    public int speed, rotateSpeed;
    public float limitZoom;
    public float zoomOut;

	// Use this for initialization
	void Start () {
        zoomOut = limitZoom;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.mousePosition.x > Screen.width - limit)
        {
            Camera.main.transform.Translate(Vector3.right * speed);
        }
        if (Input.mousePosition.x < limit)
        {
            Camera.main.transform.Translate(Vector3.left * speed);
        }
        if(Input.mousePosition.y > Screen.height - limit)
        {
            Camera.main.transform.Translate(0, speed, speed);
        }
        if (Input.mousePosition.y < limit)
        {
            Camera.main.transform.Translate(0, -speed, -speed);
        }

        

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && zoomOut < limitZoom)
        {
            Camera.main.transform.Translate(transform.forward * speed);
            zoomOut++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && zoomOut > 0)
        {
            Camera.main.transform.Translate(-transform.forward * speed);
            zoomOut--;
        }

        


        zoomOut = Mathf.Clamp(zoomOut, 0, limitZoom);

        Bounds b = GameObject.Find("Chão").transform.GetChild(0).GetComponent<Renderer>().bounds ;
        Vector2 vetx = new Vector2(b.min.x, b.max.x);
      //  Vector2 vety = new Vector2(b.min.y, b.max.y);
        Vector2 vetz = new Vector2(b.min.z, b.max.z);
        Vector3 aux = Camera.main.transform.position;// = Mathf.Clamp(zoomOut, 0, limitZoom);
        aux.x = Mathf.Clamp(aux.x,vetx.x,vetx.y);
        aux.y = Mathf.Clamp(aux.y, 49, 200);
        aux.z = Mathf.Clamp(aux.z, vetz.x, vetz.y);

        Camera.main.transform.position = aux;


    }
}
