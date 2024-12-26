using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    //Rifle Config
    public RifleConfig rifleConfig;
    public string weaponName;
    public WeaponType weaponType;
    public int damage;
    public float atkSpeed;
    public int clipSize;
    public float recoilValue;
    public AudioClip magInSound;
    public AudioClip magOutSound;
    public AudioClip releaseSlideSound;
    //Component
    PlayerWeaponManager playerWeaponManager;
    // Light muzzleFlashLight;
    GunRecoil gunRecoil;
    AudioSource audioSource;
    PlayerAimManager aim;

    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 20;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;

    [SerializeField] AudioClip gunShot;
    //Timer
    float fireRateTimer;
    public int currentAmmo;
    
    void Awake()
    {
        LoadConfig();
        playerWeaponManager = GetComponentInParent<PlayerWeaponManager>();
        gunRecoil = GetComponent<GunRecoil>();
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<PlayerAimManager>();

        // muzzleFlashLight = GetComponentInChildren<Light>();

    }
    void Update()
    {
        if (CanFire())
        {
            Fire();
        }
        // muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
       UIManager.instance.ammoTxt.text=currentAmmo+"/"+PlayerManager.instance.curTotalAmmo;
    }
    public void LoadConfig()
    {
        weaponName = rifleConfig.weaponName;
        damage = rifleConfig.damage;
        atkSpeed = rifleConfig.atkSpeed;
        clipSize = rifleConfig.clipSize;
        recoilValue = rifleConfig.recoilValue;
        magInSound = rifleConfig.magInSound;
        magOutSound = rifleConfig.magOutSound;
        releaseSlideSound = rifleConfig.releaseSlideSound;
        bulletVelocity = rifleConfig.bulletVelocity;
    }
    public void Initialize()
    {
        
    }
    void OnEnable(){
    fireRateTimer = atkSpeed;
        // lightIntensity = muzzleFlashLight.intensity;
        // muzzleFlashLight.intensity = 0;
        currentAmmo = clipSize;
        
    }
    bool CanFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < atkSpeed)
        {
            return false;
        }
        if (currentAmmo <= 0)
        {
            return false;
        }
        if (!playerWeaponManager.CanAttack())
        {
            return false;
        }
        if (playerWeaponManager.fire && playerWeaponManager.CanAttack())
        {
            return true;
        }

        return false;
    }
    void Fire()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimTargetPos);
        audioSource.PlayOneShot(gunShot);
        gunRecoil.TriggerRecoil();
        // TriggerMuzzleFlash();
        currentAmmo--;
        GameObject currenBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
        Bullet bulletScript = currenBullet.GetComponent<Bullet>();
        bulletScript.rifle = this;
        Rigidbody rb = currenBullet.GetComponent<Rigidbody>();
        rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        Debug.Log("fire");

    }
    void Fires()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimTargetPos);
        audioSource.PlayOneShot(gunShot);
        gunRecoil.TriggerRecoil();
        currentAmmo--;
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);


    }
    // void TriggerMuzzleFlash()
    // {
    //     // muzzleFlashParticle.Play();
    //     muzzleFlashLight.intensity = lightIntensity;
    // }
    public void Reload()
    {
        if (PlayerManager.instance.curTotalAmmo >= clipSize)
        {
            int ammorToReload = clipSize - currentAmmo;
            PlayerManager.instance.curTotalAmmo -= ammorToReload;
            currentAmmo += ammorToReload;
        }
        else if (PlayerManager.instance.curTotalAmmo > 0)
        {
            if (PlayerManager.instance.curTotalAmmo + currentAmmo > clipSize)
            {
                int leftOverAmo = PlayerManager.instance.curTotalAmmo + currentAmmo - clipSize;
                PlayerManager.instance.curTotalAmmo = leftOverAmo;
                currentAmmo = clipSize;
            }
            else
            {
                currentAmmo += PlayerManager.instance.curTotalAmmo;
                PlayerManager.instance.curTotalAmmo = 0;
            }
        }

    }

}
