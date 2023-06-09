using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Text speakerText;
    public Button[] choiceButtons;
    public Button nextButton;

    private Action onNextClicked;

    public void Initialize(Action onNextClickedCallback)
    {
        onNextClicked = onNextClickedCallback;
        nextButton.onClick.AddListener(() => onNextClicked?.Invoke());
    }

    public void Open()
    {
        dialoguePanel.SetActive(true);
    }

    public void Hide()
    {
        dialoguePanel.SetActive(false);
    }

    public void DisplayText(string text, string speaker)
    {
        dialogueText.text = text;
        speakerText.text = speaker;
    }

    public void DisplayChoices(string[] choices, Action<int> onChoiceSelectedCallback)
    {
        for (int i = 0; i < choices.Length; i++)
        {
            int choiceIndex = i;
            choiceButtons[i].gameObject.SetActive(true);
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => onChoiceSelectedCallback?.Invoke(choiceIndex));
            choiceButtons[i].GetComponentInChildren<Text>().text = choices[i];
        }

        nextButton.gameObject.SetActive(false);
    }

    public void HideChoices()
    {
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void ShowNextButton()
    {
        nextButton.gameObject.SetActive(true);
    }
}
