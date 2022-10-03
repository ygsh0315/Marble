using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Photon.Pun;
using UnityEngine.UI;
public class SellLandsUI : MonoBehaviourPun
{
    public TextMeshProUGUI lackMoney;
    public TextMeshProUGUI leftMoney;
    public Button SellBtn;
    public int overMoney;
    public int selectedPrice;
    public int totalMoney;
    public int charge;
    public Player player;
    public bool UIOn;
    Block SelectedBlock;
    public List<Block> selectedBlockList = new List<Block>();


    private void Update()
    {
        if (UIOn)
        {
            SelectLands();
        }
    }
    public void SelectLands()
    {
        player = GameManager.instance.currentTurnPlayer.GetComponent<Player>();
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseInfo;
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(mouseRay, out mouseInfo))
            {
                print("가리키는 대상: " + mouseInfo.transform.name);
                if (mouseInfo.transform)
                {
                    SelectedBlock = mouseInfo.transform.gameObject.GetComponent<Block>();
                    //Block SelectedBlock = GameObject.Find(mouseInfo.transform.name).GetComponent<Block>();
                    print(SelectedBlock);
                    if (SelectedBlock && SelectedBlock.LandOwner == player.gameObject)
                    {
                        if (!SelectedBlock.isSelected)
                        {
                            SelectedBlock.isSelected = true;
                            SelectedBlock.OutLine.SetActive(true);
                            selectedBlockList.Add(SelectedBlock);
                            selectedPrice += SelectedBlock.gameObject.GetComponent<BasicBlock>().totalLandPrice / 2;
                        }
                        else
                        {
                            SelectedBlock.isSelected = false;
                            SelectedBlock.OutLine.SetActive(false);
                            selectedBlockList.Remove(SelectedBlock);
                            selectedPrice -= SelectedBlock.gameObject.GetComponent<BasicBlock>().totalLandPrice / 2;
                        }
                        
                    }

                }
                SetText();
            }
            else
            {
                print("가리키는 대상 없음");
            }              
        }
        
       
    }
    public void SetText()
    {
        print("setText");
        overMoney = selectedPrice + player.money - charge;
        lackMoney.text = (-(charge - player.money - selectedPrice)).ToString();
        leftMoney.text = overMoney.ToString();
        if (overMoney < 0)
        {
            leftMoney.text = 0.ToString();
        }
        else
        {
            lackMoney.text = 0.ToString();
        }
           
    }
    
    public void OnBankruptBtn()
    {
        gameObject.SetActive(false);
        player.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, -(totalMoney + 1));
        UIOn = false;
    }
    public void OnAutoSelectBtn()
    {
        for(int i = 0; i<player.ownLandList.Count; i++)
        {
            if (overMoney < 0)
            {
                SelectedBlock = player.ownLandList[i].GetComponent<Block>();
                SelectedBlock.isSelected = true;
                SelectedBlock.OutLine.SetActive(true);
                selectedBlockList.Add(SelectedBlock);
                selectedPrice += SelectedBlock.gameObject.GetComponent<BasicBlock>().totalLandPrice / 2;
                SetText();
            }
            else
            {
                break;
            }
        }
    }
    public void OnSellBtn()
    {
        if(charge - player.money - selectedPrice>0)
        {
            SellBtn.interactable = false;
        }
        else
        {
            SellBtn.interactable = true;
        }
        player.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, selectedPrice);
        player.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, -charge); 
        GameManager.instance.MapList[player.currentMapIndex].GetComponent<Block>().LandOwner.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, charge);
        for (int i = 0; i < selectedBlockList.Count; i++)
        {
            selectedBlockList[i].LandOwner = null;
            if (selectedBlockList[i].gameObject.GetComponent<BasicBlock>())
            {
                photonView.RPC("RPCOnSellBtn", RpcTarget.All, selectedBlockList[i].GetComponent<Block>().blockID);
            }
        }
        gameObject.SetActive(false);
        UIOn = false;
        player.GetComponent<Player>().TurnCheck();
    }
    [PunRPC]
    public void RPCOnSellBtn(int blockId)
    {
        BasicBlock block = (BasicBlock)GameManager.instance.GetBlock(blockId);
        block.OnSellBtn();
    }
}
