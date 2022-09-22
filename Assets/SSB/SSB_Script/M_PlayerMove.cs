using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WASD 앞뒤좌우로 움직이게 한다 
public class M_PlayerMove : MonoBehaviour
{
    //이동속도
    public float speed = 3; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //WASD
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //방향을 구한다 
        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir.Normalize();
        //움직이게 한다
        transform.position += dir * speed * Time.deltaTime;
    }
}
