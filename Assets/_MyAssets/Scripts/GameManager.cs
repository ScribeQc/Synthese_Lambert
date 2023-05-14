using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float _time;
    private int _score;
    private int _scoreMultiplier;
    private int _spawnMultiplier;
    private bool _gameOver;
    [SerializeField] private GameObject _background = default;
    [SerializeField] private GameObject _tree = default;
    [SerializeField] private GameObject _cloud = default;

    // Start is called before the first frame update
    void Start()
    {
        _gameOver = false;
        _time = 0;
        _score = 0;
        _scoreMultiplier = 1;
        _spawnMultiplier = 1;
        _background.GetComponent<SpriteRenderer>().color = new Color(115f, 140f, 115f, 255f);
        _cloud.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _time = Time.time;
        UpdateMultiplier();
        UpdateBackground();
    }

    public string GetTimeString()
    {
        var minutes = Mathf.FloorToInt(_time / 60f);
        var seconds = Mathf.FloorToInt(_time - minutes * 60f);
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public string GetTimeString(float time)
    {
        var minutes = Mathf.FloorToInt(time / 60f);
        var seconds = Mathf.FloorToInt(time - minutes * 60f);
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public int GetScore()
    {
        return _score;
    }

    public string GetScoreString()
    {
        return _score.ToString("00000000");
    }

    public void AddScore(int i)
    {
        _score += (i * _scoreMultiplier);
    }

    public void AddScoreMultiplier(int i)
    {
        _scoreMultiplier += i;
    }

    public int GetScoreMultiplier()
    {
        return _scoreMultiplier;
    }

    public void AddSpawnMultiplier(int i)
    {
        _spawnMultiplier += i;
    }

    public int GetSpawnMultiplier()
    {
        return _spawnMultiplier;
    }

    public void UpdateMultiplier()
    {
        int[] scoreArray = new int[GetScoreString().Length];
        for (int i = 0; i < GetScoreString().Length; i++)
        {
            scoreArray[i] = int.Parse(GetScoreString()[i].ToString());
            if(scoreArray[5] > 0)
            {
                _scoreMultiplier = scoreArray[5] + 1;
                _spawnMultiplier = scoreArray[5] + 1;
            }
        }
    }

    public void UpdateBackground()
    {
        while(_score < 50000)
        {
            _background.GetComponent<SpriteRenderer>().color = new Color(115f, 140f, 115f, 255f);
            _cloud.SetActive(false);
        }
        while(_score >= 50000)
        {
            _background.GetComponent<SpriteRenderer>().color = new Color(60f, 60f, 60f, 255f);
            _tree.SetActive(false);
            _cloud.SetActive(false);
        }
        while(_score > 100000)
        {
            _background.GetComponent<SpriteRenderer>().enabled = false;
            _cloud.SetActive(true);
        }
    }

    public void GameOver()
    {
        _gameOver = true;
        Debug.Log("Game Over");
    }

    public bool IsGameOver()
    {
        return _gameOver;
    }
}
