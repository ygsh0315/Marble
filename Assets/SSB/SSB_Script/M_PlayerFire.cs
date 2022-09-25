using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerFire : MonoBehaviour
{
    //총알공장
    public GameObject bulletFactory;
    //총구
    public Transform gun;
    //탄창 -> 콜렉션  
    GameObject[] bulletPool;
    //탄창에 들어갈 총알 개수 
    public int bulletPoolSize = 5;

    void Start()
    {
        
        //태어났을 때 탄창과 총알을 미리 생성해서 넣고 싶다 
        //1. 탄창이 필요 
        bulletPool = new GameObject[bulletPoolSize];
        for (int i = 0; i < bulletPoolSize; i++)
        {            
            //2. 총알이 있어야 한다 
            GameObject bullet = Instantiate(bulletFactory);

            //3.탄창에 총알을 넣고 싶다 
            bulletPool[i] = bullet;

            //4.탄창에 있을때는 총알을 비활성화 시키고 싶다 
            bullet.SetActive(false);

           
        }
    }

    // Update is called once per frame
    void Update()
    {
        //사용자가 발사버튼을 누르면 총알을 발사하고 싶다

        //1.사용자가 발사버튼을 눌렀으니까
        if(Input.GetButtonDown("Fire1"))
        {   //탄창에서 비활성화 되어있는 총알을 꺼내고 싶다 
            //반복적으로 탄창에서 총알을 하나씩 꺼내야 한다 
            for (int i = 0; i < bulletPoolSize; i++)
            {

            }
            //탄창에서 꺼낸 총알을 꺼낸다.
            GameObject bullet = bulletPool[0];
            //만약 탄창에서 꺼낸 총알이 비활성화 되어있다면
            if(bulletFactory.activeSelf == false) 
            {
                bullet.SetActive(true);
                //3. 총알을 발사하고 싶다 
                bullet.transform.position = gun.transform.position;
                //그만 찾고싶다
              
            }
       

        }
    }
}
