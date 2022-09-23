using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WASD 앞뒤좌우로 움직이게 한다
//필요속성 : 이동속도 

//Stage밖으로 벗어나면 죽는다
//필요속성 : Stage


public class M_PlayerMove : MonoBehaviour
{
    //이동속도
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
        //방향을 구한다 
        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir.Normalize();
        //움직이게 한다
        transform.position += dir * speed * Time.deltaTime;
    }

    

    public void Die()
    {
        dieDistance = Vector3.Distance(stage.position, transform.position);
        //만약 스테이지를 벗어나게되면
        if( dieDistance >5f)
        {
            //죽음처리한다
            Destroy(gameObject, 2);
        }

    }
}
