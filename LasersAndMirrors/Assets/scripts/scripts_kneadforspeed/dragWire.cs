using UnityEngine;
using System.Collections;

public class dragWire : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 startPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			var touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Moved) {
				startPos = new Vector3 (Mathf.Clamp (touch.position.x / Screen.width * 0.33f, 0, 8), 0, 0);

			}

			if (touch.deltaPosition.x > 0)
				GameObject.Find ("Cube").transform.position += startPos;
			else if (touch.deltaPosition.x < 0)
				GameObject.Find ("Cube").transform.position -= startPos;
		}

	}

	void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

	}

	void OnMouseDrag(){
		Vector3 cursorPoint = new Vector3 (Input.mousePosition.x, Screen.height/2, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (cursorPoint) + offset;
		transform.position = cursorPosition;
	}
}
