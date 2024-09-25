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
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }   
    public float health = 3;

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void removeObj(){
        Destroy(gameObject);
    }
}
