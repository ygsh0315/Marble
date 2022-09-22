using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingColor : MonoBehaviour
{
    public Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
       materials = Resources.LoadAll<Material>("Color");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
