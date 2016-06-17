using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Menu_Script : MonoBehaviour {

    public string scenename;
	// Use this for initialization
	void Start () {
	
	}
    // Update is called once per frame
    void Update()
    {
        GameObject clickedGmObj = null;
        if (Input.GetMouseButtonDown(0))
        {
            clickedGmObj = GetClickedGameObject();
            if (clickedGmObj != null && clickedGmObj.CompareTag("Button"))
            {
                Debug.Log("Spiel wird gestartet!");
                SceneManager.LoadScene(scenename);
            }
        }
    }


    GameObject GetClickedGameObject()
    {
        // Builds a ray from camera point of view to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Casts the ray and get the first game object hit
        if (Physics.Raycast(ray, out hit, 100))
            return hit.transform.gameObject;
        else
            return null;
    }
}
