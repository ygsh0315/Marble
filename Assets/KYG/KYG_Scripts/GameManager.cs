using System.Collections;
using System.Collections.Generic;
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
    public GameObject Map;
    public int mapCount;
    public int turnIndex;
    public List<GameObject> MapList = new List<GameObject>();
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
        PlayerList.Add(Player1);
        PlayerList.Add(Player2);
        PlayerList.Add(Player3);
        PlayerList.Add(Player4);
    }

    // Update is called once per frame
    void Update()
    {
        if (turnIndex > PlayerList.Count - 1)
        {
            turnIndex -= PlayerList.Count;
        }
        ChangeCurrentTurnPlayer(turnIndex);
    }

    public void ChangeCurrentTurnPlayer(int i)
    {
        
        currentTurnPlayer = PlayerList[i];
    }
}
