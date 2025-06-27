

using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;
using System.Threading;
using Unity.Android.Gradle;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.UI;
using TMPro;



public class Push : Agent
{
    public GameObject ground;
    public GameObject area;

    /// The area bounds are used to spawn the elements
    [HideInInspector]
    public Bounds areaBounds;

    PushBlockSettings m_PushBlockSettings;

    public GameObject goal;
    public GameObject goalBlue;

    /// number of groceries is range from 1 to 4
    [SerializeField]
    [Range(1, 4)]
    public int NrBoxesByDestination = 4;

    public GameObject block;
    public GameObject block1;
    public GameObject block2;
    public GameObject block3;

    public GameObject blockBlue;
    public GameObject blockBlue1;
    public GameObject blockBlue2;
    public GameObject blockBlue3;


    List<GameObject> blocks = new List<GameObject>();

    int countDone = 0;

    /// Detects when the block touches the goal.
    [HideInInspector]
    public SimpleDetectGoal goalDetect;
    public bool useVectorObs;

    Rigidbody m_BlockRb;  //cached on initialization
    Rigidbody m_AgentRb;  //cached on initialization
    Material m_GroundMaterial; //cached on Awake()

    /// We will be changing the ground material based on success/failure
    Renderer m_GroundRenderer;

    EnvironmentParameters m_ResetParams;

    public TextMeshProUGUI countText;

    protected override void Awake()
    {
        base.Awake();
        m_PushBlockSettings = FindObjectOfType<PushBlockSettings>();


        // Initialize the blocks list with the block prefabs
        // depending on the number of groceries
        if (NrBoxesByDestination == 4)
        {
            blocks.Add(block);
            blocks.Add(block1);
            blocks.Add(block2);
            blocks.Add(block3);

            blocks.Add(blockBlue);
            blocks.Add(blockBlue1);
            blocks.Add(blockBlue2);
            blocks.Add(blockBlue3);
        }

        else if (NrBoxesByDestination == 3)
        {
            blocks.Add(block);
            blocks.Add(block1);
            blocks.Add(block2);

            blocks.Add(blockBlue);
            blocks.Add(blockBlue1);
            blocks.Add(blockBlue2);
        }

        else if (NrBoxesByDestination == 2)
        {
            blocks.Add(block);
            blocks.Add(block1);

            blocks.Add(blockBlue);
            blocks.Add(blockBlue1);
        }

        else if (NrBoxesByDestination == 1)
        {
            blocks.Add(block);

            blocks.Add(blockBlue);
        }

        // // orinigal working code
        // blocks.Add(block);
        // blocks.Add(block1);
        // blocks.Add(block2);
        // blocks.Add(block3);

        // blocks.Add(blockBlue);
        // blocks.Add(blockBlue1);
        // blocks.Add(blockBlue2);
        // blocks.Add(blockBlue3);

        // Destroy excess blocks not in the list
        List<GameObject> allPotentialBlocks = new List<GameObject>
        {
            block, block1, block2, block3,
            blockBlue, blockBlue1, blockBlue2, blockBlue3
        };

        foreach (GameObject potentialBlock in allPotentialBlocks)
        {
            if (potentialBlock != null && !blocks.Contains(potentialBlock))
            {
                Destroy(potentialBlock);
            }
        }
    }

    public override void Initialize()
    {
        countDone = 0;
        UpdateCountText();

        foreach (var bl in blocks)
        {
            goalDetect = bl.GetComponent<SimpleDetectGoal>();
            goalDetect.agent = this;

            // Cache the block rigidbody
            m_BlockRb = bl.GetComponent<Rigidbody>();
        }

        // Cache the agent rigidbody
        m_AgentRb = GetComponent<Rigidbody>();

        // Get the ground's bounds
        areaBounds = ground.GetComponent<Collider>().bounds;

        // Get the ground renderer so we can change the material when a goal is scored
        m_GroundRenderer = ground.GetComponent<Renderer>();

        // Starting material
        m_GroundMaterial = m_GroundRenderer.material;
        m_ResetParams = Academy.Instance.EnvironmentParameters;
        SetResetParameters();
    }

    /// Use the ground's bounds to pick a random spawn position.
    public Vector3 GetRandomSpawnPos()
    {
        var foundNewSpawnLocation = false;
        var randomSpawnPos = Vector3.zero;
        while (foundNewSpawnLocation == false)
        {
            var randomPosX = Random.Range(-areaBounds.extents.x * m_PushBlockSettings.spawnAreaMarginMultiplier,
                areaBounds.extents.x * m_PushBlockSettings.spawnAreaMarginMultiplier);

            var randomPosZ = Random.Range(-areaBounds.extents.z * m_PushBlockSettings.spawnAreaMarginMultiplier,
                areaBounds.extents.z * m_PushBlockSettings.spawnAreaMarginMultiplier);

            randomSpawnPos = ground.transform.position + new Vector3(randomPosX, 1f, randomPosZ);

            if (Physics.CheckBox(randomSpawnPos, new Vector3(2.5f, 0.01f, 2.5f)) == false)
            {
                foundNewSpawnLocation = true;
            }
        }
        return randomSpawnPos;
    }


    public void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = "" + countDone;
        }
    }

    /// Called when the agent moves the block into the goal.
    public void ScoredAGoal()
    {
        countDone = countDone + 1;

        UpdateCountText();


        if (countDone == 1) // +5.
        {
            AddReward(5f);
        }
        else if (countDone == 2) // +7
        {
            AddReward(7f);

            if (NrBoxesByDestination == 1)
            {
                EndEpisode();
            }
        }
        else if (countDone == 3) // +10
        {
            AddReward(10f);
        }

        else if (countDone == 4) // +12
        {
            AddReward(12f);

            if (NrBoxesByDestination == 2)
            {
                EndEpisode();
            }
        }

        else if (countDone == 5) // +14
        {
            AddReward(14f);
        }

        else if (countDone == 6) // +16
        {
            AddReward(16f);

            if (NrBoxesByDestination == 3)
            {
                EndEpisode();
            }
        }

        else if (countDone == 7) // +18
        {
            AddReward(18f);
        }


        else if (countDone == 8) //+50
        {
            AddReward(50f);

            if (NrBoxesByDestination == 4)
            {
                EndEpisode();
            }
        }
    }


    public void LostAGoal()
    {
        AddReward(-15f);
        //StartCoroutine(GoalScoredSwapGroundMaterial(m_PushBlockSettings.failMaterial, 0.5f));

        countDone = countDone - 1;

        if (countDone < 0)
        {
            countDone = 0;
        }
        ;

        UpdateCountText();
    }

    /// Swap ground material, wait time seconds, then swap back to the regular material.
    IEnumerator GoalScoredSwapGroundMaterial(Material mat, float time)
    {
        m_GroundRenderer.material = mat;
        yield return new WaitForSeconds(time); // Wait for 2 sec
        m_GroundRenderer.material = m_GroundMaterial;
    }

    /// Moves the agent according to the selected action.
    public void MoveAgent(ActionSegment<int> act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = act[0];

        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
            case 5:
                dirToGo = transform.right * -0.75f;
                break;
            case 6:
                dirToGo = transform.right * 0.75f;
                break;
        }
        transform.Rotate(rotateDir, Time.fixedDeltaTime * 200f);
        m_AgentRb.AddForce(dirToGo * m_PushBlockSettings.agentRunSpeed,
            ForceMode.VelocityChange);

    }

    /// Called every step of the engine. Here the agent takes an action.
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Move the agent using the action.
        MoveAgent(actionBuffers.DiscreteActions);

        // Penalty given each step to encourage agent to finish task quickly.
        AddReward(-1f / MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 3;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 4;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }
    }

    void ResetBlock()
    {
        foreach (var bl in blocks)
        {
            goalDetect = bl.GetComponent<SimpleDetectGoal>();
            goalDetect.agent = this;

            bl.transform.position = GetRandomSpawnPos();

            // Cache the block rigidbody
            m_BlockRb = bl.GetComponent<Rigidbody>();
            // // Reset block velocity back to zero.
            m_BlockRb.velocity = Vector3.zero;
            // Reset block angularVelocity back to zero.
            m_BlockRb.angularVelocity = Vector3.zero;

            float xValue = Random.Range(1.5f, 3.0f);
            float yValue = Random.Range(0.5f, 1.2f);
            float zValue = Random.Range(1.5f, 3.0f);

            m_BlockRb.transform.localScale = new Vector3(xValue, yValue, zValue);

            countDone = 0;
        }
    }


    /// In the editor, if "Reset On Done" is checked then AgentReset() will be
    /// called automatically anytime we mark done = true in an agent script.
    public override void OnEpisodeBegin()
    {
        countDone = 0;

        var rotation = Random.Range(0, 4);
        var rotationAngle = rotation * 90f;

        area.transform.Rotate(new Vector3(0f, 0f, 0f));

        foreach (var bl in blocks)
        {
            ResetBlock();
        }

        m_AgentRb.velocity = Vector3.zero;
        m_AgentRb.angularVelocity = Vector3.zero;

        SetResetParameters();
    }

    public void SetGroundMaterialFriction()
    {
        var groundCollider = ground.GetComponent<Collider>();
        groundCollider.material.dynamicFriction = m_ResetParams.GetWithDefault("dynamic_friction", 0);
        groundCollider.material.staticFriction = m_ResetParams.GetWithDefault("static_friction", 0);
    }

    public void SetBlockProperties()
    {
        var scale = m_ResetParams.GetWithDefault("block_scale", 2);

        //Set the scale of the block
        m_BlockRb.transform.localScale = new Vector3(scale, 0.75f, scale);

        // Set the drag of the block
        m_BlockRb.drag = m_ResetParams.GetWithDefault("block_drag", 0.5f);
    }

    void SetResetParameters()
    {
        SetGroundMaterialFriction();
        SetBlockProperties();
    }
}
