using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_WaterColumn : MonoBehaviour
{
    //4초간 up상태, 2초간 down상태 

    //필요속성 : 현재시간, 일정시간 

    float currTime;
    public float downTime = 2;
    public float upTime = 2;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //1.시간이 흐른다 
        currTime += Time.deltaTime;
        //2.2초가 흐르면
        // 만약 경과시간 downTime 보다 작다면
        // 2초동안 물기둥이 처음에 내려가고
        if (currTime < downTime)
        {
            //3.물기둥이 내려간다
            Vector3 dir = -transform.up;
            dir.Normalize();
            transform.position += dir * 2 * Time.deltaTime;

        }
        //4.현재시간이 upTime을 초과하면  
        // 물기둥이 2초동안 내라가고, 다시 4초가 지나면 물기둥이 올라가도록 하고 싶다.
        else if (currTime >= downTime && currTime < (downTime + upTime))
        {
            //5.물기둥이 올라간다 
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
