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
    //��ǥ�� ����
    public GameObject landFlagFactory;

    //�� �Ǽ� ����
    public int landPrice;

    //�����
    public int landTallFee;
    #endregion

    #region tear1
    //object1����
    public GameObject tear1Factory;

    //tear1 �Ǽ� ����
    public int tear1Price;

    //�����
    public int tear1TallFee;
    #endregion

    #region tear2
    //object2����
    public GameObject tear2Factory;

    //tear2 �Ǽ� ����
    public int tear2Price;

    //�����
    public int tear2TallFee;
    #endregion

    #region tear3
    //object3����
    public GameObject tear3Factory;
    
    //tear3 �Ǽ� ����
    public int tear3Price;

    //�����
    public int tear3TallFee;
    #endregion

    #region landMark
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
        
    }
    public void OnBasicBlock(Transform player)
    {
        print("BasicBlock");
        if (!LandOwner)
        {

        }else if(LandOwner == player)
        {

        }
        else
        {

        }
    }
}
