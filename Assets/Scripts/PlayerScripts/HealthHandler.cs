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
    public float Timer = 0f;
    public float HealthTimer = 1f;
    public float RegenRate = 1f;


    void Start()
    {
        HealthInit();
    }

    // Update is called once per frame
    void Update()
    {
        HealthHandling();
    }

    public void HealthInit() 
    {
        CurrentHealth = Health;
        HealthSlider.minValue = 0;
        HealthSlider.maxValue = Health;
    }

    public void HealthHandling() 
    {
        HealthSlider.value = CurrentHealth;
        HealthText.text = CurrentHealth.ToString() + " / " + Health.ToString();
        if (CurrentHealth < Health)
        {
            Timer += Time.deltaTime;
            if (Timer >= HealthTimer)
                Regenerate(RegenRate);
        }
        else
        {
            Timer = 0f;
        }
    }

    public void Damage(float DamageAmount)
    {
        if (CurrentHealth - DamageAmount >= 0)
        {
            CurrentHealth -= DamageAmount;
        }
        else if (CurrentHealth - DamageAmount < 0)
        {
            CurrentHealth = 0;
            return;
        }
    }

    public void Regenerate(float RegenRate)
    {
        Timer = 0f;
        if (CurrentHealth + RegenRate <= Health)
        {
            CurrentHealth += RegenRate;
        }
        else if (CurrentHealth + RegenRate > Health)
        {
            CurrentHealth = Health;
            return;
        }

    }
}
