﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * This script will show the total score of the player and the way he/she reached
 * and offers the function to get back to game-menu
 *
 */

public class End_GUI : MonoBehaviour {
	private int score;

	private GUIStyle fontStyleKlein = new GUIStyle();
	private GUIStyle fontStyleGross = new GUIStyle();
	public GUIStyle neustart = new GUIStyle();
	public GUIStyle zurueck = new GUIStyle();

	private float screenWidth = Screen.width;
	private float screenHeight = Screen.height;

	void start(){
	}

	void Update(){

	}


	void OnGUI() {
		score = GUI_ScriptGB.score * GUI_ScriptGB.wave;
		fontStyleKlein.fontSize = (int)screenWidth / 35;
		fontStyleGross.fontSize = (int)screenWidth / 25;
		fontStyleKlein.alignment = TextAnchor.MiddleCenter;
		fontStyleGross.alignment = TextAnchor.MiddleCenter;

		neustart.fixedWidth = Screen.width/6;
		neustart.fixedHeight = Screen.height/4;
		zurueck.fixedWidth = Screen.width/8; //80
		zurueck.fixedHeight = Screen.height / 12; //39



		//Score usw.
		GUILayout.BeginArea (new Rect (Screen.width/2-250, Screen.height/5+5, 500, 500));
		//Die erreichten Werte werden untereinander angezeigt
		GUILayout.Label ("Überlebte Wellen", fontStyleKlein);
		GUILayout.Label ("" + GUI_ScriptGB.wave, fontStyleGross);
		GUILayout.Label ("\nScore", fontStyleKlein);
		GUILayout.Label ("" + GUI_ScriptGB.score, fontStyleGross);
		GUILayout.Label("\nFinal Score", fontStyleKlein);
		GUILayout.Label ("" + score, fontStyleGross + "\n\n");

		/*if (GUILayout.Button("Menü", pauseButton))
        {
            //Wieder zum Menü
            Application.LoadLevel(0);

        }*/
		GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect(Screen.width/2-neustart.fixedWidth/2, Screen.height - Screen.height/4, 500, 500));
		if (GUILayout.Button ("Nochmal spielen", neustart)) {
            SceneManager.LoadScene(5);
            GUI_ScriptGB.timeAnz =0;
			GUI_ScriptGB.score = 0;
			score = 0;
			GUI_ScriptGB.wave = 0;
		}
		GUILayout.EndArea ();

		GUILayout.BeginArea (new Rect(10, Screen.height-40, 200,200));
		if(GUILayout.Button("Menü", zurueck)){
            SceneManager.LoadScene(0);
        }
        GUILayout.EndArea ();
	}
}
