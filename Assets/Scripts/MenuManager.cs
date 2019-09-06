using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuScreen;
    [SerializeField]
    private GameObject _soundSettings;

    private bool _isSoundSettingsOpen;
    private GameControl _gameControl;

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
            _menuScreen.SetActive(false);
            Time.timeScale = _gameControl.gameSpeed;
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
