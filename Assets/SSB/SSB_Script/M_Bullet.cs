using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Bullet : MonoBehaviour
{
    // ��� ������ �̵��ϰ� �ʹ� 
    // �ʿ�Ӽ� : �ӵ� 
    public float speed = 10;
 
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //��� ������ �̵��ϰ� �ʹ� 
        Vector3 dir = Vector3.forward;
        dir.Normalize();
        //�̵��ϰ�ʹ�
        transform.position += dir * speed * Time.deltaTime;
    }
}
