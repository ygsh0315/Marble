using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBlock : MonoBehaviour
{
    //�� ����
    public GameObject LandOwner;
    
    //�����
    public int charge = 0;

    #region land

    int landCount = 0;

    public bool land = false;
    //��ǥ�� ����
    public GameObject landFlagFactory;

    //�� �Ǽ� ����
    public int landPrice;

    //�����
    public int landTallFee;
    #endregion

    #region tear1
    int tear1Count = 0;

    public bool tear1 = false;
    //object1����
    public GameObject tear1Factory;

    //tear1 �Ǽ� ����
    public int tear1Price;

    //�����
    public int tear1TallFee;
    #endregion

    #region tear2
    int tear2Count = 0;

    public bool tear2 = false;
    //object2����
    public GameObject tear2Factory;

    //tear2 �Ǽ� ����
    public int tear2Price;

    //�����
    public int tear2TallFee;
    #endregion

    #region tear3
    int tear3Count = 0;

    public bool tear3 = false;
    //object3����
    public GameObject tear3Factory;
    
    //tear3 �Ǽ� ����
    public int tear3Price;

    //�����
    public int tear3TallFee;
    #endregion

    #region landMark
    int landMarkCount = 0;

    public bool landMark = false;
    //landMark ����
    public GameObject landMarkFactory;

    //���帶ũ �Ǽ� ����
    public int landMarkPrice;

    //�����
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
    }
    public void OnBasicBlock(Transform player)
    {
        print("BasicBlock");
        if (!LandOwner)
        {
            GameUI.instance.Purchase();
        }
        else if(LandOwner == player)
        {
            if(!landMark)
            {
                GameUI.instance.Purchase();
            }
        }
        else
        {
            player.GetComponent<Player>().money -= charge;
            if (!landMark)
            {
                GameUI.instance.TakeOver();
            }
        }
    }
}
