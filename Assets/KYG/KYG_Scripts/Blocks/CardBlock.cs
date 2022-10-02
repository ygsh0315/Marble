using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBlock : Block
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCardBlock(Transform player)
    {
        print("CardBlock");
        player.GetComponent<Player>().Chance();
        //player.GetComponent<Player>().TurnCheck();
    }
}
