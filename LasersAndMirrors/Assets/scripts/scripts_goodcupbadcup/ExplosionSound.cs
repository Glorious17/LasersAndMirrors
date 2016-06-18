using UnityEngine;
using System.Collections;

public class ExplosionSound : MonoBehaviour {
    public AudioClip explode;
    public AudioSource bomb;

    public void PlayExplosion()
    {
        Debug.Log("Triggered ASS FUCK");
        bomb = GetComponent<AudioSource>();
        bomb.clip = explode;
        bomb.Play();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
