using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class PurchaseUI : MonoBehaviourPun
{
    public TextMeshProUGUI LandName;
    public TextMeshProUGUI chargeText;
    public int charge=0;
    public int landCharge;
    public int landCount;
    public bool land;
    public int tear1Charge;
    public int tear1Count;
    public bool tear1;
    public int tear2Charge;
    public int tear2Count;
    public bool tear2;
    public int tear3Charge;
    public int tear3Count;
    public bool tear3;
    public GameObject player;
    public GameObject currentBlock;
    public Toggle landTog;
    public Toggle tear1Tog;
    public Toggle tear2Tog;
    public Toggle tear3Tog;
    public Button purchaseBtn;
    // Start is called before the first frame update
    void Start()
    {
        
        landTog.onValueChanged.AddListener(landToggle);
        tear1Tog.onValueChanged.AddListener(tear1Toggle);
        tear2Tog.onValueChanged.AddListener(tear2Toggle);
        tear3Tog.onValueChanged.AddListener(tear3Toggle);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            charge = landCharge * landCount + tear1Charge * tear1Count + tear2Charge * tear2Count + tear3Charge * tear3Count;
            chargeText.text = charge.ToString();
            LandName.text = currentBlock.name;
            if (!landTog.isOn && !tear1Tog.isOn && !tear2Tog.isOn && !tear3Tog.isOn)
            {
                purchaseBtn.interactable = false;
            }
            else
            {
                purchaseBtn.interactable = true;
            }
        }
    }

    public void Interactable(GameObject blockInfo, GameObject playerInfo)
    {
        
        landTog.interactable = false;
        landTog.isOn = true;
        land = true;

        // 해당 블록에 1번 건물이 지어져 있고, 플레이어가 땅 + 1번 건물 가격보다 돈이 없다면
        BasicBlock block = blockInfo.GetComponent<BasicBlock>();
        if (block.HasMoney(1, playerInfo.GetComponent<Player>()))
        {
            tear1Tog.interactable = false;
            tear1Tog.isOn = false;
            tear1 = false;
        }
        else
        {
            tear1Tog.interactable = true;
            tear1Tog.isOn = true;
            tear1 = true;
        }
        if (block.HasMoney(2, playerInfo.GetComponent<Player>()))
        {
            tear2Tog.interactable = false;
            tear2Tog.isOn = false;
            tear2 = false;
        }
        else
        {
            tear2Tog.interactable = true;
            tear2Tog.isOn = true;
            tear2 = true;
        }
        if (block.HasMoney(3, playerInfo.GetComponent<Player>()))
        {
            tear3Tog.interactable = false;
            tear3Tog.isOn = false;
            tear3 = false;
        }
        else
        {
            tear3Tog.interactable = true;
            tear3Tog.isOn = true;
            tear3 = true;   
        }
    }
    public void process(GameObject blockInfo, GameObject playerInfo)
    {
        Interactable(blockInfo, playerInfo);
        landCharge = blockInfo.GetComponent<BasicBlock>().landPrice;
        tear1Charge = blockInfo.GetComponent<BasicBlock>().tear1Price;
        tear2Charge = blockInfo.GetComponent<BasicBlock>().tear2Price;
        tear3Charge = blockInfo.GetComponent<BasicBlock>().tear3Price;
        land = blockInfo.GetComponent<BasicBlock>().land;
        tear1 = blockInfo.GetComponent<BasicBlock>().tear1;
        tear2 = blockInfo.GetComponent<BasicBlock>().tear2;
        tear3 = blockInfo.GetComponent<BasicBlock>().tear3;
        player = playerInfo;
        currentBlock = blockInfo;
    }
    public void landToggle(bool isOn)
    {
        if(isOn)
        {
            landCount = 1;
            land = true;
        }
        else
        {
            landCount = 0;
            land = false;
        }
    }
    public void tear1Toggle(bool isOn)
    {
        if(isOn)
        {
            tear1Count = 1;
            tear1 = true;
        }
        else
        {
            tear1Count = 0;
            tear1 = false;
        }
    }
    public void tear2Toggle(bool isOn)
    {
        if(isOn)
        {
            tear2Count = 1;
            tear2 = true;
        }
        else
        {
            tear2Count = 0;
            tear2 = false;
        }
    }
    public void tear3Toggle(bool isOn)
    {
        if(isOn)
        {
            tear3Count = 1;
            tear3 = true;
        }
        else
        {
            tear3Count = 0;
            tear3 = false;
        }
    }
    public void OnPurchaseBtn()
    {
        //photonView.RPC("RPCOnPurchaseBtn", RpcTarget.All, currentBlock, player,gameObject);
        currentBlock.GetComponent<BasicBlock>().LandOwner = player;

        //currentBlock.GetComponent<BasicBlock>().land = land;
        //currentBlock.GetComponent<BasicBlock>().tear1 = tear1;
        //currentBlock.GetComponent<BasicBlock>().tear2 = tear2;
        //currentBlock.GetComponent<BasicBlock>().tear3 = tear3;
        if (landTog.isOn)
        {
            //currentBlock.GetComponent<BasicBlock>().landFlagFactory.SetActive(true);
            currentBlock.GetComponent<BasicBlock>().landCount = 1;
            currentBlock.GetComponent<BasicBlock>().land = true;
        }
        if (tear1Tog.isOn)
        {
            currentBlock.GetComponent<BasicBlock>().tear1Factory.SetActive(true);

            currentBlock.GetComponent<BasicBlock>().tear1Count = 1;
            currentBlock.GetComponent<BasicBlock>().tear1 = true;
        }
        if (tear2Tog.isOn)
        {
            currentBlock.GetComponent<BasicBlock>().tear2Factory.SetActive(true);
            currentBlock.GetComponent<BasicBlock>().tear2Count = 1;
            currentBlock.GetComponent<BasicBlock>().tear2 = true;
        }
        if (tear3Tog.isOn)
        {
            currentBlock.GetComponent<BasicBlock>().tear3Factory.SetActive(true);
            currentBlock.GetComponent<BasicBlock>().tear3Count = 1;
            currentBlock.GetComponent<BasicBlock>().tear3 = true;
        }
        player.GetComponent<Player>().money -= charge;
        player.GetComponent<Player>().TurnCheck();
        gameObject.SetActive(false);
        //player.GetComponent<Player>().state = Player.PlayerState.End;
    }
    //[PunRPC]
    //public void RPCOnPurchaseBtn(GameObject currentBlock, GameObject player, GameObject gameObject)
    //{
       
    //}

    public void OnCancelBtn()
    {
        gameObject.SetActive(false);
        player.GetComponent<Player>().TurnCheck();// = false;
        //player.GetComponent<Player>().state = Player.PlayerState.End;
    }
}
