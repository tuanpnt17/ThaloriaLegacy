using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;
    public Button nextButton;

    private string[] dialogues;
    private int currentIndex = 0;

    // 🔹 Tạo sự kiện khi hội thoại kết thúc
    public event Action OnDialogueEnd;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(ShowNextDialogue);
    }

    public void StartDialogue(string[] newDialogues, Action onEnd)
    {
        dialogues = newDialogues;
        currentIndex = 0;
        OnDialogueEnd += onEnd;  // 🔹 Đăng ký sự kiện khi hội thoại kết thúc
        dialoguePanel.SetActive(true);
        Time.timeScale = 0f; // ⏸ Dừng game
        ShowNextDialogue();
    }

    public void ShowNextDialogue()
    {
        if (currentIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentIndex];
            currentIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f; // ▶ Tiếp tục game
        OnDialogueEnd?.Invoke(); // 🔹 Gọi sự kiện
        OnDialogueEnd = null; // 🔹 Xóa hết listener để tránh lỗi đăng ký nhiều lần
    }

    private void Update()
    {
        if (dialoguePanel.activeSelf && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            ShowNextDialogue();
        }
    }

    private void OnDestroy()
    {
        nextButton.onClick.RemoveAllListeners();
    }
}
