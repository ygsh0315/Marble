using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_WaterColumn : MonoBehaviour
{
    //4�ʰ� up����, 2�ʰ� down���� 

    //�ʿ�Ӽ� : ����ð�, �����ð� 

    float currTime;
    public float downTime = 2;
    public float upTime = 2;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //1.�ð��� �帥�� 
        currTime += Time.deltaTime;
        //2.2�ʰ� �帣��
        // ���� ����ð� downTime ���� �۴ٸ�
        // 2�ʵ��� ������� ó���� ��������
        if (currTime < downTime)
        {
            //3.������� ��������
            Vector3 dir = -transform.up;
            dir.Normalize();
            transform.position += dir * 2 * Time.deltaTime;

        }
        //4.����ð��� upTime�� �ʰ��ϸ�  
        // ������� 2�ʵ��� ���󰡰�, �ٽ� 4�ʰ� ������ ������� �ö󰡵��� �ϰ� �ʹ�.
        else if (currTime >= downTime && currTime < (downTime + upTime))
        {
            //5.������� �ö󰣴� 
            Vector3 dir = transform.up;
            dir.Normalize();
            transform.position += dir * 2 * Time.deltaTime;

        }
        else if( currTime >= (downTime + upTime))
        {
            currTime = 0;
        }
    }
}
