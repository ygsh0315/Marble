using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TakeOverUI : MonoBehaviour
{
    public GameObject player;
    public GameObject currentBlock;
    public TextMeshProUGUI LandName;
    public TextMeshProUGUI TakeOverChargeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LandName.text = currentBlock.name;
        TakeOverChargeText.text = currentBlock.GetComponent<BasicBlock>().takeOverCharge.ToString();
    }
    public void process(GameObject blockInfo, GameObject playerInfo)
    {
        player = playerInfo;
        currentBlock = blockInfo;
    }
    public void OnTakeOverBtn()
    {
        player.GetComponent<Player>().money -= currentBlock.GetComponent<BasicBlock>().takeOverCharge;
        currentBlock.GetComponent<BasicBlock>().LandOwner = player;
        gameObject.SetActive(false);
        currentBlock.GetComponent<BasicBlock>().OnBasicBlock(player);
    }

    public void OnCancelBtn()
    {
        gameObject.SetActive(false);
        //player.GetComponent<Player>().state = Player.PlayerState.End;
    }

}
