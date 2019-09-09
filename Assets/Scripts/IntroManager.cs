using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField]
    private Text _companyName;
    [SerializeField]
    private Text _gameName;

    [SerializeField]
    private float _fadeDuration = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartIntro());
    }

    private IEnumerator StartIntro()
    {
        _companyName.DOFade(1f, _fadeDuration);
        yield return new WaitForSeconds(_fadeDuration);
        _companyName.DOFade(0f, _fadeDuration);
        yield return new WaitForSeconds(_fadeDuration);

        _gameName.DOFade(1f, _fadeDuration);
        yield return new WaitForSeconds(_fadeDuration);
        _gameName.DOFade(0f, _fadeDuration);
        yield return new WaitForSeconds(_fadeDuration);
        SceneManager.LoadScene("Main");
    }
}
