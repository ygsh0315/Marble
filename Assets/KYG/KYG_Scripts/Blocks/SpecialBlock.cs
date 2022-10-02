using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class SpecialBlock : Block
{
    public TextMeshProUGUI landName;
    public TextMeshProUGUI landMagText;
    //public TextMeshProUGUI landPriceText;
    public int landMag = 1;
    //관광지
    public GameObject tourOne;
    public GameObject tourTwo;
    public GameObject tourThree;
    public GameObject tourFour;
    List<GameObject> tourBlocks = new List<GameObject>();
    
    GameObject tourO;
    GameObject tourT;
    GameObject tourTh;
    GameObject tourF;
    //통행료
    public int charge = 60000;
    public int chargeTwo = 0;
    public int chargeThree = 0;


    //땅표시 공장
    public GameObject landFlagFactory;


    //땅 건설 가격
    public int landPrice;

    public int landCount = 0;

    public bool land = false;
    //통행료
    public int lantalFee = 0;



    public int chargeTwoCount = 0;

    public bool charge2 = false;

    public int chargeThreeCount = 0;

    public bool charge3 = false;

    public bool festival = false;
    public int festivalCount = 0;



    // Start is called before the first frame update
    void Start()
    {
        landName.text = gameObject.name;
        //landPriceText.text = (landPrice/10000).ToString() + " 만";
        tourBlocks.Add(tourOne);
        tourBlocks.Add(tourTwo);
        tourBlocks.Add(tourThree);
        for (int i = 0; i < tourBlocks.Count; i++)
        {
            if (gameObject.name == tourBlocks[i].name)
            {
                tourBlocks.RemoveAt(i);
            }
        }
        tourO = tourOne.GetComponent<SpecialBlock>().LandOwner;
        tourT = tourTwo.GetComponent<SpecialBlock>().LandOwner;
        tourTh = tourThree.GetComponent<SpecialBlock>().LandOwner;
        tourF = tourFour.GetComponent<SpecialBlock>().LandOwner;
    }

    // Update is called once per frame
    void Update()
    {
        FestivalCount();
        if (landMag >= 2)
        {
            landMagText.text = "X " + landMag;
        }
        else if (!LandOwner)
        {
            landMagText.text = "";
        }
        else
        {
            landMagText.text = (charge / 10000).ToString() + " 만";
        }
        if (festival == false)
        {
            landMag = 1;
        }
    }
    public void FestivalCount()
    {
        if (festivalCount > 12)
        {         
            festivalCount = 0;
            festival = false;
        }
    }
    public void OnSpecialBlock(GameObject player)
    {
        print("SpecialBlock");
        if (!LandOwner)
        {
            if (player.GetComponent<Player>().money >= landPrice)
            {
                GameUI.instance.SPurchase(gameObject, player);
            }
            else
            {
                player.GetComponent<Player>().TurnCheck();
            }
        }
        else if (LandOwner != player)
        {
            if (tourO == tourT || tourO == tourTh || tourO == tourF || tourT == tourTh || tourT == tourF || tourTh == tourF)
            {

                if (festival == true)
                {
                    charge *= 4 * landMag;
                }
                else
                {
                charge *= 2 * landMag;

                }
                player.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, -charge);
                LandOwner.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, charge);
                player.GetComponent<Player>().TurnCheck();
            }
            else if (tourO == tourT == tourTh || tourO == tourT == tourF || tourO == tourTh == tourF || tourT == tourTh == tourF)
            {
                if (festival == true)
                {
                    charge *= 6 * landMag;
                }
                else
                {
                charge *= 3 * landMag;

                }
                player.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, -charge);
                LandOwner.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, charge);
                player.GetComponent<Player>().TurnCheck();
            }
            else if (tourO == tourT == tourTh == tourF)
            {
                if(festival == true)
                {
                    charge *= 8 * landMag;
                }
                else
                {
                    charge *= 4 * landMag;

                }

                player.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, -charge);
                LandOwner.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, charge);
                player.GetComponent<Player>().TurnCheck();
            }
            else
            {
                charge *= 1 * landMag;
                player.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, -charge);
                LandOwner.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, charge);
                player.GetComponent<Player>().TurnCheck();
            }
        }
        else
        {
            player.GetComponent<Player>().TurnCheck();
        }
    }
}
