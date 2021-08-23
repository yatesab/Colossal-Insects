using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    [SerializeField] private float damageAmount = 50f;

    private EnemyController _enemy;
    private Animator _animator;
    private float currentHealth;
    private string animatorDeathParam = "Death";

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _enemy = GetComponentInParent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = _enemy.GetHealth();

        if (currentHealth <= 0)
        {
            KillEnemy();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (currentHealth <= 0) return;

        if (collider.tag == "Weapon")
        {
            currentHealth = _enemy.SubtractHealth(damageAmount);
        }
    }

    private void KillEnemy()
    {
        _animator.SetBool(animatorDeathParam, true);
    }
}
