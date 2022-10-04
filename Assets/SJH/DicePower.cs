using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePower : MonoBehaviour
{
    [SerializeField]
    private Slider diceSlider;
  
    public float curPower;
    public int maxPower;


    public float power
    {
        get
        {
            return diceSlider.value;
        }
        set
        {
            curPower = value;
            diceSlider.value = value;
        }
    }



    public bool diceButton = true;
    // Start is called before the first frame update
    void Start()
    {
        diceSlider.value = 0;
        diceSlider.maxValue = maxPower;
        diceButton = true;
    }
    bool start = true;
    bool end = false;
    float currentTime = 0;
    float createTime = 0.005f;
    // Update is called once per frame
    void Update()
    {
        if (diceSlider.value <= 100 && start == true && diceButton == false)
        {
            
            currentTime += Time.deltaTime;
            if (currentTime > createTime)
            {
                diceSlider.value++;
                currentTime = 0;
            }
            if (diceSlider.value == 100)
            {
                start = false;
            }
        }
        if (diceSlider.value >= 0 && start == false && diceButton == false)
        {
            currentTime += Time.deltaTime;
            if (currentTime > createTime)
            {
                diceSlider.value--;
                currentTime = 0;
            }
            if (diceSlider.value ==0)
            {
                start = true;
            }
        }
        if(diceButton == true)
        {
            //print(power);
        }
        //else if (diceSlider.value == diceSlider.maxValue)
        //{
        //    currentTime += Time.deltaTime;

        //}
    }
}
