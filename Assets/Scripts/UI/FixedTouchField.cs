using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update

    [HideInInspector]
    public Vector2 TouchDist;
    [HideInInspector]
    public Vector2 PointOld;
    [HideInInspector]  
    public bool Pressed;
    [HideInInspector]
    public int PointId;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Pressed){
            if(PointId>= 0 && PointId< Input.touches.Length){
                TouchDist=Input.touches[PointId].position-PointOld;
                PointOld=Input.touches[PointId].position;
            }else{
                TouchDist=new Vector2(Input.mousePosition.x, Input.mousePosition.y)-PointOld;
                PointOld=Input.mousePosition;
            }
        }else{
            TouchDist =new Vector2();
        }
    }
    public void OnPointerDown(PointerEventData eventData){
        Pressed=true;
        PointId=eventData.pointerId;
        PointOld=eventData.position;
    }
    public void OnPointerUp(PointerEventData eventData){
        Pressed=false;
    }
}
