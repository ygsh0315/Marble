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
    public int salary = 300000;
    public int sameDiceCount = 0;
    public bool sameDice = false;
    public GameObject RollDiceBtn;
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
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
        if (money <= 0)
        {
            GameManager.instance.turnIndex++;
            Destroy(gameObject);
            GameManager.instance.PlayerList.Remove(gameObject);
        }
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
        getBlockInfo();
        if(sameDice == true)
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
                currentBlock.GetComponent<SpecialBlock>().OnSpecialBlock(transform);
                break;
            case "CardBlock":            
                currentBlock.GetComponent<CardBlock>().OnCardBlock(transform);
                break;
            case "TrapBlock":              
                currentBlock.GetComponent<TrapBlock>().OnTrapBlock(transform);
                break;
            case "DoubleBlock":               
                currentBlock.GetComponent<DoubleBlock>().OnDoubleBlock(transform);
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
        if(dice1 == dice2)
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
        if(dice1 == dice2)
        {
            sameDice = true; 
        }
        else
        {
            sameDice = false;
        }
        state = PlayerState.Turn;
    }
}
