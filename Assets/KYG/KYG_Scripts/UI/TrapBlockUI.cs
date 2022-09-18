using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TrapBlockUI : MonoBehaviour
{
    public Button payMoney;
    public GameObject playerInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void process(GameObject player)
    {
        playerInfo = player;
        if (player.GetComponent<Player>().money < 200000)
        {
            payMoney.interactable = false;
        }
    }

    public void OnPayMoneyBtn()
    {
        playerInfo.GetComponent<Player>().money -= 200000;
        playerInfo.GetComponent<Player>().trapCount = 0;
    }

    public void OnPayCardBtn()
    {
        print("payCard");
    }

    public void OnRollDiceBtn()
    {

    }
}
