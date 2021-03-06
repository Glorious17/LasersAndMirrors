﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * This script will show the total score of the player and the way he/she reached
 * and offers the function to get back to game-menu
 *
 */

public class End_GUI : MonoBehaviour {
	private int finalscore;

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
		finalscore = GUI_ScriptGB.score * GUI_ScriptGB.wave;
		fontStyleGross.font = (Font)Resources.Load("Fonts/Unique");
		fontStyleKlein.font = (Font)Resources.Load("Fonts/Unique");
		fontStyleKlein.fontSize = (int)screenWidth / 35;
		fontStyleGross.fontSize = (int)screenWidth / 25;
		fontStyleKlein.alignment = TextAnchor.MiddleCenter;
		fontStyleGross.alignment = TextAnchor.MiddleCenter;
		fontStyleGross.padding.bottom = 6;

		neustart.fixedWidth = Screen.width/6;
		neustart.fixedHeight = Screen.height/4;
		zurueck.fixedWidth = Screen.width/8; //80
		zurueck.fixedHeight = Screen.height / 12; //39



		//Score usw.
		GUILayout.BeginArea (new Rect (Screen.width/2-250, Screen.height/5+5, 500, Screen.height));
		//Die erreichten Werte werden untereinander angezeigt
		GUILayout.Label ("Überlebte Wellen", fontStyleKlein);
		GUILayout.Label ("" + (GUI_ScriptGB.wave-1), fontStyleGross);
		GUILayout.Label ("Score", fontStyleKlein);
		GUILayout.Label ("" + GUI_ScriptGB.score, fontStyleGross);
		GUILayout.Label("Final Score", fontStyleKlein);
		GUILayout.Label ("" + finalscore, fontStyleGross);

		/*if (GUILayout.Button("Menü", pauseButton))
        {
            //Wieder zum Menü
            Application.LoadLevel(0);

        }*/
		GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect(Screen.width/2-neustart.fixedWidth/2, Screen.height - Screen.height/4, 500, 500));
		if (GUILayout.Button ("", neustart)) {
            SceneManager.LoadScene(5);
            GUI_ScriptGB.timeAnz =0;
			GUI_ScriptGB.score = 0;
			finalscore = 0;
			GUI_ScriptGB.wave = 0;
		}
		GUILayout.EndArea ();

		GUILayout.BeginArea (new Rect(10, Screen.height-Screen.height/10, zurueck.fixedWidth, zurueck.fixedHeight));
		if(GUILayout.Button("", zurueck)){
            SceneManager.LoadScene(0);
        }
        GUILayout.EndArea ();
	}
}

