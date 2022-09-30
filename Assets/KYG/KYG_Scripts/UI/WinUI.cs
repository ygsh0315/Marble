using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class WinUI : MonoBehaviour
{
    public TextMeshProUGUI winner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.winner)
        {
            winner.text = GameManager.instance.winner.GetComponent<PhotonView>().Owner.NickName +" " +GameManager.instance.winType;

        }
    }
}
