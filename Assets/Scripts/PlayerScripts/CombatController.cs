using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public bool CanAttack;
    public bool GotInput;
    public bool Attacking;
    public bool FirstAttack;

    public float LastInputTime = Mathf.NegativeInfinity;
    public float InputTimer;
    public float HitBoxRadius;
    public float Attack1Damage;

    public Surroundings SR;
    public AnimatorController AC;

    public Transform HitBox;
    public LayerMask WhatIsDamageable;

    public void Start()
    {
        SR = GetComponent<Surroundings>();
        AC = GetComponent<AnimatorController>();
        AC.Anim.SetBool("CanAttack", CanAttack);
    }

    public void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    public void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                GotInput = true;
                LastInputTime = Time.time;
            }
        }
    }

    public void CheckAttacks()
    {
        if (GotInput)
        {
            if (!Attacking)
            {
                GotInput = false;
                Attacking = true;
                FirstAttack = !FirstAttack;
                AC.Anim.SetBool("Attack1", true);
                AC.Anim.SetBool("FirstAttack", FirstAttack);
                AC.Anim.SetBool("Attacking", Attacking);
            }
        }
        if (Time.time >= LastInputTime + InputTimer)
        {
            GotInput = false;
        }
    }

    public void CheckAttackHitbox()
    {
        Collider2D[] DetectedObjects = Physics2D.OverlapCircleAll(HitBox.position, HitBoxRadius, WhatIsDamageable);

        foreach (Collider2D Enemy in DetectedObjects)
        {
            Enemy.GetComponent<Enemy>().CurrentHealth -= Attack1Damage;
            Debug.Log("You hit " + Enemy.name);
        }
    }

    public void FinishAttack1()
    {
        Attacking = false;
        AC.Anim.SetBool("Attacking", Attacking);
        AC.Anim.SetBool("Attack1", false);
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
