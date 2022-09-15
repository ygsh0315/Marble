using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBlock : MonoBehaviour
{
    //땅 주인
    public GameObject LandOwner;
    
    //통행료
    public int charge = 0;

    #region land
    //땅표시 공장
    public GameObject landFlagFactory;

    //땅 건설 가격
    public int landPrice;

    //통행료
    public int landTallFee;
    #endregion

    #region tear1
    //object1공장
    public GameObject tear1Factory;

    //tear1 건설 가격
    public int tear1Price;

    //통행료
    public int tear1TallFee;
    #endregion

    #region tear2
    //object2공장
    public GameObject tear2Factory;

    //tear2 건설 가격
    public int tear2Price;

    //통행료
    public int tear2TallFee;
    #endregion

    #region tear3
    //object3공장
    public GameObject tear3Factory;
    
    //tear3 건설 가격
    public int tear3Price;

    //통행료
    public int tear3TallFee;
    #endregion

    #region landMark
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
