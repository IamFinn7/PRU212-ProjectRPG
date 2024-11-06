using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InventoryBGController inventory;
    [SerializeField] private WeaponStatusHandle weaponStatusHandle;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private TMP_Text StatusTxt;
    public float moveSpeed = 0.5f;
    public float maxSpeed = 2.5f;
    public bool canMove = true;
    Vector2 movementInput; 
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer render;
    private bool facingRight = true;
    public bool isRange = false;

    // Tạo tham chiếu đến DataPlayer
    private DataPlayer dataPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        weaponStatusHandle.ChangeStatus(isRange);
        dataPlayer = DataPlayer.MyInstance; // Lấy instance của DataPlayer
    }

    void FixedUpdate()
    {
        if (canMove && movementInput != Vector2.zero)
        {
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
            animator.SetBool("isMoving", true);
        }   
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UpdateFacingDirectionByMouse(mousePos);

        if (isRange)
        {
            animator.SetTrigger("swordAttack");
            AttackRange();
        }
        else
        {
            animator.SetTrigger("swordAttack");
        }
    }

    private void UpdateFacingDirectionByMouse(Vector3 mousePos)
    {
        if (mousePos.x > transform.position.x)
        {
            FaceRight(true);
        }
        else
        {
            FaceRight(false);
        }
    }

    private void FaceRight(bool isRight)
    {
        render.flipX = !isRight;
        gameObject.BroadcastMessage("IsFacingRight", isRight);
        facingRight = isRight;
    }

    public void AddItem(ItemController item)
    {
        inventory.AddItem(item, item.DropValue);
    }

    void AttackRange()
    {
        bool sus = CheckCoinEnough();

        if (sus)
        {
            Vector3 screenPosition = gameObject.transform.position;
            float offsetWidth = render.bounds.size.x / 4 * (facingRight ? 1 : -1);
            Vector3 newPosition = new Vector3(screenPosition.x + offsetWidth, screenPosition.y, screenPosition.z);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector3 direction = (mousePosition - newPosition).normalized;
            var bullet = Instantiate(bulletPrefab, newPosition, Quaternion.identity);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            float bulletSpeed = 1.5f;
            bulletRb.velocity = direction * bulletSpeed;
        }
        else
        {
            StartCoroutine(ShowTemporaryMessage("NEED COIN", 1f));
        }
    }

    private IEnumerator ShowTemporaryMessage(string message, float duration)
    {
        StatusTxt.text = message;
        yield return new WaitForSeconds(duration);
        StatusTxt.text = ""; // Clear the message after the duration
    }

    bool CheckCoinEnough()
    {
        int sus = inventory.FindItemByName("Coin");

        if (sus >= 0)
        {
            inventory.ReduceQuantityOfItem(sus);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeWeapon()
    {
        isRange = !isRange;
        weaponStatusHandle.ChangeStatus(isRange);
    }

    // Phương thức Die và Respawn gọi từ DataPlayer
    // public void Die()
    // {
    //     dataPlayer.Die();
    // }

    // public void Respawn()
    // {
    //     Debug.Log("a");
    //     dataPlayer.Respawn();
    // }

    // public void UpdateCountDie()
    // {
    //     dataPlayer.UpdateCountDie();
    // }
}
