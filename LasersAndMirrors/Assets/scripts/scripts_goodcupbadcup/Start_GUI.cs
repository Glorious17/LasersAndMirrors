using UnityEngine;
using System.Collections;

public class Start_GUI : MonoBehaviour {
	
	public bool isPaused;
	public int leben = 3;
	public int score = 0;
	public float time = 0;
	public int timeAnz = 0;
	
	public GUIStyle fontStyle = new GUIStyle();
	public GUIStyle pauseButton = new GUIStyle();

	private float screenWidth = Screen.width;
	private float screenHeight = Screen.height;
	
	void start(){	
	}
	
	void Update(){
	}
	
	void OnGUI() {
		pauseButton.fixedWidth = screenWidth / 6;
		pauseButton.fixedHeight = screenHeight / 6;
		pauseButton.fontSize = (int)screenWidth / 38;
		fontStyle.fontSize = (int)screenWidth / 38;

		Vector2 contentOffset = pauseButton.contentOffset;
		contentOffset.y = pauseButton.fixedHeight/3.3333f;
		pauseButton.contentOffset = contentOffset;

        GUILayout.BeginArea(new Rect(Screen.width - Screen.width / 2 - pauseButton.fixedWidth / 2, Screen.height - Screen.height / 2 - pauseButton.fixedHeight / 2, pauseButton.fixedWidth, 500));
        if (GUILayout.Button("Start", pauseButton))
		{
			//Wird ein neues Spiel gestartet werden erst alle Werte zurück gesetzt
            Application.LoadLevel(1);
			GUI_ScriptGB.timeAnz =0;
			GUI_ScriptGB.score = 0;
			score = 0;
			GUI_ScriptGB.wave = 0;
        }
		GUILayout.Label ("");
		//zurück zum Obermenü aller Spiele
		if (GUILayout.Button("Hauptmenü", pauseButton))
		{
            Debug.Log("NADA!");
		}
		GUILayout.EndArea ();
	}
}
