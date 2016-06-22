using UnityEngine;
using System.Collections;

public class StartCounter : MonoBehaviour {

	public float secs = 4;
	public Texture2D eins;
	public Texture2D zwei;
	public Texture2D drei;
	private int time = 1;
	private float timer = 0;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine ("Wait");
	}

	void Update(){
		timer = timer + Time.deltaTime;
		if (timer >= 1 && time <= 3) {
			time++; 
			timer = 0;
			Debug.Log (time);
		}
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds (secs);
		Camera.main.GetComponent<diagonalMovement>().enabled = true;
	}

	void OnGUI(){

		eins.Resize ((int)(Screen.width / 6), (int)(Screen.height / 4));
		eins.Apply();
		zwei.Resize ((int)(Screen.width / 6), (int)(Screen.height / 4));
		zwei.Apply();
		drei.Resize ((int)(Screen.width / 6), (int)(Screen.height / 4));
		drei.Apply();


		if (time == 1) {
			GUILayout.BeginArea (new Rect (Screen.width / 2 - 33, Screen.height / 2 - 55, 150, 150));
			GUILayout.Label (drei);
			GUILayout.EndArea ();
		} else if (time == 2) {
			GUILayout.BeginArea (new Rect (Screen.width / 2 - 33, Screen.height / 2 - 55, 150, 150));
			GUILayout.Label (zwei);
			GUILayout.EndArea ();
		} else if (time == 3) {
			GUILayout.BeginArea (new Rect (Screen.width / 2 - 33, Screen.height / 2 - 55, 150, 150));
			GUILayout.Label (eins);
			GUILayout.EndArea ();
		} else {

		}
	}
}
