using UnityEngine;

public class GlobalUpdate : MonoBehaviour
{
    void Update()
    {
        /*здесь ни в коем случае не использовать цикл foreach так как он может приводить к ошибкам*/
        for (int i = 0; i < MonoCache.allUpdated.Count; i++) MonoCache.allUpdated[i].OnTick();
    }
    void FixedUpdate()
    {
        for (int i = 0; i < MonoCache.allFixedUpdated.Count; i++) MonoCache.allFixedUpdated[i].OnFixedTick();
    }
    void LateUpdate()
    {
        for (int i = 0; i < MonoCache.allLateUpdated.Count; i++) MonoCache.allLateUpdated[i].OnLateTick();
    }
}
