using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CBasicBlock : Block
{
    public override void Init()    
    {
        landName.text = gameObject.name;
    }
    public override void Update()
    {
        if (landMag >= 2)
        {
            landMagText.text = "X " + landMag;
        }
        charge = (landTallFee * landCount + tear1TallFee * tear1Count + tear2TallFee * tear2Count + tear3TallFee * tear3Count + landMarkTallFee * landMarkCount) * landMag;
        takeOverCharge = (landPrice * landCount + tear1Price * tear1Count + tear2Price * tear2Count + tear3Price * tear3Count + landMarkPrice * landMarkCount) * 2;
    }
    public override void Do(GameObject player)
    {
        Debug.Log("BasicBlock");
        if (!LandOwner)
        {
            if (player.GetComponent<Player>().money >= landPrice)
            {
                GameUI.instance.Purchase(gameObject, player);
            }
            else
            {
                player.GetComponent<Player>().onTurn = false;
            }
        }
        else if (LandOwner == player)
        {
            if (!landMark)
            {
                if (land && tear1 && tear2 && tear3 && player.GetComponent<Player>().money >= landMarkPrice)
                {
                    GameUI.instance.LandMarkPurchase(gameObject, player);
                }
                else if (!tear1 || !tear2 || !tear3)
                {
                    if (!tear1 && player.GetComponent<Player>().money >= tear1Price)
                    {
                        GameUI.instance.Purchase(gameObject, player);
                    }
                    else
                    {
                        player.GetComponent<Player>().onTurn = false;
                    }
                    if (!tear2 && player.GetComponent<Player>().money >= tear2Price)
                    {
                        GameUI.instance.Purchase(gameObject, player);
                    }
                    else
                    {
                        player.GetComponent<Player>().onTurn = false;
                    }
                    if (!tear3 && player.GetComponent<Player>().money >= tear3Price)
                    {
                        GameUI.instance.Purchase(gameObject, player);
                    }
                    else
                    {
                        player.GetComponent<Player>().onTurn = false;
                    }


                }
                else
                {
                    player.GetComponent<Player>().onTurn = false;
                }
            }
            else
            {
                player.GetComponent<Player>().onTurn = false;
            }
        }
        else
        {
            player.GetComponent<Player>().money -= charge;
            LandOwner.GetComponent<Player>().money += charge;
            if (!landMark)
            {
                if (player.GetComponent<Player>().money >= takeOverCharge)
                {
                    GameUI.instance.TakeOver(gameObject, player);
                }
            }
            player.GetComponent<Player>().onTurn = false;
        }
    }

}
