using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ConncectionManager : MonoBehaviourPunCallbacks
{
    // �г��� InputField
    public InputField inputNickName;
    // ���� Button
    public Button BtnConnect;

    void Start()
    {
        //�г���(InputField ����)�� ����� �� ȣ��Ǵ� �Լ� ���
        inputNickName.onValueChanged.AddListener(OnvalueChanged);
        //�г���(InputField)���� Enter�� ������ ȣ��Ǵ� �Լ� ���
        inputNickName.onSubmit.AddListener(Onsubmit);
        //�г���(InputField)���� Focusing�� �Ҿ����� ȣ��Ǵ� �Լ� ���
        inputNickName.onEndEdit.AddListener(OnEndEdit);
    }

    public void OnvalueChanged(string s)
    {
        //���࿡ s�� ���̰� 0���� ũ�ٸ�
        if (s.Length > 0)
        {
            //���ӹ�ư�� Ȱ��ȭ �ϰڴ�
            BtnConnect.interactable = true;

        }
        //�ƴ϶��
        else
        {
            //���ӹ�ư�� ��Ȱ��ȭ �ϰڴ�
            BtnConnect.interactable = false;
        }
        print("OnvalueChanged : " + s);
    }

    public void Onsubmit(string s)
    {
        //���࿡ s�� ���̰� 0���� ũ�ٸ�
        if(s.Length >0)
        {
            //��������
            OnClickConnect();
        }
        print("Onsubmit" + s);
    }

    public void OnEndEdit(string s)
    {
        print("OnEndEdit" + s);
    }

    public void OnClickConnect()
    {
        //���� ���� ��û
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
        PhotonNetwork.NickName = "Player" + Random.Range(1, 1000);/*inputNickName.text*/;
        //�κ�����
        PhotonNetwork.JoinLobby();
    }

    //�κ����ӿ� �����ߴٸ� ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("OnJoinedLobby");
        //LobbyScene���� �̵�
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    
    void Update()
    {
        
    }
}
