using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBlock : MonoBehaviour
{
    //∞¸±§¡ˆ
    public GameObject tourOne;
    public GameObject tourTwo;
    public GameObject tourThree;
    List<GameObject> tourBlocks = new List<GameObject>();
    //∂• ¡÷¿Œ
    public GameObject LandOwner;
    GameObject tourO;
    GameObject tourT;
    GameObject tourTh;
    //≈Î«‡∑·
    public int chargeOne = 0;
    public int chargeTwo = 0;
    public int chargeThree = 0;

    // Start is called before the first frame update
    void Start()
    {
        //tourBlocks.Add(tourOne);
        //tourBlocks.Add(tourTwo);
        //tourBlocks.Add(tourThree);
        //for (int i = 0; i < tourBlocks.Count; i++)
        //{
        //    if (gameObject.name == tourBlocks[i].name)
        //    {
        //        tourBlocks.RemoveAt(i);
        //    }
        //}
    //     tourO = tourOne.GetComponent<SpecialBlock>().LandOwner;
    //     tourT = tourTwo.GetComponent<SpecialBlock>().LandOwner;
    //     tourTh = tourThree.GetComponent<SpecialBlock>().LandOwner;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSpecialBlock(Transform player)
    {
        print("SpecialBlock");
        //if (!LandOwner)
        //{
        //    GameUI.instance.Purchase();
        //}
        //else if (LandOwner != player)
        //{            
        //    if (tourO == tourT || tourO == tourTh || tourT == tourTh)
        //    {
                
        //        player.GetComponent<Player>().money -= chargeTwo;
        //    }
        //    else if (tourO == tourT == tourTh)
        //    {
        //        player.GetComponent<Player>().money -= chargeThree;
        //    }
        //    else
        //    {
        //        player.GetComponent<Player>().money -= chargeOne;
        //    }
        //}

    }
}
