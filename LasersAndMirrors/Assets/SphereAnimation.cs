using UnityEngine;
using System.Collections;

public class SphereAnimation : MonoBehaviour {

    private float factor = 0.02f, lerp = 0.01f, deltaLerp = 0.02f;
    private bool stop = false;

    public Color colorstart, colorend;
    private Renderer renderer;
	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        lerp += deltaLerp;
        if(lerp >= 0.99f)
        {
            deltaLerp = -0.005f;
        }
        if (lerp < 0.01f)
        {
            stop = true;
        }

        if (!stop)
        {
            renderer.material.color = Color.Lerp(colorstart, colorend, lerp);
        }
        if (transform.localScale != new Vector3(2.0f,2.0f,2.0f))
        {
            transform.localScale += new Vector3(factor,factor, factor);
            transform.Translate(0.015f, 0.0f, 0.0f);
        }
        
	}
}
