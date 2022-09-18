using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    // ���� (�� �̸� (0/0))
    public Text roomInfo;

    //Ŭ���� �Ǿ��� �� ȣ��Ǵ� �Լ��� ������ �ִ� ����
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
        //���ӿ�����Ʈ�� �̸��� roomName���� �ϰڴ�
        name = roomName; 
       //���̸�(0/0
        roomInfo.text = roomName + " (" + currPlayer + " / " + maxPlayer + ")";
    }

    public void Onclick()
    {
        //���� onClickAction�� null�� �ƴ϶��
        if(onClickAction != null)
        {
            //onClickAction ����
            onClickAction(name);

        }

        //1.InputRoomName���ӿ�����Ʈ ã��
        GameObject go = GameObject.Find("InputRoomName");
        //2.InputField������Ʈ ��������
        InputField inputField = go.GetComponent<InputField>();
        //3.text�� roomName ����
        inputField.text = name;
    }
}
