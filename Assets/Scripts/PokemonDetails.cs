using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonDetails : MonoBehaviour
{
    public string Name;
    public Sprite icon, Type1, Type2;
    public int HP, Attack, Defense, SpAttack, SpDefense, Speed;
    public string Fact;

    public Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public void attack()
    {
        animator.Play("Fight_attack");
    }
}
