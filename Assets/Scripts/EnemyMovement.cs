using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyMovement : ActorMovement
{
    private GameObject cachedPlayerReference;
    private float stopChasingDistance = 1.0f;
    void Update()
    {
        // Default movement is zero.
        mx = 0;

        // Seek the player!
        if (cachedPlayerReference == null)
        {
            cachedPlayerReference = GameObject.FindGameObjectWithTag("Player");
        }


        // TODO: Only seek if the player's close enough...
        // Set a movement in the direction of the player, if we can see him!
        if (cachedPlayerReference != null)
        {
            if (cachedPlayerReference.transform.position.x > (this.transform.position.x + stopChasingDistance))
            {
                // Player is to our right - go right
                mx = 1;
            } else if (cachedPlayerReference.transform.position.x < (this.transform.position.x - stopChasingDistance))
            {
                // Player is to our left - go left
                mx = -1;
            }
        }
    }

    public void LoadSceneWithIntegerID(int whichScene)
    {
        SceneManager.LoadScene(whichScene);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            SceneManager.LoadScene(3);
        }
    }
    
      
    }





