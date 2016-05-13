﻿using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	/* BUGS
	 * 
	 * Erstellen neuer Spiegel nur alle 2 Klicks
	 * muss auf Touch ausprobiert werden
	*/
	public GameObject mirror;
	public GameObject marker;
    public GameObject barrierObject;

	private float z = 5;
	//Ein 6x10 Grid
	private const int x = 10;
	private const int y = 6;

	private float[] gridX;
	private float[] gridY;

	//Die Länge eines Feldes
	private float fieldx;
	private float fieldy;

	private bool [,] besetzt;

	// Use this for initialization
	void Start () 
	{
		gridX = new float[x];
		gridY = new float[y];

		fieldx = (float)Screen.width / (float)x;
		fieldy = (float)Screen.height / (float)y;

		for (int i = 0; i < x; i++) 
		{
			gridX [i] = (i + 1) * fieldx;
		}

		for (int i = 0; i < y; i++) 
		{
			gridY [i] = (i + 1) * fieldy;
		}

		besetzt = new bool[y,x];

		for (int i = 0; i < y; i++) {
			for (int j = 0; j < x; j++) {
				besetzt [i, j] = false;
			}
		}

		//Marker ();
        barrier();
	}

	// Update is called once per frame
	void Update () 
	{
		/*
		Touch[] myTouches = Input.touches;

		for(int i = 0; i < Input.touchCount; i++)
		{

			bool xtreffer = false;
			bool ytreffer = false;

			for (int j = 0; j < x && xtreffer == false; j++) 
			{
				if (myTouches [i].position.x <= gridX [j]) 
				{
					Debug.Log ("X: " + j);
					xtreffer = true;
				}
			}

			for (int j = 0; j < y  && ytreffer == false; j++) 
			{
				if (myTouches [i].position.y <= gridY [j]) 
				{
					Debug.Log ("Y: " + j);
					ytreffer = true;
				}
			}

			xtreffer = false;
			ytreffer = false;
		}
		*/
	}

	void OnMouseDown()
	{
		Debug.Log ("CLICK");
		bool xtreffer = false;
		bool ytreffer = false;

		int xpos = 0;
		int ypos = 0;

		for (int j = 0; j < x && xtreffer == false; j++) 
		{
			if (Input.mousePosition.x <= gridX [j]) 
			{
				Debug.Log ("X: " + j);
				xpos = j;
				xtreffer = true;
			}
		}

		for (int j = 0; j < y  && ytreffer == false; j++) 
		{
			if (Input.mousePosition.y <= gridY [j]) 
			{
				Debug.Log ("Y: " + j);
				ypos = j;
				ytreffer = true;
			}
		}

		xtreffer = false;
		ytreffer = false;

		Debug.Log ("PRE-DEPLOY");
		if (besetzt [ypos, xpos] == false) {
			Debug.Log ("DEPLOY");
			Vector3 spawnPos = new Vector3 (xpos * fieldx + fieldx / 2, ypos * fieldy + fieldy / 2, z);
			Vector3 pos = Camera.main.ScreenToWorldPoint (spawnPos);
			Object newMirror = Instantiate (mirror, pos, Quaternion.Euler (0, 45, 0));

			newMirror.name = "" + ypos + xpos;

			besetzt [ypos, xpos] = true;

			Camera.main.GetComponent<DrawLaser> ().startLaser ();
			Camera.main.GetComponent<DrawLaser> ().drawLaser ();
		} 
		else 
		{
			GameObject selectMirror = GameObject.Find ("" + ypos + xpos);
			selectMirror.transform.Rotate(0, 90.0f, 0);
			Camera.main.GetComponent<DrawLaser>().startLaser();
			Camera.main.GetComponent<DrawLaser>().drawLaser();
		}
	}

	void Marker()
	{
		//setze die Marker auf das Feld
		for(float ypos = fieldy/2; ypos < fieldy*y; ypos += fieldy)
		{
			for (float xpos = fieldx/2; xpos < fieldx*x; xpos += fieldx) 
			{
				Vector3 spawnPos = new Vector3(xpos, ypos, z);
				Vector3 pos = Camera.main.ScreenToWorldPoint(spawnPos);
				Instantiate(marker, pos, Quaternion.identity);
			}
		}
	}

    void barrier()
    {
        int rand = Random.Range(0,6);
        for(int pos = 0; pos < 10; pos += 3)
        {
            if (pos < 9)
            {
                if (rand == 0 || rand == 5)
                    spawnBarrier(rand, (Random.Range(0, 3) + pos));
                else
                {
                    if (Random.Range(0, 1) == 0)
                        spawnBarrier(rand, pos);
                    else
                        spawnBarrier(rand, pos + 2);
                }
            }
            else
            {
                if (rand == 0 || rand == 5)
                    spawnBarrier(rand, (Random.Range(0, 2) + pos));
                else
                {
                    spawnBarrier(rand, pos);
                }
            }
        }
    }

    void spawnBarrier(int x, int y)
    {
        Instantiate(barrierObject, new Vector3(fieldx * x, 0, fieldy * y), Quaternion.identity);
    }
}
