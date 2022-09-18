using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBlock : MonoBehaviour
{
    //������
    public GameObject tourOne;
    public GameObject tourTwo;
    public GameObject tourThree;
    List<GameObject> tourBlocks = new List<GameObject>();
    //�� ����
    public GameObject LandOwner;
    GameObject tourO;
    GameObject tourT;
    GameObject tourTh;
    //�����
    public int charge = 0;
    public int chargeTwo = 0;
    public int chargeThree = 0;


    //��ǥ�� ����
    public GameObject landFlagFactory;

    
    //�� �Ǽ� ����
    public int landPrice;

    public int landCount = 0;

    public bool land = false;
    //�����
    public int lantalFee = 0;

    

    public int chargeTwoCount = 0;

    public bool charge2 = false;

    public int chargeThreeCount = 0;

    public bool charge3 = false;




    
    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        charge = lantalFee * landCount + chargeTwo * chargeTwoCount + chargeThree * chargeThreeCount;
    }
    public void OnSpecialBlock(GameObject player)
    {
        print("SpecialBlock");
        if (!LandOwner)
        {
            GameUI.instance.SPurchase(gameObject, player);
        }
        else if (LandOwner != player)
        {            
            if (tourO == tourT || tourO == tourTh || tourT == tourTh)
            {
                charge = chargeTwo;
                player.GetComponent<Player>().money -= chargeTwo;

            }
            else if (tourO == tourT == tourTh)
            {
                charge = chargeThree; 
                player.GetComponent<Player>().money -= chargeThree;
            }
            else
            {
                charge = lantalFee ;
                player.GetComponent<Player>().money -= lantalFee;
            }
        }

    }
}
