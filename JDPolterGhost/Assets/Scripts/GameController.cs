using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public bool dadScaredOff = false;
    public bool momScaredOff = false;

    public AIAgentBase dadAgent;
    public AIAgentBase momAgent;

    void Update()
    {
        if(dadAgent.fearLevel > dadAgent.maxFearLevel)
        {
            dadScaredOff = true;
        }
        if (momAgent.fearLevel > momAgent.maxFearLevel)
        {
            momScaredOff = true;
        }
        if(dadScaredOff && momScaredOff)
        {
            //wait some time, then game is won
            Debug.Log("You won the Game!");
        }
    }
}
