using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    public enum Language
    {
        Danish,
        English
    }

    public static Language CurrentLanguage;

    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(SwitchLanguage);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SwitchLanguage()
    {
        if (gameObject.name.Contains("Danish"))
        {
            // Ja, det bo' dejlig!
            CurrentLanguage = Language.Danish;
            AkSoundEngine.SetState("dia_lang", "DK");
        }
        else
        {
            // Ye olde butcherede Englishe!
            CurrentLanguage = Language.English;
            AkSoundEngine.SetState("dia_lang", "EN");
        }
    }

}
