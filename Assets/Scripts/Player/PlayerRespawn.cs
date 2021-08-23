using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float threshold = -100f;

    private Rigidbody _playerBody;

    // Start is called before the first frame update
    void Start()
    {
        _playerBody = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Easy hack for getting your player reset if you fall off
        // better respawn will be needed.
        if (playerTransform.position.y < threshold)
        {
            playerTransform.position = spawnPoint != null ? spawnPoint.position : Vector3.zero;
            _playerBody.velocity = new Vector3(0, 0, 0);
        }
    }
}
