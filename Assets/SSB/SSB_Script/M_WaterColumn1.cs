using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_WaterColumn1 : MonoBehaviour
{
    bool first2Wait = true;
    bool firstMoveDown = false;
    bool firstMoveUp = false;

    float currTime;
    
    enum WaterState
    {
        FirstWait,
        FirstMoveDown,
        MoveUp,
        MoveUpWait,
        FirstMoveDownWait
    }

    WaterState state = WaterState.FirstWait;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //������ �ִ� ���¿��� 2�ʵڿ� �������� 2�ʰ� ����, �ö�ͼ� 4�ʰ� ���� �ݺ�
        if(first2Wait)
        {
            currTime += Time.deltaTime;
        // 1. ó�� ��ٸ��� Ȱ��ȭ ���̶�� 2�� ��ٸ���. 
            if(currTime > 2)
            {
                // 2. ���� 2�ʰ� �ƴٸ� �������� ��� Ȱ��ȭ �׸��� ��ٸ��� ���� ��Ȱ��ȭ
                first2Wait = false;
                firstMoveDown = true;
            }
        }
        // 3. ���� �������� ����� Ȱ��ȭ �ƴٸ�
        if (firstMoveDown)
        {
            //  -> Ư�� ��ġ���� ��������.
            transform.position += Vector3.down * 5 * Time.deltaTime;
            //  -> ���� Ư�� ��ġ���� �������ٸ�
            if (transform.position.y < -2)
            {
                //     -> �ö󰡴� ��� Ȱ��ȭ
                firstMoveUp = true;
                //     -> �������� ��� ��Ȱ��ȭ
                firstMoveDown = false;
            }
        }
        // 4. ���� �ö󰡴� ����� Ȱ��ȭ �ƴٸ�
        //  -> Ư�� ��ġ���� �ö󰣴�.
        //  -> ���� Ư�� ��ġ���� �ö󰬴ٸ�
        //  -> 4�ʰ� ��ٸ���.


        switch(state)
        {
            case WaterState.FirstWait:
                FirstWait();
                break;
            case WaterState.FirstMoveDown:
                FirstMoveDown();
                break;
            case WaterState.FirstMoveDownWait:
                FirstMoveDownWait();
                break;
            case WaterState.MoveUp:
                MoveUp();
                break;
            case WaterState.MoveUpWait:
                MoveUpWait();
                break;
        }
        // 1. ó�� ���¸� 2�� ��ٸ��� ���·� ����
        //  -> 2�� ��ٸ��� ���� ó��
        //  -> �������� ������ ���¸� �������� 2�� ��ٸ��� ���·� ��ȯ
        // 2. �������� 2�� ��ٸ��� ���¿�����
        //  -> ��ٸ��� ���ó��
        //  -> 2�� ������ ���¸� �ö󰡴� ���·� ��ȯ
        // 3. �ö󰡴� ���¿�����
        //  -> �ö󰡴� ��� ó��
        //  -> �ö󰡱� ������ ���¸� 4�� ��ٸ��� ���·� ��ȯ
        // 4. 4�� ��ٸ��� ���¿�����
        //  -> 4�� ��ٸ��� ó��
        //  -> 
    }


    private void FirstWait()
    {
        currTime += Time.deltaTime;
        // 1. ó�� ��ٸ��� Ȱ��ȭ ���̶�� 2�� ��ٸ���. 
        if (currTime > 2)
        {
            currTime = 0;
            state = WaterState.FirstMoveDown;
        }
    }

        // 2. �������� 2�� ��ٸ��� ���¿�����
    private void FirstMoveDown()
    {
        //  -> Ư�� ��ġ���� ��������.
        transform.position += Vector3.down * 5 * Time.deltaTime;
        //  -> ���� Ư�� ��ġ���� �������ٸ�
        if (transform.position.y < -2)
        {
            //     -> �ö󰡴� ��� Ȱ��ȭ
            //     -> �������� ��� ��Ȱ��ȭ
            state = WaterState.MoveUp;
        }
    }

    //4�� ��ٸ� ���� 
    private void FirstMoveDownWait()
    {
        throw new NotImplementedException();
    }

    private void MoveUp()
    {
        throw new NotImplementedException();
    }

    private void MoveUpWait()
    {
        throw new NotImplementedException();
    }


}
