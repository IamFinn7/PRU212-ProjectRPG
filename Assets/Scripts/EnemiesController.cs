using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public ZoneDetected zoneDetected;
    public float moveSpeed = 500f;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer render;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }

    //FixedUpdate được gọi trong 1 chu kỳ nhất định 
        //(để mọi tính toán diễn ra mượt hơn)
    //Update được gọi trong mỗi frame
    void FixedUpdate(){
        if (zoneDetected.detectedObj != null) //Player đang nằm trong vùng kích hoạt
        {
            //normalize -> dùng để lấy hướng mà không bị ảnh hưởng bới khoảng cách
            Vector2 direction = (zoneDetected.detectedObj.transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);

            if(direction.x < 0)
            {
                render.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
            }
            else if(direction.x > 0)
            {
                render.flipX = false;
                gameObject.BroadcastMessage("IsFacingRight", true);
            }
        }
    }
    void removeEnemy()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
    }

    void CheckPlayer()
    {
        gameObject.BroadcastMessage("CheckPlayerInHitBox");
    }
}
