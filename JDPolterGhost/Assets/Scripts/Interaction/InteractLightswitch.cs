using UnityEngine;
using System.Collections;

public class InteractLightswitch : InteractableObject
{
    public Light[] roomLights;

    public override bool Activate()
    {
        if(spookey == false)
        {
            StartCoroutine(FlipLights());
            return true;
        }
        else return false;
    }

    private IEnumerator FlipLights()
    {
        spookey = true;
        SwitchLights(false);

        yield return new WaitForSeconds(timeout);

        spookey = false;
        SwitchLights(true);
        ClearFrightenedAgents();
    }

    private void SwitchLights(bool state)
    {
        if (roomLights.Length > 0)
        {
            foreach (Light light in roomLights)
            {
                light.enabled = state;
            }
        }
        else Debug.Log("You need at least one light defined!", this);
    }
}
