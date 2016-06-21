using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GUI_ScriptGB : MonoBehaviour {
	
	public bool isPaused;
	public int leben = 3;
	public static int score;
	public float time = 0;
	public static int timeAnz = 0;
	public static int wave;
	
	private float screenWidth = Screen.width;
	private float screenHeight = Screen.height;

	public Texture liveDown;
	public int lifeCounter;
	private float heightTimer = 0;
	
	public GUIStyle lives = new GUIStyle ();

	//Die Styles die die Schrift und Buttons.
	//Können in Unity dierekt verändert werden, sofern imm Scrpit nichts angebeben wurde für den wert
	public GUIStyle fontStyle = new GUIStyle();
	public GUIStyle pauseButton = new GUIStyle();
	
	void start() {
		isPaused = false;
	}
	
	void Update() {
		//Zeit wird hoch gezählt. Mit Hilfe der deltatime. 
		time = time + Time.deltaTime;
		if (time >= 1.0f) {
			timeAnz += 1;
			time = 0;
		}

	}
	
	void OnGUI() {
		//Ist in jeder GUI gleich:
		//Der Pauebutton bekommt eine dynamische Größe entsprechend der Bildschirmgröße
		pauseButton.fixedWidth = screenWidth / 6;
		pauseButton.fixedHeight = screenHeight / 6;
		pauseButton.fontSize = (int)screenWidth / 38;
		fontStyle.fontSize = (int)screenWidth / 38;

		//Ebenfalls in jeder GUI gleich:
		//Schrift wird im Button positioiert
		Vector2 contentOffset = pauseButton.contentOffset;
		contentOffset.y = pauseButton.fixedHeight/3.3333f;
		pauseButton.contentOffset = contentOffset;

		
		//Pausebutton Oben Rechts
		GUILayout.BeginArea(new Rect(Screen.width - Screen.width/6, 5, pauseButton.fixedWidth, pauseButton.fixedHeight));
		if (!isPaused)
		{
			//Der Pausebuuton oben rechts in der Ecke. 
			if (GUILayout.Button("Pause", pauseButton))
			{
				Time.timeScale = 0;
				isPaused = true;
			}
		} else
			//Ist der Pausebutton gedrückt worden, verschwidet dieser und diese Schrift erscheint
			GUILayout.Label ("Pausiert", fontStyle);
		GUILayout.EndArea ();
		
		//Pausenmenü In der Mitte
		GUILayout.BeginArea(new Rect(Screen.width/2-pauseButton.fixedWidth/2, Screen.height/5.5f, pauseButton.fixedWidth, pauseButton.fixedHeight*2.5f));
		if (isPaused)
		{
			//Ist das Spiel pausiert erscheint in der Mitte den Bildschirms ein Menü
			//Dies ist der Button der das Spiel fortsetzt
			if (GUILayout.Button("Weiter", pauseButton))
			{
				Time.timeScale = 1;
				isPaused = false;
			}
			GUILayout.Label("");
			//Dies ist de Button, der zurück zum Menü fürht
			if(GUILayout.Button ("Menü", pauseButton))
			{
				SceneManager.LoadScene(0);
				Time.timeScale = 1;
			}
			
			
			
		}
		
		GUILayout.EndArea();
	
		GUILayout.BeginArea(new Rect(5, Screen.height/6, 350, 300));

		//Wenn alle Leben weg sind wird der Gema Over Screen geladen
		if (leben <= 0)
		{
            SceneManager.LoadScene(6);
        }
        //Oben Links die anzeige der Werte
        GUILayout.Label("Score: " + score, fontStyle);
		GUILayout.Label ("");
		GUILayout.Label("Welle: " + wave, fontStyle);
		GUILayout.EndArea();

		if (lifeCounter > timeAnz) {
			GUILayout.BeginArea (new Rect (Screen.width / 3, Screen.height / 3+heightTimer, 300, 300));
			GUILayout.Label (liveDown);
			GUILayout.EndArea ();
			heightTimer -= 0.5f;
		} else
			heightTimer = 0;

		drawLives();
	}


	//Die Herzen <3
	private void drawLives()
	{
		GUILayout.BeginHorizontal();
		for (int i = 0; i < leben; i++)
		{
			lives.fixedWidth = screenWidth/15;
			lives.fixedHeight = screenHeight/9;

			GUILayout.Label(" ", lives);
		}
		GUILayout.EndHorizontal();
		
	}
}
