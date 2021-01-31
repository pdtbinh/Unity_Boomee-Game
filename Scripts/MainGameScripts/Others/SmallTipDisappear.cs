using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTipDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(disappear());
    }

    IEnumerator disappear()
    {
        yield return new WaitForSeconds(8f);
        Destroy(gameObject);
    }
}
