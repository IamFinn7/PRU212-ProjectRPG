using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitEnemyController : MonoBehaviour
{
    private string tagTarget = "Player";
    public Vector3 faceRight = new Vector3(0.15f, -0.0023f, 0);
    public Vector3 faceLeft = new Vector3(-0.15f, -0.0023f, 0);
    public float damage = 1;
    public Collider2D localPlayerCollider = null;
    Animator animator;
    void Start()
    {
       animator = GetComponentInParent<Animator>();
    }

     public void IsFacingRight(bool isRight){
        if(isRight){
            gameObject.transform.localPosition = faceRight;
        }
        else{
            gameObject.transform.localPosition = faceLeft;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        //Nếu phát hiện tag Player thì gây damage
        if(col.tag == tagTarget){
            animator.SetBool("isAttacking", true);
            localPlayerCollider = col;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.tag == tagTarget){
            animator.SetBool("isAttacking", false);
            localPlayerCollider = null;
        }
    }

    public void CheckPlayerInHitBox(){
        if (localPlayerCollider != null)
        {
            IDamageAble damageAble = localPlayerCollider.GetComponent<IDamageAble>();
             
            if(damageAble != null)
            {
                damageAble.OnHit(damage);
            }
        }

    }
}
