using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour
{

    public TextMeshProUGUI dice1Number;
    public TextMeshProUGUI dice2Number;
    public GameObject even;
    public GameObject odd;
    public GameObject diceOne;
    public GameObject diceTwo;
    DicePower dicePower;
    void Start()
    {
        diceOne = GameObject.Find("dice");
        diceTwo = GameObject.Find("diceTwo");
        dicePower = GetComponent<DicePower>();
        dice1Number = GameUI.instance.dice1Number.GetComponent<TextMeshProUGUI>();
        dice2Number = GameUI.instance.dice2Number.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //public void RollDice()
    //{
    //    int dice1 = Random.Range(6, 7);
    //    int dice2 = Random.Range(6, 7);
    //    dice1Number.text = dice1.ToString();
    //    dice2Number.text = dice2.ToString();
    //    GameManager.instance.currentTurnPlayer.GetComponent<Player>().PlayerMove(dice1, dice2);

    //}

    public void PointerDown()
    {
        dicePower.diceButton = false;
        dicePower.power = 0;
    }

    public void PointerUp()
    {
        int dice1 = 0;
        int dice2 = 0;
        
        //int dice1 = Random.Range(6, 7);
        //int dice2 = Random.Range(6, 7);
        int[] a = { 2, 4, 6 };
        int[] b = { 1, 3, 5 };
        float value = GetComponent<DicePower>().power;
        if (even.GetComponent<EvenOdd>().even == true)
        {
            dice1 = a[Random.Range(0, 3)];
            dice2 = a[Random.Range(0, 3)];
        }
        else if (odd.GetComponent<EvenOdd>().odd == true)
        {
            dice1 = a[Random.Range(0, 3)];
            dice2 = b[Random.Range(0, 3)];
        }
        else
        {
            if (value >= 0 && value <= 33)
            {
                dice1 = Random.Range(1, 5);
                dice2 = Random.Range(1, 5);
            }
            if (value >= 34 && value <= 66)
            {

                dice1 = Random.Range(2, 6);
                dice2 = Random.Range(2, 6);

            }
            if (value >= 67 && value <= 100)
            {

                dice1 = Random.Range(3, 7);
                dice2 = Random.Range(3, 7);
            }
        }
        if (GameManager.instance.cDice == true)
        {
            dice1 = GameManager.instance.cDice1;
            dice2 = GameManager.instance.cDice2;
        }
        dice1Number.text = dice1.ToString();
        dice2Number.text = dice2.ToString();
        GameManager.instance.currentTurnPlayer.GetComponent<Player>().PlayerMove(dice1, dice2);
        diceOne.GetComponent<DiceScript>().Dice(dice1);
        diceTwo.GetComponent<DiceScriptTwo>().Dice(dice2);
        dicePower.diceButton = true;
        even.GetComponent<EvenOdd>().even = false;
        odd.GetComponent<EvenOdd>().odd = false;
    }
}
