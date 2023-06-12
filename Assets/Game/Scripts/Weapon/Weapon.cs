using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float weaponDamage;


    public float GetWeaponDamage() => weaponDamage;
}
