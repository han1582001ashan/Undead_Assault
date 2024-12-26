
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponConfig", menuName ="Scriptable/Weapon/RifleConfig")]
public class RifleConfig : WeaponConfig
{  
    public static RifleConfig instance;
    public int clipSize;
    public float recoilValue;
    public float bulletVelocity;
    public AudioClip magInSound;
    public AudioClip magOutSound;
    public AudioClip releaseSlideSound;

   
    void Awake(){
        instance=this;
        weaponType=WeaponType.Gun;
    }
    
}
