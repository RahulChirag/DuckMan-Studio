using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dialogue/Node")]
public class DialogueData : ScriptableObject
{
    public List<ConversationNode> conversationNodes = new List<ConversationNode>();
}

[System.Serializable]
public class ConversationNode
{
    public string dialogueText;
    public string speaker;
    public List<string> choices;
    public List<int> nextNodeIndices;
}