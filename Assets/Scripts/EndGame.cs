using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private float _timeUntilEnd = 2f;

    [SerializeField]
    private float _timeUntilDoorCloses = 1.5f;

    [SerializeField]
    private GameObject _timmy;

    [SerializeField]
    private GameObject _paperBall;

    [SerializeField]
    private GameObject _blackImage;

    private MenuManager _menuManager;
    private ThrowableObject[] _balls;

    // Start is called before the first frame update
    void Start()
    {
        _menuManager = FindObjectOfType<MenuManager>();
        StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
        _blackImage.SetActive(true);
        _balls = FindObjectsOfType<ThrowableObject>();
        foreach (ThrowableObject ball in _balls)
            Destroy(ball.gameObject);

        _timmy.SetActive(false);
        _paperBall.SetActive(true);
        yield return new WaitForSeconds(_timeUntilEnd);
        _blackImage.SetActive(false);
        yield return new WaitForSeconds(_timeUntilDoorCloses);
        _menuManager.EndGame = true;
        _menuManager.OpenMenu();
    }
}
