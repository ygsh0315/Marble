using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenOdd : MonoBehaviour
{
    public bool even = false;
    public bool odd = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        if(gameObject.name == "Even")
        {
            even = true;
        }
        else if(gameObject.name == "Odd")
        {
            odd = true;
        }

    }
}
