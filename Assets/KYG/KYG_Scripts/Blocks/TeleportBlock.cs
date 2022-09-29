using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBlock : Block
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
      
    }
    public void OnTeleportBlock(Transform player)
    {
        print("TeleportBlock");
        player.GetComponent<Player>().teleportUI = true;
        player.GetComponent<Player>().telePort = true;

        
    }

    //public override void OnBlock(GameObject player)
    //{
    //    print("TeleportBlock");
    //    player.GetComponent<Player>().teleportUI = true;
    //    player.GetComponent<Player>().telePort = true;
    //}


}
