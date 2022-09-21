using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public int landMag = 1;
    public TextMeshProUGUI landName;
    public TextMeshProUGUI landMagText;
    //�����s
    public int charge = 0;
    //�����
    public int landTallFee;

    #region land
    public int landCount = 0;

    public bool land = false;
    //��ǥ�� ����
    public GameObject landFlagFactory;

    //�� �Ǽ� ����
    public int landPrice;


    #endregion

    #region tear1
    public int tear1Count = 0;

    public bool tear1 = false;
    //object1����
    public GameObject tear1Factory;

    //tear1 �Ǽ� ����
    public int tear1Price;

    //�����
    public int tear1TallFee;
    #endregion



    #region tear2
    public int tear2Count = 0;

    public bool tear2 = false;
    //object2����
    public GameObject tear2Factory;

    //tear2 �Ǽ� ����
    public int tear2Price;

    //�����
    public int tear2TallFee;
    #endregion

    #region tear3
    public int tear3Count = 0;

    public bool tear3 = false;
    //object3����
    public GameObject tear3Factory;

    //tear3 �Ǽ� ����
    public int tear3Price;

    //�����
    public int tear3TallFee;
    #endregion

    #region landMark
    public int landMarkCount = 0;

    public bool landMark = false;
    //landMark ����
    public GameObject landMarkFactory;

    //���帶ũ �Ǽ� ����
    public int landMarkPrice;

    //�����
    public int landMarkTallFee;
    #endregion


    //�� ����
    public GameObject LandOwner;

    //�μ� ���
    public int takeOverCharge = 0;

    public virtual void Init()
    {
    }
    public virtual void Do(GameObject player)
    {

    }

    public virtual void Update()
    {

    }
}
