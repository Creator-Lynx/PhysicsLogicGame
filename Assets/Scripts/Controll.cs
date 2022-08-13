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
    [SerializeField] Vector2 axis;

    bool ChoosedDirection = false;


    bool UseOneDirectionControll;
    int horizontalMultiplier = 1, verticalMultiplier = 1;
    public enum ControllType
    {
        legacy,
        oneDirection
    }
    /// <summary>
    /// Set state for choose what system of controll we want to use. 
    /// If 0 we use system when only one direction of movement is use,
    /// If 1 cubes can move in both directions at same time.
    /// </summary>
    public void SetControllType(int type)
    {
        PlayerPrefs.SetInt("ControllType", type);
        UseOneDirectionControll = (type == (int)ControllType.legacy) ? true : false;
    }
    private void Awake()
    {
        SetControllType(PlayerPrefs.GetInt("ControllType", 0));
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        canUseInputAxis = false;

    }
    public void OnDrag(PointerEventData eventData)
    {
        canUseInputAxis = false;
        Vector2 moving = new Vector2();
        //cs = cos(a);
        //sn = sin(a);    
        //rx = x * cs - y * sn;
        //ry = x * sn + y * cs;
        /*чтобы получить нормальное интуитивное управление 
        (изначально движение вверх по экрану приводит к движению по диагонали вверх и влево)
        необходимо повернуть изначальный вектор движения пальцем перед тем, как использовать его в механике. 
        угол поворота камеры  -145, то есть минимальных угол ориентировочно 55 градусов
        чтобы повернуть вектор на угол, нужно умножить его на матрицу поворота, 
        состоящую из синусов и косинусов с определенными знаками
        преобразованное умножение вектора на матрицу через координаты было выше*/
        float a = Mathf.Deg2Rad * angleShift;
        float cs = Mathf.Cos(a);
        float sn = Mathf.Sin(a);
        moving.x = eventData.delta.x * cs - eventData.delta.y * sn;
        moving.y = eventData.delta.x * sn + eventData.delta.y * cs;

        Horizontal = Mathf.Abs(moving.x) > deadZone ? Mathf.Clamp(moving.x, -clamp, clamp) : 0;
        Vertical = Mathf.Abs(moving.y) > deadZone ? Mathf.Clamp(moving.y, -clamp, clamp) : 0;
        if (!ChoosedDirection && UseOneDirectionControll)
        {
            ChoosedDirection = true;
            if (Mathf.Abs(moving.x) > Mathf.Abs(moving.y))
            {
                verticalMultiplier = 0;
            }
            else
            {
                horizontalMultiplier = 0;
            }
        }
        Horizontal *= horizontalMultiplier;
        Vertical *= verticalMultiplier;
        //Horizontal = axis.x;
        //Vertical = axis.y;

    }
    public void OnEndDrag(PointerEventData eventData)
    {

        Horizontal = 0;
        Vertical = 0;
        ChoosedDirection = false;
        horizontalMultiplier = 1;
        verticalMultiplier = 1;
        canUseInputAxis = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        Horizontal = 0;
        Vertical = 0;
        canUseInputAxis = true;
    }

    bool canUseInputAxis = true;
    public void Update()
    {
        if (canUseInputAxis)
        {
            Horizontal = Input.GetAxis("Horizontal") * clamp;
            Vertical = Input.GetAxis("Vertical") * clamp;
        }
    }
}
