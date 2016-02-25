using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class AmbientGradient : MonoBehaviour
{
    public Gradient skyColor;

    private Light ambientLight;

    void Start()
    {
        ambientLight = GetComponent<Light>();
    }

    void Update()
    {
        float gradientPoint = GameTime.TimeOfDay() * (1f / 24f);
        ambientLight.color = skyColor.Evaluate(gradientPoint);
    }
}
