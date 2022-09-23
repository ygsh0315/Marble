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
        //가만히 있는 상태에서 2초뒤에 내려가서 2초간 정지, 올라와서 4초간 정지 반복
        if(first2Wait)
        {
            currTime += Time.deltaTime;
        // 1. 처음 기다리기 활성화 중이라면 2초 기다린다. 
            if(currTime > 2)
            {
                // 2. 만약 2초가 됐다면 내려가는 기능 활성화 그리고 기다리기 정지 비활성화
                first2Wait = false;
                firstMoveDown = true;
            }
        }
        // 3. 만약 내려가는 기능이 활성화 됐다면
        if (firstMoveDown)
        {
            //  -> 특정 위치까지 내려간다.
            transform.position += Vector3.down * 5 * Time.deltaTime;
            //  -> 만약 특정 위치까지 내려갔다면
            if (transform.position.y < -2)
            {
                //     -> 올라가는 기능 활성화
                firstMoveUp = true;
                //     -> 내려가는 기능 비활성화
                firstMoveDown = false;
            }
        }
        // 4. 만약 올라가는 기능이 활성화 됐다면
        //  -> 특정 위치까지 올라간다.
        //  -> 만약 특정 위치까지 올라갔다면
        //  -> 4초간 기다린다.


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
        // 1. 처음 상태를 2초 기다리기 상태로 설정
        //  -> 2초 기다리기 상태 처리
        //  -> 내려가기 끝나면 상태를 내려가서 2초 기다리는 상태로 전환
        // 2. 내려가서 2초 기다리기 상태에서는
        //  -> 기다리기 기능처리
        //  -> 2초 끝나면 상태를 올라가는 상태로 전환
        // 3. 올라가는 상태에서는
        //  -> 올라가는 기능 처리
        //  -> 올라가기 끝나면 상태를 4초 기다리기 상태로 전환
        // 4. 4초 기다리기 상태에서는
        //  -> 4초 기다리기 처리
        //  -> 
    }


    private void FirstWait()
    {
        currTime += Time.deltaTime;
        // 1. 처음 기다리기 활성화 중이라면 2초 기다린다. 
        if (currTime > 2)
        {
            currTime = 0;
            state = WaterState.FirstMoveDown;
        }
    }

        // 2. 내려가서 2초 기다리기 상태에서는
    private void FirstMoveDown()
    {
        //  -> 특정 위치까지 내려간다.
        transform.position += Vector3.down * 5 * Time.deltaTime;
        //  -> 만약 특정 위치까지 내려갔다면
        if (transform.position.y < -2)
        {
            //     -> 올라가는 기능 활성화
            //     -> 내려가는 기능 비활성화
            state = WaterState.MoveUp;
        }
    }

    //4초 기다릴 차례 
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
