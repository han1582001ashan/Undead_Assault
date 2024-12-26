using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBaseState
{
    public abstract void EnterState(PlayerWeaponManager playerWeaponManager);
    public abstract void UpdateState(PlayerWeaponManager playerWeaponManager);
    public abstract void Attack(PlayerWeaponManager playerWeaponManager);

  
}
