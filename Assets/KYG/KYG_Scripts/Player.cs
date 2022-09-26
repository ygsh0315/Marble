using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
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
    public List<GameObject> linePool = new List<GameObject>();
    public bool line = true;
    public bool special = true;
    public bool onTurn = false;
    public bool hasInfo = false;
    public bool isTraped = false;
    public bool telePort = false;
    public bool bankrupt = false;
    public bool festival = false;
    public bool startB = false;
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
    }

    // Update is called once per frame
    void Update()
    {

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
                if(mouseInfo.transform)
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
                        festival = true;
                    }
                    if (GameManager.instance.MapList[i].GetComponent<SpecialBlock>())
                    {
                        GameManager.instance.MapList[i].GetComponent<SpecialBlock>().festival = true;
                        festival = true;
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
                    }
                }
                else
                {
                    if (ownLandList.Contains(GameManager.instance.MapList[i]))
                    {
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
                    }
                }
                else
                {
                    if (ownLandList.Contains(GameManager.instance.MapList[i]))
                    {
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
                        lineOne.Remove(lineOne[j]);

                    }
                    if (ownLandList[i] == lineTwo[j])
                    {
                        lineOne.Remove(lineTwo[j]);
                    }
                    if (ownLandList[i] == lineThree[j])
                    {
                        lineOne.Remove(lineThree[j]);
                    }
                    if (ownLandList[i] == lineFour[j])
                    {
                        lineOne.Remove(lineFour[j]);
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
        if (GameManager.instance.currentTurnPlayer)
        {
            for (int i = 0; i < GameManager.instance.currentTurnPlayer.GetComponent<Player>().ownLandList.Count; i++)
            {
                for (int j = 0; j < color1.Count; j++)
                {

                    if (GameManager.instance.currentTurnPlayer.GetComponent<Player>().ownLandList[i] == color1[j])
                    {
                        color1.Remove(color1[j]);
                    }

                }
                for (int j = 0; j < color2.Count; j++)
                {
                    if (GameManager.instance.currentTurnPlayer.GetComponent<Player>().ownLandList[i] == color2[j])
                    {
                        color2.Remove(color2[j]);
                    }
                }
                for (int j = 0; j < color3.Count; j++)
                {
                    if (GameManager.instance.currentTurnPlayer.GetComponent<Player>().ownLandList[i] == color3[j])
                    {
                        color3.Remove(color3[j]);
                    }
                }
                for (int j = 0; j < color4.Count; j++)
                {
                    if (GameManager.instance.currentTurnPlayer.GetComponent<Player>().ownLandList[i] == color4[j])
                    {
                        color4.Remove(color4[j]);
                    }
                }
                for (int j = 0; j < color5.Count; j++)
                {

                    if (GameManager.instance.currentTurnPlayer.GetComponent<Player>().ownLandList[i] == color5[j])
                    {
                        color5.Remove(color5[j]);
                    }
                }
                for (int j = 0; j < color6.Count; j++)
                {
                    if (GameManager.instance.currentTurnPlayer.GetComponent<Player>().ownLandList[i] == color6[j])
                    {
                        color6.Remove(color6[j]);
                    }
                }
                for (int j = 0; j < color7.Count; j++)
                {
                    if (GameManager.instance.currentTurnPlayer.GetComponent<Player>().ownLandList[i] == color7[j])
                    {
                        color7.Remove(color7[j]);
                    }
                }
                for (int j = 0; j < color8.Count; j++)
                {
                    if (GameManager.instance.currentTurnPlayer.GetComponent<Player>().ownLandList[i] == color8[j])
                    {
                        color8.Remove(color8[j]);
                    }
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
            
            Destroy(gameObject);
            GameManager.instance.PlayerList.Remove(gameObject);
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
        //if (GameManager.instance.currentTurnPlayer)
        //{

        //    if (GameManager.instance.currentTurnPlayer.GetComponent<Player>().isTraped)
        //    {
        //        GameManager.instance.turnIndex++;
        //        trapCount++;
        //    }
        //}
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
       
        GameManager.instance.turnIndex++;
      
        

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
            for (int i = 0; i < GameManager.instance.MapList.Count; i++)
            {
                if (GameManager.instance.MapList[i] == telePortt)
                {
                    GameManager.instance.currentTurnPlayer.transform.position =
                    GameManager.instance.MapList[i].transform.position + new Vector3(0, 1.5f, 0);
                    GameManager.instance.currentTurnPlayer.GetComponent<Player>().currentMapIndex = i;
                }
            }
            telePortCount++;
            if (telePortCount == 1)
            {
                telePort = false;
            }
        }
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
}