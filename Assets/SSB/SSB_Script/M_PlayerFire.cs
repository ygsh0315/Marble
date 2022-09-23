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

            //5.만약 반복이 5번 했다면 그만하고 싶다 
            if(i ==4 )
            {
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //사용자가 발사버튼을 누르면 총알을 발사하고 싶다

        //1.사용자가 발사버튼을 눌렀으니까
        if(Input.GetButtonDown("Fire1"))
        {

            //3. 총알을 발사하고 싶다 
           // bullet.transform.position = gun.transform.position;

        }
    }
}
