using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTrapBlock(GameObject player)
    {
        print("TrapBlock");
        player.GetComponent<Player>().isTraped = true;
        player.GetComponent<Player>().trapCount++;
        player.GetComponent<Player>().TurnCheck();
        //GameUI.instance.TrapBlockUI.GetComponent<TrapBlockUI>().process(player);
        
    }
}
