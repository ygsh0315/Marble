using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        CreateRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //방생성
    public void CreateRoom()
    {
        //방옵션 셋팅
        RoomOptions roomOptions = new RoomOptions();

        //최대인원
        roomOptions.MaxPlayers = 10;
        //룸 목록에 보이냐? 보이지 않느냐?
        roomOptions.IsVisible = true;

        //방을 만든다
        PhotonNetwork.CreateRoom("모두의마블", roomOptions, TypedLobby.Default);
    }

    //방 생성 완료
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }
    //방 생성 실패
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed" + returnCode + "," + message);
        
    }

    //방 참가 요청
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("XR_B반");
    }

    //방 참가가 완료 되었을 때 호출되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
    }

    //방 참가가 실패 되었을 때 호출되는 함수
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        print("OnJoinRandomFailed, " + returnCode + ", " + message);
    }
}
