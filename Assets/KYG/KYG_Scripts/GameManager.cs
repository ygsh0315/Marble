
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
        if(!instance)
        {
            instance = this;
        }
    }
    public int startMoney = 2000000;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;
    public GameObject winner;
    public GameObject Map;
    public int mapCount;
    public int turnIndex;
    public List<GameObject> MapList = new List<GameObject>();
    public List<GameObject> FestivalList = new List<GameObject>();
    public List<GameObject> PlayerList = new List<GameObject>();
    public GameObject currentTurnPlayer;
    // Start is called before the first frame update
    void Start()
    {
        mapCount = Map.transform.childCount;
        for (int i = 0; i< mapCount; i++)
        {
            MapList.Add(Map.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i<mapCount; i++)
        {
            if (MapList[i].GetComponent<BasicBlock>())
            {
                FestivalList.Add(MapList[i]);
            }
        }
        PlayerList.Add(Player1);
        PlayerList.Add(Player2);
        PlayerList.Add(Player3);
        PlayerList.Add(Player4);

        FestivalList = FestivalList.OrderBy(a => Guid.NewGuid()).Take(3).ToList();
        Festival();
    }

    // Update is called once per frame
    void Update()
    {
        
        Winner();
        turnCalculate();
        ChangeCurrentTurnPlayer(turnIndex);
        RankCalCulate();
    }

    private void Festival()
    {
        for(int i = 0; i<FestivalList.Count; i++)
        {
            FestivalList[i].GetComponent<BasicBlock>().landMag *= 2;
        }
    }

    private void Winner()
    {
        if(PlayerList.Count == 1)
        {
            winner = PlayerList[0];
            GameUI.instance.WinUI.SetActive(true);
        }
    }

    public void turnCalculate()
    {
        if (turnIndex > PlayerList.Count - 1)
        {
            turnIndex -= PlayerList.Count;
        }
    }
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
    public void ChangeCurrentTurnPlayer(int i)
    {
        
        currentTurnPlayer = PlayerList[i];
    }
}
