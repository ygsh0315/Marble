using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    //    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

    //    RaycastHit hitinfo;
    //    if(Physics.Raycast(ray,out hitinfo))
    //    {

    //    }
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseInfo;
        if (Physics.Raycast(mouseRay, out mouseInfo))
        {
            print("����Ű�� ���: " + mouseInfo.transform.name);

        }
        else
        {
            print("����Ű�� ��� ����");
        }
    }
    public void OnTeleportBlock(Transform player)
    {
        print("TeleportBlock");
        player.GetComponent<Player>().TurnCheck();
    }
}
