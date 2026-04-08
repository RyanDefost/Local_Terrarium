using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestReseter : MonoBehaviour
{
    [SerializeField] private Dialogue _introText;
    [Space]
    [SerializeField] private Dialogue _endingText;
    [SerializeField] private Camera _endingCamera;
    [SerializeField] private Canvas _endingCanvas;

    [Space]
    [SerializeField] private Canvas _startCanvas;
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _playerSpawn;
    private Image _fadePannel;

    private DialogueSystem _dialogueSystem;
    private QuestManager _questManager;
    private float fadeTime = 2;

    public void EndQuest()
    {

        //StartCoroutine(FadeBackOut(_fadePannel));

        _dialogueSystem.ActivateDialogue(_endingText);
        _dialogueSystem.OnStopDialogue += SetEnd;
        //Camera.SetupCurrent(_endingCamera);
        //Cursor.lockState = CursorLockMode.Confined;
    }

    private void SetEnd()
    {
        _dialogueSystem.OnStopDialogue -= SetEnd;
        StateChanger.Instance.SetState("End");

        _endingCanvas.gameObject.SetActive(true);
    }

    public void ResetGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void PlayReset()
    {
        _startCanvas.gameObject.SetActive(true);
        _fadePannel = _startCanvas.GetComponentInChildren<Image>();

        _dialogueSystem = MultiServiceLocator.GetService<DialogueSystem>();
        _questManager = MultiServiceLocator.GetService<QuestManager>();
        //--
        StateChanger.Instance.SetState("Talk");
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
            c.a = 0.0f + Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
        }

        _player.transform.localPosition = _playerSpawn;
        yield return new WaitForSeconds(1f);
        _dialogueSystem.ActivateDialogue(_introText);
        _questManager.NextQuest();

        //
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeBackIn(image));
    }

    IEnumerator FadeBackOut(Image image)
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
    }

    IEnumerator FadeBackIn(Image image)
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
