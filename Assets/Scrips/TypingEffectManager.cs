using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // ?? chuy?n scene sang Menu chính

public class TypingEffectManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // UI TextMeshPro ?? hi?n th? ch?
    public float typingSpeed = 0.001f; // T?c ?? gõ ch?
    public float waitTimeAfterText = 5.0f; // Th?i gian ch? sau khi hi?n th? xong m?i ?o?n
    public CanvasGroup textCanvasGroup; // ?? t?o hi?u ?ng fade out
    public AudioSource typingSound;

    // Danh sách các ?o?n v?n b?n s? hi?n th?
    private string[] storyTexts = new string[]
    {
        "[Prologue] – The Awakening of Drakthor\r\nThe once-glorious kingdom of Thaloria now lies in ruins. ",
        "Drakthor the Eternal Dragon, an immortal beast, has awakened from Infernal Hollow, a cavern buried deep within a colossal volcano.",
        "With a burning desire to reduce the world to ashes and create a new empire for dragons",
        "He has summoned The Four Harbingers of Doom, ancient entities wielding catastrophic power.",
    };

    private int currentTextIndex = 0; // ??m s? th? t? ?o?n hi?n t?i

    void Start()
    {
        StartCoroutine(ShowTextSequence()); // B?t ??u hi?u ?ng ch?y ch?
    }

    IEnumerator ShowTextSequence()
    {
        while (currentTextIndex < storyTexts.Length)
        {
            yield return StartCoroutine(TypeText(storyTexts[currentTextIndex])); // Hi?n t?ng ký t?
            yield return new WaitForSeconds(waitTimeAfterText); // ??i m?t lúc sau khi hi?n xong ?o?n
            yield return StartCoroutine(FadeOutText()); // Ch? bi?n m?t

            currentTextIndex++; // Chuy?n sang ?o?n ti?p theo
        }

        LoadMainMenu(); // Khi hoàn thành t?t c? ?o?n, vào menu chính
    }

    IEnumerator TypeText(string fullText)
    {
        dialogueText.text = ""; // Xóa n?i dung c?
        textCanvasGroup.alpha = 1; // ??m b?o ch? hi?n th?
        if (typingSound != null)
        {
            typingSound.loop = false; // Không cho l?p vô h?n
            typingSound.Play();
        }
        for (int i = 0; i < fullText.Length; i++)
        {
            dialogueText.text += fullText[i]; // Hi?n th? t?ng ký t?
            //if (typingSound != null && !typingSound.isPlaying) // Phát âm thanh n?u không b? trùng
            //{

            //}
            if (typingSound != null && !typingSound.isPlaying)
            {
                typingSound.Play();
            }
            yield return new WaitForSeconds(typingSpeed);
        }
        if (typingSound != null)
        {
            typingSound.Stop();
        }
    }

    IEnumerator FadeOutText()
    {
        float fadeDuration = 1.5f; // Th?i gian m? d?n
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            textCanvasGroup.alpha = 1 - (elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textCanvasGroup.alpha = 0; // Hoàn toàn bi?n m?t
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuScene"); // Thay "MainMenu" b?ng tên scene menu c?a b?n
    }
}
