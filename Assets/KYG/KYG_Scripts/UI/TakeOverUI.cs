using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TakeOverUI : MonoBehaviourPun
{
    public GameObject player;
    public GameObject currentBlock;
    public TextMeshProUGUI LandName;
    public TextMeshProUGUI TakeOverChargeText;
    public int takeOverCharge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        takeOverCharge = currentBlock.GetComponent<BasicBlock>().takeOverCharge;
        LandName.text = currentBlock.name;
        TakeOverChargeText.text = takeOverCharge.ToString();
    }
    public void process(GameObject blockInfo, GameObject playerInfo)
    {
        player = playerInfo;
        currentBlock = blockInfo;
    }
    public void OnTakeOverBtn()
    {
        photonView.RPC("RpcOnTakeOverBtn", RpcTarget.All, player.GetComponent<PhotonView>().ViewID, 
            takeOverCharge,
        currentBlock.GetComponent<BasicBlock>().blockID,
        currentBlock.GetComponent<BasicBlock>().LandOwner.GetComponent<PhotonView>().ViewID);
        //player.GetComponent<Player>().money -= currentBlock.GetComponent<BasicBlock>().takeOverCharge;
        //currentBlock.GetComponent<BasicBlock>().LandOwner.GetComponent<Player>().money += currentBlock.GetComponent<BasicBlock>().takeOverCharge;
        //currentBlock.GetComponent<BasicBlock>().LandOwner = player;
       // player.GetComponent<Player>().TakeOverColor();
        gameObject.SetActive(false);
        if (!currentBlock.GetComponent<BasicBlock>().land || !currentBlock.GetComponent<BasicBlock>().tear1 || !currentBlock.GetComponent<BasicBlock>().tear2 || !currentBlock.GetComponent<BasicBlock>().tear3 && player.GetComponent<Player>().money >= currentBlock.GetComponent<BasicBlock>().landTallFee)
        {
            currentBlock.GetComponent<BasicBlock>().OnBasicBlock(player);
        }
        else if (currentBlock.GetComponent<BasicBlock>().land && currentBlock.GetComponent<BasicBlock>().tear1 && currentBlock.GetComponent<BasicBlock>().tear2 && currentBlock.GetComponent<BasicBlock>().tear3 && player.GetComponent<Player>().money >= currentBlock.GetComponent<BasicBlock>().landMarkPrice)
        {
            GameUI.instance.LandMarkPurchase(currentBlock, player);
        }
        else
        {
            player.GetComponent<Player>().TurnCheck();
        }
    }
    [PunRPC]
    public void RpcOnTakeOverBtn(int playerViewId,int takeOverCharge, int blockId, int landOwnerViewId)
    {
        GameObject p = GameManager.instance.GetPlayer(playerViewId);
        BasicBlock block = (BasicBlock)GameManager.instance.GetBlock(blockId);
        GameObject l = GameManager.instance.GetPlayer(landOwnerViewId);

        p.GetComponent<Player>().money -= takeOverCharge;
        l.GetComponent<Player>().money += takeOverCharge;

        block.OnTakeOver(p);
    }
    public void OnCancelBtn()
    {
        player.GetComponent<Player>().TurnCheck();
        gameObject.SetActive(false);
        //player.GetComponent<Player>().state = Player.PlayerState.End;
    }

}
