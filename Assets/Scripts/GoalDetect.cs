//Detect when the orange block has touched the goal.
//Detect when the orange block has touched an obstacle.
//Put this script onto the orange block. There's nothing you need to set in the editor.
//Make sure the goal is tagged with "goal" in the editor.

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GoalDetect : MonoBehaviour
{
    /// <summary>
    /// The associated agent.
    /// This will be set by the agent script on Initialization.
    /// Don't need to manually set.
    /// </summary>
    [HideInInspector]
    public PushAgentBasic agent;  //
    //public Push agent;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("block"))
        {
            //other.gameObject.SetActive(false);
            agent.ScoredAGoal();
        }

        // else if (other.gameObject.CompareTag("orangeBlock"))
        // {
        //     other.gameObject.SetActive(false);
        //     agent.ScoredAGoal();
        // }


        // else if (other.gameObject.CompareTag("block"))
        // {
        //     other.gameObject.SetActive(false);
        //     agent.ScoredAGoal();
        // }


    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("block"))
        {
            agent.LostAGoal();
            //other.gameObject.SetActive(true); // this desactivates the goal
        }
        // else if (other.gameObject.CompareTag("newGoal2"))
        // {
        //     agent.LostAGoal();
        //     //other.gameObject.SetActive(true); // this desactivates the goal
        // }
    }

}
