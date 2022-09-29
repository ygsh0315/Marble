using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : Block
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStartBlock(Transform player)
    {
        print("StartBlock");
        player.GetComponent<Player>().startB = true;
        player.GetComponent<Player>().startUI = true;
        //player.GetComponent<Player>().TurnCheck();
    }

    //public override void OnBlock(GameObject player)
    //{
    //    print("StartBlock");
    //    player.GetComponent<Player>().startB = true;
    //    player.GetComponent<Player>().startUI = true;
    //}
}
