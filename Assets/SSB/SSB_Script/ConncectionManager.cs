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

        //�����ߴ� �������� �����Ѵ�
        PhotonNetwork.ConnectUsingSettings();
    }

    //������ ���� ���� ����
    public override void OnConnected()
    {
        base.OnConnected();
        print("OnConnected");
    }
    //������ ���� ���� , �κ� ���� �� ���� ����
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("OnConnectedToMaster");

        //�г��� ����

        //�κ�����
        PhotonNetwork.JoinLobby();
    }

    //�κ����ӿ� �����ߴٸ� ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("OnJoinedLobby");
    }
    void Update()
    {
        
    }
}
