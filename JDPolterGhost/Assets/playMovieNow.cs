using UnityEngine;
using System.Collections;

public class playMovieNow : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        playMe();   
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    void playMe()
    {

        MovieTexture movieTexture = (MovieTexture)GetComponent<Renderer>().material.mainTexture;

        movieTexture.Play();

        Invoke("playMe",400);
    }

}
