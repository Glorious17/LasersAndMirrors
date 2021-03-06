﻿using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour 
{
	public GameObject mirror;
	public GameObject marker;
    public GameObject barrierObject;
    public GameObject checkpointObject;

	public int maxMirror = 5;
	public int mirrorCount = 0;
    private int rand;

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
    private bool [,] barrierExists;

	//private Vector3 laserStartPos;
	public GameObject laser;
	public GameObject lot;

	//Wände
	public GameObject wOben;
	public GameObject wUnten;
	public GameObject wLinks;
	public GameObject wRechts;

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
        barrierExists = new bool[x,y];

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                besetzt[i, j] = false;
            }
        }
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                barrierExists[j, i] = false;
            }
        }

        Marker ();
        barrier(); //Macht noch Probleme
        checkpoint();
		LaserStartPos ();
		LaserEndPos ();
		Walls ();
	}

	/*
	// Update is called once per frame
	void Update () 
	{		
		Touch[] myTouches = Input.touches;
		for(int i = 0; i < Input.touchCount; i++){
			down (myTouches[i].position.x, myTouches[i].position.x);
		}
	}
	*/

	void OnMouseDown()
	{
		Debug.Log ("CLICK");
		down (Input.mousePosition.x, Input.mousePosition.y);
	}

	void down(float px, float py)
	{
		bool xtreffer = false;
		bool ytreffer = false;

		int xpos = 0;
		int ypos = 0;

		for (int j = 0; j < x && xtreffer == false; j++) 
		{
			if (px <= gridX [j]) 
			{
				Debug.Log ("X: " + j);
				xpos = j;
				xtreffer = true;
			}
		}

		for (int j = 0; j < y  && ytreffer == false; j++) 
		{
			if (py <= gridY [j]) 
			{
				Debug.Log ("Y: " + j);
				ypos = j;
				ytreffer = true;
			}
		}

		xtreffer = false;
		ytreffer = false;


		if (besetzt [ypos, xpos] == false && !barrierExists[xpos,ypos]) {
			if(mirrorCount < maxMirror){
				mirrorCount++;
				Debug.Log ("DEPLOY");
				Vector3 spawnPos = new Vector3 (xpos * fieldx + fieldx / 2, ypos * fieldy + fieldy / 2, z);
				Vector3 pos = Camera.main.ScreenToWorldPoint (spawnPos);
				Object newMirror = Instantiate (mirror, pos, Quaternion.Euler (0, 45, 0));

				newMirror.name = "" + ypos + xpos;

				besetzt [ypos, xpos] = true;
			}
		} 
		else if(!barrierExists[xpos, ypos])
		{
			Debug.Log ("TURN");
			GameObject selectMirror = GameObject.Find ("" + ypos + xpos);
			if(selectMirror.GetComponent<Degree>().getMirrorRot())
				selectMirror.transform.Rotate(0, 22.5f, 0);
		
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

    void checkpoint()
    {
        for (int pos = 1; pos < 8; pos+=3)
        {
            spawnCheckpoint(pos, Random.Range(0, 3)+1);
        }
    }

    void barrier() // Durchläuft eine Schleife die wichtig zur Positionsbestimmung für die Hindernisse ist. 
	//Anschließend werden insgesamt 5 Hindernisse gespawned.
    {
        int[] position;
        int x;
        int y;
		spawnBarrier (2, Random.Range(0,5));
        for (int pos = 3; pos < 9; pos += 3)
        {
            position = location(pos);
            x = position[0];
            y = position[1];
            do
            {
                position = location(pos);
            } while (position[0] == x && position[1] == y);
            spawnBarrier(x,y);
            spawnBarrier(position[0], position[1]);
        }
    }

    void spawnCheckpoint(int x, int y)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(gridX[x] - (fieldx / 2), gridY[y] - (fieldy / 2), z));
        Instantiate(checkpointObject, pos, Quaternion.identity);
    }

	void spawnBarrier(int x, int y) //Laesst die Hindernisse Spawnen
    {
        Debug.Log("x = " + x + " y = " + z);
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(gridX[x]-(fieldx/2), gridY[y] - (fieldy/2), z));
        barrierExists[x,y] = true;
        Instantiate(barrierObject, pos, Quaternion.identity);
    }

    int[] location(int pos) // Ausgelagerte Methode zum Bestimmen von ausgewählten Punkten auf dem Grid. Nützlich für die Hindernisse.
    {
        int[] ro = new int[2];
        rand = Random.Range(0, 6);
        if (pos < 9)
        {
            if (rand == 0 || rand == 5)
            {
                ro[0] = (Random.Range(0, 3) + pos);
                ro[1] = rand;
            }
            else
            {
                if (Random.Range(0, 2) == 0)
                {
                    ro[0] = pos;
                    ro[1] = rand;
                }
                else
                {
                    ro[0] = pos + 2;
                    ro[1] = rand;
                }
            }
        }
        else
        {
            ro[0] = pos;
            ro[1] = rand;
        }
        return ro;
    }

	void LaserStartPos() //Zufällige Startposition des Lasers
	{
		int rand = Random.Range (0,y);

		float xpos = 0; //fieldx/2;
		float ypos = gridY[rand] - fieldy/2;

		Vector3 pos = new Vector3(xpos, ypos, z);

		laser.transform.position = Camera.main.ScreenToWorldPoint(pos);
	}

	void LaserEndPos()
	{
		int rand = Random.Range (0,y);

		float xpos = gridX[x-1] - fieldx/2; //fieldx/2;
		float ypos = gridY[rand] - fieldy/2;

		Vector3 pos = new Vector3(xpos, ypos, z);

		lot.transform.position = Camera.main.ScreenToWorldPoint(pos);
	}

	void Walls()
	{
		Vector3 posr = new Vector3 (gridX[x-1] + 0.5f*fieldx, wRechts.transform.position.y, z);
		wRechts.transform.position = Camera.main.ScreenToWorldPoint (posr);

		Vector3 posl = new Vector3 (gridX[0] - 1.5f*fieldx, wLinks.transform.position.y, z);
		wLinks.transform.position = Camera.main.ScreenToWorldPoint (posl);

		Vector3 poso = new Vector3 (wOben.transform.position.x, gridY[y-1] + 0.5f*fieldy, z);
		wOben.transform.position = Camera.main.ScreenToWorldPoint (poso);

		Vector3 posu = new Vector3 (wUnten.transform.position.x, gridY[0] - 1.5f*fieldy, z);
		wUnten.transform.position = Camera.main.ScreenToWorldPoint (posu);
	}
}
