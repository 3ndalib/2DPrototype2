using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    public Slider HealthSlider;
    public Text HealthText;

    public float Health;
    public float CurrentHealth;
    public float DamageAmount;


    void Start()
    {
        CurrentHealth = Health;
        HealthSlider.minValue = 0;
        HealthSlider.maxValue = Health;
    }

    // Update is called once per frame
    void Update()
    {
        HealthSlider.value = CurrentHealth;
        HealthText.text = CurrentHealth.ToString() + " / " + Health.ToString();
    }

}
