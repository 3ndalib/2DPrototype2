using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public float HitBoxRadius;
    public float AttackDamage;
    public float LastClickedTime = 0;
    public float MaxComboDelay = 0.9f;

    public int NoOfClicks = 0;

    public Surroundings SR;
    public AnimatorController AC;

    public Transform HitBox;
    public LayerMask WhatIsDamageable;

    public void Start()
    {
        SR = GetComponent<Surroundings>();
        AC = GetComponent<AnimatorController>();
    }

    public void Update()
    {
        CheckCombatInput();
    }

    public void CheckCombatInput()
    {
        OnClick();
    }

    public void OnClick()
    {
        if (Time.time - LastClickedTime > MaxComboDelay)
        {
            NoOfClicks = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            LastClickedTime = Time.time;
            NoOfClicks++;
            if (NoOfClicks == 1) 
            {
                AC.Anim.SetBool("Punch1", true);
                    
            }
            NoOfClicks = Mathf.Clamp(NoOfClicks, 0, 2);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            LastClickedTime = Time.time;
            NoOfClicks++;
            if (NoOfClicks == 1)
            {
                AC.Anim.SetBool("Kick1", true);

            }
            NoOfClicks = Mathf.Clamp(NoOfClicks, 0, 2);
        }
    }

    public void Return1Punch() 
    {
        if (NoOfClicks >= 2)
        {
            AC.Anim.SetBool("Punch2", true);
        }
        else 
        {
            AC.Anim.SetBool("Punch1", false);
            NoOfClicks = 0;
        }
    }

    public void Return1Kick() 
    {
        if (NoOfClicks >= 2)
        {
            AC.Anim.SetBool("Kick2", true);
        }
        else
        {
            AC.Anim.SetBool("Kick1", false);
            NoOfClicks = 0;
        }
    }

    public void Return2Punch() 
    {
        AC.Anim.SetBool("Punch1", false);     
        AC.Anim.SetBool("Punch2", false);
        NoOfClicks = 0;
    }

    public void Return2Kick()
    {
        AC.Anim.SetBool("Kick1", false);
        AC.Anim.SetBool("Kick2", false);
        NoOfClicks = 0;
    }

    public void CheckAttackHitbox()
    {
        Collider2D[] DetectedObjects = Physics2D.OverlapCircleAll(HitBox.position, HitBoxRadius, WhatIsDamageable);

        foreach (Collider2D Enemy in DetectedObjects)
        {
            Enemy.GetComponent<Enemy>().Damage(AttackDamage);
            Debug.Log("You hit " + Enemy.name);
        }
    }

    public void OnDrawGizmos()
    {
        if (HitBox.position == null) 
        {
            return;
        }
        Gizmos.DrawWireSphere(HitBox.position, HitBoxRadius);
    }
}
