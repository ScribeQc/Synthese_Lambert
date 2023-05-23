using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private AudioClip _clip;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        StartCoroutine(ColorRoutine());
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -9f)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator ColorRoutine()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Explosion")
        {
            if(_player != null)
            {
                _player.Repair();
            }
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}
