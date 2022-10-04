using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EventBlock : Block
{
    public int specialMoney = 1000000;
    float currentTime;
    public float createTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnEventBlock(Transform player)
    {
        print("EventBlock");
        player.GetComponent<Player>().eventBlock = true;
        player.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, specialMoney);
    }

    //public override void OnBlock(GameObject player)
    //{
    //    print("EventBlock");
    //    player.GetComponent<Player>().TurnCheck();
    //}
    

}
