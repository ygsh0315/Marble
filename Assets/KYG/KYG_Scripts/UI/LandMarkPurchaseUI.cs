using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class LandMarkPurchaseUI : MonoBehaviourPun
{
    public TextMeshProUGUI LandName;
    public TextMeshProUGUI chargeText;
    public GameObject player;
    public GameObject currentBlock;
    public int landMarkPrice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LandName.text = currentBlock.name;
        chargeText.text = currentBlock.GetComponent<BasicBlock>().landMarkPrice.ToString();
    }
    public void process(GameObject blockInfo, GameObject playerInfo)
    {
        player = playerInfo;
        currentBlock = blockInfo;
    }
    public void OnPurchaseBtn()
    {
        landMarkPrice = currentBlock.GetComponent<BasicBlock>().landMarkPrice;
        photonView.RPC("RpcOnPurchaseBtn", RpcTarget.All,
        player.GetComponent<PhotonView>().ViewID,
        landMarkPrice,
        currentBlock.GetComponent<Block>().blockID);
        //currentBlock.GetComponent<BasicBlock>().OnPurchaseBtn();
        //player.GetComponent<Player>().money -= currentBlock.GetComponent<BasicBlock>().landMarkPrice;
        player.GetComponent<Player>().TurnCheck();
        gameObject.SetActive(false);
    }

    [PunRPC]
    public void RpcOnPurchaseBtn(int playerViewId, int landMarkPrice, int blockId)
    {
        GameObject p = GameManager.instance.GetPlayer(playerViewId);
        p.GetComponent<Player>().money -= landMarkPrice;
        BasicBlock block = (BasicBlock)GameManager.instance.GetBlock(blockId);
        block.OnLandMarkPurchase();
    }

    public void OnCancelBtn()
    {
        player.GetComponent<Player>().TurnCheck();
        gameObject.SetActive(false);
        //player.GetComponent<Player>().state = Player.PlayerState.End;
    }
}
