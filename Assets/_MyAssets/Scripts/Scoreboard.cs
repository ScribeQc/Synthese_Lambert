using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTxt = default;
    [SerializeField] private TextMeshProUGUI _timeTxt = default;
    GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _scoreTxt.text = "SCORE: " + _gameManager.GetScoreString();
        _timeTxt.text = "TEMPS: " + _gameManager.GetTimeFinalString();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreTxt.text = "SCORE: " + _gameManager.GetScoreString();
        _timeTxt.text = "TEMPS: " + _gameManager.GetTimeFinalString();
    }

    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }
}
