using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Credit_Script : MonoBehaviour {

	private TextAsset txt;
	private GUIStyle font = new GUIStyle(); 
	public GUIStyle back = new GUIStyle();
	private float scroll = 0;
	private string line;

	// Use this for initialization
	void Start () {
		txt = (TextAsset)Resources.Load ("Credits", typeof(TextAsset));
		line = txt.text;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		font.font = (Font)Resources.Load ("Fonts/Unique");
		font.alignment = TextAnchor.MiddleCenter;
		font.fontSize = Screen.width / 37;
		font.normal.textColor = Color.white;
		back.fixedWidth = Screen.width/8; //80
		back.fixedHeight = Screen.height / 12; //39

		GUILayout.BeginArea (new Rect (0, Screen.height-scroll, Screen.width, Screen.height*4));
		GUILayout.Label (line, font);
		GUILayout.EndArea ();

		scroll += Screen.height/1666.66f;
		if (scroll >= Screen.height*3.5)
			scroll = 0;


		GUILayout.BeginArea (new Rect(10, Screen.height-Screen.height/10, back.fixedWidth, back.fixedHeight));
		if(GUILayout.Button("", back)){
			SceneManager.LoadScene(0);
		}
		GUILayout.EndArea ();
	}
}
