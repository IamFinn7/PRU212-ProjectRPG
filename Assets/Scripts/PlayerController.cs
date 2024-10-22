using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.5f; //player's speed
    public float maxSpeed = 2.5f;
    public bool canMove = true;
    Vector2 movementInput; 
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer render;


    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>(); //input from keyboard
    }

    void OnFire(){
        animator.SetTrigger("swordAttack");//kích hoạt animator chém
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
            //nếu bấm phím để di chuyển
        if(canMove && movementInput != Vector2.zero)
        {
            //rb.velocity là vector(gồm hướng và độ lớn của vận tốc) của Rigidbody
            //ClampMagnitude: giới hạn độ lớn của vector
            //nếu tổng vận tốc vượt quá MaxSpeed thì nó sẽ giảm xuống bằng MaxSpeed
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);
            if (movementInput.x < 0)
            {
                render.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
            }
            else if (movementInput.x > 0)
            {
                render.flipX = false;
                gameObject.BroadcastMessage("IsFacingRight", true);
            }
            animator.SetBool("isMoving",true);
        }   
        else
        {
            animator.SetBool("isMoving", false);
        }
    } 
}