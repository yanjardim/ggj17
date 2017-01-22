using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {
    private const int limit = 10;
    public int speed;
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
    }
}
