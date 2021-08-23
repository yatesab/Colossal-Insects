using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigTracker : MonoBehaviour
{
    private Transform playerRig;

    // Start is called before the first frame update
    void Start()
    {
        playerRig = GameObject.Find("/PlayerRig").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Follow Rigs Position
        transform.position = playerRig.position;

        // Follow Rigs Rotation
        transform.rotation = playerRig.rotation;
    }
}
