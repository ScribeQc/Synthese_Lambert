using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float _time;
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        _time = 0;
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _time = Time.time;
    }

    public string GetTimeString()
    {
        var minutes = Mathf.FloorToInt(_time / 60f);
        var seconds = Mathf.FloorToInt(_time - minutes * 60f);
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
        _score += i;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
