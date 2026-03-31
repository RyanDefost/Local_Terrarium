using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class IntroStarter : MonoBehaviour
{
    public bool autoActivate = true;

    [SerializeField] private Dialogue _introText;
    [SerializeField] private Canvas _startCanvas;
    private Image _fadePannel;

    private DialogueSystem _dialogueSystem;

    private float fadeTime = 2;

    private void Start()
    {
        if (!autoActivate) return;
        PlayIntro();
    }
    public void PlayIntro()
    {
        _startCanvas.gameObject.SetActive(true);
        _fadePannel = _startCanvas.GetComponentInChildren<Image>();


        _dialogueSystem = MultiServiceLocator.GetService<DialogueSystem>();


        //--
        StateChanger.Instance.SetState("Talk");

        _dialogueSystem.ActivateDialogue(_introText);
        _dialogueSystem.OnStopDialogue += StartFade;
    }

    private void StartFade()
    {
        _dialogueSystem.OnStopDialogue -= StartFade;
        StartCoroutine(FadeOut(_fadePannel));
    }
    IEnumerator FadeOut(Image image)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return new WaitForSeconds(0.01f);
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
        }
    }
}
