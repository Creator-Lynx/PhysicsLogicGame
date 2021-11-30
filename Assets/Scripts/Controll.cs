using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static float Horizontal;
    public static float Vertical;
    [SerializeField] float angleShift;
    [SerializeField] float clamp;

    [SerializeField] float deadZone = 0.4f;

    public void OnBeginDrag(PointerEventData eventData)
    {


    }
    public void OnDrag(PointerEventData eventData)
    {
        /*if(Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y)){
            if(eventData.delta.x > 0){
                Horizontal = 1;
            }else{
                Horizontal = -1;
            }
        }else{
            if(eventData.delta.y > 0){
                Vertical = 1;
            }else{
                Vertical = -1;
            }
        }*/
        Vector2 moving = new Vector2();
        //cs = cos(a);
        //sn = sin(a);    
        //rx = x * cs - y * sn;
        //ry = x * sn + y * cs;
        /*чтобы получить нормальное интуитивное управление (изначально движение вверх по экрану приводит к движению по диагонали вверх и влево)
        необходимо повернуть изначальный вектор движения пальцем перед тем, как использовать его в механике. 
        угол поворота камеры  -145, то есть минимальных угол ориентировочно 55 градусов
        чтобы повернуть вектор на угол, нужно умножить его на матрицу поворота, состоящую из синусов и косинусов с определенными знаками
        преобразованное умножение вектора на матрицу через координаты было выше*/
        float a = Mathf.Deg2Rad * angleShift;
        float cs = Mathf.Cos(a);
        float sn = Mathf.Sin(a);
        moving.x = eventData.delta.x * cs - eventData.delta.y * sn;
        moving.y = eventData.delta.x * sn + eventData.delta.y * cs;
        Horizontal = Mathf.Abs(moving.x) > deadZone ? Mathf.Clamp(moving.x, -clamp, clamp) : 0;
        Vertical = Mathf.Abs(moving.y) > deadZone ? Mathf.Clamp(moving.y, -clamp, clamp) : 0;

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //if(eventData.delta.x <= 1f && eventData.delta.x >= -1f)  Horizontal = 0;
        //if(eventData.delta.y <= 1f && eventData.delta.y >= -1f)  Vertical = 0;
        Horizontal = 0;
        Vertical = 0;

    }
    // Start is called before the first frame update
    void Start()
    {
        Horizontal = 0;
        Vertical = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerPrefs.SetInt("isBought", 0);
        }
    }

}
