using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlock : MonoBehaviour
{
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
        player.GetComponent<Player>().TurnCheck();
    }
}
