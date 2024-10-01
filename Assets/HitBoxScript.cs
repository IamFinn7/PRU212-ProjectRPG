using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HitBoxScript : MonoBehaviour
{
    public float damage = 1;
    public Vector3 faceRight = new Vector3(0.1456f, -0.005f, 0);
    public Vector3 faceLeft = new Vector3(-0.1456f, -0.005f, 0);
    public Collider2D swordCollider;

    public void IsFacingRight(bool isRight){
        if(isRight){
            gameObject.transform.localPosition = faceRight;
        }
        else{
            gameObject.transform.localPosition = faceLeft;
        }
    }

    //hàm được tự gọi ra khi có va chạm giữa Obj có RigidBody 2D vs 1 Collider 2D khác
    void OnCollisionEnter2D(Collision2D col){
        //col.collider: truy cập vào Collider của Obj đang va chạm với với "col"
        col.collider.SendMessage("OnHit");
    }  
}
