using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightTimer : MonoBehaviour
{
    [Range(0f, 24f)]
    public float startTime;
    [Range(0f, 24f)]
    public float endTime;
    private Light roomLight;

    void Start()
    {
        roomLight = GetComponent<Light>();
    }

    void Update()
    {
        if(GameTime.TimeOfDay() > startTime && GameTime.TimeOfDay() < endTime)
        {
            roomLight.enabled = true;
        }
        else
        {
            roomLight.enabled = false;
        }
    }
}
