using UnityEngine;
using System.Collections;

/// <summary>
/// Container for in-game time
/// </summary>
public static class GameTime 
{
    //Length of an in-game day in minutes
    public static float lengthOfDay = 1.5f;

    /// <summary>
    /// Returns the in-game time, in hours, as a float
    /// </summary>
    /// <returns></returns>
	public static float TimeOfDay()
    {
        float time;
        float timeScaleFactor = (60f * 24f) / lengthOfDay;

        time = (Time.time * timeScaleFactor) % (3600f * 24f);

        return time / 3600f;
    }
}
