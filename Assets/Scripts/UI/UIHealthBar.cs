using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public float curHealth;
    public float lerpTime;
    public float maxHealth;
    public float chipSpeed;
    public Image frontBar;
    public Image backBar;
    void Start()
    {
        curHealth=maxHealth;
        
    }
    void Update()
    {
        curHealth=Math.Clamp(curHealth, 0, maxHealth);
        UpdateHealthUI();
    }
    public void SetHealth(float curHealth, float maxHealth){
        this.curHealth=curHealth;
        this.maxHealth=maxHealth;
    }
    public void UpdateHealthUI(){
        float fillF= frontBar.fillAmount;
        float fillB= frontBar.fillAmount;
        float hFraction = curHealth/maxHealth;
        if(fillB>hFraction){
            frontBar.fillAmount=hFraction;
            lerpTime+=Time.deltaTime;
            float percentComplete =lerpTime/chipSpeed;
            backBar.fillAmount=Mathf.Lerp(fillB, hFraction, percentComplete);
        }
    } 
}
