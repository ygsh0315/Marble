using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FestivalBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnFestivalBlock(Transform player)
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);
            Debug.Log("This hit at " + hit.point);
            if (hit.transform.gameObject.GetComponent<BasicBlock>())
            {
                hit.transform.gameObject.GetComponent<BasicBlock>().landMag *= 2;
            }
        }
    }
}
