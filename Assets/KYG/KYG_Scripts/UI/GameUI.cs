using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class GameUI : MonoBehaviourPun
{
    public static GameUI instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public GameObject PurchaseUI;

    public GameObject SPurchaseUI;

    public GameObject TakeOverUI;

    public GameObject LandMarkUI;

    public GameObject TrapBlockUI;

    public GameObject FestivalUI;

    public GameObject StartUI;

    public GameObject TeleportUI;

    public GameObject WinUI;

    public GameObject GameStartUI;

    public GameObject[] PlayerUiList;
    // Start is called before the first frame update
    void Start()
    {
        PurchaseUI.SetActive(false);
        TakeOverUI.SetActive(false);
        LandMarkUI.SetActive(false);
        //TrapBlockUI.SetActive(false);
        WinUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Purchase(GameObject block, GameObject player)
    {
        PurchaseUI.SetActive(true);
        PurchaseUI.GetComponent<PurchaseUI>().process(block, player);
        
    }
    public void SPurchase(GameObject block, GameObject player)
    {
        SPurchaseUI.SetActive(true);
        SPurchaseUI.GetComponent<SPurchaseUI>().process(block, player);
    }
    public void LandMarkPurchase(GameObject block, GameObject player)
    {
        LandMarkUI.SetActive(true);
        LandMarkUI.GetComponent<LandMarkPurchaseUI>().process(block, player);
    }
    public void TakeOver(GameObject block, GameObject player)
    {
        TakeOverUI.SetActive(true);
        TakeOverUI.GetComponent<TakeOverUI>().process(block, player);
    }
    public void GameStart()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPCGameStart", RpcTarget.All);
            GameManager.instance.ChangeCurrentTurnPlayer();
        }
    }
    [PunRPC]
    void RPCGameStart()
    {
        GameStartUI.SetActive(false);
    }
}
