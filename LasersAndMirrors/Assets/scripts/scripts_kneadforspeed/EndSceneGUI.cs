using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EndSceneGUI : MonoBehaviour {

	public GUIStyle button = new GUIStyle();
	private GUIStyle schrift = new GUIStyle();
	private float screenWidth = Screen.width;
	private float screenHeight = Screen.height;

    private List<int> wire = new List<int>();

    public GameObject wire_thin, wire_medium, wire_thick, wire_fat, invisible, d_zu_m, d_zu_di, m_zu_d, m_zu_di, di_zu_d, di_zu_m;

    // Use this for initialization
    void Start () {
        wire = GameObject.Find("Main Camera").GetComponent<Touchscript>().Feedback;

        plotWire();
    }


	void OnGUI() {
		schrift.fontSize = (int)screenWidth / 35;
		schrift.normal.textColor = Color.black;
		schrift.fontStyle = FontStyle.Bold;
		button.fixedWidth = screenWidth / 6;
		button.fixedHeight = screenHeight / 6;
		button.fontSize = (int)screenWidth / 30;
		Vector2 contentOffset = button.contentOffset;
		contentOffset.y = button.fixedHeight/3.3333f;
		button.contentOffset = contentOffset;
		//int buttonWidth = 200, buttonHeight = 100;
		GUILayout.BeginArea (new Rect (Screen.width/2- button.fixedWidth/2, Screen.height /4,Screen.width/2 ,Screen.height/2 ));
		
		if (GUILayout.Button("Menü", button))
		{
            SceneManager.LoadScene("carousel_menu");
		}
		GUILayout.Label ("", schrift);
		GUILayout.Label ("Score: " + GUI_Script.score, schrift);
		GUILayout.EndArea ();
	}

    public void plotWire()
    {
        Vector3 wirePos = new Vector3(-6, 0, 0); //Startposition of first wire part

		foreach (int part in wire)
        {
            switch (part) //reading out value of wire evaluation (1 = super, 2= good, 3 = meh, 4 = baaad)
            {
				case 1:
					GameObject spawnThin = (GameObject)Instantiate (wire_thin, wirePos, Quaternion.Euler (0, 0, -90));
					spawnThin.transform.parent = invisible.transform;    
					break;
			
				case 2:
					GameObject spawnMedium = (GameObject)Instantiate (wire_medium, wirePos, Quaternion.Euler (0, 90, 0));
					spawnMedium.transform.parent = invisible.transform;
					break;
                case 3:
					GameObject spawnThick = (GameObject)Instantiate (wire_thick, wirePos, Quaternion.Euler (0, 90, 0));
					spawnThick.transform.parent = invisible.transform;
					break;
				case 4:
					GameObject spawnThinToThick = (GameObject)Instantiate (d_zu_di, wirePos, Quaternion.Euler (0, 90,0));
					spawnThinToThick.transform.parent = invisible.transform;
					break;
				case 5:
					GameObject spawnThinToMedium = (GameObject)Instantiate (d_zu_m, wirePos, Quaternion.Euler (0, 90, 0));
					spawnThinToMedium.transform.parent = invisible.transform;
					break;
				case 6:
					GameObject spawnMediumToThin = (GameObject)Instantiate (m_zu_d, wirePos, Quaternion.Euler (0, 90, 0));
					spawnMediumToThin.transform.parent = invisible.transform;
					break;
				case 7: 
					GameObject spawnMediumToThick = (GameObject)Instantiate (m_zu_di, wirePos, Quaternion.Euler (0, 90, 0));
					spawnMediumToThick.transform.parent = invisible.transform;
					break;
				case 8:
					GameObject spawnThickToThin = (GameObject)Instantiate (di_zu_d, wirePos, Quaternion.Euler (0, 90, 0));
					spawnThickToThin.transform.parent = invisible.transform;
					break;
				case 9:
					GameObject spawnThickToMedium = (GameObject)Instantiate (di_zu_m, wirePos, Quaternion.Euler (0, 90, 0));
					spawnThickToMedium.transform.parent = invisible.transform;
					break;
				default:
                    Debug.Log("Out of Bounds");
                    break;
            }
            wirePos.x += 0.5f; //translating the next wire part
        }
    }
}
