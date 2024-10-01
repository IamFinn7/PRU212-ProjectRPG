using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public float health = 3;
    Animator animator;
    public float Health
    {
        set
        {
            if(value < health){
                animator.SetTrigger("hit");
            }
            
            health = value;

            if (health <= 0)
            {
                animator.SetBool("isAlive", false);
            }
        }
        get
        {
            return health;
        }
    }   
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", true);
    }

    void OnHit(float damage){
        Health -= damage;
    }

    void removeEnemy(){
        Destroy(gameObject);
    }
}
