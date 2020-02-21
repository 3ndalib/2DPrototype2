using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyDifficulty
{
    Green,
    Blue,
    Red
}
public class Enemy : MonoBehaviour
{
    public Slider HealthSlider;
    public Text HealthText;
    public Rigidbody2D RB;
    public BoxCollider2D BC;
    public Transform Canvas;
    public Quaternion InitRot;

    public EnemyDifficulty Difficulty;

    public Color DetectingColor;
    public Color NotDetectingColor;

    public float Health;
    public float CurrentHealth;
    public float AttackDamage;


    public virtual void Start()
    {      
        BC = GetComponent<BoxCollider2D>();
        RB = GetComponent<Rigidbody2D>();

        HealthInit();
    }

    public virtual void Update()
    {
        HealthHandling();
    }

    public virtual void FixedUpdate()
    {
        
    }

    public void HealthInit()
    {
        Canvas = transform.Find("Canvas");
        InitRot = Canvas.transform.rotation;
        CurrentHealth = Health;
        HealthSlider.minValue = 0;
        HealthSlider.maxValue = Health;
    }

    public void HealthHandling()
    {
        Canvas.transform.rotation = InitRot;
        HealthSlider.value = CurrentHealth;
        HealthText.text = CurrentHealth.ToString() + " / " + Health.ToString();
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
        if (CurrentHealth == 0) 
        {
            Death();
        }
    }

    public void Death() 
    {
        Destroy(gameObject);
    }
}
