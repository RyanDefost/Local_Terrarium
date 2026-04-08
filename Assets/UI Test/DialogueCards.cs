using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueDeck : MonoBehaviour
{
    public RectTransform frontCard;
    public RectTransform backCard;

    public TextMeshProUGUI frontText;
    public TextMeshProUGUI backText;

    public CanvasGroup frontCanvas;
    public CanvasGroup backCanvas;

    private AudioSource audioSource;

    public float moveTime = 0.45f;
    public float arcHeight = 140f;

    public List<DialogueContent> dialogue;
    /*    {
               "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy",
        "Goop is goop",
        "dont let the goop consume you",
        "Gooperdy goop",
        "thy shall not be gooped",
        "goopy yoopy"
    };*/

    int dialogueIndex = 0;

    Vector2 frontPos;
    Vector2 backPos;

    bool isAnimating = false;

    private void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void StartDialogue()
    {
        dialogueIndex = 0;
        frontPos = frontCard.anchoredPosition;
        backPos = backCard.anchoredPosition;

        frontCard.SetAsLastSibling();

        frontCard.rotation = Quaternion.identity;
        backCard.rotation = Quaternion.Euler(0, 0, 2f);

        frontCanvas.alpha = 1f;
        backCanvas.alpha = 0.4f;

        frontText.text = dialogue[0].Text;

        if (dialogue.Count > 1)
            backText.text = dialogue[1].Text;

        backText.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isAnimating) return;
            NextDialogue();
            audioSource.Play();
        }
    }

    public void NextDialogue()
    {
        if (dialogueIndex >= dialogue.Count - 1)
        {
            var _dialogueSystem = MultiServiceLocator.GetService<DialogueSystem>();
            _dialogueSystem.DeactivateDialogue();

            return;
        }

        dialogueIndex++;
        StartCoroutine(CardAnimation());
    }

    IEnumerator CardAnimation()
    {
        isAnimating = true;

        float t = 0;

        Vector2 startFront = frontCard.anchoredPosition;
        Vector2 startBack = backCard.anchoredPosition;

        Quaternion startFrontRot = frontCard.rotation;

        Color backColor = backText.color;

        backCard.SetAsLastSibling();

        while (t < moveTime)
        {
            t += Time.deltaTime;

            float lerp = t / moveTime;
            float ease = Mathf.Sin(lerp * Mathf.PI * 0.5f);

            Vector2 frontTarget = backPos;
            Vector2 frontArc = Vector2.Lerp(startFront, frontTarget, ease);
            frontArc.y += Mathf.Sin(ease * Mathf.PI) * arcHeight;

            frontCard.anchoredPosition = frontArc;

            float overshoot = 1.1f;
            Vector2 backTarget = frontPos * overshoot;
            Vector2 backArc = Vector2.Lerp(startBack, backTarget, ease);
            backArc.y += Mathf.Sin(ease * Mathf.PI) * (arcHeight * 0.5f);

            backCard.anchoredPosition = backArc;

            frontCard.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.9f, ease);
            backCard.localScale = Vector3.Lerp(Vector3.one * 0.9f, Vector3.one, ease);

            frontCard.rotation = Quaternion.Lerp(startFrontRot, Quaternion.Euler(0, 0, -10f), ease);
            backCard.rotation = Quaternion.Lerp(backCard.rotation, Quaternion.identity, ease);

            frontCanvas.alpha = Mathf.Lerp(1f, 0.4f, ease);
            backCanvas.alpha = Mathf.Lerp(0.4f, 1f, ease);

            float textFade = Mathf.Clamp01((ease - 0.3f) / 0.7f);
            backColor.a = textFade;
            backText.color = backColor;

            yield return null;
        }

        backCard.anchoredPosition = frontPos;
        frontCard.anchoredPosition = backPos;

        SwapCards();

        frontCard.rotation = Quaternion.identity;
        backCard.rotation = Quaternion.Euler(0, 0, 4f);

        frontText.text = dialogue[dialogueIndex].Text;

        if (dialogueIndex + 1 < dialogue.Count)
        {
            backText.text = dialogue[dialogueIndex + 1].Text;
        }

        backText.color = new Color(0, 0, 0, 0);

        StartCoroutine(FadeInBackCard());

        isAnimating = false;
    }

    void SwapCards()
    {
        RectTransform tempCard = frontCard;
        frontCard = backCard;
        backCard = tempCard;

        CanvasGroup tempCanvas = frontCanvas;
        frontCanvas = backCanvas;
        backCanvas = tempCanvas;

        TextMeshProUGUI tempText = frontText;
        frontText = backText;
        backText = tempText;
    }

    IEnumerator FadeInBackCard()
    {
        float t = 0;
        float duration = 0.15f;

        backCanvas.alpha = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float lerp = t / duration;

            backCanvas.alpha = Mathf.Lerp(0f, 0.2f, lerp);
            yield return null;
        }

        backCanvas.alpha = 0.4f;
    }
}