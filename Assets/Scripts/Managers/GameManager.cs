using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public bool putAntenna, rotateAntenna;
    public bool canSelect;
    public Tile selected;
    public int gap;
    public float angle;
    public int maxAntennas = 4;
    
    public Slider gapField, angleField;

    public void Awake()
    {
        
        instance = this;
    }
    
    // Use this for initialization
    void Start () {
		  gapField = GameObject.Find("Canvas").transform.FindChild("SettingsPanel").FindChild("Gap").GetComponent<Slider>();
          angleField = GameObject.Find("Canvas").transform.FindChild("SettingsPanel").FindChild("Angle").GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        if (selected)
        {
            GameObject.Find("Canvas").transform.FindChild("SettingsPanel").gameObject.SetActive(true);

        }
        else GameObject.Find("Canvas").transform.FindChild("SettingsPanel").gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (GameObject.FindGameObjectWithTag("End").transform.parent.GetComponent<Antenna>().active)
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
    public void SetPutAntenna(bool value)
    {
        putAntenna = value;
        canSelect = true;
    }
    public void SetRotateAntenna(bool value)
    {
        rotateAntenna = value;
        canSelect = true;
    }

    public void SetGap()
    {
        /*gap = int.Parse(gapField.text);
        gap = Mathf.Clamp(gap, 1, 360);*/

        selected.obj.transform.GetChild(0).GetComponent<Antenna>().gap = (int)gapField.value;
    }
    public void SetAngle()
    {
        
        
        selected.obj.transform.GetChild(0).GetComponent<Antenna>().angle = (int)angleField.value;
    }

    public void Cancel()
    {
        canSelect = false;
        putAntenna = false;
        rotateAntenna = false;
        selected = null;
    }
}
