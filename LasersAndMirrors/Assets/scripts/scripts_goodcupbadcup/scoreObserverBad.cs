using UnityEngine;
using System.Collections;

/*
 * Process Objects that pass the BadCollider
 */

public class scoreObserverBad : MonoBehaviour {

	public Camera mainCam;
    public AudioClip life_down, life_up, rotate, freeze;
    public AudioSource sounds;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("Bad")) {
			GUI_ScriptGB.score++;				//Increase score of player if badCup was terminated
		} else if (other.tag.Equals ("PowerUp")) {
			switch (other.gameObject.name) {
			case "OneUp(Clone)":
				if (GameObject.Find("Main Camera").GetComponent<GUI_ScriptGB>().leben < 5 && GameObject.Find("OneUp(Clone)").GetComponent<CollisionCounter>().collided==false)
                    {         //if powerUp was OneUp the live will increase
						GameObject.Find("OneUp(Clone)").GetComponent<CollisionCounter>().collided=true;
						GameObject.Find ("Main Camera").GetComponent<GUI_ScriptGB> ().leben++;
                        sounds = GetComponent<AudioSource>();
                        sounds.clip = life_up;
                        sounds.Play();
                    }
                    
				break;
			case "SlowDown(Clone)":
				if (GameObject.Find ("SlowDown(Clone)").GetComponent<CollisionCounter> ().collided == false) {
					GameObject.Find("SlowDown(Clone)").GetComponent<CollisionCounter>().collided=true;
					sounds = GetComponent<AudioSource> ();
					sounds.clip = freeze;
					sounds.Play ();
					GameObject.Find ("Treadmill").GetComponent<TreadmillRegulator> ().slowDownStart = GUI_ScriptGB.timeAnz;	//if powerUp was SlowDown the cups will move slower
					GameObject.Find ("Treadmill").GetComponent<TreadmillRegulator> ().slowDownActive = true;
				}
				break;
			case "Invert(Clone)":
				if (GameObject.Find ("Invert(Clone)").GetComponent<CollisionCounter> ().collided == false) {
					GameObject.Find ("Invert(Clone)").GetComponent<CollisionCounter> ().collided = true;
					sounds = GetComponent<AudioSource> ();
					sounds.clip = rotate;
					sounds.Play ();
					mainCam.GetComponent<InvertScreen> ().i = 0;
					GameObject.Find ("Treadmill").GetComponent<TreadmillRegulator> ().slowDownStart = GUI_ScriptGB.timeAnz;		//if powerup was Invert the Camera will rotate with animation
					GameObject.Find ("Treadmill").GetComponent<TreadmillRegulator> ().slowDownDuration = 1.4f;
					GameObject.Find ("Treadmill").GetComponent<TreadmillRegulator> ().slowDownActive = true;
				}
				break;
			default:
				break;
			}
			
		} else {
            sounds = GetComponent<AudioSource>();
            sounds.clip = life_down;
            sounds.Play();
            
			GameObject.Find ("Main Camera").GetComponent<GUI_ScriptGB> ().leben--;
			GameObject.Find ("Main Camera").GetComponent<GUI_ScriptGB> ().lifeCounter = GUI_ScriptGB.timeAnz + 2;
		}
		}
}
