using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerMoney;
    public TextMeshProUGUI totalMoney;
    public TextMeshProUGUI playerRank;
    public GameObject isGone;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        isGone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            isGone.SetActive(false);
            playerName.text = player.GetComponent<PhotonView>().Owner.NickName;
            playerMoney.text = player.GetComponent<Player>().money.ToString();
            totalMoney.text = player.GetComponent<Player>().TotalMoney.ToString();
            playerRank.text = player.GetComponent<Player>().playerRank.ToString() + " À§";
        }
        else
        {
            isGone.SetActive(true);
        }
    }
}
