using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DiceNumberTextScript : MonoBehaviour
{
    TextMeshProUGUI text;
    public static int diceNumber;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = diceNumber.ToString();
    }
}
