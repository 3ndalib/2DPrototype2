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

    public EnemyDifficulty Difficulty;

    public Color DetectingColor;
    public Color NotDetectingColor;

    public float Health;
    public float CurrentHealth;
    public float DamageAmount;


    public virtual void Start()
    {
        BC = GetComponent<BoxCollider2D>();
        RB = GetComponent<Rigidbody2D>();

        CurrentHealth = Health;       
        HealthSlider.minValue = 0;
        HealthSlider.maxValue = Health;
    }

    public virtual void Update()
    {
        HealthSlider.value = CurrentHealth;

        HealthText.text = CurrentHealth.ToString() + " / " + Health.ToString();
    }

}
