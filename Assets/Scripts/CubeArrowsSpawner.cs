using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeArrowsSpawner : MonoBehaviour
{
    Player player;
    [SerializeField] GameObject ArrowsPrefab;
    void Start()
    {
        player = GetComponent<Player>();
        if (player == null || player.enabled == false) return;
        if (player.isHor)
        {
            GameObject arr = Instantiate(ArrowsPrefab, transform.position + new Vector3(0f, 0.6f, 0f),
            Quaternion.Euler(0f, 0f, 0f), transform);
            arr.GetComponent<Animation>().Play();
            Destroy(arr, 4f);
        }
        if (player.isVert)
        {
            GameObject arr = Instantiate(ArrowsPrefab, transform.position + new Vector3(0f, 0.6f, 0f),
            Quaternion.Euler(0f, 90f, 0f), transform);
            arr.GetComponent<Animation>().Play();
            Destroy(arr, 4f);
        }
    }


}
