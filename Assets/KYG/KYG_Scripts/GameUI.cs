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
    // Start is called before the first frame update
    void Start()
    {
        PurchaseUI.SetActive(false);
        TakeOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Purchase()
    {
        PurchaseUI.SetActive(true);
    }

    public void TakeOver()
    {
        TakeOverUI.SetActive(true);
    }
}