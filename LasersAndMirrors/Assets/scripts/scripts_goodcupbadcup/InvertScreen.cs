using UnityEngine;
using System.Collections;

/*
 * This script will rotate the screenview of the player 
 * 
 */

public class InvertScreen : MonoBehaviour 
{
	public GameObject descam;
	public GameObject falseposCam;
	public GameObject trueposCam;
	public GameObject mainCam;

	public GameObject refCam;

	const int max = 50;
	float xpos;
	float yrot;

	public int i = max;

	/*
	* Get the start x-/y-position on beginning 
	*/
	void Start()
	{
		xpos = (mainCam.transform.position.x  - descam.transform.position.x ) / max;
		yrot = (mainCam.transform.localEulerAngles.y - descam.transform.localEulerAngles.y) / max;
	}


	/*
	* If camera isn't on the final position, camera will rotate with animation
	*/
	void Update()
	{
		//if final position is reached
		if (i < max) 
		{
			if(descam.Equals(falseposCam)) mainCam.transform.Translate(-xpos,0,0, Space.World);
			else mainCam.transform.Translate(xpos,0,0, Space.World);

			mainCam.transform.Rotate (0,yrot,0, Space.World);
			i++;

			//pause game while rotation animationTime.timeScale = 0;
			Time.timeScale = 0;
            if (i == max){
				Time.timeScale = 1;
                if (descam.Equals(falseposCam)) {
					descam = trueposCam;
				} else {
					descam = falseposCam;
				}
				//rotate camera 180° for same drag-direction as before
				refCam.transform.Rotate (0, 180, 0, Space.World);
			}
		}
	}
}
