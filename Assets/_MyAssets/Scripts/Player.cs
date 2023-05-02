using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private GameObject _projectilePrefab = default;
    [SerializeField] private GameObject _bombPrefab = default;
    [SerializeField] private float _fireRate = 5f;
    private float _canFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -4, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.E) && Time.time > _canFire)
        {
            Fire();
        }
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            StartCoroutine(Bomb());
        }
    }

    private void Move()
    {
        // Input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Mouvement
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        // Limites
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4), 0);
        if(transform.position.x > 12f)
        {
            transform.position = new Vector3(-12f, transform.position.y, 0);
        }
        else if(transform.position.x < -12f)
        {
            transform.position = new Vector3(12f, transform.position.y, 0);
        }
    }

    private void Fire()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_projectilePrefab, (transform.position + new Vector3(0, 1.05f, 0)), Quaternion.identity);
    }

    IEnumerator Bomb()
    {
        _canFire = Time.time + _fireRate;
        float worldSpace = Mathf.Abs(transform.position.y) + 4f;
        Vector3 position = new Vector3(transform.position.x, transform.position.y, 0);
        for (int i = 0; i < worldSpace; i++)
        {
            Instantiate(_bombPrefab, (position + new Vector3(0, -i-1f, 0)), Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
        }
    }
}