using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public void LoadSceneWithIntegerID(int whichScene)
    {
        SceneManager.LoadScene(whichScene);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            SceneManager.LoadScene(2);
            //Debug.Log("Working");
        }
    }


}

