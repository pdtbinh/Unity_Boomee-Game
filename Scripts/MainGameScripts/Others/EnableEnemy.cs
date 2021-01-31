using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null && enemy.GetComponent<EnemyMovementNew>() != null)
        {
            enemy.GetComponent<EnemyMovementNew>().enabled = true;
        }
    }
}
