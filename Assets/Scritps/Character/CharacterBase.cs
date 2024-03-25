using System;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public int maxHp;
    public IntVariable hp;
    public int CurrentHp { get=>hp.currentValue; set=>hp.SetValue(value);}
    public int MaxHp { get=>hp.maxValue; }
    protected Animator animator;
    public bool isDead;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        hp.maxValue = maxHp;
        CurrentHp = MaxHp;
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHp>damage)
        {
            CurrentHp -= damage;
        }
        else
        {
            CurrentHp = 0;
            isDead = true;
        }
    }
}
