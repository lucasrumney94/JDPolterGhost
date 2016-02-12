using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class hauntedText : MonoBehaviour {

    private Interaction playerInteraction;
    private Text myText;

	// Use this for initialization
	void Start ()
    {
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
        myText = this.gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        myText.enabled = playerInteraction.hauntingObject;
	}
}
