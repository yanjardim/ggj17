using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public List<GameObject> disableWhenCredits;
    public List<GameObject> enableWhenCredits;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credits()
    {
        for(int i = 0; i < disableWhenCredits.Count; i++)
        {
            disableWhenCredits[i].SetActive(false);
        }
        for (int i = 0; i < enableWhenCredits.Count; i++)
        {
            enableWhenCredits[i].SetActive(true);
        }
    }

    public void Back()
    {
        for (int i = 0; i < disableWhenCredits.Count; i++)
        {
            disableWhenCredits[i].SetActive(true);
        }
        for (int i = 0; i < enableWhenCredits.Count; i++)
        {
            enableWhenCredits[i].SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }



}
