using UnityEngine;

public class DamageAble : MonoBehaviour, IDamageAble
{
    public float health = 3f;
    Animator animator;
    Rigidbody2D rb;
    public float Health { 
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
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", true);
    }

    public void OnHit(float damage)
    {
         Health -= damage;
    }
    public void OnHit(float damage, Vector2 knockBackValue)
    {
        Health -= damage;
        rb.AddForce(knockBackValue);
    }


}