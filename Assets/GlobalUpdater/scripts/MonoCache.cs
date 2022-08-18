using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCache : MonoBehaviour
{
    /* обязательно статичный, используем из разных экземпляров этого класса (наследников этого класса). 
    каждый записывает себя, но список должен быть единым. публичный, чтобы иметь доступ из Update manager*/
    public static List<MonoCache> allUpdated = new List<MonoCache>(1001);
    public static List<MonoCache> allFixedUpdated = new List<MonoCache>(1001);
    public static List<MonoCache> allLateUpdated = new List<MonoCache>(1001);

    //методы для подписки/отписки от события update при изменении состояния объекта
    private void OnEnable() => allUpdated.Add(this);
    private void OnDisable() => allUpdated.Remove(this);
    private void OnDestroy() => allUpdated.Remove(this);

    /// <summary>
    /// manualy adding your object to list of FixedUpdated
    /// </summary>
    protected void AddFixedUpdate() => allFixedUpdated.Add(this);
    /// <summary>
    /// manualy adding your object to list of LateUpdated
    /// </summary>
    protected void AddLateUpdate() => allLateUpdated.Add(this);
    /// <summary>
    /// manualy removing your object to list of FixedUpdated
    /// </summary>
    protected void RemoveFixedUpdate() => allFixedUpdated.Remove(this);
    /// <summary>
    /// manualy removing your object to list of LateUpdated
    /// </summary>
    protected void RemoveLateUpdate() => allLateUpdated.Remove(this);



    /*Данная форма метода необходима для того, чтобы не вызывать метод через base делать это напрямую. (что?)*/
    //public void Tick() => OnTick();
    //public void FixedTick() => OnFixedTick();
    //public void LateTick() => OnLateTick();

    public virtual void OnTick() { }
    public virtual void OnFixedTick() { }
    public virtual void OnLateTick() { }
}
