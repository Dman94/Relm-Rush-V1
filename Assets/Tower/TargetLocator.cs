using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{

    [SerializeField] Transform Weapon;
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem projectileParticles;

    Transform Target;

    void Update()
    {
        FindClosestTarget();
        AimAndFireWeapon();
    }
    void AimAndFireWeapon()
    {
        Weapon.transform.LookAt(Target);
        float targetDistance = Vector3.Distance(transform.position, Target.position);

        if (targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }
    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float MaxDistance = Mathf.Infinity;

        foreach (Enemy target in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, target.transform.position);

            if (targetDistance < MaxDistance)
            {
                closestTarget = target.transform;
                MaxDistance = targetDistance;
            }
        }
        Target = closestTarget;
    }
   
    void Attack(bool isActive)
    {
        var Emission = projectileParticles.emission;
        Emission.enabled = isActive;
    }
}
