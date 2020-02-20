using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{

    public float HitBoxRadius;
    public float AttackDamage;
    public float TimerValue = 1;
    public float DelayTimer;

    public Surroundings SR;
    public AnimatorController AC;

    public Transform HitBox;
    public LayerMask WhatIsDamageable;

    public void Start()
    {
        SR = GetComponent<Surroundings>();
        AC = GetComponent<AnimatorController>();
        DelayTimer = TimerValue;
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
        if (Input.GetMouseButtonDown(0)) 
        {
            AC.Anim.SetBool("Punch1", true);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            AC.Anim.SetBool("Kick1", true);
        }
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
