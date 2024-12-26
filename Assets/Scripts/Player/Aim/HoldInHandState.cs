using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldInHandState : AimBaseState
{
    // Start is called before the first frame update
    public override void EnterState(PlayerAimManager aim)
    {
        aim.rightHandAim.weight = 0;
        aim.leftHandAim.weight = 0;
        CameraController.instance.SetDefaultDistance();
        
    }

    public override void UpdateState(PlayerAimManager aim)
    {
        if (UIManager.instance.attackButtonPress)
        {
            aim.SwitchState(aim.attackState);
        }
    }
}
