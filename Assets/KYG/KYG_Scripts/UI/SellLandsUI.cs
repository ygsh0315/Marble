using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Photon.Pun;
public class SellLandsUI : MonoBehaviourPun
{
    public TextMeshProUGUI lackMoney;
    public TextMeshProUGUI leftMoney;
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
        player.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, selectedPrice);
        player.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, -charge);
        GameManager.instance.MapList[player.currentMapIndex].GetComponent<Block>().LandOwner.GetComponent<PhotonView>().RPC("RpcAddMoney", RpcTarget.All, charge);
        photonView.RPC("RPCOnSellBtn", RpcTarget.All);

        gameObject.SetActive(false);
        UIOn = false;
        player.GetComponent<Player>().TurnCheck();
    }
    [PunRPC]
    public void RPCOnSellBtn()
    {
        for (int i = 0; i < selectedBlockList.Count; i++)
        {
            selectedBlockList[i].LandOwner = null;
            if (selectedBlockList[i].gameObject.GetComponent<BasicBlock>())
            {
                selectedBlockList[i].GetComponent<BasicBlock>().land = false;
                selectedBlockList[i].GetComponent<BasicBlock>().landCount = 0;
                selectedBlockList[i].GetComponent<BasicBlock>().tear1 = false;
                selectedBlockList[i].GetComponent<BasicBlock>().tear1Count = 0;
                selectedBlockList[i].GetComponent<BasicBlock>().tear1Factory.SetActive(false);
                selectedBlockList[i].GetComponent<BasicBlock>().tear2 = false;
                selectedBlockList[i].GetComponent<BasicBlock>().tear2Count = 0;
                selectedBlockList[i].GetComponent<BasicBlock>().tear2Factory.SetActive(false);
                selectedBlockList[i].GetComponent<BasicBlock>().tear3 = false;
                selectedBlockList[i].GetComponent<BasicBlock>().tear3Count = 0;
                selectedBlockList[i].GetComponent<BasicBlock>().tear3Factory.SetActive(false);
                selectedBlockList[i].GetComponent<BasicBlock>().landMark = false;
                selectedBlockList[i].GetComponent<BasicBlock>().landMarkCount = 0;
                selectedBlockList[i].GetComponent<BasicBlock>().landMarkFactory.SetActive(false);
                selectedBlockList[i].OutLine.SetActive(false);
            }
            
        }
    }
}
