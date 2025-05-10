using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bglooper : MonoBehaviour
{      
    public int numBgCount = 1;

    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체의 이름을 디버그 로그에 출력
       // Debug.Log("Triggered: " + collision.name);

        if (collision.CompareTag("Ground"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }
       
    }
}