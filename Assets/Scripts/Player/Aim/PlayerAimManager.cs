using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Mathematics;
using UnityEngine.Animations.Rigging;

public class PlayerAimManager : MonoBehaviour
{
    //State
    public AimBaseState currentState;
    public AimState attackState = new AimState();
    public HoldInHandState holdInHandState = new HoldInHandState();
    //Camera
    [HideInInspector]
    public float currentFOV;
    private float fovSmoothSpeed = 10f;
    [SerializeField] Transform cameraTargetPos;
    private float rotationSensitivity = 5f;
    public Vector2 LockAxis;
    [SerializeField] public Transform aimTargetPos;
    public Vector3 actualAimPos;
    [SerializeField] private float aimSmoothSpeed = 10f;
    [SerializeField] LayerMask aimMask;
    public MultiAimConstraint rightHandAim;
    public TwoBoneIKConstraint leftHandAim;

    //Animator
    public Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        SwitchState(holdInHandState);
    }

    // Update is called once per frame
    public void PlayerAimUpdate()
    {
        currentState.UpdateState(this);
    }
    public void AimToTarget()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimTargetPos.position = Vector3.Lerp(aimTargetPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);

        }
    }
    private void LateUpdate()
    {
        cameraTargetPos.localEulerAngles = new Vector3(CameraController.instance.Xaxis, cameraTargetPos.localEulerAngles.y, cameraTargetPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, CameraController.instance.Yaxis, transform.eulerAngles.z);
    }
    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
