using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSpawner : MonoCache
{
    [SerializeField]
    GameObject cube;
    [SerializeField]
    float anglePerSecond = 90f;

    [SerializeField]
    bool isSpawner = false, startSpawner = false;
    [SerializeField]
    int spawnCount = 500, timeStepMiliSec = 35;

    void Start()
    {
        if (isSpawner)
        {
            if (startSpawner)
            {
                for (int i = 1; i < spawnCount; i++)
                {
                    Instantiate(cube,
                    transform.position + (1.5f * Vector3.right * (i % 50)) + (1.5f * Vector3.back * (i / 50)),
                    Quaternion.identity);

                }
            }
            else
                StartCoroutine(SpawnCorutine());
        }

    }

    IEnumerator SpawnCorutine()
    {
        for (int i = 1; i < spawnCount; i++)
        {
            yield return new WaitForSeconds((float)timeStepMiliSec / 10000);
            Instantiate(cube,
            transform.position + (1.5f * Vector3.right * (i % 50)) + (1.5f * Vector3.back * (i / 50)),
            Quaternion.identity);

        }
    }

    public override void OnTick()
    {
        transform.rotation *= Quaternion.AngleAxis(anglePerSecond * Time.deltaTime, Vector3.up);
    }
}
