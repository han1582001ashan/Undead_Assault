

using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerConfig playerConfig;
    public PlayerBaseState currentState;
    public PlayerDefaultState defaultState = new PlayerDefaultState();
    public PlayerDieState dieState = new PlayerDieState();

    //playerConfig
    public int maxHealth;
    public float moveSpeed;
    public float jumpHeight;
    private float rotationSpeed;
    private int totalAmmo;
    //playerData
    public int currentHP;
    public bool isDie;
    public int curTotalAmmo;

    //Component
    public PlayerAimManager aimManager;
    public PlayerWeaponManager weaponManager;
    public Transform startTransform;
    public Animator animator;
    CharacterController controller;
    public Vector3 moveDirect;
    private Camera mainCamera;
    [SerializeField] LayerMask groundMask;
    float gravity;
    float groundYOffSet;
    float horizontalInput, verticalInput;
    //Vector, tranform
    Vector3 spherePos;
    Vector3 velocity;
    void Awake()
    {
        
        instance = this;
        GameManager.OnGameStateChange += UpdateUIOnGameStateChange;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        aimManager=GetComponent<PlayerAimManager>();
        weaponManager=GetComponent<PlayerWeaponManager>();
        groundYOffSet = 0f;
        gravity = -9.81f;
        Initialize();
        SwitchState(defaultState);

    }
    public void PlayerUpdate()
    {
       
        Gravity();
         if (currentHP <= 0)
        {
            isDie=true;
            SwitchState(dieState);
        }
        
        currentState.UpdateState(this);
        

    }
     private void UpdateUIOnGameStateChange(GameState state){
    bool isplaying= state == GameState.GamePlaying;
        animator.enabled=isplaying;
    }
    public void Initialize()
    {
        mainCamera = Camera.main;
        LoadPlayerConfig();
        currentHP = maxHealth;
        curTotalAmmo=totalAmmo;
        aimManager.SwitchState(aimManager.holdInHandState);
        weaponManager.SwitchWeapon(weaponManager.rifleState);
        isDie=false;
        // transform.position=GameManager.Instance.playerStartTransform.position;
    }
    public void LoadPlayerConfig()
    {
        maxHealth = playerConfig.health;
        moveSpeed = playerConfig.moveSpeed;
        jumpHeight = playerConfig.jumpHeight;
        rotationSpeed = playerConfig.rotationSpeed;
        totalAmmo=playerConfig.ammo;
    }
    public void AnimationMove()
    {
        animator.SetFloat("MoveX", horizontalInput);
        animator.SetFloat("MoveZ", verticalInput);
    }
    public void GetDirectionAndMove()
    {
        horizontalInput = UIManager.instance.MovementJoystick.Horizontal;
        verticalInput = UIManager.instance.MovementJoystick.Vertical;
        Vector3 movementInput = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;
        if (movementDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        moveDirect = movementDirection.normalized;
        controller.Move(moveDirect * moveSpeed * Time.deltaTime);
    }
    public void Jump()
    {
        if (IsGrounded())
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            controller.Move(velocity * Time.deltaTime);
        }

    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffSet, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.1f, groundMask))
        {
            return true;
        }
        return false;
    }
    void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2;
        }
        
        controller.Move(velocity.normalized * Time.deltaTime);

    }
    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    public void GameOver(){
      
        GameManager.Instance.UpdateGameState(GameState.GameOver);
    }

}
