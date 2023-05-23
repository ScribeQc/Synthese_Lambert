using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // VARIABLES
    [SerializeField] private float _speed = 10f;
    [SerializeField] private GameObject _projectilePrefab = default;
    [SerializeField] private GameObject _tripleShotPrefab = default;
    [SerializeField] private GameObject _bombPrefab = default;
    [SerializeField] private float _fireRate = 0.1f;
    [SerializeField] private float _bombFireRate = 0.5f;
    private float _canFire = -1f;
    private bool _isTripleShotActive = false;
    private float _ogSpeed = 10f;
    private int _playerHp = 4;
    private bool isFlashing = false;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private UiManager _uiManager;
    private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -3.5f, 0);
        _gameManager = FindObjectOfType<GameManager>();
        _spawnManager = FindObjectOfType<SpawnManager>();
        _uiManager = FindObjectOfType<UiManager>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("turnLeft", false);
        _animator.SetBool("turnRight", false);
        _ogSpeed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(Input.GetKey(KeyCode.LeftShift) && Time.time > _canFire)
        {
            Fire();
        }
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && Time.timeScale == 1)
        {
            StartCoroutine(Bomb());
        }
    }

    // MECHANICS
    private void Move()
    {
        // Input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Animation
        if(Input.GetKey(KeyCode.A))
        {
            _animator.SetBool("turnLeft", true);
            _animator.SetBool("turnRight", false);
        }
        else
        {
            _animator.SetBool("turnLeft", false);
        }
        if(Input.GetKey(KeyCode.D))
        {
            _animator.SetBool("turnRight", true);
            _animator.SetBool("turnLeft", false);
        }
        else
        {
            _animator.SetBool("turnRight", false);
        }
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            _animator.SetBool("turnLeft", false);
            _animator.SetBool("turnRight", false);
        }

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

    // Actions
    private void Fire()
    {
        _canFire = Time.time + _fireRate;
        if(!_isTripleShotActive){
            Instantiate(_projectilePrefab, (transform.position + new Vector3(0, 1.05f, 0)), Quaternion.identity);
        }
        else
        {
            Instantiate(_tripleShotPrefab, (transform.position + new Vector3(0, 4.05f, 0)), Quaternion.identity);
        }
    }

    IEnumerator Bomb()
    {
        _canFire = Time.time + _bombFireRate;
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
        if(!isFlashing)
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
    }

    // POWERUPS
    public void TripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotRoutine());
    }

    public bool IsTripleShotActive()
    {
        return _isTripleShotActive;
    }

    IEnumerator TripleShotRoutine()
    {
        yield return new WaitForSeconds(10f);
        _isTripleShotActive = false;
    }

    public void Speed()
    {
        _speed *= 2f;
        StartCoroutine(SpeedRoutine());
    }

    IEnumerator SpeedRoutine()
    {
        yield return new WaitForSeconds(10f);
        _speed = _ogSpeed;
    }

    public void Repair()
    {
        if(_playerHp < 4)
        {
            _playerHp++;
            _uiManager.UpdateHp(_playerHp * 0.25f);
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
        for (int i = 0; i < 5; i++)
        {
            isFlashing = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f);
            yield return new WaitForSeconds(0.1f);
        }
        isFlashing = false;
    }


    // COLLISIONS
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyProjectile")
        {
            Destroy(other.gameObject);
            Damage();
        }
    }
}
