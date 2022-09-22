using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicBlock : MonoBehaviour
{
    public TextMeshProUGUI landName;

    public TextMeshProUGUI landMagText;

    public int landMag = 1;
    //�� ����
    public GameObject LandOwner;
    
    //�����
    public int charge = 0;

    //�μ� ���
    public int takeOverCharge = 0;


    #region land

    public int landCount = 0;

    public bool land = false;
    //��ǥ�� ����
    public GameObject landFlagFactory;

    //�� �Ǽ� ����
    public int landPrice;

    //�����
    public int landTallFee;
    #endregion

    #region tear1
    public int tear1Count = 0;

    public bool tear1 = false;
    //object1����
    public GameObject tear1Factory;

    //tear1 �Ǽ� ����
    public int tear1Price;

    //�����
    public int tear1TallFee;
    #endregion

    #region tear2
    public int tear2Count = 0;

    public bool tear2 = false;
    //object2����
    public GameObject tear2Factory;

    //tear2 �Ǽ� ����
    public int tear2Price;

    //�����
    public int tear2TallFee;
    #endregion

    #region tear3
    public int tear3Count = 0;

    public bool tear3 = false;
    //object3����
    public GameObject tear3Factory;
    
    //tear3 �Ǽ� ����
    public int tear3Price;

    //�����
    public int tear3TallFee;
    #endregion

    #region landMark
    public int landMarkCount = 0;

    public bool landMark = false;
    //landMark ����
    public GameObject landMarkFactory;

    //���帶ũ �Ǽ� ����
    public int landMarkPrice;

    //�����
    public int landMarkTallFee;
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        landName.text = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (landMag >= 2)
        {
            landMagText.text = "X " + landMag;
        }else if (!LandOwner)
        {
            landMagText.text = "";   
        }
        else
        {
            landMagText.text = (charge / 10000).ToString() + " ��";
        }
        charge = (landTallFee * landCount + tear1TallFee * tear1Count + tear2TallFee * tear2Count + tear3TallFee * tear3Count + landMarkTallFee * landMarkCount) * landMag;
        takeOverCharge = (landPrice * landCount + tear1Price * tear1Count + tear2Price * tear2Count + tear3Price * tear3Count + landMarkPrice * landMarkCount) * 2;
    }
    public void OnBasicBlock(GameObject player)
    {
        print("BasicBlock");
        if (!LandOwner)
        {
            if (player.GetComponent<Player>().money >= landPrice)
            {
                GameUI.instance.Purchase(gameObject, player);
            }
            else
            {
                player.GetComponent<Player>().TurnCheck(); 
            }
        }
        else if(LandOwner == player)
        {
            if(!landMark)
            {             
                if(land && tear1 && tear2 && tear3 && player.GetComponent<Player>().money >=landMarkPrice)
                {
                    GameUI.instance.LandMarkPurchase(gameObject, player);
                }
                else if(!tear1 || !tear2 || !tear3)
                {
                    if(!tear1 && player.GetComponent<Player>().money>= tear1Price)
                    {
                        GameUI.instance.Purchase(gameObject, player);
                    }
                    else
                    {
                        player.GetComponent<Player>().TurnCheck();
                    }
                    if (!tear2 && player.GetComponent<Player>().money >= tear2Price)
                    {
                        GameUI.instance.Purchase(gameObject, player);
                    }
                    else
                    {
                        player.GetComponent<Player>().TurnCheck();
                    }
                    if (!tear3 && player.GetComponent<Player>().money >= tear3Price)
                    {
                        GameUI.instance.Purchase(gameObject, player);
                    }
                    else
                    {
                        player.GetComponent<Player>().TurnCheck();
                    }


                }
                else
                {
                    player.GetComponent<Player>().TurnCheck();
                }
            }
            else
            {
                player.GetComponent<Player>().TurnCheck();
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
                else
                {
                    player.GetComponent<Player>().TurnCheck();
                }
            }
            else
            {
                player.GetComponent<Player>().TurnCheck();
            }
        }
    }

    public void OnPurchaseBtn()
    {
        tear1Factory.SetActive(false);
        tear2Factory.SetActive(false);
        tear3Factory.SetActive(false);
        landMarkFactory.SetActive(true);
        landMarkCount = 1;
        landMark = true;
    }

    public bool HasMoney(int type, Player player)
    {
        switch(type)
        {
            case 1:
                return (tear1 || player.money < landPrice + tear1Price);
            case 2:
                return tear2 || player.money <landPrice + tear1Price + tear2Price;
            case 3:
                return tear3 || player.money < landPrice + tear1Price + tear2Price +tear3Price;
        }

        return false;
    }
}
