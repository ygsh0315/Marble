using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WASD �յ��¿�� �����̰� �Ѵ�
//�ʿ�Ӽ� : �̵��ӵ� 

//Stage������ ����� �״´�
//�ʿ�Ӽ� : Stage


public class M_PlayerMove : MonoBehaviour
{
    //�̵��ӵ�
    public float speed = 3;

    //Stage
    public Transform stage;

    //dieDistance
    public float dieDistance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Die();
        //WASD
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //������ ���Ѵ� 
        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir.Normalize();
        //�����̰� �Ѵ�
        transform.position += dir * speed * Time.deltaTime;
    }

    

    public void Die()
    {
        dieDistance = Vector3.Distance(stage.position, transform.position);
        //���� ���������� ����ԵǸ�
        if( dieDistance >5f)
        {
            //����ó���Ѵ�
            Destroy(gameObject, 2);
        }

    }
}
