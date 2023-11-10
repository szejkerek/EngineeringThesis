using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "CannonSettings", menuName = "Cannon/CannonSettings", order = 1)]
public class CannonSettings : ScriptableObject
{
    [field: Header("Projectiles")]
    [field: SerializeField] public List<ProjectileSO> Projectiles { private set; get; }
    [field: SerializeField] public AssetLabelReference GoodBulletLabel { private set; get; }
    [field: SerializeField] public AssetLabelReference BadBulletLabel { private set; get; }
    [field: SerializeField] public AssetLabelReference SpecialBulletLabel { private set; get; }

    [field: Header("Settings")]
    [field: SerializeField] public float RandomTargetRange { private set; get; }
    [field: SerializeField] public float RotationSmoothing { private set; get; }
    [field: SerializeField] public float Height { private set; get; }
    [field: SerializeField] public float Gravity { private set; get; }
}