using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    void Start()
    {
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, volume: 0.5f);
    }

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
