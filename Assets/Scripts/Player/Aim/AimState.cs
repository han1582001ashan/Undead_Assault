using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    // Start is called before the first frame update
    public override void EnterState(PlayerAimManager aim)
    {
         aim.rightHandAim.weight=1;
        aim.leftHandAim.weight=1; 
        CameraController.instance.SetAttackDistance();
    }

    public override void UpdateState(PlayerAimManager aim)
    {
         aim.AimToTarget();
        if (!UIManager.instance.attackButtonPress)
        {
            aim.SwitchState(aim.holdInHandState);
        }

    }
}
