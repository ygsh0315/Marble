using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class SPurchaseUI : MonoBehaviourPun
{
    public TextMeshProUGUI LandName;
    public TextMeshProUGUI chargeText;
    public int charge=0;
    public int landCharge;
    public int landCount;
    public bool land;
    public int chargeOne;
    public int chargeOneCount = 0;
    public bool charge1 = false;

    public int chargeTwo;
    public int chargeTwoCount = 0;

    public bool charge2 = false;
    public int chargeThree;
    public int chargeThreeCount = 0;

    public bool charge3 = false;

    public GameObject player;
    public GameObject currentBlock;

    public Button purchaseBtn;
    // Start is called before the first frame update
    void Start()
    {
        
     

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            
            charge = landCharge + chargeOne * chargeOneCount + chargeTwo * chargeTwoCount + chargeThree * chargeThreeCount;
            chargeText.text = charge.ToString();
            LandName.text = currentBlock.name;
         
        }
    }

 
    public void process(GameObject blockInfo, GameObject playerInfo)
    {
       
        landCharge = blockInfo.GetComponent<SpecialBlock>().landPrice;      
        land = blockInfo.GetComponent<SpecialBlock>().land;
        chargeTwo = blockInfo.GetComponent<SpecialBlock>().chargeTwo;
        charge2 = blockInfo.GetComponent<SpecialBlock>().charge2;
        chargeThree = blockInfo.GetComponent<SpecialBlock>().chargeThree;
        charge3 = blockInfo.GetComponent<SpecialBlock>().charge3;      
        player = playerInfo;
        currentBlock = blockInfo;
    }


    public void OnPurchaseBtn()
    {
        photonView.RPC("RPCOnPurchaseBtn", RpcTarget.All,
            player.GetComponent<PhotonView>().ViewID,
            charge,
            currentBlock.GetComponent<Block>().blockID);
        player.GetComponent<Player>().TurnCheck();
        gameObject.SetActive(false);
    }
    [PunRPC]
    public void RPCOnPurchaseBtn(int playerViewId, int charge, int blockId)
    {
        GameObject p = GameManager.instance.GetPlayer(playerViewId);
        p.GetComponent<Player>().money -= charge;

        SpecialBlock block = (SpecialBlock)GameManager.instance.GetBlock(blockId);
        block.onPurchase(p);
    }
    public void OnCancelBtn()
    {
        gameObject.SetActive(false);
        player.GetComponent<Player>().TurnCheck();
        //player.GetComponent<Player>().state = Player.PlayerState.End;
    }
}
