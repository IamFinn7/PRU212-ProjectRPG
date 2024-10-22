using UnityEngine;

public class ZoneDetected : MonoBehaviour
{
    private string targetTag = "Player";
    public Collider2D detectedObj = null;
    void OnTriggerEnter2D (Collider2D col){
        if (col.tag == targetTag)
        {
            //Khi Player đi vào vùng của Collider này kích hoạt Trigger
            //Sau đó "detectedObj" được gán giá trị là "Player"
            detectedObj = col;
        }
    }
    void OnTriggerExit2D (Collider2D col){
        if (col.tag == targetTag)
        {
            detectedObj = null;
        }
    }

}