using UnityEngine;

public class HandleRangeAttack : MonoBehaviour
{
    [SerializeField] private bool isRanged = false;
    // [SerializeField] private GameObject MeleeObject = null;

    private string targetTag = "Player";
    public bool PlayerIn = false;
    private bool Melee_PlayerInHitRange = false;
    private BossController bossController ;
    void Start()
    {
       bossController = GetComponentInParent<BossController>();

    }

    void FixedUpdate()
    {
        if (isRanged)
        {
            HandleRange(); 
        }
        else
        {
            HandleMelee();
        }
    }

    void HandleRange()
    {
        if (bossController != null && PlayerIn && !Melee_PlayerInHitRange){
            if(bossController.canAttack)
            {
                int skill = RandomSkill();
                bossController.Attack(skill);
            }
        }
    }

    void HandleMelee()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(targetTag))
        {
            GetComponentInParent<MovingHandle>().targetInZoneAttack = true;

            IDamageAble damageAble = col.GetComponent<IDamageAble>();
            if (damageAble != null && damageAble.Health > 0)
            {
                PlayerIn = true;
            }
            else if (damageAble.Health <= 0)
            {
                PlayerIn = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag(targetTag))
        {
            GetComponentInParent<MovingHandle>().targetInZoneAttack = false;

            PlayerIn = false;
        }
    }

    private int RandomSkill()
    {
        int random = Random.Range(1, 101);
        return random <= 90 ? 1 : 2; // 50% for 1, 25% for 2, 25% for 3
    }
}
