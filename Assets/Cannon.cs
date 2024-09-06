using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject ammo;
    [SerializeField] private Transform shootingPoint;
    public void Shoot(float forceModifier, float damageModifier)
    {
        GameObject spawnedObj = Instantiate(ammo, shootingPoint.position, shootingPoint.rotation);
        Ammo spawnedObjScr = spawnedObj.GetComponent<Ammo>();
        spawnedObjScr.force *= forceModifier;
        spawnedObjScr.damage *= damageModifier;
    }
}
