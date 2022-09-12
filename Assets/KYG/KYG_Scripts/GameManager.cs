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
    public int round = 1;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;
    public GameObject Map;
    public int mapCount;
    public List<GameObject> MapList = new List<GameObject>();
    public int currentMapIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        mapCount = Map.transform.childCount;
        for (int i = 0; i< mapCount; i++)
        {
            MapList.Add(Map.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerMovee(int dice1, int dice2)
    {
        StartCoroutine(IEMove(dice1, dice2));
    }
    //public void PlayerMove(int dice1, int dice2)
    //{
        //int destinationIndex = dice1 + dice2;
        //for (int i = 1; i<= destinationIndex; i++)
        //{
            //if (currentMapIndex + i > 31)
            //{
                //currentMapIndex -= 32;
            //}
            //Player1.transform.position = MapList[currentMapIndex + i].transform.position + new Vector3(0,1.5f,0);
            //StartCoroutine(IEMove(dice1, dice2));
        //}
        //currentMapIndex += destinationIndex;
    //}

    IEnumerator IEMove(int dice1, int dice2)
    {
        int destinationIndex = dice1 + dice2;
        for (int i = 1; i <= destinationIndex; i++)
        {
            if (currentMapIndex + i > 31)
            {
                currentMapIndex -= 32;
            }
            Player1.transform.position = MapList[currentMapIndex + i].transform.position + new Vector3(0, 1.5f, 0);
            yield return new WaitForSeconds(0.5f);
        }
        currentMapIndex += destinationIndex;
    }
}
