using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Credit_Script : MonoBehaviour {

	public string path;
	private TextReader reader;
	private List<string> credits = new List<string> ();
	public GUIStyle font = new GUIStyle(); 
	public GUIStyle back = new GUIStyle();
	private float scroll = 0;

	// Use this for initialization
	void Start () {
		path = "Assets/Resource/Credits.txt";
		reader = new StreamReader (path);
		string temp;
		while((temp = reader.ReadLine()) != null){
			credits.Add(temp);
		} 
		reader.Close ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){

		font.alignment = TextAnchor.MiddleCenter;
		font.fontSize = Screen.width / 39;
		back.fixedWidth = Screen.width/8; //80
		back.fixedHeight = Screen.height / 12; //39

		GUILayout.BeginArea (new Rect (Screen.width/2-250, Screen.height-scroll, 500, 1500));
		for (int i = 0; i < 43; i++) {
			string line = credits [i];
			GUILayout.Label (line, font);
		}
		GUILayout.EndArea ();

		scroll += 0.3f;

		GUILayout.BeginArea (new Rect(10, Screen.height-40, 200,200));
		if(GUILayout.Button("", back)){
			SceneManager.LoadScene(0);
		}
		GUILayout.EndArea ();
	}
}
