using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if(transform.position.y > 8f)
        {
            Destroy(this.gameObject);
        }
    }
}
