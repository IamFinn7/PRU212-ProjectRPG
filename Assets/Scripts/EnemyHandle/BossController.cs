using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour
{
    [SerializeField] private BulletController skillPrefab;
    [SerializeField] private GameObject summonCastPrefab;
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

      
        int projectileCount = Random.Range(1, 10);
        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 direction = (targetPosition - skillPosition).normalized;
            var skill = Instantiate(skillPrefab, skillPosition, Quaternion.identity);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            skill.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            Rigidbody2D skillRb = skill.GetComponent<Rigidbody2D>();
            float skillSpeed = 1.5f;

            if (projectileCount > 3)
            {
                float spreadAngle = 10f; // Spread angle in degrees
                angle += (i - 1) * spreadAngle; // Adjust angles to create spread effect
                skill.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Set the new angle

                // Recalculate the direction with the new angle
                direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
            }
            skillRb.velocity = direction * skillSpeed;
        }
    }

    void Die()
    {
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        Destroy(gameObject);
    }

    public event Action OnEnemyDestroyed;
    void OnDestroy()
    {
        if(OnEnemyDestroyed != null){
            OnEnemyDestroyed();
        }
    }

     public void SecondSkill()
    {
        Vector3 currentPosition = transform.position;

        List<Vector3> surroundingPositions = new List<Vector3>();

        surroundingPositions.Add(new Vector3(currentPosition.x - 0.3f, currentPosition.y, currentPosition.z));
        surroundingPositions.Add(new Vector3(currentPosition.x + 0.3f, currentPosition.y, currentPosition.z));
        surroundingPositions.Add(new Vector3(currentPosition.x, currentPosition.y + 0.3f, currentPosition.z));
        surroundingPositions.Add(new Vector3(currentPosition.x, currentPosition.y - 0.3f, currentPosition.z));

        foreach (Vector3 position in surroundingPositions)
        {
            Instantiate(summonCastPrefab, position, Quaternion.identity);
        }

    }
    public bool canAttack = false;

    public void LockAttack() => canAttack = false;
    public void UnLockAttack() => canAttack = true;
}
