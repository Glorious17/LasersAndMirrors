using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Credit_Script : MonoBehaviour {

	public string path;
	private TextReader reader;
	private List<string> credits = new List<string> ();
	private GUIStyle font = new GUIStyle(); 
	public GUIStyle back = new GUIStyle();
	private float scroll = 0;
	private string line;

	// Use this for initialization
	void Start () {
		path = Application.dataPath+"/Resources/Credits";
		reader = new StreamReader (path);
		string temp;

//		string f = Resources.Load ("msgDB").ToString(); // from Resources/msgDB.txt
//		string[] msgDB = f.Replace("\r\n","\n").Replace("\r","\n").Split("\n"[0]);


		while((temp = reader.ReadLine()) != null){
			credits.Add(temp);
		} 
		reader.Close ();
		for (int i = 0; i < 43; i++) {
			line += credits [i] + "\n";
		}
	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){

		font.alignment = TextAnchor.MiddleCenter;
		font.fontSize = Screen.width / 37;
		font.normal.textColor = Color.white;
		back.fixedWidth = Screen.width/8; //80
		back.fixedHeight = Screen.height / 12; //39

		GUILayout.BeginArea (new Rect (Screen.width/2-250, Screen.height-scroll, 500, 1500));
		GUILayout.Label (line, font);
		GUILayout.EndArea ();

		scroll += 0.3f;
		if (scroll >= 1050)
			scroll = 0;

		GUILayout.BeginArea (new Rect(10, Screen.height-40, 200,200));
		if(GUILayout.Button("", back)){
			SceneManager.LoadScene(0);
		}
		GUILayout.EndArea ();
	}
}
