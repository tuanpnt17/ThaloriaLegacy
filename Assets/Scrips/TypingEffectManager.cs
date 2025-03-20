
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypingEffectManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.05f;
    public float waitTimeAfterText = 3.0f;
    public CanvasGroup textCanvasGroup;
    public AudioSource typingSound;
    public GameObject skipButton;

    private string[] storyTexts = new string[]
    {
        "[Prologue] – The Awakening of Drakthor\nThe once-glorious kingdom of Thaloria now lies in ruins.",
        "Drakthor the Eternal Dragon, an immortal beast, has awakened from Infernal Hollow, a cavern buried deep within a colossal volcano.",
        "With a burning desire to reduce the world to ashes and create a new empire for dragons.",
        "He has summoned The Four Harbingers of Doom, ancient entities wielding catastrophic power."
    };

    private int currentTextIndex = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool isSkipping = false;

    void Start()
    {
        StartCoroutine(ShowTextSequence());
    }

    IEnumerator ShowTextSequence()
    {
        while (currentTextIndex < storyTexts.Length)
        {
            isSkipping = false;
            typingCoroutine = StartCoroutine(TypeText(storyTexts[currentTextIndex]));
            yield return typingCoroutine;

            if (!isSkipping) // N?u ch?a Skip, ??i tr??c khi fade out
            {
                yield return new WaitForSeconds(waitTimeAfterText);
            }

            yield return StartCoroutine(FadeOutText());

            currentTextIndex++; // Chuy?n sang ?o?n ti?p theo
        }

        LoadMainMenu();
    }

    IEnumerator TypeText(string fullText)
    {
        dialogueText.text = "";
        textCanvasGroup.alpha = 1;
        isTyping = true;

        if (typingSound != null && !typingSound.isPlaying)
        {
            typingSound.Play();
        }

        foreach (char letter in fullText.ToCharArray())
        {
            if (isSkipping) // N?u b?m Skip, hi?n th? toàn b? ?o?n ngay l?p t?c
            {
                dialogueText.text = fullText;
                break;
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        typingSound?.Stop();
    }

    IEnumerator FadeOutText()
    {
        float fadeDuration = 1.5f;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            textCanvasGroup.alpha = 1 - (elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textCanvasGroup.alpha = 0;
    }

    public void SkipToNextText()
    {
        isSkipping = true; // ?ánh d?u tr?ng thái Skip

        if (isTyping) // N?u ch? ?ang ch?y, hi?n th? toàn b? ngay l?p t?c
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = storyTexts[currentTextIndex];
            isTyping = false;
            typingSound?.Stop();
        }
        else // N?u ?ã hi?n th? xong, chuy?n ?o?n ngay l?p t?c
        {
            if (currentTextIndex < storyTexts.Length - 1)
            {
                currentTextIndex++;
                StopAllCoroutines();
                StartCoroutine(ShowTextSequence());
            }
            else
            {
                LoadMainMenu();
            }
        }
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
