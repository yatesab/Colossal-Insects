using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float health = 500f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float SubtractHealth(float subtractAmount)
    {
        health -= subtractAmount;
        return health;
    }

    public void SetHealth(float newHealth)
    {
        health = newHealth;
    }

    public float GetHealth()
    {
        return health;
    }
}
