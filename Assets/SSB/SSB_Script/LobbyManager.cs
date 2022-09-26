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

    //방정보들 Dictionary (key값으로 해당되는 value값을 찾는다)
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();

    //룸 리스트 Content
    public Transform trListContent;

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
    //방생성
    public void CreateRoom()
    {
        //방옵션 셋팅
        RoomOptions roomOptions = new RoomOptions();

        //최대인원
        roomOptions.MaxPlayers = byte.Parse(inputMaxPlayer.text);
        //룸 목록에 보이냐? 보이지 않느냐?
        roomOptions.IsVisible = true;
        //custom정보를 셋팅
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        


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

        //룸리스트 UI를 전체 삭제 
        DeleteRoomListUI();
        //룸리스트 정보를 업데이트
        UpdateRoomCache(roomList);
        //룸리스트 UI를 전체 생성
        CreateRoomListUI();

    }
    void DeleteRoomListUI()
    {

        foreach(Transform tr in trListContent) //foreach는 trListContent의 자식들을 tr에 담아준다. 자식을 다 담을때까지 반복문을 한다.(자식 개수만큼)
        {
            Destroy(tr.gameObject);
        }
    }

    void UpdateRoomCache(List<RoomInfo>roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            //수정, 삭제
            if(roomCache.ContainsKey(roomList[i].Name)) //roomList를 돌면서 변경된 사항들이 캐시에 들어가있는지 확인
            {
                //만약에 해당 룸이 삭제된 것이라면
                if(roomList[i].RemovedFromList) //true면 방이 사라진것 
                {
                    //roomCache에서 해당 정보를 삭제
                    roomCache.Remove(roomList[i].Name);

                }
                //그렇지 않다면 (해당 룸이 삭제되지 않았다면)
                else
                {
                    //정보 수정
                    roomCache[roomList[i].Name] = roomList[i];

                }
            }
            //추가
            else 
            {
                    roomCache[roomList[i].Name] = roomList[i];
            }
        }
    }

    public GameObject roomItemFactory;
    void CreateRoomListUI()
    {
        foreach (RoomInfo info in roomCache.Values) //이건 이해못함
        {
            //룸 아이템을 만든다.
            GameObject go = Instantiate(roomItemFactory, trListContent); //trListContent가 부모
            //룸 아이템 정보를 셋팅(방제목(0/0))
            RoomItem item = go.GetComponent<RoomItem>();
            item.SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);

            //roomItem버튼이 클릭되면 호출되는 함수 등록
            item.onClickAction = SetRoomName;
        }

        void SetRoomName(string room)
        {
            inputRoomName.text = room;
        }
    }
}
