using UnityEngine;
using UnityEngine.UI;

public class DamageAble : MonoBehaviour, IDamageAble
{
    [SerializeField] public float health = 3f;
    [SerializeField] public float maxHealth = 3f;
    [SerializeField] public Image HealthBarFill;
    Animator animator;
    Rigidbody2D rb;
    // private AudioManager audioManager; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", true);

        if (HealthBarFill != null)
        {
            HealthBarFill.fillAmount = health/maxHealth;
        }
    }

    // private void Awake()
    // {
    //     audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    // }


    public float Health { 
         set
        {
            if(value < health){
                animator.SetTrigger("hit");
            }
            
            health = value;

            if (HealthBarFill != null)
            {
            HealthBarFill.fillAmount = health/maxHealth;
            }

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

    public void OnHit(float damage)
    {
         Health -= damage;
    }

    public void OnHit(float damage, Vector2 knockBackValue)
    {
        Health -= damage;
        rb.AddForce(knockBackValue);
    }

    public float MaxHealth
    {
        set
        {

        }
        get
        {
            return maxHealth;
        }
    }

    public void AddHealth(int healthValue)
    {
        health = Mathf.Min(health + healthValue, maxHealth);


        if (HealthBarFill != null)
        {
            HealthBarFill.fillAmount = health / maxHealth;
        }
    }

    public void SetIsAlive(bool isAlive)
    {
        animator.SetBool("isAlive", isAlive);
    }
}