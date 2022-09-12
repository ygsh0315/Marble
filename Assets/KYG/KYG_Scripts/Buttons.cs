using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI dice1Number;
    public TextMeshProUGUI dice2Number;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RollDice()
    {
        int dice1 = Random.Range(1, 7);
        int dice2 = Random.Range(1, 7);
        dice1Number.text = dice1.ToString();
        dice2Number.text = dice2.ToString();
        GameManager.instance.PlayerMove(dice1, dice2);
    }
}
