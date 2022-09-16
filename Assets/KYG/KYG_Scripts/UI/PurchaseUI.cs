using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PurchaseUI : MonoBehaviour
{
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
    public Toggle tear2og;
    public Toggle tear3Tog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            charge = landCharge * landCount + tear1Charge * tear1Count + tear2Charge * tear2Count + tear3Charge * tear3Count;
            chargeText.text = charge.ToString();
        }
    }
    public void process(GameObject blockInfo, GameObject playerInfo)
    {
        landCharge = blockInfo.GetComponent<BasicBlock>().landPrice;
        tear1Charge = blockInfo.GetComponent<BasicBlock>().tear1Price;
        tear2Charge = blockInfo.GetComponent<BasicBlock>().tear2Price;
        tear3Charge = blockInfo.GetComponent<BasicBlock>().tear3Price;
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
        currentBlock.GetComponent<BasicBlock>().LandOwner = player;
        currentBlock.GetComponent<BasicBlock>().land = land;
        currentBlock.GetComponent<BasicBlock>().tear1 = tear1;
        currentBlock.GetComponent<BasicBlock>().tear2 = tear2;
        currentBlock.GetComponent<BasicBlock>().tear3 = tear3;
        player.GetComponent<Player>().money -= charge;
        gameObject.SetActive(false);
        player.GetComponent<Player>().state = Player.PlayerState.End;
    }

    public void OnCancelBtn()
    {
        gameObject.SetActive(false);
        player.GetComponent<Player>().state = Player.PlayerState.End;
    }
}
