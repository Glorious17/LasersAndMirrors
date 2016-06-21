﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class End_Gui_Lazemaze : MonoBehaviour {

	public GUIStyle fontSmall = new GUIStyle ();
	public GUIStyle fontBig = new GUIStyle ();
	public GUIStyle backButton = new GUIStyle();
	public GUIStyle restartButton = new GUIStyle ();


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){

		int score = PlayerPrefs.GetInt("3_points");;
		fontSmall.fontSize = (int)Screen.width / 30;
		fontBig.fontSize = (int)Screen.width / 20;
		fontSmall.alignment = TextAnchor.MiddleCenter;
		fontBig.alignment = TextAnchor.MiddleCenter;

		restartButton.fixedWidth = Screen.width/6;
		restartButton.fixedHeight = Screen.height/4;
		backButton.fixedWidth = Screen.width/8; //80
		backButton.fixedHeight = Screen.height / 12; //39

		GUILayout.BeginArea (new Rect (Screen.width/2-250, Screen.height/5+5, 500, 500));
		GUILayout.Label ("Score", fontSmall);
		GUILayout.Label ("" + score, fontBig);
		GUILayout.EndArea ();

		GUILayout.BeginArea (new Rect(Screen.width/2-restartButton.fixedWidth/2, Screen.height-Screen.height/4, 500,500));
		if (GUILayout.Button ("", restartButton)) {
			SceneManager.LoadScene(1);
			PlayerPrefs.SetInt ("3_points", 0); //Reset von points
		}
		GUILayout.EndArea ();

		GUILayout.BeginArea (new Rect(10, Screen.height-40, 200,200));
		if (GUILayout.Button ("", backButton)) {
			SceneManager.LoadScene (0);
			PlayerPrefs.SetInt ("3_points", 0); //Reset von points
		}
		GUILayout.EndArea ();
	}

}