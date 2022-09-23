using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_WaterColumn : MonoBehaviour
{
    //4초간 up상태, 2초간 down상태 

    //필요속성 : 현재시간, 일정시간 

    float currTime;
    public float downTime = 2;
    
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {

        //1.시간이 흐른다 
        currTime += Time.deltaTime;
        //2.2초가 흐르면
        if(currTime > downTime)
        {
            //3.물기둥이 내려간다
            Vector3 dir = Vector3.down;
            transform.position += dir * 2 * Time.deltaTime;
           
        }

    }
}
