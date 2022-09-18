using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
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

    public GameObject TakeOverUI;

    public GameObject LandMarkUI;

    public GameObject WinUI;
    // Start is called before the first frame update
    void Start()
    {
        PurchaseUI.SetActive(false);
        TakeOverUI.SetActive(false);
        LandMarkUI.SetActive(false);
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
}
