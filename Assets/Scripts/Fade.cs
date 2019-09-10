using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private enum FadeTypes
    {
        FadeIn,
        FadeOut
    };

    [SerializeField]
    private FadeTypes _fadeType;
    [SerializeField]
    private float _fadeDuration = 5f;

    private Image _image;

    [SerializeField]
    private int _nextSceneIndex = 0;

    void Start()
    {
        _image = GetComponent<Image>();
        if (_fadeType == FadeTypes.FadeIn)
            _image.DOFade(0f, _fadeDuration);
        else
        {
            _image.DOFade(1f, _fadeDuration);
            StartCoroutine(ToNextScene());
        }

    }

    private IEnumerator ToNextScene()
    {
        yield return new WaitForSeconds(_fadeDuration);
        SceneManager.LoadScene(_nextSceneIndex);
    }
}
