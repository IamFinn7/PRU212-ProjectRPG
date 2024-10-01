using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    Animator animator;
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
            }
        }
        get
        {
            return health;
        }
    }   
    public float health = 3;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnHit(){
        print("Hit");
    }
}
