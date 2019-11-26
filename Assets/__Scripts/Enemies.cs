using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    protected static Vector3[] directions = new Vector3[] {                 // a 
        Vector3.right, Vector3.up, Vector3.left, Vector3.down };
    [Header("Set in Inspector: Enemy")]                                      // b 
    public float maxHealth = 1;                               // c 
    [Header("Set Dynamically: Enemy")]
    public float health;                                      // c 
    protected Animator anim;                                        // c 
    protected Rigidbody rigid;                                       // c 
    protected SpriteRenderer sRend;                                       // c 
    protected virtual void Awake()
    {                                         // d 
        health = maxHealth;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        sRend = GetComponent<SpriteRenderer>();
    }
}

