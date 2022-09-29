using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ConncectionManager : MonoBehaviourPunCallbacks
{
    // 닉네임 InputField
    public InputField inputNickName;
    // 접속 Button
    public Button BtnConnect;

    void Start()
    {
        //닉네임(InputField 내용)이 변경될 때 호출되는 함수 등록
        inputNickName.onValueChanged.AddListener(OnvalueChanged);
        //닉네임(InputField)에서 Enter를 쳤을때 호출되는 함수 등록
        inputNickName.onSubmit.AddListener(Onsubmit);
        //닉네임(InputField)에서 Focusing을 잃었을때 호출되는 함수 등록
        inputNickName.onEndEdit.AddListener(OnEndEdit);
    }

    public void OnvalueChanged(string s)
    {
        //만약에 s의 길이가 0보다 크다면
        if (s.Length > 0)
        {
            //접속버튼을 활성화 하겠다
            BtnConnect.interactable = true;

        }
        //아니라면
        else
        {
            //접속버튼을 비활성화 하겠다
            BtnConnect.interactable = false;
        }
        print("OnvalueChanged : " + s);
    }

    public void Onsubmit(string s)
    {
        //만약에 s의 길이가 0보다 크다면
        if(s.Length >0)
        {
            //접속하자
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
        //서버 접속 요청
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
        PhotonNetwork.NickName = "Player" + Random.Range(1, 1000);/*inputNickName.text*/;
        //로비진입
        PhotonNetwork.JoinLobby();
    }

    //로비접속에 성공했다면 호출
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("OnJoinedLobby");
        //LobbyScene으로 이동
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    
    void Update()
    {
        
    }
}
