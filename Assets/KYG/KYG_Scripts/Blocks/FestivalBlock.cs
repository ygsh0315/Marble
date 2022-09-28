using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FestivalBlock : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnFestivalBlock(Transform player)
    {
        print("FestivalBlock");
        player.GetComponent<Player>().festivalUI = true;
        player.GetComponent<Player>().festival = true;
        player.GetComponent<Player>().TurnCheck();
    }
}
