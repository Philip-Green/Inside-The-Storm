using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollowX : MonoBehaviour
{
    public Transform target;
    
    void Update()
    {
        // Just update the x pos every frame to match the target.
        transform.position = new Vector3(
            target.position.x,
            transform.position.y,
            transform.position.z
        );
        
    }
}
