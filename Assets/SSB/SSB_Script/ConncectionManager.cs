using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ConncectionManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

        //연결했던 셋팅으로 접속한다
        PhotonNetwork.ConnectUsingSettings();
    }

    //마스터 서버 접속 성공
    public override void OnConnected()
    {
        base.OnConnected();
        print("OnConnected");
    }
    //마스터 접속 성공 , 로비 생성 및 진입 가능
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("OnConnectedToMaster");

        //닉네임 설정

        //로비진입
        PhotonNetwork.JoinLobby();
    }

    //로비접속에 성공했다면 호출
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("OnJoinedLobby");
    }
    void Update()
    {
        
    }
}
