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
    int sameDiceCount = 0;
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

    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
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
        
        //if (GameManager.instance.currentTurnPlayer != transform.gameObject)
        //{
        //    RollDiceBtn.SetActive(false);
        //}
        //else
        //{
        //    RollDiceBtn.SetActive(true);
        //}
    }
    private void Move()
    {
        RollDiceBtn.SetActive(false);
    }

    private void Turn()
    {

        state = PlayerState.End;
    }

    private void End()
    {
        RollDiceBtn.SetActive(true);
        GameManager.instance.turnIndex++;
        state = PlayerState.Idle;
    }

    public void PlayerMove(int dice1, int dice2)
    {
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
                currentMapIndex -= 32;
            }
            transform.position = GameManager.instance.MapList[currentMapIndex + i].transform.position + new Vector3(0, 1.5f, 0);
            yield return new WaitForSeconds(0.1f);
        }
        currentMapIndex += destinationIndex;
        if(dice1 == dice2)
        {
            state = PlayerState.Idle;
            RollDiceBtn.SetActive(true);
            sameDiceCount++;
            if(sameDiceCount == 3)
            {
                transform.position = GameManager.instance.MapList[8].transform.position + new Vector3(0, 1.5f, 0);
                state = PlayerState.Turn;
            }
        }
        else
        {
            state = PlayerState.Turn;
        }
        
    }
}
