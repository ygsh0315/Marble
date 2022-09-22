using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpecialBlock : MonoBehaviour
{
    public TextMeshProUGUI landName;
    public TextMeshProUGUI landPriceText;
    //°ü±¤Áö
    public GameObject tourOne;
    public GameObject tourTwo;
    public GameObject tourThree;
    public GameObject tourFour;
    List<GameObject> tourBlocks = new List<GameObject>();
    //¶¥ ÁÖÀÎ
    public GameObject LandOwner;
    GameObject tourO;
    GameObject tourT;
    GameObject tourTh;
    GameObject tourF;
    //ÅëÇà·á
    public int charge = 60000;
    public int chargeTwo = 0;
    public int chargeThree = 0;


    //¶¥Ç¥½Ã °øÀå
    public GameObject landFlagFactory;


    //¶¥ °Ç¼³ °¡°Ý
    public int landPrice;

    public int landCount = 0;

    public bool land = false;
    //ÅëÇà·á
    public int lantalFee = 0;



    public int chargeTwoCount = 0;

    public bool charge2 = false;

    public int chargeThreeCount = 0;

    public bool charge3 = false;





    // Start is called before the first frame update
    void Start()
    {
        landName.text = gameObject.name;
        landPriceText.text = (landPrice/10000).ToString() + " ¸¸";
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
            if (tourO == tourT || tourO == tourTh || tourT == tourTh)
            {

                charge *= 2;
                player.GetComponent<Player>().money -= charge;
                LandOwner.GetComponent<Player>().money += charge;
                player.GetComponent<Player>().TurnCheck();
            }
            else if (tourO == tourT == tourTh || tourO == tourT == tourF || tourO == tourTh == tourF || tourT == tourTh == tourF)
            {
                charge *= 3;
                player.GetComponent<Player>().money -= charge;
                LandOwner.GetComponent<Player>().money += charge;
                player.GetComponent<Player>().TurnCheck();
            }
            else if (tourO == tourT == tourTh == tourF)
            {
                charge *= 4;
                player.GetComponent<Player>().money -= charge;
                LandOwner.GetComponent<Player>().money += charge;
                player.GetComponent<Player>().TurnCheck();
            }
            else
            {
                charge *= 1;
                player.GetComponent<Player>().money -= charge;
                LandOwner.GetComponent<Player>().money += charge;
                player.GetComponent<Player>().TurnCheck();
            }
        }
        else
        {
            player.GetComponent<Player>().TurnCheck();
        }
    }
}
