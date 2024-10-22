using UnityEngine;

public interface IDamageAble{
    public float Health {set; get;}
    void OnHit(float damage);
    void OnHit(float damage, Vector2 knockBackValue);
}