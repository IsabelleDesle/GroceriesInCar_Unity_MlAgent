//Put this script on cars

using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;
using System.Threading;
using System.Numerics;

public class CarTraffic4 : MonoBehaviour
{

    void Update()
    {
        UnityEngine.Vector3 StartPosition = new UnityEngine.Vector3(130, 0, 1000);
        UnityEngine.Vector3 RotatePosition = new UnityEngine.Vector3(0, 180, 0);

        float Speed = 70f;
        // move the car forward
        transform.position += transform.forward * Time.deltaTime * Speed;


        if (transform.position.z < -200)
        {
            transform.rotation = UnityEngine.Quaternion.Euler(RotatePosition);
            transform.position = StartPosition;
        }
    }
}