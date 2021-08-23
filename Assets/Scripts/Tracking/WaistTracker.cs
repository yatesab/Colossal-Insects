using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaistTracker : MonoBehaviour
{

    [SerializeField] private CapsuleCollider collider;
    [SerializeField] private Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newLocalPosition = collider.center;

        newLocalPosition.y = collider.height / 2.5f;
        
        // Set new position in local space
        transform.localPosition = newLocalPosition;

        Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, camera.eulerAngles.y, transform.eulerAngles.z);

        transform.rotation = Quaternion.Euler(eulerRotation);
    }
}
