using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        scoreText.text = "Score: " + _score.ToString("000000");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + _score.ToString("000000");
    }

    public void AddScore(int i)
    {
        _score += i;
    }
}
