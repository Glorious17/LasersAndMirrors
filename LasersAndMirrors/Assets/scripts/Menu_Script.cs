using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Menu_Script : MonoBehaviour {

    public string scenename;
	// Use this for initialization
	void Start () {
	
	}

    void OnMouseDown()
    {
        SceneManager.LoadScene(scenename);
    }
}
