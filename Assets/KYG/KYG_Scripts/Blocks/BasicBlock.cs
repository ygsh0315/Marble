using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBlock : MonoBehaviour
{
    //땅 주인
    public GameObject LandOwner;
    
    //통행료
    public int charge = 0;

    //인수 비용
    public int takeOverCharge = 0;


    #region land

    public int landCount = 0;

    public bool land = false;
    //땅표시 공장
    public GameObject landFlagFactory;

    //땅 건설 가격
    public int landPrice;

    //통행료
    public int landTallFee;
    #endregion

    #region tear1
    public int tear1Count = 0;

    public bool tear1 = false;
    //object1공장
    public GameObject tear1Factory;

    //tear1 건설 가격
    public int tear1Price;

    //통행료
    public int tear1TallFee;
    #endregion

    #region tear2
    public int tear2Count = 0;

    public bool tear2 = false;
    //object2공장
    public GameObject tear2Factory;

    //tear2 건설 가격
    public int tear2Price;

    //통행료
    public int tear2TallFee;
    #endregion

    #region tear3
    public int tear3Count = 0;

    public bool tear3 = false;
    //object3공장
    public GameObject tear3Factory;
    
    //tear3 건설 가격
    public int tear3Price;

    //통행료
    public int tear3TallFee;
    #endregion

    #region landMark
    public int landMarkCount = 0;

    public bool landMark = false;
    //landMark 공장
    public GameObject landMarkFactory;

    //랜드마크 건설 가격
    public int landMarkPrice;

    //통행료
    public int landMarkTallFee;
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        charge = landTallFee * landCount + tear1TallFee * tear1Count + tear2TallFee * tear2Count + tear3TallFee * tear3Count + landMarkTallFee * landMarkCount;
        takeOverCharge = landPrice * landCount + tear1Price * tear1Count + tear2Price * tear2Count + tear3Price * tear3Count;
    }
    public void OnBasicBlock(GameObject player)
    {
        print("BasicBlock");
        if (!LandOwner)
        {
            if(player.GetComponent<Player>().money>=landTallFee)
            GameUI.instance.Purchase(gameObject, player);
        }
        else if(LandOwner == player)
        {
            if(!landMark)
            {
                GameUI.instance.Purchase(gameObject, player);
            }
        }
        else
        {
            player.GetComponent<Player>().money -= charge;
            if (!landMark)
            {
                if (player.GetComponent<Player>().money >= takeOverCharge)
                {
                    GameUI.instance.TakeOver(gameObject, player);
                }
            }
        }
    }
}
