using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RifleState : WeaponBaseState
{
    public override void EnterState(PlayerWeaponManager playerWeaponManager)
    {
        PlayerManager.instance.animator.SetLayerWeight(PlayerManager.instance.animator.GetLayerIndex("Rifle Layer"), 1);
        playerWeaponManager.rifleWeapon.SetActive(true);
        playerWeaponManager.curMagazine.SetActive(true);
        playerWeaponManager.reloadMagazine.SetActive(false);
        playerWeaponManager.InitializeRifle();
    }

    public override void UpdateState(PlayerWeaponManager playerWeaponManager)
    {
        if (playerWeaponManager.CanAttack())
        {
            Attack(playerWeaponManager);
        }
        else
        {
            EndAttack(playerWeaponManager);
        }
        if (playerWeaponManager.CanReload())
        {
            playerWeaponManager.Reload();
        }
    }

    public override void Attack(PlayerWeaponManager playerWeaponManager)
    {
        playerWeaponManager.animator.SetBool("Fire", true);
    }
    public void EndAttack(PlayerWeaponManager playerWeaponManager)
    {
        playerWeaponManager.animator.SetBool("Fire", false);
        playerWeaponManager.canAttack = false;
        playerWeaponManager.fire = false;
    }





}
