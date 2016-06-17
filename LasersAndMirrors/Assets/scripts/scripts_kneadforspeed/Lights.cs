using UnityEngine;
using System.Collections;

public class Lights : MonoBehaviour {

	public GameObject[] myLights;

	public bool[] red;
	public bool[] yellow;
	public bool[] green;
	public bool[] grey;

	private int maxLight = 4;
	public float alpha;
	private float lampDuration = 0.2f;
	private Color defaultColor;

	void Start () 
	{
		defaultColor = new Color (0,0,0,0);
		red = new bool[maxLight];
		yellow = new bool[maxLight];
		green = new bool[maxLight];
		grey = new bool[maxLight];

		for (int i = 0; i < maxLight; i++) 
		{
			myLights [i].GetComponent<Renderer>().material.color = defaultColor;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < maxLight; i++) 
		{
			if (red [i] == true) 
			{
				red [i] = false;
				myLights [i].GetComponent<Renderer> ().material.color = new Color (1, 0, 0, alpha);
				StartCoroutine (Wait (red[i], myLights[i]));
			}
				
			if (yellow [i] == true) 
			{
				yellow [i] = false;
				myLights[i].GetComponent<Renderer>().material.color = new Color (1, 1, 0, alpha);
				StartCoroutine (Wait (yellow[i], myLights[i]));

			}

			if (green [i] == true) 
			{
				green [i] = false;
				myLights[i].GetComponent<Renderer>().material.color = new Color (0, 1, 0, alpha);
				StartCoroutine (Wait (green[i], myLights[i]));
			}

			if (grey [i] == true) 
			{
				grey [i] = false;
				myLights [i].GetComponent<Renderer> ().material.color = new Color (0.1f, 0.1f, 0.1f, alpha);
				StartCoroutine (Wait (grey[i], myLights[i]));
			}
		}
	}

	IEnumerator Wait(bool color, GameObject myLight)
	{
		yield return new WaitForSeconds (lampDuration);
		if (color != true) 
		{
			myLight.GetComponent<Renderer> ().material.color = defaultColor;
		}
	}
}
