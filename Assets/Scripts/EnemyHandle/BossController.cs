using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private BulletController skillPrefab;
    Animator animator;
    SpriteRenderer render;
    private void Start()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        animator.SetBool("isMoving", false);
        // Attack(2);  
    }

    public void Attack(int skill)
    {
        animator.SetTrigger("Attack");
        animator.SetInteger("idSkill", skill);
    }

    public void FirstSkill()
    {
        Collider2D temp = GetComponentInChildren<ZoneDetected>().detectedObj;
        Vector2 targetPosition = Vector2.zero;

        if(temp != null)
        {
            targetPosition = temp.gameObject.transform.localPosition;
        }

        bool facingRight = GetComponent<MovingHandle>().facingRight;

        Vector2 currentPosition = transform.position;

        Vector2 screenPosition = gameObject.transform.localPosition;
        float offsetWidth = render.bounds.size.x  / 4 * (facingRight ? 1 : -1);

        Vector2 skillPosition = new Vector2(screenPosition.x + offsetWidth, screenPosition.y);

        Vector2 direction = (targetPosition - skillPosition).normalized;

        var skill = Instantiate(skillPrefab, skillPosition, Quaternion.identity);
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        skill.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D skillRb = skill.GetComponent<Rigidbody2D>();
        skillRb.velocity = direction * 0.5f;

    }
}
