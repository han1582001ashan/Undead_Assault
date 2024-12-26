using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerWeaponManager : MonoBehaviour
{
    WeaponBaseState currentWeaponState;

    public RifleState rifleState = new RifleState();
    public PlayerManager playerManager;
    public Animator animator;
    public GameObject rifleWeapon;
    public GameObject curMagazine;
    public GameObject reloadMagazine;
    public Rifle rifle;
    AudioSource audioSource;
    public bool reloading;

    public bool canAttack;
    public bool fire;
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
        audioSource = rifleWeapon.GetComponent<AudioSource>();
        canAttack = false;
        reloading = false;
    }
    public void WeaponUpdate()
    {
        currentWeaponState.UpdateState(this);
    }
    public void InitializeRifle()
    {
        rifle = rifleWeapon.GetComponent<Rifle>();
        rifle.Initialize();
    }

    public bool CanAttack()
    {
        if (PlayerManager.instance.isDie)
        {
            return false;
        }
        if (reloading)
        {
            return false;
        }
        if (!UIManager.instance.attackButtonPress)
        {
            return false;
        }
        if (UIManager.instance.attackButtonPress)
        {
            return true;
        }
        return false;
    }
    public void StartAttack()
    {
        fire = true;
    }
    public void Reload()
    {
        PlayerManager.instance.animator.SetTrigger("Reload");
        reloading = true;
    }
    public void Reloaddone()
    {
        rifle.Reload();
        reloading = false;
        UIManager.instance.reloadButtonPress = false;
    }
    public bool CanReload()
    {
        if (rifle.currentAmmo == rifle.clipSize) return false;
        if (PlayerManager.instance.curTotalAmmo == 0) return false;
        if(reloading){
            return false;
        }
        if (UIManager.instance.reloadButtonPress){
            return true;
        }
        return false;
        
    }
    public void SwitchWeapon(WeaponBaseState weapon)
    {
        currentWeaponState = rifleState;
        currentWeaponState.EnterState(this);
    }
    public void MagOut()
    {
        audioSource.PlayOneShot(rifle.magOutSound);
        curMagazine.SetActive(false);
        reloadMagazine.SetActive(true);
    }
    public void MagIn()
    {
        audioSource.PlayOneShot(rifle.magInSound);
        curMagazine.SetActive(true);
        reloadMagazine.SetActive(false);
    }
    public void ReleaseSlide()
    {
        audioSource.PlayOneShot(rifle.releaseSlideSound);
    }

}
