using UnityEngine;

public class ZoneDetected : MonoBehaviour
{
    private string targetTag = "Player";
    public Collider2D detectedObj = null;
    public bool PlayIn = false;
    void OnTriggerEnter2D (Collider2D col){
        if (col.tag == targetTag)
        {
            IDamageAble damageAble = col.GetComponent<IDamageAble>();

            if (damageAble.Health > 0)
            {
                //Khi Player đi vào vùng của Collider này kích hoạt Trigger
                //Sau đó "detectedObj" được gán giá trị là "Player"
                detectedObj = col;
                PlayIn = true;
            }
            else
            {
                detectedObj = null;
                PlayIn = false;
            }
        }
    }
    void OnTriggerExit2D (Collider2D col){
        if (col.tag == targetTag)
        {
            detectedObj = null;
        }
    }

}