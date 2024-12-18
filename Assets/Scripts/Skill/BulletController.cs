using UnityEngine;
public class BulletController : MonoBehaviour
{
    private Vector3 startPosition;

    [SerializeField] private float knockBackForce = 500f;
    [SerializeField] private float bulletDamage = 1f;
    [SerializeField] private bool isEnemy = true;
    private string targetTag;


    [SerializeField] private float distanceBullet = 1f;

    void Start(){
        startPosition = gameObject.transform.position;

         if (isEnemy)
        {
            targetTag = "Player";
            bulletDamage = 1f;
        }
        else
        {
            targetTag = "Enemies";
            bulletDamage = 3f;
        }

    }

    void Update(){
         if (Vector3.Distance(transform.position, startPosition) > distanceBullet){
            Destroy(gameObject);
         }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageAble damageAble = col.GetComponent<IDamageAble>();

        if (damageAble != null && col.tag == targetTag)
        {
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction = (Vector2)(col.gameObject.transform.position - parentPosition).normalized;

            Vector2 knockBackValue = direction * knockBackForce;

            damageAble.OnHit(bulletDamage, knockBackValue);
            
            Destroy(gameObject);
        }
    }


}