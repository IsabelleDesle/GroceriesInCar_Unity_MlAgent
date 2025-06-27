//Detect when the orange block has touched the goal.
//Detect when the orange block has touched an obstacle.
//Put this script onto the orange block. There's nothing you need to set in the editor.
//Make sure the goal is tagged with "goal" in the editor.

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleDetectGoal : MonoBehaviour
{

    [HideInInspector]
    //public PushAgentBasic agent;  //
    public Push agent;




    /// <summary>
    //changed to collsion again, back to basic 11/04 20:23
    /// </summary>
    /// 
    // void OnCollisionEnter(Collision col)
    // {
    //     // Touched goal.
    //     if (col.gameObject.CompareTag("goal"))
    //     {
    //         agent.ScoredAGoal();

    //     }
    // }


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("goal"))
        {
            //other.gameObject.SetActive(false); // this desactivates the goal
            agent.ScoredAGoal();
        }


        if (other.gameObject.CompareTag("goalBlue"))
        {
            //other.gameObject.SetActive(false); // this desactivates the goal
            agent.ScoredAGoal();
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("goal"))
        {
            agent.LostAGoal();
            //other.gameObject.SetActive(true); // this desactivates the goal
        }

        if (other.gameObject.CompareTag("goalBlue"))
        {
            agent.LostAGoal();
            //other.gameObject.SetActive(true); // this desactivates the goal
        }

    }

}

