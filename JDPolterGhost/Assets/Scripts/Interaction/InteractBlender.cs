using UnityEngine;
using System.Collections;

public class InteractBlender : InteractableObject
{


    public override bool Activate()
    {
        if(spookey == false)
        {

            return true;
        }
        return false;
    }

    private void Blend()
    {
        //TODO: Play audio clip


    }
}
