using System.Collections;
using UnityEngine;

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

    [SerializeField]
    private Fade _fade;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(_timeUntilPaper);
        _timmy.SetActive(false);
        _paperBall.SetActive(true);
        yield return new WaitForSeconds(_timeUntilEnd);
        _fade.gameObject.SetActive(true);
    }
}
