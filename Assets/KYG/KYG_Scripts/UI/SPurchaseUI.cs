using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SPurchaseUI : MonoBehaviour
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
        currentBlock.GetComponent<SpecialBlock>().LandOwner = player;
        
        player.GetComponent<Player>().money -= charge;
        gameObject.SetActive(false);
    }

    public void OnCancelBtn()
    {
        gameObject.SetActive(false);
        //player.GetComponent<Player>().state = Player.PlayerState.End;
    }
}
