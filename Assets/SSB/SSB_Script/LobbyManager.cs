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

    //�������� Dictionary (key������ �ش�Ǵ� value���� ã�´�)
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();

    //�� ����Ʈ Content
    public Transform trListContent;

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
        btnCreate.interactable = s.Length > 0 && inputMaxPlayer.text.Length > 0;
    }

    public void OnMaxPlayervalueChanged(string s)
    {
        if (int.Parse(s) > 4) inputMaxPlayer.text = 4.ToString();
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
        //custom������ ����
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        


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

        //�븮��Ʈ UI�� ��ü ���� 
        DeleteRoomListUI();
        //�븮��Ʈ ������ ������Ʈ
        UpdateRoomCache(roomList);
        //�븮��Ʈ UI�� ��ü ����
        CreateRoomListUI();

    }
    void DeleteRoomListUI()
    {

        foreach(Transform tr in trListContent) //foreach�� trListContent�� �ڽĵ��� tr�� ����ش�. �ڽ��� �� ���������� �ݺ����� �Ѵ�.(�ڽ� ������ŭ)
        {
            Destroy(tr.gameObject);
        }
    }

    void UpdateRoomCache(List<RoomInfo>roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            //����, ����
            if(roomCache.ContainsKey(roomList[i].Name)) //roomList�� ���鼭 ����� ���׵��� ĳ�ÿ� ���ִ��� Ȯ��
            {
                //���࿡ �ش� ���� ������ ���̶��
                if(roomList[i].RemovedFromList) //true�� ���� ������� 
                {
                    //roomCache���� �ش� ������ ����
                    roomCache.Remove(roomList[i].Name);

                }
                //�׷��� �ʴٸ� (�ش� ���� �������� �ʾҴٸ�)
                else
                {
                    //���� ����
                    roomCache[roomList[i].Name] = roomList[i];

                }
            }
            //�߰�
            else 
            {
                    roomCache[roomList[i].Name] = roomList[i];
            }
        }
    }

    public GameObject roomItemFactory;
    void CreateRoomListUI()
    {
        foreach (RoomInfo info in roomCache.Values) //�̰� ���ظ���
        {
            //�� �������� �����.
            GameObject go = Instantiate(roomItemFactory, trListContent); //trListContent�� �θ�
            //�� ������ ������ ����(������(0/0))
            RoomItem item = go.GetComponent<RoomItem>();
            item.SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);

            //roomItem��ư�� Ŭ���Ǹ� ȣ��Ǵ� �Լ� ���
            item.onClickAction = SetRoomName;
        }

        void SetRoomName(string room)
        {
            inputRoomName.text = room;
        }
    }
}
