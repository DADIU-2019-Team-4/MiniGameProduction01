using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private float _timeUntilPaper = 2f;
    [SerializeField]
    private float _timeUntilEnd = 2f;

    [SerializeField]
    private GameObject _timmy;

    [SerializeField]
    private GameObject _paperBall;

    private MenuManager _menuManager;
    private GameControl _gameControl;

    // Start is called before the first frame update
    void Start()
    {
        _menuManager = FindObjectOfType<MenuManager>();
        _gameControl = FindObjectOfType<GameControl>();
        StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(_timeUntilPaper);
        foreach (GameObject ball in _gameControl.Balls)
        {
            ball.SetActive(false);
        }
        _timmy.SetActive(false);
        _paperBall.SetActive(true);
        yield return new WaitForSeconds(_timeUntilEnd);
        _menuManager.OpenMenu();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}
