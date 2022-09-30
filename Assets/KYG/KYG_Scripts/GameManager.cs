
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
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
    public List<Block> BlockList = new List<Block>();
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
        turnIndex = -1;
        MapSet();
        PlayerSet();
        Festival();
    }
    // Update is called once per frame
    void Update()
    {
        TrapCheck();
        
        Winner();
        //turnCalculate();
        //ChangeCurrentTurnPlayer(turnIndex);
        ColorMonopolyCheck();
        RankCalCulate();
    }

    private void Festival()
    {
        for (int i = 0; i < FestivalList.Count; i++)
        {
            FestivalList[i].GetComponent<BasicBlock>().landMag *= 2;
        }
    }
    private void PlayerSet()
    {
        //PlayerList.Add(Player1);
        //PlayerList.Add(Player2);
        //PlayerList.Add(Player3);
        //PlayerList.Add(Player4);
        PhotonNetwork.Instantiate("Player", MapList[0].transform.position + new Vector3(0, 1.5f, 0), Quaternion.Euler(0,-135,0));
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

            Block block = MapList[i].GetComponent<Block>(); 
           
            block.blockID = i;
            BlockList.Add(block);

        }
        for (int i = 1; i < 8; i++)
        {
            if (MapList[i].GetComponent<BasicBlock>() || MapList[i].GetComponent<SpecialBlock>())
                Line1BlockList.Add(MapList[i]);
        }
        for (int i = 9; i < 16; i++)
        {
            if (MapList[i].GetComponent<BasicBlock>() || MapList[i].GetComponent<SpecialBlock>())
                Line2BlockList.Add(MapList[i]);
        }
        for (int i = 17; i < 24; i++)
        {
            if (MapList[i].GetComponent<BasicBlock>() || MapList[i].GetComponent<SpecialBlock>())
                Line3BlockList.Add(MapList[i]);
        }
        for (int i = 25; i < 32; i++)
        {
            if (MapList[i].GetComponent<BasicBlock>() || MapList[i].GetComponent<SpecialBlock>())
                Line4BlockList.Add(MapList[i]);
        }
        //축제블럭 리스트를 랜덤으로 섞고 3개를 추출
        FestivalList = FestivalList.OrderBy(a => Guid.NewGuid()).Take(3).ToList();

    }
    private void Winner()
    {
        if (GameUI.instance.GameStartUI.activeSelf) return;
        BankruptCheck();
        #region 정훈이형 코드
        LineMonopolyCheck();
        SpecialMonopolyCheck();
        #endregion
        if (winner)
            GameUI.instance.WinUI.SetActive(true);
    }

    public void LineMonopolyCheck()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            if (PlayerList[i])
            {
                if (PlayerList[i].GetComponent<Player>().line == false)
                {
                    if (PlayerList[i].GetComponent<Player>().lineOne.Any() == false || PlayerList[i].GetComponent<Player>().lineTwo.Any() == false || PlayerList[i].GetComponent<Player>().lineThree.Any() == false || PlayerList[i].GetComponent<Player>().lineFour.Any() == false)
                    {
                        winner = PlayerList[i];
                        winType = "라인 독점!";
                        GameUI.instance.WinUI.SetActive(true);
                    }
                }
            }
        }
    }

    public void ColorMonopolyCheck()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            if (PlayerList[i].GetComponent<Player>().colorCount >= 3)
            {
                winner = PlayerList[i];
                winType = "컬러 독점!";
                GameUI.instance.WinUI.SetActive(true);
            }
        }
    }
    public void SpecialMonopolyCheck()
    {

        for (int i = 0; i < PlayerList.Count; i++)
        {
            if (PlayerList[i])
            {
                if (PlayerList[i].GetComponent<Player>().line == false)
                {
                    if (PlayerList[i].GetComponent<Player>().specialBlocks.Any() == false)
                    {
                        winner = PlayerList[i];
                        GameUI.instance.WinUI.SetActive(true);
                        winType = "관광지 독점!";
                    }
                }
            }
        }

    }


    public void BankruptCheck()
    {
        if (PlayerList.Count == 1)
        {
            winner = PlayerList[0];
            winType = "파산승리!";
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
    public void turnCalculate()
    {
        turnIndex++;
        if (turnIndex > PlayerList.Count - 1)
        {
            turnIndex -= PlayerList.Count;
        }
        if (turnIndex < 0)
        {
            turnIndex = PlayerList.Count - 1;
        }
        
    }
    public void ChangeCurrentTurnPlayer()
    {
        photonView.RPC("RpcChangeCurrentTurnPlayer", RpcTarget.All);
    }

    [PunRPC]
    public void RpcChangeCurrentTurnPlayer()
    {
        if (PlayerList.Count <= 0) return;
        turnCalculate();
        int i = turnIndex;
        currentTurnPlayer = PlayerList[i];
        if (currentTurnPlayer)
        {
            if (currentTurnPlayer.GetComponent<Player>().trapCount >= 4)
            {
                currentTurnPlayer.GetComponent<Player>().isTraped = false;
                currentTurnPlayer.GetComponent<Player>().trapCount = 0;
            }
            if (currentTurnPlayer.GetComponent<Player>().isTraped == true)
            {
                currentTurnPlayer.GetComponent<Player>().trapCount++;

                currentTurnPlayer.GetComponent<Player>().state = Player.PlayerState.End;
            }
        }
    }
    public void TrapCheck()
    {
        if (currentTurnPlayer)
        {
           // print(currentTurnPlayer);
            //if (currentTurnPlayer.GetComponent<Player>().isTraped == true)
            //{
            //    currentTurnPlayer.GetComponent<Player>().state = Player.PlayerState.End;
            //}
        }
    }

    public Material[] playerMat;
    public void AddPlayer(GameObject player)
    {
        PlayerList.Add(player);

        //정렬
        PlayerList.Sort(SortByViewID);

        //자기색 셋팅
        for(int i = 0; i < PlayerList.Count; i++)
        {
            PlayerList[i].GetComponent<Player>().myColor = playerMat[i];
            GameUI.instance.PlayerUiList[i].GetComponent<PlayerUI>().player = PlayerList[i];
        }
    }

    int SortByViewID(GameObject g1, GameObject g2)
    {
        PhotonView pv1 = g1.GetComponent<PhotonView>();
        PhotonView pv2 = g2.GetComponent<PhotonView>();
        if (pv1.ViewID > pv2.ViewID)
        {
            return 1;
        }

        else if (pv1.ViewID < pv2.ViewID)
        {
            return -1;
        }

        return 0;
    }

    public Block GetBlock(int blockId)
    {
        for(int i = 0; i < BlockList.Count; i++)
        {
            if(BlockList[i].blockID == blockId)
            {
                return BlockList[i];
            }
        }
        return null;
    }

    public GameObject GetPlayer(int viewId)
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            if (PlayerList[i].GetComponent<PhotonView>().ViewID == viewId)
            {
                return PlayerList[i];
            }
        }
        return null;
    }
}