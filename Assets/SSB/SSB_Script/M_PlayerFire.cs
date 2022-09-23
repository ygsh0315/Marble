using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerFire : MonoBehaviour
{
    //�Ѿ˰���
    public GameObject bulletFactory;
    //�ѱ�
    public Transform gun;
    //źâ -> �ݷ���  
    GameObject[] bulletPool;
    //źâ�� �� �Ѿ� ���� 
    public int bulletPoolSize = 5;

    void Start()
    {
        
        //�¾�� �� źâ�� �Ѿ��� �̸� �����ؼ� �ְ� �ʹ� 
        //1. źâ�� �ʿ� 
        bulletPool = new GameObject[bulletPoolSize];
        for (int i = 0; i < bulletPoolSize; i++)
        {            
            //2. �Ѿ��� �־�� �Ѵ� 
            GameObject bullet = Instantiate(bulletFactory);

            //3.źâ�� �Ѿ��� �ְ� �ʹ� 
            bulletPool[i] = bullet;

            //4.źâ�� �������� �Ѿ��� ��Ȱ��ȭ ��Ű�� �ʹ� 
            bullet.SetActive(false);

            //5.���� �ݺ��� 5�� �ߴٸ� �׸��ϰ� �ʹ� 
            if(i ==4 )
            {
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //����ڰ� �߻��ư�� ������ �Ѿ��� �߻��ϰ� �ʹ�

        //1.����ڰ� �߻��ư�� �������ϱ�
        if(Input.GetButtonDown("Fire1"))
        {

            //3. �Ѿ��� �߻��ϰ� �ʹ� 
           // bullet.transform.position = gun.transform.position;

        }
    }
}
