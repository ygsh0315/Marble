using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public int landMag = 1;
    public TextMeshProUGUI landName;
    public TextMeshProUGUI landMagText;
    //통행료s
    public int charge = 0;
    //통행료
    public int landTallFee;

    #region land
    public int landCount = 0;

    public bool land = false;
    //땅표시 공장
    public GameObject landFlagFactory;

    //땅 건설 가격
    public int landPrice;


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


    //땅 주인
    public GameObject LandOwner;

    //인수 비용
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
