using UnityEngine;
using System.Collections;

public class StartSceneGUI : MonoBehaviour {

	public GUIStyle button = new GUIStyle();
	private float screenWidth = Screen.width;
	private float screenHeight = Screen.height;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI() {
		button.fixedWidth = screenWidth / 6;
		button.fixedHeight = screenHeight / 6;
		button.fontSize = (int)screenWidth / 30;

		Vector2 contentOffset = button.contentOffset;
		contentOffset.y = button.fixedHeight/3.3333f;
		button.contentOffset = contentOffset;
		//int buttonWidth = 200, buttonHeight = 100;
		GUILayout.BeginArea(new Rect(Screen.width / 2 - button.fixedWidth/2, Screen.height /3, Screen.width/2, Screen.height/2));
		if (GUILayout.Button("Start", button))
		{
			Application.LoadLevel(0);
		}
		if (GUILayout.Button("Menü", button))
		{
			Debug.Log("NADA!");
		}
		GUILayout.EndArea ();
	}
}
