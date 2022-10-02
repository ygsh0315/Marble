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
    public bool startB = false;
    public bool shield = false;
    public bool takeMoney = false;
    public int festivaCount = 0;
    public int telePortCount = 0;
    bool one = true;
    bool two = true;
    bool three = true;
    bool four = true;
    bool five = true;
    bool six = true;
    bool seven = true;
    bool eight = true;
    public int trapCount = 0;
    public int colorCount = 0;
    public int[] chanceCard = { 1, 2, 3, 4, 5, 6 };
    public Material myColor;

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
        color1.Add(GameObject.Find("피시 신전"));
        color1.Add(GameObject.Find("잠보 신전"));
        color2.Add(GameObject.Find("심바 신전"));
        color2.Add(GameObject.Find("추어 신전"));
        color3.Add(GameObject.Find("카라 신전"));
        color3.Add(GameObject.Find("파즈 신전"));
        color4.Add(GameObject.Find("루나 신전"));
        color4.Add(GameObject.Find("디오스 신전"));
        color5.Add(GameObject.Find("도란 신전"));
        color5.Add(GameObject.Find("나래 신전"));
        color6.Add(GameObject.Find("해솔 신전"));
        color6.Add(GameObject.Find("가온 신전"));
        color7.Add(GameObject.Find("파르테논 신전"));
        color7.Add(GameObject.Find("아폴론 신전"));
        color8.Add(GameObject.Find("니케 신전"));
        color8.Add(GameObject.Find("헤라 신전"));
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

    private void TeleportUI()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            GameUI.instance.TeleportUI.SetActive(false);
            TurnCheck();
        }
    }
    public void StartUI()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            GameUI.instance.StartUI.SetActive(false);
            TurnCheck();
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
        if(currentTime > createTime)
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

    public void StartBonus()
    {
        if (startB == true)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseInfo;
            if (Physics.Raycast(mouseRay, out mouseInfo))
            {
                print("가리키는 대상: " + mouseInfo.transform.name);
            }
            else
            {
                print("가리키는 대상 없음");
            }
            if (Input.GetButtonDown("Fire1"))
            {
                if (mouseInfo.transform)
                {

                    GameObject startBonus = GameObject.Find(mouseInfo.transform.name);
                }
                for (int i = 0; i < ownLandList.Count; i++)
                {
                    if (ownLandList[i].gameObject.name == mouseInfo.transform.name)
                    {
                        if (ownLandList[i].GetComponent<BasicBlock>())
                        {
                            ownLandList[i].GetComponent<BasicBlock>().OnBasicBlock(gameObject);
                        }

                    }
                }

            }
        }
    }

    public void Festival()
    {

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseInfo;
        if (Physics.Raycast(mouseRay, out mouseInfo))
        {
            print("가리키는 대상: " + mouseInfo.transform.name);

        }
        else
        {
            print("가리키는 대상 없음");
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
                        GameManager.instance.MapList[i].GetComponent<BasicBlock>().landMag *=2;
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
                        if (GameManager.instance.MapList[i].gameObject.name == "수피아 토템")
                        {
                            lineOne.Add(GameManager.instance.MapList[i]);
                        }
                        if (GameManager.instance.MapList[i].gameObject.name == "헤비치 토템")
                        {
                            lineTwo.Add(GameManager.instance.MapList[i]);
                        }
                        if (GameManager.instance.MapList[i].gameObject.name == "나린 토템")
                        {
                            lineThree.Add(GameManager.instance.MapList[i]);
                        }
                        if (GameManager.instance.MapList[i].gameObject.name == "미니래 토템")
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
                        List<GameObject> [] color = { color1, color2, color3, color4 , color5, color6 , color7 , color8 };
                        List<GameObject>[] line = { lineOne, lineTwo, lineThree, lineFour };
                        string[] blockName = { 
                            "피시 신전" , "잠보 신전" , "심바 신전", "추어 신전",
                            "카라 신전", "파즈 신전" , "루나 신전", "디오스 신전",
                            "도란 신전", "나래 신전", "해솔 신전", "가온 신전",
                            "파르테논 신전", "아폴론 신전", "니케 신전", "헤라 신전"
                        };

                        for(int j = 0; j < blockName.Length; j++)
                        {
                            if(GameManager.instance.MapList[i].gameObject.name == blockName[j])
                            {
                                line[j / 4].Add(GameManager.instance.MapList[i]);
                                color[j / 2].Add(GameManager.instance.MapList[i]);
                            }
                        }
                       


                        //if (GameManager.instance.MapList[i].gameObject.name == "피시 신전")
                        //{                            
                        //    lineOne.Add(GameManager.instance.MapList[i]);
                        //    color1.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "잠보 신전")
                        //{
                        //    lineOne.Add(GameManager.instance.MapList[i]);
                        //    color1.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "심바 신전")
                        //{
                        //    lineOne.Add(GameManager.instance.MapList[i]);
                        //    color2.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "추어 신전")
                        //{
                        //    lineOne.Add(GameManager.instance.MapList[i]);
                        //    color2.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "카라 신전")
                        //{
                        //    lineTwo.Add(GameManager.instance.MapList[i]);
                        //    color3.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "파즈 신전")
                        //{
                        //    lineTwo.Add(GameManager.instance.MapList[i]);
                        //    color3.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "루나 신전")
                        //{
                        //    lineTwo.Add(GameManager.instance.MapList[i]);
                        //    color4.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "디오스 신전")
                        //{
                        //    lineTwo.Add(GameManager.instance.MapList[i]);
                        //    color4.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "도란 신전")
                        //{
                        //    lineThree.Add(GameManager.instance.MapList[i]);
                        //    color5.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "나래 신전")
                        //{
                        //    lineThree.Add(GameManager.instance.MapList[i]);
                        //    color5.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "해솔 신전")
                        //{
                        //    lineThree.Add(GameManager.instance.MapList[i]);
                        //    color6.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "가온 신전")
                        //{
                        //    lineThree.Add(GameManager.instance.MapList[i]);
                        //    color6.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "파르테논 신전")
                        //{
                        //    lineFour.Add(GameManager.instance.MapList[i]);
                        //    color7.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "아폴론 신전")
                        //{
                        //    lineFour.Add(GameManager.instance.MapList[i]);
                        //    color7.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "니케 신전")
                        //{
                        //    lineFour.Add(GameManager.instance.MapList[i]);
                        //    color8.Add(GameManager.instance.MapList[i]);
                        //}
                        //if (GameManager.instance.MapList[i].gameObject.name == "헤라 신전")
                        //{
                        //    lineFour.Add(GameManager.instance.MapList[i]);
                        //    color8.Add(GameManager.instance.MapList[i]);
                        //}
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
            for (int j=0; j < lineThree.Count; j++)
            {
                if (ownLandList[i] == lineThree[j])
                {
                    lineThreePool.Add(lineThree[j]);
                    lineThree.Remove(lineThree[j]);
                }
            }
            for (int j=0; j< lineFour.Count; j++)
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
    public  void TakeOverColor()
    {
       for(int i = 0; i< ownLandList.Count; i++)
        {
            for(int j =0; j < lineOnePool.Count; j++)
            {
                if(ownLandList[i] == lineOnePool[j])
                {
                    lineOne.Add(lineOnePool[j]);
                }              
            }
            for (int j=0; j< lineTwoPool.Count; j++)
            {
                if (ownLandList[i] == lineTwoPool[j])
                {
                    lineTwo.Add(lineTwoPool[j]);
                }
            }
            for (int j=0; j< lineThreePool.Count; j++)
            {
                if (ownLandList[i] == lineThreePool[j])
                {
                    lineThree.Add(lineThreePool[j]);
                }
            }
            for(int j=0; j< lineFourPool.Count; j++)
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
        }
        if (color2.Count == 0 && two == true)
        {
            colorCount++;
            two = false;
        }
        if (color3.Count == 0 && three == true)
        {
            colorCount++;
            three = false;
        }
        if (color4.Count == 0 && four == true)
        {
            colorCount++;
            four = false;
        }
        if (color5.Count == 0 && five == true)
        {
            colorCount++;
            five = false;
        }
        if (color6.Count == 0 && six == true)
        {
            colorCount++;
            six = false;
        }
        if (color7.Count == 0 && seven == true)
        {
            colorCount++;
            seven = false;
        }
        if (color8.Count == 0 && eight == true)
        {
            colorCount++;
            eight = false;
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
                    ownLandList[i].GetComponent<BasicBlock>().tear1Factory.SetActive(false);
                    ownLandList[i].GetComponent<BasicBlock>().tear2 = false;
                    ownLandList[i].GetComponent<BasicBlock>().tear2Count = 0;
                    ownLandList[i].GetComponent<BasicBlock>().tear2Factory.SetActive(false);
                    ownLandList[i].GetComponent<BasicBlock>().tear3 = false;
                    ownLandList[i].GetComponent<BasicBlock>().tear3Count = 0;
                    ownLandList[i].GetComponent<BasicBlock>().tear3Factory.SetActive(false);
                    ownLandList[i].GetComponent<BasicBlock>().landMark = false;
                    ownLandList[i].GetComponent<BasicBlock>().landMarkCount = 0;
                    ownLandList[i].GetComponent<BasicBlock>().landMarkFactory.SetActive(false);
                }
            }
            GameManager.instance.turnIndex--;
            bankrupt = true;
            RollDiceBtn.SetActive(true);
            GameManager.instance.PlayerList.Remove(gameObject);
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
                landPrice += ownLandList[i].GetComponent<BasicBlock>().takeOverCharge / 2;

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
    public float createTime = 1;
    public void TurnCheck()
    {
        trapUI = false;
        teleportUI = false;
        startUI = false;
        festivalUI = false;
        currentTime = 0;
        hasInfo = false;
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
            print("가리키는 대상: " + mouseInfo.transform.name);

        }
        else
        {
            print("가리키는 대상 없음");
        }
        if (Input.GetButtonDown("Fire1"))
        {

            GameObject telePortt = GameObject.Find(mouseInfo.transform.name);
            for (int result = 0;  result< GameManager.instance.MapList.Count; result++)
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
                money += salary;
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
                money += salary;
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
        int n = UnityEngine.Random.Range(0, chanceCard.Length + 1);
        if (n == 1)
        {
            ChanceTrap();
        }
        if (n == 2)
        {
            ChanceTeleport();
        }
        if (n == 3)
        {
            ChanceStart();
        }
        if (n == 4)
        {
            ChanceShield();
        }
        if (n == 5)
        {
            ChanceMoney();
        }
        if (n == 6)
        {
            ChanceTakeMoney();
        }
    }

    public void ChanceTrap()
    {
        transform.position = GameManager.instance.MapList[8].transform.position + new Vector3(0, 1.5f, 0);
        isTraped = true;
    }

    public void ChanceTeleport()
    {
        transform.position = GameManager.instance.MapList[24].transform.position + new Vector3(0, 1.5f, 0);
        telePort = true;
    }

    public void ChanceStart()
    {
        transform.position = GameManager.instance.MapList[32].transform.position + new Vector3(0, 1.5f, 0);
        telePort = true;
    }

    public void ChanceShield()
    {
        shield = true;
    }

    public void ChanceMoney()
    {

    }
    public void ChanceTakeMoney()
    {
        takeMoney = true;

    }
}