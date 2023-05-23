using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private GameObject _projectilePrefab = default;
    [SerializeField] private GameObject _bombPrefab = default;
    [SerializeField] private float _fireRate = 3f;
    private float _canFire = -1f;
    private GameManager _gameManager;
    
    private UiManager _uiManager;
    private Player _player;
    private Animator _animator;
    private float randomY = 0;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UiManager>();
        _player = FindObjectOfType<Player>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("turnLeft", false);
        _animator.SetBool("turnRight", false);
        randomY = Random.Range(1f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if(Time.time > _canFire)
        {
            StartCoroutine(Fire());
        }
    }

    public void CalculateMovement()
    {
        var enemyPosition = transform.position.x;
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        float worldSpace = Mathf.Abs(transform.position.x);

        if (transform.position.y <= randomY)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8, 8), randomY, 0);
            Vector3 movement = new Vector3(worldSpace * Mathf.Sin(Time.time * _speed), randomY, 0);
            transform.Translate(movement *_speed * Time.deltaTime);
        }

        // Animation
        if (transform.position.x < enemyPosition)
        {
            _animator.SetBool("turnLeft", true);
            _animator.SetBool("turnRight", false);
        }
        if (transform.position.x > enemyPosition)
        {
            _animator.SetBool("turnLeft", false);
            _animator.SetBool("turnRight", true);
        }
        if(transform.position.x == enemyPosition)
        {
            _animator.SetBool("turnLeft", false);
            _animator.SetBool("turnRight", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                _player.Damage();
            }
            Instantiate(_bombPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(other.tag == "PlayerProjectile")
        {
            Destroy(other.gameObject);
            if(_uiManager != null)
            {
                _gameManager.AddScore(50 * _gameManager.GetScoreMultiplier());
            }
            Instantiate(_bombPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator Fire()
    {
        _canFire = Time.time + _fireRate;
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        Instantiate(_projectilePrefab, (transform.position + new Vector3(0, -1.05f, 0)), Quaternion.Euler(180, 0, 0));
        yield return new WaitForSeconds(3f);
    }
}
