using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetPoints : MonoBehaviour {

	public string menu;
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("3_points", 0);

		//SceneManager.LoadScene(menu);
	}
}
