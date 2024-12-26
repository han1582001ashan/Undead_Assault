using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public static CameraController instance;
    public float Yaxis;
    public float Xaxis;
    private float rotationSensitivity = 5f;
    public float rotationMin = -40f;
    public float rotationMax = 80f;
    float smoothTime = 0.12f;
    public Transform target;
    public Vector3 targetRotation;
    public Vector3 currentVel;
    public Vector2 LockAxis;
    public float attackDistance=1.5f;
    public float defaultDistance=2.5f;
    public float currentDistance;
    public Vector2 moveWhenAttack;

    // public FixedTouchField fixedTouchField;
    // Update is called once per frame
    void Update()
    {
       
        Follow();
       
        
    }
    public void SetFollowTarget(Transform curtarget)
    {
        this.target = curtarget;
    }
    public void SetDefaultDistance(){
        this.currentDistance= Mathf.Lerp(this.defaultDistance, this.currentDistance, 0.1f*Time.deltaTime);
    }
    public void SetAttackDistance(){
        this.currentDistance=Mathf.Lerp(this.attackDistance, this.currentDistance, 0.1f*Time.deltaTime);;
    }
    private void Follow()
    {
        moveWhenAttack.x = UIManager.instance.AttackJoystick.Horizontal;
        moveWhenAttack.y = UIManager.instance.AttackJoystick.Vertical;
        LockAxis = UIManager.instance.fixedTouchField.TouchDist;
        Yaxis += LockAxis.x * rotationSensitivity * Time.deltaTime + moveWhenAttack.x;
        Xaxis -= LockAxis.y * rotationSensitivity * Time.deltaTime + moveWhenAttack.y;
        Xaxis = Mathf.Clamp(Xaxis, rotationMin, rotationMax);
        targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
        transform.eulerAngles = targetRotation;
        transform.position = target.position - transform.forward * currentDistance;
    }

     private void OnEnable()
        {
            instance = this;
        }
        private void OnDisable()
        {
            instance = null;
        }

}
