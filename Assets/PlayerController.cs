using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.5f; //player's speed
    public float collisionOffset = 1f; //khoảng cách giữa player vs vật thể
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollision = new List<RaycastHit2D>();
    Vector2 movementInput; 
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer render;

    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>(); //input from keyboard
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
        if(movementInput != Vector2.zero)
        {
            bool success = tryMove(movementInput);
            animator.SetBool("isMoving", success);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (movementInput.x < 0)
        {
            render.flipX = true;
        }
        else if (movementInput.x > 0)
        {
            render.flipX = false;
        }
    } 

    //hàm dùng để check có vật cản không
    private bool tryMove(Vector2 direction){
        if (direction != Vector2.zero)
        {
            //check barrier, if yes -> return 1, if no -> return 0
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollision,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            ); 

            //if no -> allow movement -> return true
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }

            return false;
        }
        return false;
    }

}
