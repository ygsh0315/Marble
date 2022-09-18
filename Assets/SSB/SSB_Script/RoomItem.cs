using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    // 내용 (방 이름 (0/0))
    public Text roomInfo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(string roomName, int currPlayer, byte maxPlayer)
    {
        roomInfo.text = roomName + " (" + currPlayer + " / " + maxPlayer + ")";
    }
}
