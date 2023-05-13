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
    private int _playerHp = 4;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private UiManager _uiManager;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -4, 0);
        _gameManager = FindObjectOfType<GameManager>();
        _spawnManager = FindObjectOfType<SpawnManager>();
        _uiManager = FindObjectOfType<UiManager>();
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
        if(transform.position.x > 9.3f)
        {
            transform.position = new Vector3(-9.3f, transform.position.y, 0);
        }
        else if(transform.position.x < -9.3f)
        {
            transform.position = new Vector3(9.3f, transform.position.y, 0);
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
            //Instantiate(_bombPrefab, (position + new Vector3(0, -i-1f, 0)), Quaternion.identity);
            Instantiate(_bombPrefab, (position + new Vector3(0, i-6f, 0)), Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Damage()
    {
        _playerHp--;
        _uiManager.UpdateHp(_playerHp * 0.25f);
        Flash();
        if(_playerHp < 1)
        {
            _spawnManager.OnPlayerDeath();
            Vector3 position = new Vector3(transform.position.x, transform.position.y, 0);
            Instantiate(_bombPrefab, position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void Heal()
    {
        if(_playerHp < 4)
        {
            _playerHp++;
            _uiManager.UpdateHp(_playerHp * 0.25f);
            Flash();
        }
    }

    public int GetHp()
    {
        return _playerHp;
    }

    private void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyProjectile")
        {
            Destroy(other.gameObject);
            Damage();
        }
    }
}
