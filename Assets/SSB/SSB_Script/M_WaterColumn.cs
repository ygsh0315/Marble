using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_WaterColumn : MonoBehaviour
{
    //4�ʰ� up����, 2�ʰ� down���� 

    //�ʿ�Ӽ� : ����ð�, �����ð� 

    float currTime;
    public float downTime = 2;
    
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {

        //1.�ð��� �帥�� 
        currTime += Time.deltaTime;
        //2.2�ʰ� �帣��
        if(currTime > downTime)
        {
            //3.������� ��������
            Vector3 dir = Vector3.down;
            transform.position += dir * 2 * Time.deltaTime;
           
        }

    }
}
