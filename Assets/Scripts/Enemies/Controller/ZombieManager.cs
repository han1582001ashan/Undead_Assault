using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    ZombieBaseState currentState;
    public ZombieDefaultState defaultState = new ZombieDefaultState();
    public ZombieDieState dieState = new ZombieDieState();

    //zombie property
    [SerializeField] public string zombieName;
    [SerializeField] public ZombieType zombieType;
    [SerializeField] public int maxHealth;
    [SerializeField] public float defaultSpeed;
    [SerializeField] public float slowSpeed;
    [SerializeField] public int damage;
    public float atkSpeed;
    public float atkRange;
    public int coin;

    public ZombieConfig zombieConfig;
    public Animator animator;
    public LayerMask playerLayerMask;
    public Vector3 spherePos;
    public Transform transforms;
    public NavMeshAgent agent;

    public bool isDie;
    public bool isTakeDamage;
    public bool isAttack = true;
    public bool inAttackRange;

    public int curHealth;
    [SerializeField] public float currentSpeed;
    [SerializeField] public float currentMoveValue;
    [SerializeField] public float idleMoveValue;
    [SerializeField] public float slowMoveValue;
    [SerializeField] public float defaultMoveValue;
    [SerializeField] public float changeSpeedTime;
    public float atkTimer;
    public float resetRate;
    public float resetRateTimer;

public float buffValue;
    void Awake()
    {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GameManager.OnGameStateChange += UpdateUIOnGameStateChange;
        LoadZombieConfig();
        SwitchState(defaultState);
        
    }

    public void ZombieUpdate()
    {
        buffValue= GameManager.Instance.currentLevel * GameManager.Instance.levelConfig.increaseMapValueEachLevel;
        UIManager.instance.EnemyHealBar.enabled= isTakeDamage;
        currentState.UpdateState(this);
        if (curHealth <= 0)
        {
            SwitchState(dieState);
            GameManager.Instance.coin+= this.coin;
            GameManager.Instance.zombiesOnMap.Remove(gameObject);
            
            
        }
    }
    private void UpdateUIOnGameStateChange(GameState state){
    bool isplaying= state == GameState.GamePlaying;
    
    }
    public void SetSpeed()
    {
        if (CheckPlayerInAttackRange())
        {
            SetIdleSpeed();
    
        }
        else
        {
            if (isTakeDamage)
            {
                SetSlowSpeed();
            }
            else
            {
                SetDefaultSpeed();
            }

        }
    }
    public void LoadZombieConfig()
    {
        zombieConfig = GameManager.Instance.zombieConfigDictionnary[this.zombieType];
        defaultSpeed = zombieConfig.moveSpeed;
        zombieName = zombieConfig.zombieName;
        maxHealth = zombieConfig.HP + Mathf.FloorToInt(zombieConfig.HP*buffValue);
        damage = zombieConfig.atkDamage + Mathf.FloorToInt(zombieConfig.atkDamage*buffValue);
        slowSpeed = defaultSpeed * 0.3f;
        atkSpeed = zombieConfig.atkSpeed;
        atkRange = zombieConfig.atkRange;
        coin= zombieConfig.coin;
        resetRate = 5;
        idleMoveValue = 0;
        slowMoveValue = 0.5f;
        defaultMoveValue = 1;
    }
    public void Initialize()
    {

        animator.SetLayerWeight(animator.GetLayerIndex("AttackLayer"), 0);
        curHealth = maxHealth;
        slowSpeed = defaultSpeed * 0.3f;
        isTakeDamage = false;
        isAttack = false;
        isDie = false;
        resetRateTimer = 0;
        atkTimer = atkSpeed;
        currentSpeed = defaultSpeed;
        currentMoveValue = defaultMoveValue;
        SetDefaultSpeed();
    }
    public void ChasePlayer(Transform target)
    {
        agent.destination = target.position;
        agent.speed = currentSpeed;
        transform.LookAt(target.position);
        animator.SetFloat("MoveSpeed", currentMoveValue);
    }

    public void TakeDamage(int damage)
    {
        resetRateTimer = 0;
        curHealth -= damage;
        UIManager.instance.EnemyHealBar.SetHealth(curHealth, maxHealth);
        isTakeDamage = true;


    }
    public void RemoveZombie()
    {
        Destroy(this.gameObject, 7f);
    }

    public bool CanReset()
    {
        resetRateTimer += Time.deltaTime;
        if (resetRateTimer < resetRate)
        {
            return false;
        }
        if (isDie)
        {
            return false;
        }
        if (resetRateTimer >= resetRate)
        {
            resetRateTimer = resetRate;
            return true;
        }
        return false;
    }
    public void Reset()
    {
        isTakeDamage = false;
    }
    public bool CheckPlayerInAttackRange()
    {
        spherePos = transform.position;
        if (Physics.CheckSphere(spherePos, agent.radius + 1f, playerLayerMask))
        {
            return true;
        }
        return false;
    }
    public bool CanAttack()
    {
        atkTimer += Time.deltaTime;
        if (isDie)
        {
            return false;
        }
        if (atkTimer < atkSpeed)
        {
            return false;
        }
        if (CheckPlayerInAttackRange() && atkTimer >= atkSpeed)
        {
            return true;
        }
        return false;
    }
    public void Attack()
    {
        atkTimer = 0;
        animator.SetLayerWeight(animator.GetLayerIndex("AttackLayer"), 1);
        animator.SetTrigger("Attack");
    }

    public void TurnOnAttack()
    {
        isAttack = true;
    }

    public void TurnOffAttack()
    {
        isAttack = false;
    }
    public void CDAttack()
    {
        atkTimer = 0;
    }
    public void SetDefaultSpeed()
    {
        this.currentSpeed = defaultSpeed;
        this.currentMoveValue = this.defaultMoveValue;
    }
    public void SetSlowSpeed()
    {
        this.currentSpeed = slowSpeed;
        this.currentMoveValue = this.slowMoveValue;
    }
    public void SetIdleSpeed()
    {
        this.currentSpeed = 0f;
        this.currentMoveValue = this.idleMoveValue;
    }
    public void SwitchState(ZombieBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

}
