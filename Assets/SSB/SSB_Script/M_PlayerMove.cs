using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WASD �յ��¿�� �����̰� �Ѵ� 
public class M_PlayerMove : MonoBehaviour
{
    //�̵��ӵ�
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
        //������ ���Ѵ� 
        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir.Normalize();
        //�����̰� �Ѵ�
        transform.position += dir * speed * Time.deltaTime;
    }
}
