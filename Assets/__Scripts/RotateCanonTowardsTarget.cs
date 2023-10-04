using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCanonTowardsTarget : MonoBehaviour
{

    [SerializeField] Transform canonBase;
    [SerializeField] Transform canonRifle;

    [SerializeField] float baseSpeed;
    [SerializeField] float rifleSpeed;
    [SerializeField] float rotationTolerance;

    bool baseLockedOnTarget = false;
    bool rifleLockedOnTarget = false;
    ShootObjectParabolic luncher;
    Quaternion desiredBaseRotation;
    Quaternion desiredRifleRotation;

    private void Awake()
    {
        luncher = GetComponent<ShootObjectParabolic>();
    }

    private void Update()
    {
        Rotate();
    }

    public void LockRotation()
    {
        baseLockedOnTarget = true;
        rifleLockedOnTarget = true;
    }
    
    public void UnlockRotation()
    {
        baseLockedOnTarget = false;
        rifleLockedOnTarget = false;
    }

    public void SetTarget(Vector3 target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(luncher.CalculateDirection(target));
        Vector3 eulerRotation = targetRotation.eulerAngles;
        desiredBaseRotation = Quaternion.Euler(new Vector3(canonBase.rotation.eulerAngles.x, eulerRotation.y, canonBase.rotation.eulerAngles.z));
        desiredRifleRotation = Quaternion.Euler(new Vector3(eulerRotation.x, canonRifle.rotation.eulerAngles.y, canonRifle.rotation.eulerAngles.z));

        UnlockRotation();
    }

    private void Rotate()
    {
        if (IsLockedOnTarget())
            return;

        canonRifle.rotation = Quaternion.Lerp(canonRifle.rotation, desiredRifleRotation, rifleSpeed * Time.deltaTime);
        canonBase.rotation = Quaternion.Lerp(canonBase.rotation, desiredBaseRotation, baseSpeed * Time.deltaTime);
    }

    public bool IsLockedOnTarget()
    {
        baseLockedOnTarget = IsPartAimingAtTarget(canonBase.rotation, desiredBaseRotation);
        rifleLockedOnTarget = IsPartAimingAtTarget(canonRifle.rotation, desiredRifleRotation);

        return baseLockedOnTarget && rifleLockedOnTarget;
    }

    //Quaternion helper
    private bool IsPartAimingAtTarget(Quaternion currentRotation, Quaternion desiredRotation)
    {
        return Quaternion.Angle(currentRotation, desiredRotation) <= rotationTolerance;
    }

}
