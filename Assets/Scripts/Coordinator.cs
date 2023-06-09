using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Coordinator : MonoBehaviour
{
    public Button ArcherAndWorriorAttack;
    public Button GroupAttack;

    public List<WarriorAI> warriors;
    public List<ArcherAI> archers;

    private void Start()
    {
        // Find and store references to all AI characters in the scene
        warriors = new List<WarriorAI>(FindObjectsOfType<WarriorAI>());
        archers = new List<ArcherAI>(FindObjectsOfType<ArcherAI>());

        //Assign goals to the AI characters
        ArcherAndWorriorAttack.onClick.AddListener(delegate { AssignGoals(); });
        GroupAttack.onClick.AddListener(delegate { TriggerGroupAttack(); });        
    }

    private void AssignGoals()
    {
        //Assign a target for warriors
        GameObject warriorTarget = GameObject.Find("WarriorTarget");
        foreach (WarriorAI warrior in warriors)
        {
            warrior.SetTarget(warriorTarget.transform);
        }

        //Assign a target for archers
        GameObject archerTarget = GameObject.Find("ArcherTarget");
        foreach (ArcherAI archer in archers)
        {
            archer.SetTarget(archerTarget.transform);
        }
    }

    public void TriggerGroupAttack()
    {
        //Trigger a group attack where warriors rush toward a target while archers provide ranged support
        GameObject groupAttackTarget = GameObject.Find("GroupAttackTarget");
        foreach (WarriorAI warrior in warriors)
        {
            warrior.SetTarget(groupAttackTarget.transform);
        }

        Vector3 archerDirection = groupAttackTarget.transform.position - archers[0].transform.position;
        archerDirection.y = 0f;

        foreach (ArcherAI archer in archers)
        {
            archer.SetTarget(archer.transform); // Archers stay in place to provide support

            if (archerDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(archerDirection);
                archer.transform.rotation = targetRotation;
            }
        }
    }
}

