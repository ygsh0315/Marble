using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player : MonoBehaviourPun
{
    public int round = 1;
    public int currentMapIndex = 0;
    public int money;
    public int TotalMoney;
    public int playerRank;
    public int salary = 300000;
    public int sameDiceCount = 0;
    public bool sameDice = false;
    public GameObject RollDiceBtn;
    public List<GameObject> ownLandList = new List<GameObject>();
    public List<GameObject> lines = new List<GameObject>();
    public List<GameObject> lineOne = new List<GameObject>();
    public List<GameObject> lineTwo = new List<GameObject>();
    public List<GameObject> lineThree = new List<GameObject>();
    public List<GameObject> lineFour = new List<GameObject>();
    public List<GameObject> specialBlocks = new List<GameObject>();
    public List<GameObject> color1 = new List<GameObject>();
    public List<GameObject> color2 = new List<GameObject>();
    public List<GameObject> color3 = new List<GameObject>();
    public List<GameObject> color4 = new List<GameObject>();
    public List<GameObject> color5 = new List<GameObject>();
    public List<GameObject> color6 = new List<GameObject>();
    public List<GameObject> color7 = new List<GameObject>();
    public List<GameObject> color8 = new List<GameObject>();
    public List<GameObject> colorPool = new List<GameObject>();
    public List<GameObject> lineOnePool = new List<GameObject>();
    public List<GameObject> lineTwoPool = new List<GameObject>();
    public List<GameObject> lineThreePool = new List<GameObject>();
    public List<GameObject> lineFourPool = new List<GameObject>();
    public List<GameObject> start = new List<GameObject>();
    public List<GameObject> startLand = new List<GameObject>();
    public bool line = true;
    public bool special = true;
    public bool onTurn = false;
    public bool hasInfo = false;
    public bool isTraped = false;
    public bool telePort = false;
    public bool bankrupt = false;
    public bool festival = false;
    public bool festivalUI = false;
    public bool trapUI = false;
    public bool teleportUI = false;
    public bool startUI = false;
    public bool chance1 = false;
    public bool chance2 = false;
    public bool chance3 = false;
    public bool chance4 = false;
    public bool chance5 = false;
    public bool chance6 = false;
    public bool startB = false;
    public bool shield = false;
    public bool startCount = false;
    public int festivaCount = 0;
    public int telePortCount = 0;
    public int charge = 1000;
    public bool eventBlock = false;
    bool one = true;
    bool two = true;
    bool three = true;
    bool four = true;
    bool five = true;
    bool six = true;
    bool seven = true;
    bool eight = true;
    bool bone = true;
    bool btwo = true;
    bool bthree = true;
    bool bfour = true;
    bool bfive = true;
    bool bsix = true;
    bool bseven = true;
    bool beight = true;

    public int trapCount = 0;
    public int colorCount = 0;
    public int[] chanceCard = { 1, 2, 3, 4, 5, 6 };
    public Material myColor;
    public GameObject model;
    public enum PlayerState
    {
        Idle,
        Move,
        Turn,
        End
    }

    public PlayerState state = PlayerState.Idle;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.AddPlayer(gameObject);
        money = GameManager.instance.startMoney;
        lines.AddRange(GameObject.FindGameObjectsWithTag("BasicBlock"));
        lines.AddRange(GameObject.FindGameObjectsWithTag("SpecialBlock"));
        specialBlocks.AddRange(GameObject.FindGameObjectsWithTag("SpecialBlock"));
        color1.Add(GameObject.Find("???? ????"));
        color1.Add(GameObject.Find("???? ????"));
        color2.Add(GameObject.Find("???? ????"));
        color2.Add(GameObject.Find("???? ????"));
        color3.Add(GameObject.Find("???? ????"));
        color3.Add(GameObject.Find("???? ????"));
        color4.Add(GameObject.Find("???? ????"));
        color4.Add(GameObject.Find("?????? ????"));
        color5.Add(GameObject.Find("???? ????"));
        color5.Add(GameObject.Find("???? ????"));
        color6.Add(GameObject.Find("???? ????"));
        color6.Add(GameObject.Find("???? ????"));
        color7.Add(GameObject.Find("???????? ????"));
        color7.Add(GameObject.Find("?????? ????"));
        color8.Add(GameObject.Find("???? ????"));
        color8.Add(GameObject.Find("???? ????"));
        Line();
        RollDiceBtn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (festivalUI == true)
        {
            GameUI.instance.FestivalUI.SetActive(true);
            FestivalUI();
        }
        if (trapUI == true)
        {
            GameUI.instance.TrapBlockUI.SetActive(true);
            TrapUI();
        }
        if (teleportUI == true)
        {
            GameUI.instance.TeleportUI.SetActive(true);
            TeleportUI();
        }
        if (startUI == true)
        {
            GameUI.instance.StartUI.SetActive(true);
            StartUI();
        }
        if (chance1 == true)
        {
            GameUI.instance.Chance1UI.SetActive(true);
            Chance1UI();
        }
        if (chance2 == true)
        {
            GameUI.instance.Chance2UI.SetActive(true);
            Chance2UI();
        }
        if (chance3 == true)
        {
            GameUI.instance.Chance3UI.SetActive(true);
            Chance3UI();
        }
        if (chance4 == true)
        {
            GameUI.instance.Chance4UI.SetActive(true);
            Chance4UI();
        }
        if (chance5 == true)
        {
            GameUI.instance.Chance5UI.SetActive(true);
            Chance5UI();
        }
        if (chance6 == true)
        {
            GameUI.instance.Chance6UI.SetActive(true);
            Chance6UI();
        }
        if(eventBlock == true)
        {
            print("??????");
            GameUI.instance.EventBlockUI.SetActive(true);
            EventBlockUI();
        }
        //if(startCount == true)
        //{
        //    StartTurn();
        //}

        //print(gameObject.name);
        //TrapCheck();
        StateMachine();
        StartBonus();
        Bankrupt();
        OwnLandCheck();
        TrapCount();
        TotalMoney = CalculateTotalMoney();
        Click();
        ClickTurnCheck();
        CheckLine();
        ColorCheck();
        ColorCount();


    }
    float startCurrentTime = 0;
    float startCreateTime = 6;
    public void StartTurn()
    {
        startCurrentTime += Time.deltaTime;
        if(startCurrentTime > startCreateTime)
        {
            TurnCheck();
        }
    }
    private void TeleportUI()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            GameUI.instance.TeleportUI.SetActive(false);
            //TurnCheck();
        }
    }
    public void StartUI()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            GameUI.instance.StartUI.SetActive(false);
            //TurnCheck();
        }
    }

    public void TrapUI()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            GameUI.instance.TrapBlockUI.SetActive(false);
            TurnCheck();
        }
    }


    public void FestivalUI()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            GameUI.instance.FestivalUI.SetActive(false);
            TurnCheck();
        }
    }
    //public void TrapUI()
    //{
    //    if (GameManager.instance.currentTurnPlayer.GetComponent<Player>().isTraped == true)
    //    {
    //        GameUI.instance.TrapBlockUI.SetActive(true);
    //        currentTime += Time.deltaTime;
    //        if (currentTime > createTime)
    //        {

    //        }

    //    }
    //}
    public void StartBonusList()
    {
        for (int i =0; i< ownLandList.Count; i++)
        {
            if (ownLandList[i].tag == "BasicBlock" && ownLandList[i].GetComponent<BasicBlock>().landMark == false)
            {
                start.Add(ownLandList[i]);

            }
            else if (ownLandList[i].tag == "BasicBlock" && ownLandList[i].GetComponent<BasicBlock>().landMark == true)
            {
                startLand.Add(ownLandList[i]);
            }
        }
    }
    public void StartBonus()
    {
        StartBonusList();
        startCount = true;
        if (startB == true)
        {
            print(9);
            print(ownLandList.Count);
            if (ownLandList.Count <= 0)
            {
                TurnCheck();
                startB = false;
                return;
            }
            else
            {
                if (start.Count ==0 && startLand.Count > 0)
                {
                    //?????? ???? ?????????? ????????
                    startB = false;
                    print(4);
                    TurnCheck();
                }
                else if (start.Count >0 || startLand.Count >0)
                {
                    //?????? ???????? ???? ????????
                    print(3);
                    Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit mouseInfo;
                    if (Physics.Raycast(mouseRay, out mouseInfo))
                    {
                        print("???????? ????: " + mouseInfo.transform.name);
                    }
                    else
                    {
                        print("???????? ???? ????");
                    }

                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (mouseInfo.transform)
                        {

                            GameObject startBonus = GameObject.Find(mouseInfo.transform.name);
                        }
                        for (int j = 0; j < ownLandList.Count; j++)
                        {
                            if (ownLandList[j].gameObject.name == mouseInfo.transform.name)
                            {
                                if (ownLandList[j].GetComponent<BasicBlock>())
                                {
                                    ownLandList[j].GetComponent<BasicBlock>().OnBasicBlock(gameObject);
                                    startB = false;
                                }

                            }
                        }
                    }
                }
                //for (int i = 0; i < ownLandList.Count; i++)
                //{
                //    print(2);
                //    if (ownLandList[i].tag == "BasicBlock" && ownLandList[i].GetComponent<BasicBlock>().landMark == false)
                //    {
                        
                //        print(3);
                //        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                //        RaycastHit mouseInfo;
                //        if (Physics.Raycast(mouseRay, out mouseInfo))
                //        {
                //            print("???????? ????: " + mouseInfo.transform.name);
                //        }
                //        else
                //        {
                //            print("???????? ???? ????");
                //        }

                //        if (Input.GetButtonDown("Fire1"))
                //        {
                //            if (mouseInfo.transform)
                //            {

                //                GameObject startBonus = GameObject.Find(mouseInfo.transform.name);
                //            }
                //            for (int j = 0; j < ownLandList.Count; j++)
                //            {
                //                if (ownLandList[j].gameObject.name == mouseInfo.transform.name)
                //                {
                //                    if (ownLandList[j].GetComponent<BasicBlock>())
                //                    {
                //                        ownLandList[j].GetComponent<BasicBlock>().OnBasicBlock(gameObject);
                //                        startB = false;
                //                    }

                //                }
                //            }
                //        }

                //    }
                //    //else
                //    //{ 
                //    //    startB = false;
                //    //print(4);
                //    //   TurnCheck();
                //    //}


                //}





            }

        }
    }

    public void Festival()
    {

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseInfo;
        if (Physics.Raycast(mouseRay, out mouseInfo))
        {
            print("???????? ????: " + mouseInfo.transform.name);

        }
        else
        {
            print("???????? ???? ????");
        }
        if (Input.GetButtonDown("Fire1"))
        {

            GameObject festivall = GameObject.Find(mouseInfo.transform.name);
            for (int i = 0; i < GameManager.instance.MapList.Count; i++)
            {
                if (GameManager.instance.MapList[i] == festivall)
                {
                    if (GameManager.instance.MapList[i].GetComponent<BasicBlock>())
                    {

                        GameManager.instance.MapList[i].GetComponent<BasicBlock>().festival = true;
                        if (GameManager.instance.MapList[i].GetComponent<BasicBlock>().festival == true)
                        {
                            GameManager.instance.MapList[i].GetComponent<BasicBlock>().landMag *= 2;
                        }
                        else
                        {
                            GameManager.instance.MapList[i].GetComponent<BasicBlock>().landMag = 1;
                        }
                    }
                    if (GameManager.instance.MapList[i].GetComponent<SpecialBlock>())
                    {
                        GameManager.instance.MapList[i].GetComponent<SpecialBlock>().festival = true;
                        if (GameManager.instance.MapList[i].GetComponent<SpecialBlock>().festival == true)
                        {
                            GameManager.instance.MapList[i].GetComponent<SpecialBlock>().landMag *= 2;
                        }
                        else
                        {
                            GameManager.instance.MapList[i].GetComponent<SpecialBlock>().landMag = 1;
                        }
                    }
                }
            }
            festival = false;

        }

    }
    public void Click()
    {
        if (telePort == true)
        {
            Teleport();

        }
        if (festival == true)
        {
            Festival();
        }
    }

    public void ClickTurnCheck()
    {
        if (telePortCount == 1 && telePort == false)
        {
            ReturnTelePort();
        }
    }

    public void ReturnTelePort()
    {
        telePortCount = 0;
        telePort = false;
        state = PlayerState.Idle;
        getBlockInfo();
    }

    void Check(int idx)
    {
        ownLandList.Add(GameManager.instance.MapList[idx]);
        ColorCheck();
        CheckLine();
    }

    private void OwnLandCheck()
    {
        for (int i = 0; i < GameManager.instance.MapList.Count; i++)
        {
            if (GameManager.instance.MapList[i].GetComponent<SpecialBlock>())
            {
                if (GameManager.instance.MapList[i].GetComponent<SpecialBlock>().LandOwner == gameObject)
                {
                    if (!ownLandList.Contains(GameManager.instance.MapList[i]))
                    {

                        ownLandList.Add(GameManager.instance.MapList[i]);
                        //Check(i);

                    }
                }
                else
                {
                    if (ownLandList.Contains(GameManager.instance.MapList[i]))
                    {
                        if (GameManager.instance.MapList[i].gameObject.name == "?????? ????")
                        {
                            lineOne.Add(GameManager.instance.MapList[i]);
                        }
                        if (GameManager.instance.MapList[i].gameObject.name == "?????? ????")
                        {
                            lineTwo.Add(GameManager.instance.MapList[i]);
                        }
                        if (GameManager.instance.MapList[i].gameObject.name == "???? ????")
                        {
                            lineThree.Add(GameManager.instance.MapList[i]);
                        }
                        if (GameManager.instance.MapList[i].gameObject.name == "?????? ????")
                        {
                            lineFour.Add(GameManager.instance.MapList[i]);
                        }
                        ownLandList.Remove(GameManager.instance.MapList[i]);
                    }
                }

            }
            if (GameManager.instance.MapList[i].GetComponent<BasicBlock>())
            {
                if (GameManager.instance.MapList[i].GetComponent<BasicBlock>().LandOwner == gameObject)
                {
                    if (!ownLandList.Contains(GameManager.instance.MapList[i]))
                    {
                        ownLandList.Add(GameManager.instance.MapList[i]);
                        // Check(i);
                    }
                }
                else
                {
                    if (ownLandList.Contains(GameManager.instance.MapList[i]))
                    {
                        List<GameObject>[] color = { color1, color2, color3, color4, color5, color6, color7, color8 };
                        List<GameObject>[] line = { lineOne, lineTwo, lineThree, lineFour };
                        string[] blockName = {
                            "???? ????" , "???? ????" , "???? ????", "???? ????",
                            "???? ????", "???? ????" , "???? ????", "?????? ????",
                            "???? ????", "???? ????", "???? ????", "???? ????",
                            "???????? ????", "?????? ????", "???? ????", "???? ????"
                        };

                        for (int j = 0; j < blockName.Length; j++)
                        {
                            if (GameManager.instance.MapList[i].gameObject.name == blockName[j])
                            {
                                line[j / 4].Add(GameManager.instance.MapList[i]);
                                color[j / 2].Add(GameManager.instance.MapList[i]);
                            }
                        }


                        ownLandList.Remove(GameManager.instance.MapList[i]);
                    }
                }
            }
        }
    }

    private void Line()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].gameObject.layer == LayerMask.NameToLayer("LineOne"))
            {
                lineOne.Add(lines[i]);

            }
            if (lines[i].gameObject.layer == LayerMask.NameToLayer("LineTwo"))
            {
                lineTwo.Add(lines[i]);
            }
            if (lines[i].gameObject.layer == LayerMask.NameToLayer("LineThree"))
            {
                lineThree.Add(lines[i]);
            }
            if (lines[i].gameObject.layer == LayerMask.NameToLayer("LineFour"))
            {
                lineFour.Add(lines[i]);
            }
        }
        line = false;
    }
    private void CheckLine()
    {

        for (int i = 0; i < ownLandList.Count; i++)
        {
            for (int j = 0; j < lineOne.Count; j++)
            {
                if (ownLandList[i] == lineOne[j])
                {
                    lineOnePool.Add(lineOne[j]);
                    lineOne.Remove(lineOne[j]);
                }
            }
            for (int j = 0; j < lineTwo.Count; j++)
            {
                if (ownLandList[i] == lineTwo[j])
                {
                    lineTwoPool.Add(lineTwo[j]);
                    lineTwo.Remove(lineTwo[j]);
                }
            }
            for (int j = 0; j < lineThree.Count; j++)
            {
                if (ownLandList[i] == lineThree[j])
                {
                    lineThreePool.Add(lineThree[j]);
                    lineThree.Remove(lineThree[j]);
                }
            }
            for (int j = 0; j < lineFour.Count; j++)
            {
                if (ownLandList[i] == lineFour[j])
                {
                    lineFourPool.Add(lineFour[j]);
                    lineFour.Remove(lineFour[j]);
                }
            }

        }
        for (int i = 0; i < ownLandList.Count; i++)
        {
            for (int j = 0; j < specialBlocks.Count; j++)
            {
                if (ownLandList[i] == specialBlocks[j])
                {
                    specialBlocks.Remove(specialBlocks[j]);
                }
            }
        }

    }

    private void ColorCheck()
    {
        //if (GameManager.instance.currentTurnPlayer)
        //{
        for (int i = 0; i < ownLandList.Count; i++)
        {
            for (int j = 0; j < color1.Count; j++)
            {
                if (ownLandList[i] == color1[j])
                {
                    color1.Remove(color1[j]);
                }
            }
            for (int j = 0; j < color2.Count; j++)
            {
                if (ownLandList[i] == color2[j])
                {
                    color2.Remove(color2[j]);
                }
            }
            for (int j = 0; j < color3.Count; j++)
            {
                if (ownLandList[i] == color3[j])
                {
                    color3.Remove(color3[j]);
                }
            }
            for (int j = 0; j < color4.Count; j++)
            {
                if (ownLandList[i] == color4[j])
                {
                    color4.Remove(color4[j]);
                }
            }
            for (int j = 0; j < color5.Count; j++)
            {
                if (ownLandList[i] == color5[j])
                {
                    color5.Remove(color5[j]);
                }
            }
            for (int j = 0; j < color6.Count; j++)
            {
                if (ownLandList[i] == color6[j])
                {
                    color6.Remove(color6[j]);
                }
            }
            for (int j = 0; j < color7.Count; j++)
            {
                if (ownLandList[i] == color7[j])
                {
                    color7.Remove(color7[j]);
                }
            }
            for (int j = 0; j < color8.Count; j++)
            {
                if (ownLandList[i] == color8[j])
                {
                    color8.Remove(color8[j]);
                }
            }
            //}
        }

    }
    public void TakeOverColor()
    {
        for (int i = 0; i < ownLandList.Count; i++)
        {
            for (int j = 0; j < lineOnePool.Count; j++)
            {
                if (ownLandList[i] == lineOnePool[j])
                {
                    lineOne.Add(lineOnePool[j]);
                }
            }
            for (int j = 0; j < lineTwoPool.Count; j++)
            {
                if (ownLandList[i] == lineTwoPool[j])
                {
                    lineTwo.Add(lineTwoPool[j]);
                }
            }
            for (int j = 0; j < lineThreePool.Count; j++)
            {
                if (ownLandList[i] == lineThreePool[j])
                {
                    lineThree.Add(lineThreePool[j]);
                }
            }
            for (int j = 0; j < lineFourPool.Count; j++)
            {
                if (ownLandList[i] == lineFourPool[j])
                {
                    lineFour.Add(lineFourPool[j]);
                }
            }
        }
    }
    private void ColorCount()
    {
        if (color1.Count == 0 && one == true)
        {
            colorCount++;
            one = false;
            bone = false;
        }
        if (color2.Count == 0 && two == true)
        {
            colorCount++;
            two = false;
            btwo = false;
        }
        if (color3.Count == 0 && three == true)
        {
            colorCount++;
            three = false;
            bthree = false;
        }
        if (color4.Count == 0 && four == true)
        {
            colorCount++;
            four = false;
            bfour = false;
        }
        if (color5.Count == 0 && five == true)
        {
            colorCount++;
            five = false;
            bfive = false;
        }
        if (color6.Count == 0 && six == true)
        {
            colorCount++;
            six = false;
            bsix = false;
        }
        if (color7.Count == 0 && seven == true)
        {
            colorCount++;
            seven = false;
            bseven = false;
        }
        if (color8.Count == 0 && eight == true)
        {
            colorCount++;
            eight = false;
            beight = false;
        }
        if (color1.Count != 0 && bone == false)
        {
            colorCount--;
            bone = true;
            one = true;
        }
        if (color2.Count != 0 && btwo == false)
        {
            colorCount--;
            btwo = true;
            two = true;
        }
        if (color3.Count != 0 && bthree == false)
        {
            colorCount--;
            bthree = true;
            three = true;
        }
        if (color4.Count != 0 && bfour == false)
        {
            colorCount--;
            bfour = true;
            four = true;
        }
        if (color5.Count != 0 && bfive == false)
        {
            colorCount--;
            bfive = true;
            five = true;
        }
        if (color6.Count != 0 && bsix == false)
        {
            colorCount--;
            bsix = true;
            six = true;
        }
        if (color7.Count != 0 && bseven == false)
        {
            colorCount--;
            bseven = true;
            seven = true;
        }
        if (color8.Count != 0 && beight == false)
        {
            colorCount--;
            beight = true;
            eight = true;
        }



    }
    private void Bankrupt()
    {
        if (money < 0)
        {
            for (int i = 0; i < ownLandList.Count; i++)
            {
                if (ownLandList[i].GetComponent<BasicBlock>())
                {
                    ownLandList[i].GetComponent<BasicBlock>().land = false;
                    ownLandList[i].GetComponent<BasicBlock>().landCount = 0;
                    ownLandList[i].GetComponent<BasicBlock>().tear1 = false;
                    ownLandList[i].GetComponent<BasicBlock>().tear1Count = 0;
                    Destroy(ownLandList[i].GetComponent<BasicBlock>().tear1object);
                    //ownLandList[i].GetComponent<BasicBlock>().tear1Factory.SetActive(false);
                    ownLandList[i].GetComponent<BasicBlock>().tear2 = false;
                    ownLandList[i].GetComponent<BasicBlock>().tear2Count = 0;
                    Destroy(ownLandList[i].GetComponent<BasicBlock>().tear2object);
                    //ownLandList[i].GetComponent<BasicBlock>().tear2Factory.SetActive(false);
                    ownLandList[i].GetComponent<BasicBlock>().tear3 = false;
                    ownLandList[i].GetComponent<BasicBlock>().tear3Count = 0;
                    Destroy(ownLandList[i].GetComponent<BasicBlock>().tear3object);
                    //ownLandList[i].GetComponent<BasicBlock>().tear3Factory.SetActive(false);
                    ownLandList[i].GetComponent<BasicBlock>().landMark = false;
                    ownLandList[i].GetComponent<BasicBlock>().landMarkCount = 0;
                    Destroy(ownLandList[i].GetComponent<BasicBlock>().landMarkObj);
                    //ownLandList[i].GetComponent<BasicBlock>().landMarkFactory.SetActive(false);
                }
                if (ownLandList[i].GetComponent<SpecialBlock>())
                {
                    ownLandList[i].GetComponent<SpecialBlock>().tourS.SetActive(false);
                }
            }
            bankrupt = true;
            RollDiceBtn.SetActive(true);
            GameManager.instance.PlayerList.Remove(gameObject);
            GameManager.instance.turnIndex--;
            Destroy(gameObject);
        }
    }

    private int CalculateTotalMoney()
    {
        int landPrice = 0;
        int total;
        for (int i = 0; i < ownLandList.Count; i++)
        {
            if (ownLandList[i].GetComponent<BasicBlock>())
            {
                landPrice += ownLandList[i].GetComponent<BasicBlock>().totalLandPrice / 2;

            }
            if (ownLandList[i].GetComponent<SpecialBlock>())
            {
                landPrice += ownLandList[i].GetComponent<SpecialBlock>().landPrice / 2;
            }

        }
        total = money + landPrice;
        return total;
    }

    private void StateMachine()
    {
        switch (state)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Move:
                Move();
                break;
            case PlayerState.Turn:
                Turn();
                break;
            case PlayerState.End:
                End();
                break;
        }
    }

    private void Idle()
    {
        if (GameManager.instance.currentTurnPlayer == gameObject && photonView.IsMine)
        {
            RollDiceBtn.SetActive(true);
        }
        else
        {
            RollDiceBtn.SetActive(false);
        }
    }
    private void Move()
    {
        RollDiceBtn.SetActive(false);
    }

    private void Turn()
    {
        //if (onTurn && !hasInfo)
        //    getBlockInfo();
        //if (!onTurn)
        //    TurnCheck();
    }

    private void TrapCount()
    {
        if (trapCount >= 4)
        {
            trapCount = 0;
            isTraped = false;

        }
    }
    float currentTime = 0;
    public float createTime = 2.5f;
    public void TurnCheck()
    {
        startCurrentTime = 0;
        startCount = false;
        trapUI = false;
        teleportUI = false;
        startUI = false;
        festivalUI = false;
        currentTime = 0;
        hasInfo = false;
        eventBlock = false;
        //if (trapCount == 0)
        //{
        //    isTraped = false;
        //}
        if (isTraped)
        {
            if (trapCount < 4)
            {

                // GameUI.instance.TrapBlockUI.SetActive(true);
                state = PlayerState.End;




            }

        }
        else
        {
            if (sameDice == true)
            {

                state = PlayerState.Idle;
                RollDiceBtn.SetActive(true);
            }
            else
            {
                state = PlayerState.End;
            }
        }
    }
    //public void TrapCheck()
    //{
    //    if (GameManager.instance.currentTurnPlayer)
    //    {

    //        if (GameManager.instance.currentTurnPlayer.GetComponent<Player>().isTraped == true)
    //        {
    //            GameUI.instance.TrapBlockUI.SetActive(true);
    //            currentTime += Time.deltaTime;
    //            if (currentTime > createTime)
    //            {
    //                GameUI.instance.TrapBlockUI.SetActive(false);
    //            }

    //        }
    //    }
    //    print(currentTime);
    //}
    private void getBlockInfo()
    {
        state = PlayerState.Turn;
        hasInfo = true;
        RollDiceBtn.SetActive(false);
        GameObject currentBlock = GameManager.instance.MapList[currentMapIndex];
        // currentBlock.GetComponent<Block>().OnBlock(gameObject);

        switch (currentBlock.tag)
        {
            case "StartBlock":
                currentBlock.GetComponent<StartBlock>().OnStartBlock(transform);
                break;
            case "BasicBlock":
                currentBlock.GetComponent<BasicBlock>().OnBasicBlock(gameObject);
                break;
            case "EventBlock":
                currentBlock.GetComponent<EventBlock>().OnEventBlock(transform);
                break;
            case "SpecialBlock":
                currentBlock.GetComponent<SpecialBlock>().OnSpecialBlock(gameObject);
                break;
            case "CardBlock":
                currentBlock.GetComponent<CardBlock>().OnCardBlock(transform);
                break;
            case "TrapBlock":
                currentBlock.GetComponent<TrapBlock>().OnTrapBlock(gameObject);
                break;
            case "FestivalBlock":
                currentBlock.GetComponent<FestivalBlock>().OnFestivalBlock(transform);
                break;
            case "TeleportBlock":
                currentBlock.GetComponent<TeleportBlock>().OnTeleportBlock(transform);
                break;
        }
    }

    private void End()
    {

        RollDiceBtn.SetActive(true);
        for (int i = 0; i < GameManager.instance.MapList.Count; i++)
        {
            if (GameManager.instance.MapList[i].GetComponent<BasicBlock>())
            {
                if (GameManager.instance.MapList[i].GetComponent<BasicBlock>().festival == true)
                {
                    GameManager.instance.MapList[i].GetComponent<BasicBlock>().festivalCount++;
                }
            }
            if (GameManager.instance.MapList[i].GetComponent<SpecialBlock>())
            {
                if (GameManager.instance.MapList[i].GetComponent<SpecialBlock>().festival == true)
                {
                    GameManager.instance.MapList[i].GetComponent<SpecialBlock>().festivalCount++;
                }

            }
        }
        //GameManager.instance.turnIndex++;
        GameManager.instance.ChangeCurrentTurnPlayer();
        // currentTime = 0;
        state = PlayerState.Idle;
    }

    public void PlayerMove(int dice1, int dice2)
    {
        if (dice1 == dice2)
        {
            sameDiceCount++;
        }
        else
        {
            sameDiceCount = 0;
        }
        if (sameDiceCount == 3)
        {
            transform.position = GameManager.instance.MapList[8].transform.position + new Vector3(0, 1.5f, 0);
            isTraped = true;
            //trapCount = 4;
            sameDice = false;
            currentMapIndex = 8;
            sameDiceCount = 0;
            state = PlayerState.End;
            return;
        }

        state = PlayerState.Move;
        StartCoroutine(IEMove(dice1, dice2));
    }

    public void Teleport()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseInfo;
        if (Physics.Raycast(mouseRay, out mouseInfo))
        {
            print("???????? ????: " + mouseInfo.transform.name);

        }
        else
        {
            print("???????? ???? ????");
        }
        if (Input.GetButtonDown("Fire1"))
        {

            GameObject telePortt = GameObject.Find(mouseInfo.transform.name);
            for (int result = 0; result < GameManager.instance.MapList.Count; result++)
            {
                if (GameManager.instance.MapList[result] == telePortt)
                {
                    int d = result + 8;
                    StartCoroutine(IETeleport(d));
                    //GameManager.instance.currentTurnPlayer.transform.position =
                    //GameManager.instance.MapList[result].transform.position + new Vector3(0, 1.5f, 0);

                    //GameManager.instance.currentTurnPlayer.GetComponent<Player>().currentMapIndex = i;
                }
            }
            telePortCount++;
            if (telePortCount == 1)
            {
                telePort = false;
            }
        }

    }

    IEnumerator IETeleport(int result)
    {
        int destinationIndex = result;

        for (int i = 1; i <= destinationIndex; i++)
        {
            if (currentMapIndex + i > 31)
            {
                gameObject.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, +salary);
                currentMapIndex -= 32;
            }
            transform.position = GameManager.instance.MapList[currentMapIndex + i].transform.position + new Vector3(0, 1.5f, 0);
            yield return new WaitForSeconds(0.1f);
        }
        currentMapIndex += destinationIndex;
    }



    IEnumerator IEMove(int dice1, int dice2)
    {
        int destinationIndex = dice1 + dice2;

        for (int i = 1; i <= destinationIndex; i++)
        {
            if (currentMapIndex + i > 31)
            {
                gameObject.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, +salary);
                currentMapIndex -= 32;
            }
            transform.position = GameManager.instance.MapList[currentMapIndex + i].transform.position + new Vector3(0, 1.5f, 0);
            yield return new WaitForSeconds(0.1f);
        }
        currentMapIndex += destinationIndex;
        if (dice1 == dice2)
        {
            sameDice = true;
        }
        else
        {
            sameDice = false;
        }
        //onTurn = true;

        getBlockInfo();

    }


    [PunRPC]
    void RpcAddMoney(int addMoney)
    {
        money += addMoney;
    }
    public void Chance()
    {
        int n = UnityEngine.Random.Range(1, 5);
        print(n);
        //int n = UnityEngine.Random.Range(5, chanceCard.Length);
        if (n == 1)
        {
            chance1 = true;
            ChanceTrap();
        }
        if (n == 2)
        {
            chance2 = true;
            ChanceTeleport();
        }
        //if (n == 3)
        //{
        //    chance3 = true;
        //    ChanceStart();
        //}
        if (n == 3)
        {
            chance4 = true;
            ChanceShield();
        }
        if (n == 4)
        {
            chance5 = true;
            ChanceMoney();
        }
        if (n == 5)
        {
            chance6 = true;
            ChanceTakeMoney();
        }
    }

    public void ChanceTrap()
    {
        print("ChanceTrap");
        transform.position = GameManager.instance.MapList[8].transform.position + new Vector3(0, 1.5f, 0);
        //GameObject currentBlock = GameManager.instance.MapList[8];
        currentMapIndex = 8;
        getBlockInfo();
        //isTraped = true;
    }

    public void ChanceTeleport()
    {
        print("ChanceTeleport");
        transform.position = GameManager.instance.MapList[24].transform.position + new Vector3(0, 1.5f, 0);
        currentMapIndex = 24;
        //GameObject currentBlock = GameManager.instance.MapList[24];
        getBlockInfo();
        //telePort = true;
    }

    public void ChanceStart()
    {
        print("ChanceStart");
        transform.position = GameManager.instance.MapList[0].transform.position + new Vector3(0, 1.5f, 0);
        currentMapIndex = 0;
        getBlockInfo();

        //startB = true;
    }

    public void ChanceShield()
    {
        print("ChanceShield");
        shield = true;
        //TurnCheck();
    }

    public void ChanceMoney()
    {
        print("ChanceMoney");
        for (int i = 0; i < GameManager.instance.PlayerList.Count; i++)
        {
            if (GameManager.instance.PlayerList[i] != gameObject)
            {
                GameManager.instance.PlayerList[i].GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, -charge);
                gameObject.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, +charge);
            }

        }
        //TurnCheck();

    }
    GameObject poorP;
    public void ChanceTakeMoney()
    {

        print("ChanceTakeMoney");
        int poor = GameManager.instance.currentTurnPlayer.GetComponent<Player>().TotalMoney;
        for (int i = 0; i < GameManager.instance.PlayerList.Count; i++)
        {


            if (poor <= GameManager.instance.PlayerList[i].GetComponent<Player>().TotalMoney)
            {
                poorP = GameManager.instance.PlayerList[i];
            }


        }
        for (int i = 0; i < GameManager.instance.PlayerList.Count; i++)
        {
            if (GameManager.instance.PlayerList[i] != poorP)
            {
                poorP.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, +charge);
                GameManager.instance.PlayerList[i].GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, -charge);
            }

        }
        //TurnCheck();
    }
    public void Chance1UI()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            GameUI.instance.Chance1UI.SetActive(false);
            chance1 = false;
            //TurnCheck();
        }
    }
    public void Chance2UI()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            GameUI.instance.Chance2UI.SetActive(false);
            chance2 = false;
            //TurnCheck();
        }
    }
    public void Chance3UI()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            GameUI.instance.Chance3UI.SetActive(false);
            chance3 = false;
            //TurnCheck();
        }
    }
    public void Chance4UI()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            GameUI.instance.Chance4UI.SetActive(false);
            chance4 = false;
            TurnCheck();
        }
    }
    public void Chance5UI()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            GameUI.instance.Chance5UI.SetActive(false);
            chance5 = false;
            TurnCheck();
        }
    }
    public void Chance6UI()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            GameUI.instance.Chance6UI.SetActive(false);
            chance6 = false;
            TurnCheck();
        }
    }
    float eventCurrentTime;
    public void EventBlockUI()
    {
        eventCurrentTime += Time.deltaTime;
        if (eventCurrentTime > createTime)
        {
            GameUI.instance.EventBlockUI.SetActive(false);
            eventCurrentTime = 0;
            TurnCheck();
        }
    }
}