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
    private Color full = new Color(0f, 255f, 0f);
    private Color threeQuarters = new Color(255f, 200f, 0f);
    private Color half = new Color(255f, 127/255f, 0f);
    private Color low = new Color(255f, 0f, 0f);
    private GameManager _gameManager;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _player = FindObjectOfType<Player>();

        // Score
        scoreText.text = "SCORE: " + _gameManager.GetScoreString();

        // Time
        timeText.text = "TEMPS: " + _gameManager.GetTimeString();

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
        scoreText.text = "SCORE: " + _gameManager.GetScoreString();

        // Time
        if(!_gameManager.IsGameOver())
        {
            timeText.text = "TEMPS: " + _gameManager.GetTimeString();
        }
    }

    private void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        for (int i = 0; i < 5; i++)
        {
            _hpBar.GetComponent<Image>().color = new Color(255f, 0f, 0f);
            yield return new WaitForSeconds(0.1f);
            if(_player.GetHp() < 4)
            {
                _hpBar.GetComponent<Image>().color = threeQuarters;
            }
            if(_player.GetHp() < 3)
            {
                _hpBar.GetComponent<Image>().color = half;
            }
            if(_player.GetHp() < 2)
            {
                _hpBar.GetComponent<Image>().color = low;
            }
            yield return new WaitForSeconds(0.1f);  
        }
    }

    public void UpdateHp(float hp)
    {
        _newHpBarWidth = (int)(_hpBarWidth * hp);
        _newHpBarHeight = (int)(_hpBarHeight * (hp + 0.1f));
        Flash();
        if(_newHpBarWidth < 0)
        {
            _newHpBarWidth = 0;
        }
        if(_newHpBarWidth > _hpBarWidth)
        {
            _newHpBarWidth = 440;
        }
        if(_player.GetHp() < 4)
        {
            _hpBar.GetComponent<Image>().color = threeQuarters;
        }
        if(_player.GetHp() < 3)
        {
            _hpBar.GetComponent<Image>().color = half;
        }
        if(_player.GetHp() < 2)
        {
            _hpBar.GetComponent<Image>().color = low;
        }

        var _hpBarRectTransform = _hpBar.transform as RectTransform;
        _hpBarRectTransform.sizeDelta = new Vector2(_newHpBarWidth, _newHpBarHeight);
    }
}
