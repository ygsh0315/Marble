using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    // 방이름 InputField
    public InputField inputRoomName;
    // 총인원 InputField
    public InputField inputMaxPlayer;
    // 최대 인원
    public int countMaxPlayer;

    // 방참가 Button
    public Button btnJoin;
    // 방생성 Button
    public Button btnCreate;
  
    void Start()
    {
        //방이름(InputField 내용)이 변경될 때 호출되는 함수 등록
        inputRoomName.onValueChanged.AddListener(OnRoomNamevalueChanged);
        //총인원(InputField 내용)이 변경될 때 호출되는 함수 등록
        inputMaxPlayer.onValueChanged.AddListener(OnMaxPlayervalueChanged);
    }

    public void OnRoomNamevalueChanged(string s)
    {
        //참가
        btnJoin.interactable = s.Length > 0;
        //생성, 총인원 (텍스트 길이가 0보다 크고, 총인원은 4명 이하)
        btnCreate.interactable = s.Length > 0 && inputMaxPlayer.text.Length > 0 && countMaxPlayer <= 4;
    }

    public void OnMaxPlayervalueChanged(string s)
    {
        btnCreate.interactable = s.Length > 0 && inputRoomName.text.Length > 0;
    }

    void Update()
    {
        
    }
    //방생성
    public void CreateRoom()
    {
        //방옵션 셋팅
        RoomOptions roomOptions = new RoomOptions();

        //최대인원
        roomOptions.MaxPlayers = byte.Parse(inputMaxPlayer.text);
        //룸 목록에 보이냐? 보이지 않느냐?
        roomOptions.IsVisible = true;

        //방을 만든다
        PhotonNetwork.CreateRoom(inputRoomName.text, roomOptions);
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
        PhotonNetwork.JoinRoom(inputRoomName.text);
    }

    //방 참가가 완료 되었을 때 호출되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel("GameScene");
    }

    //방 참가가 실패 되었을 때 호출되는 함수
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        print("OnJoinRandomFailed, " + returnCode + ", " + message);
    }

    //방에 대한 정보가 변경되면 호출되는 함수 (추가/삭제/수정)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        for (int i = 0; i < roomList.Count; i++)
            print(roomList[i].Name);
    }
}
