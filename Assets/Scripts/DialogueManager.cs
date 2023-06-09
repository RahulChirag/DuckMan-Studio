using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public DialogueData dialogueData;
    public DialogueUI dialogueUI;

    private int currentNodeIndex;

    private void Start()
    {
        if (dialogueUI == null)
        {
            Debug.Log("DialogueUI component is not assigned!");
            return;
        }
        
        StartConversation(0);
        dialogueUI.Initialize(OnNextClicked);
    }

    public void StartConversation(int startNodeIndex)
    {
        currentNodeIndex = startNodeIndex;
        DisplayCurrentNode();
        dialogueUI.Open();
    }

    private void DisplayCurrentNode()
    {
        if (dialogueData == null || dialogueData.conversationNodes.Count == 0)
        {
            Debug.LogError("No dialogue data assigned or no conversation nodes!");
            return;
        }

        if (currentNodeIndex >= 0 && currentNodeIndex < dialogueData.conversationNodes.Count)
        {
            ConversationNode currentNode = dialogueData.conversationNodes[currentNodeIndex];
            dialogueUI.DisplayText(currentNode.dialogueText, currentNode.speaker);

            if (currentNode.choices.Count > 0)
            {
                dialogueUI.DisplayChoices(currentNode.choices.ToArray(), OnChoiceSelected);
            }
            else
            {
                dialogueUI.HideChoices();
                dialogueUI.ShowNextButton();
            }
        }
        else
        {
            Debug.LogWarning("Invalid current node index!");
        }
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        if (dialogueData == null || dialogueData.conversationNodes.Count == 0)
        {
            Debug.LogError("No dialogue data assigned or no conversation nodes!");
            return;
        }

        if (currentNodeIndex >= 0 && currentNodeIndex < dialogueData.conversationNodes.Count)
        {
            ConversationNode currentNode = dialogueData.conversationNodes[currentNodeIndex];
            if (choiceIndex >= 0 && choiceIndex < currentNode.nextNodeIndices.Count)
            {
                int nextNodeIndex = currentNode.nextNodeIndices[choiceIndex];
                currentNodeIndex = nextNodeIndex;
                DisplayCurrentNode();
            }
            else
            {
                Debug.LogWarning("Invalid choice index!");
            }
        }
        else
        {
            Debug.LogWarning("Invalid current node index!");
        }
    }

    private void OnNextClicked()
    {
        if (dialogueData == null || dialogueData.conversationNodes.Count == 0)
        {
            Debug.LogError("No dialogue data assigned or no conversation nodes!");
            return;
        }

        if (currentNodeIndex >= 0 && currentNodeIndex < dialogueData.conversationNodes.Count)
        {
            ConversationNode currentNode = dialogueData.conversationNodes[currentNodeIndex];
            if (currentNode.nextNodeIndices.Count > 0)
            {
                int nextNodeIndex = currentNode.nextNodeIndices[0];
                currentNodeIndex = nextNodeIndex;
                DisplayCurrentNode();
            }
            else
            {
                dialogueUI.Hide();
                // Handle conversation end or any other logic
            }
        }
        else
        {
            Debug.LogWarning("Invalid current node index!");
        }
    }
}
