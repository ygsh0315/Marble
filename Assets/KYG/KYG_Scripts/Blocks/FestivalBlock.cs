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
        print("FestivalBlock");
        player.GetComponent<Player>().TurnCheck();
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    // Casts the ray and get the first game object hit
        //    Physics.Raycast(ray, out hit);
        //    if (hit.transform.gameObject.GetComponent<BasicBlock>())
        //    {
        //        print(hit.transform.gameObject.name);
        //        hit.transform.gameObject.GetComponent<BasicBlock>().landMag *= 2;
        //        player.GetComponent<Player>().TurnCheck();
        //    }
        //}
    }
}
