using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class BasicBlock : Block
{
    public TextMeshProUGUI landName;

    public TextMeshProUGUI landMagText;

    public int landMag = 1;

    //통행료
    public int charge = 0;

    //인수 비용
    public int takeOverCharge = 0;

    public int totalLandPrice;
    #region land

    public int landCount = 0;

    public bool land = false;
    //땅표시 공장
    public GameObject landFlagFactory;

    //땅 건설 가격
    public int landPrice;

    //통행료
    public int landTallFee;
    #endregion

    #region tear1
    public int tear1Count = 0;

    public bool tear1 = false;
    //object1공장
    public GameObject tear1Factory;

    //tear1 건설 가격
    public int tear1Price;

    //통행료
    public int tear1TallFee;
    #endregion

    #region tear2
    public int tear2Count = 0;

    public bool tear2 = false;
    //object2공장
    public GameObject tear2Factory;

    //tear2 건설 가격
    public int tear2Price;

    //통행료
    public int tear2TallFee;
    #endregion

    #region tear3
    public int tear3Count = 0;

    public bool tear3 = false;
    //object3공장
    public GameObject tear3Factory;

    //tear3 건설 가격
    public int tear3Price;

    //통행료
    public int tear3TallFee;
    #endregion

    #region landMark
    public int landMarkCount = 0;

    public bool landMark = false;
    //landMark 공장
    public GameObject landMarkFactory;

    //랜드마크 건설 가격
    public int landMarkPrice;

    //통행료
    public int landMarkTallFee;
    #endregion

    public Material[] materials;
    Renderer rbOne;
    Renderer rbTwo;
    Renderer rbThree;
    Renderer landM;
    public bool festival = false;
    public int festivalCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        landName.text = gameObject.name;
        materials = Resources.LoadAll<Material>("Color");
        rbOne = tear1Factory.GetComponent<MeshRenderer>();
        rbTwo = tear2Factory.GetComponent<MeshRenderer>();
        rbThree = tear3Factory.GetComponent<MeshRenderer>();
        landM = landMarkFactory.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FestivalCount();
        ColorCheck();
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
        //if (festival == true)
        //{
        //   landMag *= 2;
        //}

        charge = (landTallFee * landCount + tear1TallFee * tear1Count + tear2TallFee * tear2Count + tear3TallFee * tear3Count + landMarkTallFee * landMarkCount) * landMag;

        takeOverCharge = (landPrice * landCount + tear1Price * tear1Count + tear2Price * tear2Count + tear3Price * tear3Count + landMarkPrice * landMarkCount) * 2;

        totalLandPrice = (landPrice * landCount + tear1Price * tear1Count + tear2Price * tear2Count + tear3Price * tear3Count + landMarkPrice * landMarkCount);

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
            if (player.GetComponent<Player>().shield == true)
            {
                player.GetComponent<Player>().shield = false;
                player.GetComponent<Player>().TurnCheck();
            }
            else
            {
                if (player.GetComponent<Player>().money >= charge || player.GetComponent<Player>().TotalMoney < charge)
                {
                    player.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, -charge);
                    LandOwner.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, charge);
                }
                else if (player.GetComponent<Player>().TotalMoney >= charge)
                {
                    GameUI.instance.SellLandsUI.GetComponent<SellLandsUI>().totalMoney = player.GetComponent<Player>().TotalMoney;
                    GameUI.instance.SellLandsUI.GetComponent<SellLandsUI>().charge = charge;
                    //GameUI.instance.SellLandsUI.GetComponent<SellLandsUI>().SelectLands();
                    GameUI.instance.SellLandsUI.GetComponent<SellLandsUI>().UIOn = true;
                    GameUI.instance.SellLandsUI.SetActive(true);
                    GameUI.instance.SellLandsUI.GetComponent<SellLandsUI>().lackMoney.text = (-(charge - player.GetComponent<Player>().money)).ToString();
                }
                if (!landMark && !GameUI.instance.SellLandsUI.activeSelf)
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
                //else
                //{
                //    player.GetComponent<Player>().TurnCheck();
                //}
            }
        }
    }
    public void FestivalCount()
    {
        if (festivalCount > 12)
        {
            festival = false;
            festivalCount = 0;
        }
    }
    public void ColorCheck()
    {
        if (LandOwner)
        {
            Material mat = LandOwner.GetComponent<Player>().myColor;
            rbOne.material = mat;
            rbTwo.material = mat;
            rbThree.material = mat;
            landM.material = mat;
            //for (int i = 0; i < materials.Length; i++)
            //{
            //    if (LandOwner.gameObject.name == materials[i].name)
            //    {
            //        rbOne.material = materials[i];
            //        rbTwo.material = materials[i];
            //        rbThree.material = materials[i];
            //        landM.material = materials[i];               
            //    }
            //}
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
    public void OnSellBtn()
    {
        LandOwner = null;
        land = false;
        landCount = 0;
        tear1 = false;
        tear1Count = 0;
        tear1Factory.SetActive(false);
        tear2 = false;
        tear2Count = 0;
        tear2Factory.SetActive(false);
        tear3 = false;
        tear3Count = 0;
        tear3Factory.SetActive(false);
        landMark = false;
        landMarkCount = 0;
        landMarkFactory.SetActive(false);
        OutLine.SetActive(false);
        for (int i = 0; i < GameManager.instance.PlayerList.Count; i++)
        {
            if (GameManager.instance.PlayerList[i].GetComponent<Player>().ownLandList.Count != 0)
            {
                for (int j = 0; j < GameManager.instance.PlayerList[i].GetComponent<Player>().ownLandList.Count; j++)
                {

                    if (gameObject.name == GameManager.instance.PlayerList[i].GetComponent<Player>().ownLandList[j].gameObject.name)
                    {
                        GameManager.instance.PlayerList[i].GetComponent<Player>().ownLandList.Remove(GameManager.instance.PlayerList[i].GetComponent<Player>().ownLandList[j]);
                    }
                }

            }
        }
    }

    public bool HasMoney(int type, Player player)
    {
        switch (type)
        {
            case 1:
                return (tear1 || player.money < landPrice + tear1Price);
            case 2:
                return tear2 || player.money < landPrice + tear1Price + tear2Price;
            case 3:
                return tear3 || player.money < landPrice + tear1Price + tear2Price + tear3Price;
        }
        return false;
    }

    public void OnPurchase(GameObject player, bool isLandTog, bool isTear1Tog, bool isTear2Tog, bool isTear3Tog)
    {
        LandOwner = player;

        if (isLandTog)
        {
            landCount = 1;
            land = true;
        }
        if (isTear1Tog)
        {
            tear1Factory.SetActive(true);
            tear1Count = 1;
            tear1 = true;
        }
        if (isTear2Tog)
        {
            tear2Factory.SetActive(true);
            tear2Count = 1;
            tear2 = true;
        }
        if (isTear3Tog)
        {
            tear3Factory.SetActive(true);
            tear3Count = 1;
            tear3 = true;
        }
    }

    public void OnTakeOver(GameObject player)
    {
        LandOwner = player;
    }

    public void OnLandMarkPurchase()
    {
        landMarkFactory.SetActive(true);
        landMarkCount = 1;
        landMark = true;
        tear1Factory.SetActive(false);
        tear2Factory.SetActive(false);
        tear3Factory.SetActive(false);
    }
}
