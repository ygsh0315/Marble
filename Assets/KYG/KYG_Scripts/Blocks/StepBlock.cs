using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBlock : MonoBehaviour
{
    //∞¸±§¡ˆ
    public GameObject stepOne;
    GameObject stepO;
    
    //∂• ¡÷¿Œ
    public GameObject LandOwner;
    
    //≈Î«‡∑·
    public int chargeOne = 0;
    public int chargeTwo = 0;
    public int chargeThree = 0;

    int count = 0;
    public int charge;
    // Start is called before the first frame update
    void Start()
    {
       
        //stepO = stepOne.GetComponent<SpecialBlock>().LandOwner;
       
    }
    
    // Update is called once per frame
    void Update()
    {
        //if (count ==0)
        //{
        //    charge = chargeOne;
        //}
        //else if (count == 1)
        //{
        //    charge = chargeTwo;
        //}
        //else if (count == 2)
        //{
        //    charge = chargeThree;
        //}
    }
    public void OnSpecialBlock(Transform player)
    {
        print("StepBlock");
        //if (!LandOwner)
        //{
        //    GameUI.instance.Purchase();
        //}
        //else if (LandOwner == player)
        //{
        //    count++;
        //}
        //else if (LandOwner != player)
        //{
        //    player.GetComponent<Player>().money -= charge;
        //}

    }
}
