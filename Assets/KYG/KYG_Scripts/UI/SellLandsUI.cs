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
        print(1);
        if (Input.GetButtonDown("Fire1"))
        {
            print(2);
            if (Physics.Raycast(mouseRay, out mouseInfo))
            {
                print("가리키는 대상: " + mouseInfo.transform.name);
                if (mouseInfo.transform)
                {
                    print(3);
                    Block SelectedBlock = mouseInfo.transform.gameObject.GetComponent<Block>();
                    //Block SelectedBlock = GameObject.Find(mouseInfo.transform.name).GetComponent<Block>();
                    print(SelectedBlock);
                    if (SelectedBlock.LandOwner == player.gameObject)
                    {
                        print(4);
                        if (!SelectedBlock.isSelected)
                        {
                            print(5);
                            SelectedBlock.isSelected = true;
                            SelectedBlock.OutLine.SetActive(true);
                            selectedBlockList.Add(SelectedBlock);
                            selectedPrice += SelectedBlock.gameObject.GetComponent<BasicBlock>().totalLandPrice / 2;
                        }
                        else
                        {
                            print(6);
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
    private void SetText()
    {
        print("setText");
        overMoney = charge - totalMoney + selectedPrice;
        lackMoney.text = (charge - totalMoney).ToString();
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
    }
    public void OnAutoSelectBtn()
    {
        overMoney = charge - totalMoney + selectedPrice;
        for(int i = 0; i<player.ownLandList.Count; i++)
        {
            if (overMoney < 0)
            {
                Block autoSelectedBlock = player.ownLandList[i].GetComponent<Block>();
                autoSelectedBlock.isSelected = true;
                autoSelectedBlock.OutLine.SetActive(true);
                selectedBlockList.Add(autoSelectedBlock);
                selectedPrice += autoSelectedBlock.gameObject.GetComponent<BasicBlock>().totalLandPrice / 2;
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
        for (int i = 0; i<selectedBlockList.Count; i++)
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
            gameObject.SetActive(false);
            player.GetComponent<Player>().TurnCheck();
        }
    }
}
