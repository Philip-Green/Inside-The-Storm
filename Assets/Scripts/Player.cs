using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Platform currentPlatform;
    public GameObject gameManager;
    public int playerX;

    public int playerZ;
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    public void SetPlayerPosition(int x, int z)
    {
        Debug.Log("In SetPlayerPosition! Moving to " + x + "/" + z);
        playerX = x;
        playerZ = z;

        this.transform.position = new Vector3(playerX*6, 0, playerZ*6);
        
        SetCurrentPlatform(GameManager.Instance.GetPlatformAt(playerX,playerZ));
       
    }

    public void SetCurrentPlatform(Platform plat)
    {
        if (plat == null)
        {
            Debug.Log("Set current platform found null!");
        }
        else
        {
            currentPlatform = plat;
            currentPlatform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }
    }
}
