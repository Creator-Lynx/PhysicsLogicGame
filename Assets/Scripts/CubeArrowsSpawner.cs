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
            GameObject arr = Instantiate(ArrowsPrefab, transform.position + new Vector3(0f, 0.51f, 0f),
            Quaternion.Euler(0f, 0f, 0f), transform);
            arr.transform.localScale = new Vector3(1 / transform.localScale.x, 1, 1 / transform.localScale.z);
            RectTransform[] uis = arr.GetComponentsInChildren<RectTransform>();
            uis[0].localPosition = new Vector3(-(transform.localScale.x - 1) / 2, 0, 0);
            uis[2].localPosition = new Vector3((transform.localScale.x - 1) / 2, 0, 0);
            //uis[1].localPosition = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 0);
            arr.GetComponent<Animation>().Play();
            Destroy(arr, 4f);
        }
        if (player.isVert)
        {
            GameObject arr = Instantiate(ArrowsPrefab, transform.position + new Vector3(0f, 0.51f, 0f),
            Quaternion.Euler(0f, 90f, 0f), transform);
            arr.transform.localScale = new Vector3(1 / transform.localScale.z, 1, 1 / transform.localScale.x);
            RectTransform[] arrowRects = arr.GetComponentsInChildren<RectTransform>();
            arrowRects[0].localPosition = new Vector3(-(transform.localScale.z - 1) / 2, 0, 0);
            arrowRects[2].localPosition = new Vector3((transform.localScale.z - 1) / 2, 0, 0);
            arr.GetComponent<Animation>().Play();
            Destroy(arr, 4f);
        }
    }


}
