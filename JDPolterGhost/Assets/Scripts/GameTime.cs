using UnityEngine;
using System.Collections;

public static class GameTime 
{
    public static float timeScaleFactor = 0.01f;

	public static float TimeOfDay()
    {
        float time;

        time = Time.time % 3600;

        return time * timeScaleFactor;
    }
}
