using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    // 내용 (방 이름 (0/0))
    public Text roomInfo;

    //클릭이 되었을 때 호출되는 함수를 가지고 있는 변수
    public System.Action<string> onClickAction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(string roomName, int currPlayer, byte maxPlayer)
    {
        //게임오브젝트의 이름을 roomName으로 하겠다
        name = roomName; 
       //방이름(0/0
        roomInfo.text = roomName + " (" + currPlayer + " / " + maxPlayer + ")";
    }

    public void Onclick()
    {
        //만약 onClickAction가 null이 아니라면
        if(onClickAction != null)
        {
            //onClickAction 실행
            onClickAction(name);

        }

        //1.InputRoomName게임오브젝트 찾자
        GameObject go = GameObject.Find("InputRoomName");
        //2.InputField컴포넌트 가져오자
        InputField inputField = go.GetComponent<InputField>();
        //3.text에 roomName 넣자
        inputField.text = name;
    }
}
