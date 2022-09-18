using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBlock : MonoBehaviour
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
        player.GetComponent<Player>().onTurn = false;
    }
}
