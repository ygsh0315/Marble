
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    public GameObject currentTurnPlayer;
    public bool cDice = false;
    public int cDice1 = 0;
    public int cDice2 = 0;
    public int startMoney = 2000000;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;
    public GameObject winner;
    public string winType;
    public GameObject Map;
    public int mapCount;
    public int turnIndex;
    public List<GameObject> PlayerList = new List<GameObject>();
    public List<GameObject> MapList = new List<GameObject>();
    public List<GameObject> FestivalList = new List<GameObject>();
    public List<GameObject> SpecialBockList = new List<GameObject>();
    public List<GameObject> Line1BlockList = new List<GameObject>();
    public List<GameObject> Line2BlockList = new List<GameObject>();
    public List<GameObject> Line3BlockList = new List<GameObject>();
    public List<GameObject> Line4BlockList = new List<GameObject>();
    public List<GameObject> Group1BlockList = new List<GameObject>();
    public List<GameObject> Group2BlockList = new List<GameObject>();
    public List<GameObject> Group3BlockList = new List<GameObject>();
    public List<GameObject> Group4BlockList = new List<GameObject>();
    public List<GameObject> Group5BlockList = new List<GameObject>();
    public List<GameObject> Group6BlockList = new List<GameObject>();
    public List<GameObject> Group7BlockList = new List<GameObject>();
    public List<GameObject> Group8BlockList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //전체 맵 세팅 함수
        MapSet();
        //플레이어 세팅 함수
        PlayerSet();
        //축제 개최 함수
        Festival();
    }
    // Update is called once per frame
    void Update()
    {
        //승리자 판정 함수
        Winner();
        //턴 계산 함수
        turnCalculate();
        //턴 변경 함수
        ChangeCurrentTurnPlayer(turnIndex);
        //랭크 계산 함수
        RankCalCulate();
    }

    //축제 개최 함수
    private void Festival()
    {
        for (int i = 0; i < FestivalList.Count; i++)
        {
            //축제가 개최지들의 배율 2배
            FestivalList[i].GetComponent<BasicBlock>().landMag *= 2;
        }
    }
    //플레이어 세팅 함수
    private void PlayerSet()
    {
        //플레이어 리스트에 플레이어 추가
        PlayerList.Add(Player1);
        PlayerList.Add(Player2);
        PlayerList.Add(Player3);
        PlayerList.Add(Player4);
    }
    //맵 세팅 함수
    private void MapSet()
    {
        //맵 개수
        mapCount = Map.transform.childCount;
        for (int i = 0; i < mapCount; i++)
        {
            //맵리스트에 맵들 추가
            MapList.Add(Map.transform.GetChild(i).gameObject);
            //일반블럭 축제리스트에 추가
            if (MapList[i].GetComponent<BasicBlock>())
            {
                FestivalList.Add(MapList[i]);
            }
            //관광지 블럭리스트에 관광지블럭 추가
            if (MapList[i].GetComponent<SpecialBlock>())
            {
                SpecialBockList.Add(MapList[i]);
            }
        }

        //각 라인에 해당 블럭들 추가
        for(int i = 1; i<8; i++)
        {
            if(MapList[i].GetComponent<BasicBlock>()|| MapList[i].GetComponent<SpecialBlock>())
            Line1BlockList.Add(MapList[i]);
        }
        for(int i=9; i < 16; i++)
        {
            if (MapList[i].GetComponent<BasicBlock>() || MapList[i].GetComponent<SpecialBlock>())
                Line2BlockList.Add(MapList[i]);
        }
        for(int i=17; i<24; i++)
        {
            if (MapList[i].GetComponent<BasicBlock>() || MapList[i].GetComponent<SpecialBlock>())
                Line3BlockList.Add(MapList[i]);
        }
        for(int i=25; i < 32; i++)
        {
            if (MapList[i].GetComponent<BasicBlock>() || MapList[i].GetComponent<SpecialBlock>())
                Line4BlockList.Add(MapList[i]);
        }


        //축제블럭 리스트를 랜덤으로 섞고 3개를 추출
        FestivalList = FestivalList.OrderBy(a => Guid.NewGuid()).Take(3).ToList();

    }
    private void Winner()
    {
        //파산승리 체크
        BankruptCheck();
        //관광지독점 체크
        SpecialBlockMonopolyCheck();
        //라인독점 체크
        LineMonopolyCheck();
        #region 정훈이형 코드
        //for (int i = 0; i < PlayerList.Count; i++)
        //{
        //    if (PlayerList[i])
        //    {
        //        if (PlayerList[i].GetComponent<Player>().line == false)
        //        {


        //            if (PlayerList[i].GetComponent<Player>().lineOne.Any() == false || PlayerList[i].GetComponent<Player>().lineTwo.Any() == false || PlayerList[i].GetComponent<Player>().lineThree.Any() == false || PlayerList[i].GetComponent<Player>().lineFour.Any() == false)
        //            {
        //                winner = PlayerList[i];
        //                GameUI.instance.WinUI.SetActive(true);
        //            }
        //            //if (PlayerList[i].GetComponent<Player>().specialBlocks.Any() == false)
        //            //{
        //            //    winner = PlayerList[i];
        //            //    GameUI.instance.WinUI.SetActive(true);
        //            //}
        //        }
        //    }
        //}
        #endregion

        //만약에 승리자가 선정되면 승리 UI 추가
        if (winner)
        GameUI.instance.WinUI.SetActive(true);
    }

    //관광지 독점 체크
    public void SpecialBlockMonopolyCheck()
    {

    }
    //라인 독점 체크
    public void LineMonopolyCheck()
    {
        for(int i=0; i<Line1BlockList.Count; i++)
        {
            if (Line1BlockList[i].GetComponent<BasicBlock>())
            {

            }
        }
    }
    //파산 체크
    public void BankruptCheck()
    {
        if (PlayerList.Count == 1)
        {
            winner = PlayerList[0];
            winType = "파산승리!";
        }
    }
    //랭크 계산
    public void RankCalCulate()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            PlayerList[i].GetComponent<Player>().playerRank = 1;
            for (int j = 0; j < PlayerList.Count; j++)
            {
                if (PlayerList[i].GetComponent<Player>().TotalMoney < PlayerList[j].GetComponent<Player>().TotalMoney)
                {
                    PlayerList[i].GetComponent<Player>().playerRank++;
                }
            }
        }
    }
    //턴 계산 함수
    public void turnCalculate()
    {
        if (turnIndex > PlayerList.Count - 1)
        {
            turnIndex -= PlayerList.Count;
        }
    }
    //턴 변경 함수
    public void ChangeCurrentTurnPlayer(int i)
    {

        currentTurnPlayer = PlayerList[i];
    }
}