using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private float _time;
    private float _timeFinal;
    private int _score;
    private int _scoreMultiplier;
    private int _spawnMultiplier;
    private int _gameStatus;
    private bool _gameOver;
    private int temp = 1;
    private bool _pause = false;
    [SerializeField] private GameObject _background = default;
    [SerializeField] private GameObject _tree = default;
    [SerializeField] private GameObject _deadTree = default;
    [SerializeField] private GameObject _pauseMenu = default;
    [SerializeField] private GameObject _dialogueColonel = default;
    [SerializeField] private TextMeshProUGUI _colonelText = default;
    [SerializeField] private GameObject _dialoguePilote = default;
    [SerializeField] private TextMeshProUGUI _piloteText = default;
    private int _dialogue;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameOver = false;
        _pause = false;
        _pauseMenu.SetActive(false);
        _gameStatus = 1;
        _time = 0;
        _timeFinal = 0;
        _score = 0;
        _scoreMultiplier = 1;
        _spawnMultiplier = 1;
        _deadTree.SetActive(false);
        StartCoroutine(Dialogue1());
    }

    // Update is called once per frame
    void Update()
    {
        _time = Time.timeSinceLevelLoad;
        UpdateMultiplier();
        UpdateStatus();
        if(_gameStatus == 2)
        {
            _deadTree.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public string GetTimeString()
    {
        var minutes = Mathf.FloorToInt(_time / 60f);
        var seconds = Mathf.FloorToInt(_time - minutes * 60f);
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public string GetTimeFinalString()
    {
        var minutes = Mathf.FloorToInt(_timeFinal / 60f);
        var seconds = Mathf.FloorToInt(_timeFinal - minutes * 60f);
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

    public int GetScoreMultiplier()
    {
        return _scoreMultiplier;
    }

    public int GetSpawnMultiplier()
    {
        return _spawnMultiplier;
    }

    public void UpdateMultiplier()
    {
        var minutes = Mathf.FloorToInt(_time / 60f);
        if(minutes > 1)
        {
            _scoreMultiplier = minutes * _gameStatus;
            _spawnMultiplier = minutes * _gameStatus;
        }
    }

    public void UpdateStatus()
    {
        if(_score >= 50000)
        {
            _gameStatus = 2;
            _background.GetComponent<SpriteRenderer>().color = new Color(50/255f, 50/255f, 50/255f);
            if(temp == 1)
            {
                Destroy(_tree);
                _deadTree.SetActive(true);
                _deadTree.GetComponent<ParticleSystem>().Play();
                StartCoroutine(Dialogue2());
                temp++;
            }
        }
        if(_score > 1000000)
        {
            _gameStatus = 3;
            Destroy(_background);
            Destroy(_deadTree);
            StartCoroutine(Dialogue3());
        }
    }

    public int GetGameStatus()
    {
        return _gameStatus;
    }

    IEnumerator Dialogue1()
    {
        Time.timeScale = 0;
        _dialogue = 0;
        while(_dialogue == 0)
        {
            _dialogueColonel.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _dialogue = 1;
            }
            yield return null;
        }
        // yield return new WaitForSecondsRealtime(5f);
        while(_dialogue == 1)
        {
            _dialogueColonel.SetActive(false);
            _dialoguePilote.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _dialogue = 2;
            }
            yield return null;
        }
        // yield return new WaitForSecondsRealtime(2f);
        while(_dialogue == 2)
        {
            _dialoguePilote.SetActive(false);
            _dialogue = 0;
            yield return null;
        }
        Time.timeScale = 1;
    }

    IEnumerator Dialogue2()
    {
        Time.timeScale = 0;
        _dialogueColonel.SetActive(true);
        _colonelText.text = "Thunder-1, ici le Colonel. Vous venez de rentrer dans la zone démilitarisée. Soyez prudent, un grand nombre d'enni s'approche de votre position.";
        yield return new WaitForSecondsRealtime(3f);
        _dialogueColonel.SetActive(false);
        _dialoguePilote.SetActive(true);
        _piloteText.text = "Bien reçu Colonel.";
        yield return new WaitForSecondsRealtime(2f);
        _dialoguePilote.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator Dialogue3()
    {
        Time.timeScale = 0;
        _colonelText.text = "Thunder-1, il ne reste que vous. Vous venez de passer la frontière et vous êtes maintenant en territoire ennemi. Faites demi-tour immédiatement! On ne veut pas un mort de plus aujourd'hui... C'est l'heure de rentrer à la maison.";
        _dialogueColonel.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        _dialogueColonel.SetActive(false);
        _piloteText.text = "Négatif Colonel. Rien ne m'attend à la maison... De toute façon, je n'ai plus assez de carburant pour le retour.";
        _dialoguePilote.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        _dialoguePilote.SetActive(false);
        _colonelText.text = "Allumez-les tous, Thunder-1.";
        _dialogueColonel.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        _dialogueColonel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        _pause = !_pause;
        if(_pause)
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
        }
    }

    public void GameOver()
    {
        _timeFinal = _time;
        _gameOver = true;
        Debug.Log("Game Over");
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndMenu");
    }

    public bool IsGameOver()
    {
        return _gameOver;
    }
}
