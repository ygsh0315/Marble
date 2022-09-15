using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePower : MonoBehaviour
{
    [SerializeField]
    private Slider diceSlider;
    int curPower;
    public int maxPower;

    //public int power
    //{
    //    get
    //    {
    //        return curPower;
    //    }
    //    set
    //    {
    //        curPower = value;
    //        diceSlider.value = value;
    //    }
    //}



    // Start is called before the first frame update
    void Start()
    {
        diceSlider.value = 0;
        diceSlider.maxValue = maxPower;
    }
    bool start = true;
    bool end = false;
    float currentTime = 0;
    float createTime = 0.01f;
    public bool diceButton = false;
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
            print(diceSlider.value);
        }
        //else if (diceSlider.value == diceSlider.maxValue)
        //{
        //    currentTime += Time.deltaTime;

        //}
    }
}
