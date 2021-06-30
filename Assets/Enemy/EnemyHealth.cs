using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Enemy))]

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitpoints = 5;

    [Tooltip("Adds amount to maxHitpoints when enemie dies")]
    [SerializeField] int DifficultyRound = 1;

   int currentHitPoints = 0;
    

    Enemy enemy;
    

    void OnEnable()
    {
        currentHitPoints = maxHitpoints;
    }

    void Start()
    {
        enemy = FindObjectOfType<Enemy>();
    }


    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitPoints--;

        if (currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
            maxHitpoints += DifficultyRound;
        }
    }
}
