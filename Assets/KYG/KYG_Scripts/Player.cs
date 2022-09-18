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
    public bool line = true;
    public bool onTurn = false;
    public bool hasInfo = false;
    public bool isTraped = false;
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
        Line();
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
        Bankrupt();
        OwnLandCheck();
        TotalMoney = CalculateTotalMoney();

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
    }
    private void Bankrupt()
    {
        if (money <= 0)
        {
            GameManager.instance.turnIndex++;
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
            landPrice += ownLandList[i].GetComponent<BasicBlock>().takeOverCharge / 2;
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

    }
    private void Move()
    {
        RollDiceBtn.SetActive(false);
    }

    private void Turn()
    {
        if(onTurn && !hasInfo)
        getBlockInfo();
        if(!onTurn)
        TurnCheck();
    }

    private void TurnCheck()
    {
        hasInfo = false;
        if (isTraped)
        {
            GameUI.instance.TrapBlockUI.SetActive(true);
        }
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

    private void getBlockInfo()
    {
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
                currentBlock.GetComponent<TrapBlock>().OnTrapBlock(transform);
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
        GameManager.instance.turnIndex++;
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
            sameDice = false;
            currentMapIndex = 8;
            sameDiceCount = 0;
            state = PlayerState.Turn;
            return;
        }
        state = PlayerState.Move;
        StartCoroutine(IEMove(dice1, dice2));
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
        onTurn = true;
        state = PlayerState.Turn;
    }
}