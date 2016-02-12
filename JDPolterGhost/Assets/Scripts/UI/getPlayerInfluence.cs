using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class getPlayerInfluence : MonoBehaviour {

    private Interaction playerInteraction;
    private Text myText;

	// Use this for initialization
	void Start ()
    {

        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
        myText = this.GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        myText.text = "Influence: " + playerInteraction.influence.ToString();
	}
}
