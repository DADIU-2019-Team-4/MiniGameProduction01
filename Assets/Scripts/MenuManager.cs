using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuScreen;
    [SerializeField]
    private GameObject _soundSettings;

    private bool _isSoundSettingsOpen;
    private GameControl _gameControl;

    public bool EndGame { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _gameControl = FindObjectOfType<GameControl>();
        _soundSettings.SetActive(false);
    }

    public void CloseMenu()
    {
        if (!_isSoundSettingsOpen)
        {
            if (!EndGame)
            {
                _menuScreen.SetActive(false);
                Time.timeScale = _gameControl.gameSpeed;
            }
            else
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    public void OpenMenu()
    {
        Time.timeScale = 0f;
        _menuScreen.SetActive(true);
    }

    public void CloseSoundSettings()
    {
        _isSoundSettingsOpen = false;
        _soundSettings.SetActive(false);
    }

    public void OpenSoundSettings()
    {
        _isSoundSettingsOpen = true;
        _soundSettings.SetActive(true);
    }

    public void ExitGame()
    {
        if (!_isSoundSettingsOpen)
            Application.Quit();
    }
}
