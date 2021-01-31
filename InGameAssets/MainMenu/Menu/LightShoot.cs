using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShoot : MonoBehaviour
{
    public GameObject enemyBomb;

    private float shootingForce;

    private float scale;

    // Start is called before the first frame update
    void Start()
    {
        shootingForce = 15f;

        scale = 5.2f;

        StartCoroutine("shootRegularly");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void justGonnaShootLightly()
    {
        GameObject bomb = Instantiate(enemyBomb, transform.position, transform.rotation);

        bomb.transform.localScale = new Vector3(scale, scale, scale);

        bomb.GetComponent<Rigidbody>().velocity = bomb.transform.forward * shootingForce;
    }

    IEnumerator shootRegularly()
    {
        yield return new WaitForSeconds(6.5f);
        justGonnaShootLightly();

        while (true)
        {
            yield return new WaitForSeconds(14f);
            justGonnaShootLightly();
        }
    }
}
