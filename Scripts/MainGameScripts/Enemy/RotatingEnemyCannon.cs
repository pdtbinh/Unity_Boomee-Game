using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemyCannon : MonoBehaviour
{
    public float rotatingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotatingSpeed = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        float sinOfTime = Mathf.Sin(Time.time * rotatingSpeed);
        float angleRotated = sinOfTime * 30 - 25;
        transform.localRotation = Quaternion.Euler(0, angleRotated, 0);
    }
}
