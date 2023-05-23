using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject _instructionContainer = default;
    [SerializeField] private GameObject _optionContainer = default;

    // Start is called before the first frame update
    void Start()
    {
        _instructionContainer.SetActive(false);
        _optionContainer.SetActive(false);
    }

    public void StartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void Menu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }

    public void OpenIntstructions() {
        _instructionContainer.SetActive(true);
    }

    public void CloseInstructions() {
        _instructionContainer.SetActive(false);
    }

    public void OpenOptions() {
        _optionContainer.SetActive(true);
    }

    public void CloseOptions() {
        _optionContainer.SetActive(false);
    }

    public void MuteGame() {
        AudioListener.pause = !AudioListener.pause;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
