using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannonShooting : MonoBehaviour
{
    public CannonSettings Settings => settings;
    [SerializeField] CannonSettings settings;

    [SerializeField] private Transform shootingPoint;

    Transform target;
    Vector3 targetDirection;
    RotatePart[] rotateParts;

    private void Start()
    {
        SetTarget("Player");
        rotateParts = GetComponentsInChildren<RotatePart>();
    }

    private void Update()
    {
        RotateTower(targetDirection);
    }

    private void RotateTower(Vector3 dir)
    {
        foreach (var rotatePart in rotateParts)
        {
            rotatePart.Rotate(dir.normalized, settings.RotationSmoothing);
        }
    }

    private void SetTarget(string tag)
    {
        target = GameObject.FindGameObjectWithTag(tag).transform;
        targetDirection = CalculateDirection(target.position);
    }

    public void Shoot(ProjectileSO projectile)
    {
        Vector3 diffusedTargetPosition = CalculateRandomTargetPosition(target);
        targetDirection = CalculateDirection(diffusedTargetPosition);    

        InitProjectile(projectile, targetDirection);
    }

    private void InitProjectile(ProjectileSO data, Vector3 direction)
    {
        GameObject obj = Instantiate(data.Model, shootingPoint.position, shootingPoint.rotation);
        obj.AddComponent<ConstantForce>();
        Projectile projectile = obj.AddComponent<Projectile>();
        projectile.SetGravity(settings.Gravity);
        projectile.SetVelocity(direction);
        projectile.SetCrossSectionMaterial(data.CrossSectionMaterial);
        projectile.SetEffects(data.Effects.ToList<IEffect>());
    }

    private Vector3 CalculateRandomTargetPosition(Transform player)
    {
        // Generate a random point within a circle of the specified radius
        float randomAngle = Random.Range(0f, 360f);
        float randomDistance = Random.Range(0f, Mathf.Abs(settings.RandomTargetRange));

        // Calculate the new position based on the random angle and distance
        Vector3 offset = new Vector3(
            Mathf.Cos(randomAngle * Mathf.Deg2Rad) * randomDistance,
            0f,
            Mathf.Sin(randomAngle * Mathf.Deg2Rad) * randomDistance
        );

        return player.position + offset;
    }

    public Vector3 CalculateDirection(Vector3 target)
    {
        float displacementY = target.y - shootingPoint.position.y;
        Vector3 displacementXZ = new Vector3(target.x - shootingPoint.position.x, 0, target.z - shootingPoint.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * settings.Gravity * settings.Height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * settings.Height / settings.Gravity) + Mathf.Sqrt(2 * (displacementY - settings.Height) / settings.Gravity));


        return velocityY + velocityXZ;
    }


}
