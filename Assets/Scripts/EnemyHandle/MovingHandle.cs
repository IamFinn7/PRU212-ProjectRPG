using UnityEngine;
public class MovingHandle : MonoBehaviour
{
    //khoảng cách giữa obj với vật thể muốn né
    [SerializeField] private float obstacleDetectionRange = 1f;
    // vật thể muốn né
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float moveSpeed = 500f;
    [SerializeField] private ZoneDetected zoneDetected;
    public bool facingRight = true;
    public bool targetInZoneAttack = false;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer render;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        GameObject target = zoneDetected.detectedObj != null ? zoneDetected.detectedObj.gameObject : null;

        if (target != null && targetInZoneAttack == false)
        {
            HandleMoving(target.transform.position);
        } else
        {
            HandleMoving(target.transform.position);
            animator.SetBool("isMoving", false);
        }
    }

    void HandleMoving(Vector2 targetPosition)
    {
        //normalize -> dùng để lấy hướng mà không bị ảnh hưởng bới khoảng cách
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        //Nếu phía trước có vật cản
        if (IsObstacleInDirection(direction))
        {
            //Tạo ra 1 vector mới để obj di chuyển né vật cản
            Vector2 newDirection = FindAlternativeDirection(direction);
            direction = newDirection;
        }

        MoveInDirection(direction);
        FlipSprite(direction);

        animator.SetBool("isMoving", true);
    }

    //Hàm dùng để kiểm tra xem có vật cản hay không
    bool IsObstacleInDirection( Vector2 direction)
    {
        //Nếu obj tiến tới với 1 frame thì có đụng vật cản hay không
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleDetectionRange, obstacleLayer);
        //Nếu có thì return true, không thì false
        return hit.collider != null;
    }

    private Vector2 FindAlternativeDirection(Vector2 currentDirection)
    {
        float randomAngle = -90;

        //Tạo ra 1 hướng ngẫu nhiên để obj rẽ hướng
        Vector2 newDirection = Quaternion.Euler(0, 0, randomAngle) * currentDirection;

        //normalize -> dùng để lấy hướng mà không bị ảnh hưởng bới khoảng cách
        return newDirection.normalized;
    }

    void MoveInDirection(Vector2 direction)
    {
        rb.AddForce(direction * moveSpeed * Time.deltaTime);
    }

    void FlipSprite(Vector2 direction)
    {
        render.flipX = direction.x < 0;
        facingRight = direction.x > 0;
    }
}
