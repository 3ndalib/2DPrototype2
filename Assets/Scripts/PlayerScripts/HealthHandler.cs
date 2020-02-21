using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    public Slider HealthSlider;
    public Text HealthText;

    public PlayerController PC;
    public Surroundings SR;

    public float Health;
    public float CurrentHealth;
    public float Timer = 0f;
    public float HealthTimer = 1f;
    public float RegenRate = 1f;
    public Vector2 CollisionForce;


    void Start()
    {
        PC = GetComponent<PlayerController>();
        SR = GetComponent<Surroundings>();

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
        if (CurrentHealth == 0)
        {
            Death();
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

    public void Death()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CollisionForce = new Vector2(CollisionForce.x * -SR.FacingDirection, CollisionForce.y);
            PC.RB.AddForce(CollisionForce, ForceMode2D.Impulse);
            Debug.Log("OnCollisionEnter2D");
            Damage(collision.gameObject.GetComponent<Enemy>().AttackDamage);
        }
    }
}
