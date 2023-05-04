using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private UiManager _uiManager;

    void Start()
    {
        _uiManager = FindObjectOfType<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    public void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Explosion")
        {
            Destroy(this.gameObject, 0f);
            _uiManager.AddScore(10);
        }
    }
}
