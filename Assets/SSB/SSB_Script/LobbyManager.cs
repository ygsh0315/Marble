using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    // ���̸� InputField
    public InputField inputRoomName;
    // ���ο� InputField
    public InputField inputMaxPlayer;
    // �ִ� �ο�
    public int countMaxPlayer;

    // ������ Button
    public Button btnJoin;
    // ����� Button
    public Button btnCreate;
  
    void Start()
    {
        //���̸�(InputField ����)�� ����� �� ȣ��Ǵ� �Լ� ���
        inputRoomName.onValueChanged.AddListener(OnRoomNamevalueChanged);
        //���ο�(InputField ����)�� ����� �� ȣ��Ǵ� �Լ� ���
        inputMaxPlayer.onValueChanged.AddListener(OnMaxPlayervalueChanged);
    }

    public void OnRoomNamevalueChanged(string s)
    {
        //����
        btnJoin.interactable = s.Length > 0;
        //����, ���ο� (�ؽ�Ʈ ���̰� 0���� ũ��, ���ο��� 4�� ����)
        btnCreate.interactable = s.Length > 0 && inputMaxPlayer.text.Length > 0 && countMaxPlayer <= 4;
    }

    public void OnMaxPlayervalueChanged(string s)
    {
        btnCreate.interactable = s.Length > 0 && inputRoomName.text.Length > 0;
    }

    void Update()
    {
        
    }
    //�����
    public void CreateRoom()
    {
        //��ɼ� ����
        RoomOptions roomOptions = new RoomOptions();

        //�ִ��ο�
        roomOptions.MaxPlayers = byte.Parse(inputMaxPlayer.text);
        //�� ��Ͽ� ���̳�? ������ �ʴ���?
        roomOptions.IsVisible = true;

        //���� �����
        PhotonNetwork.CreateRoom(inputRoomName.text, roomOptions);
    }

    //�� ���� �Ϸ�
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }
    //�� ���� ����
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed" + returnCode + "," + message);
        
    }

    //�� ���� ��û
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(inputRoomName.text);
    }

    //�� ������ �Ϸ� �Ǿ��� �� ȣ��Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel("GameScene");
    }

    //�� ������ ���� �Ǿ��� �� ȣ��Ǵ� �Լ�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        print("OnJoinRandomFailed, " + returnCode + ", " + message);
    }

    //�濡 ���� ������ ����Ǹ� ȣ��Ǵ� �Լ� (�߰�/����/����)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        for (int i = 0; i < roomList.Count; i++)
            print(roomList[i].Name);
    }
}
