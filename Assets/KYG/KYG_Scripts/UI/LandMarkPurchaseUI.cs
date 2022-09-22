using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LandMarkPurchaseUI : MonoBehaviour
{
    public TextMeshProUGUI LandName;
    public TextMeshProUGUI chargeText;
    public GameObject player;
    public GameObject currentBlock;
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
        currentBlock.GetComponent<BasicBlock>().OnPurchaseBtn();
        player.GetComponent<Player>().money -= currentBlock.GetComponent<BasicBlock>().landMarkPrice;
        player.GetComponent<Player>().TurnCheck();
        gameObject.SetActive(false);
    }

    public void OnCancelBtn()
    {
        player.GetComponent<Player>().TurnCheck();
        gameObject.SetActive(false);
        //player.GetComponent<Player>().state = Player.PlayerState.End;
    }
}
