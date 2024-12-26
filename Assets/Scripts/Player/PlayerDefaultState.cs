

public class PlayerDefaultState : PlayerBaseState
{
    public override void EnterState(PlayerManager playerManager)
    {
        PlayerManager.instance.weaponManager.SwitchWeapon(PlayerManager.instance.weaponManager.rifleState);
    }

    public override void UpdateState(PlayerManager playerManager)
    {

        playerManager.GetDirectionAndMove();
        playerManager.AnimationMove();
         PlayerManager.instance.weaponManager.WeaponUpdate();
         PlayerManager.instance.aimManager.PlayerAimUpdate();
        UIManager.instance.PlayerHealBar.SetHealth(playerManager.currentHP, playerManager.maxHealth);
    }
}
