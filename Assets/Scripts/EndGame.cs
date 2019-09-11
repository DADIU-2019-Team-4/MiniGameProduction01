using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class EndGame : MonoBehaviour
{
    [SerializeField]
    private float _timeUntilEnd = 2f;

    [SerializeField]
    private float _timeUntilDoorCloses = 1.5f;

    [SerializeField]
    private float _fadeTime = 3f;

    [SerializeField]
    private GameObject _timmy;

    [SerializeField]
    private GameObject _paperBall;

    [SerializeField]
    private GameObject _blackImage;

    private MenuManager _menuManager;
    private InputController _inputController;
    private GameControl _gameControl;
    private ThrowableObject[] _balls;

    // Start is called before the first frame update
    void Start()
    {
        _menuManager = FindObjectOfType<MenuManager>();
        _inputController = FindObjectOfType<InputController>();
        _gameControl = FindObjectOfType<GameControl>();
        _blackImage.SetActive(false);
        Time.timeScale = _gameControl.StartGameSpeed;
        StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
        _blackImage.SetActive(true);
        _blackImage.GetComponent<Image>().DOFade(1f, _fadeTime);
        yield return new WaitForSeconds(_fadeTime);
        _balls = FindObjectsOfType<ThrowableObject>();
        foreach (ThrowableObject ball in _balls)
            Destroy(ball.gameObject);

        Destroy(_inputController.uncontrollableBalls);

        _timmy.SetActive(false);
        _paperBall.SetActive(true);
        yield return new WaitForSeconds(_timeUntilEnd);
        _blackImage.GetComponent<Image>().DOFade(0f, _fadeTime);
        yield return new WaitForSeconds(_fadeTime);
        _blackImage.SetActive(false);
        yield return new WaitForSeconds(_timeUntilDoorCloses);
        _menuManager.EndGame = true;
        _menuManager.OpenMenu();
    }
}
