using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestReseter : MonoBehaviour
{
    [SerializeField] private Dialogue _introText;
    [SerializeField] private Canvas _startCanvas;
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _playerSpawn;
    private Image _fadePannel;

    private DialogueSystem _dialogueSystem;
    private QuestManager _questManager;
    private float fadeTime = 2;

    public void PlayReset()
    {
        _startCanvas.gameObject.SetActive(true);
        _fadePannel = _startCanvas.GetComponentInChildren<Image>();

        _dialogueSystem = MultiServiceLocator.GetService<DialogueSystem>();
        _questManager = MultiServiceLocator.GetService<QuestManager>();
        //--
        StateChanger.Instance.SetState("Talk");
        Debug.Log("TEST<><>");
        StartCoroutine(FadeIn(_fadePannel));
    }

    IEnumerator FadeIn(Image image)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return new WaitForSeconds(0.01f);
            elapsedTime += Time.deltaTime;
            c.a = 0.0f + Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
        }

        _player.transform.localPosition = _playerSpawn;
        yield return new WaitForSeconds(1f);
        _dialogueSystem.ActivateDialogue(_introText);
        _questManager.NextQuest();

        //
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOut(image));
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
