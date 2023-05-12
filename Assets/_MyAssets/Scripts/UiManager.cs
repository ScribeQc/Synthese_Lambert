using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image _hpBar;
    private int _hpBarWidth = 420;
    private int _hpBarHeight = 50;
    private int _newHpBarWidth;
    private int _newHpBarHeight;
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        // Score
        _score = 0;
        scoreText.text = "SCORE: " + _score.ToString("00000000");

        // Time
        var minutes = Mathf.FloorToInt(Time.time / 60F);
        var seconds = Mathf.FloorToInt(Time.time - minutes * 60);
        timeText.text = "TEMPS: " + minutes.ToString("00") + ":" + seconds.ToString("00");

        // HpBar
        var _hpBarRectTransform = _hpBar.transform as RectTransform;
        _hpBarRectTransform.sizeDelta = new Vector2(_hpBarWidth, _hpBarHeight);
        _newHpBarWidth = _hpBarWidth;
        _newHpBarHeight = _hpBarHeight;
    }

    // Update is called once per frame
    void Update()
    {
        // Score
        scoreText.text = "SCORE: " + _score.ToString("00000000");

        // Time
        var minutes = Mathf.FloorToInt(Time.time / 60F);
        var seconds = Mathf.FloorToInt(Time.time - minutes * 60);
        timeText.text = "TEMPS: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void AddScore(int i)
    {
        _score += i;
    }

    public int GetScore()
    {
        return _score;
    }

    public void UpdateHp(float hp)
    {
        _newHpBarWidth = (int)(_hpBarWidth * hp);
        _newHpBarHeight = (int)(_hpBarHeight * hp);
        if(_newHpBarWidth < 0)
        {
            _newHpBarWidth = 0;
        }
        if(_newHpBarWidth > _hpBarWidth)
        {
            _newHpBarWidth = 440;
        }
        var _hpBarRectTransform = _hpBar.transform as RectTransform;
        _hpBarRectTransform.sizeDelta = new Vector2(_newHpBarWidth, _newHpBarHeight);
    }
}
