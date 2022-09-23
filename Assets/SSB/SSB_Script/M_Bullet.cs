using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Bullet : MonoBehaviour
{
    // 계속 앞으로 이동하고 싶다 
    // 필요속성 : 속도 
    public float speed = 10;
 
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //계속 앞으로 이동하고 싶다 
        Vector3 dir = Vector3.forward;
        dir.Normalize();
        //이동하고싶다
        transform.position += dir * speed * Time.deltaTime;
    }
}
