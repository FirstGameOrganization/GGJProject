using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MapController.Instance.Load("Assets/XmlTileMapData/TestMap.xml");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}