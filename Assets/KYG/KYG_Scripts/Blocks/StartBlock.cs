using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : MonoBehaviour
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
        player.GetComponent<Player>().TurnCheck();
    }
}
