using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ExplosionRoutine());
    }

    IEnumerator ExplosionRoutine()
    {
        yield return new WaitForSeconds(1.05f);
        Destroy(this.gameObject);
    }
}
