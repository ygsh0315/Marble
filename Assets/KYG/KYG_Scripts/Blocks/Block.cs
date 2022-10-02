using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block : MonoBehaviour
{
    //∂• ¡÷¿Œ
    public GameObject LandOwner;
    public int blockID;
    public GameObject OutLine;
    public bool isSelected;
    public virtual void OnBlock(GameObject player)
    {
    }
}