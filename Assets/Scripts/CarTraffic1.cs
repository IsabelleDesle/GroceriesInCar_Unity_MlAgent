//Put this script on cars

using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;
using System.Threading;
using System.Numerics;

public class CarTraffic1 : MonoBehaviour
{

    void Update()
    {
        UnityEngine.Vector3 StartPosition = new UnityEngine.Vector3(-500, 0, 85);
        UnityEngine.Vector3 RotatePosition = new UnityEngine.Vector3(0, 90, 0);

        float Speed = 70f;
        // move the car forward
        transform.position += transform.forward * Time.deltaTime * Speed;

        // when the car goes out of bounds, reset
        if (
         transform.position.x > 400)

        {
            transform.rotation = UnityEngine.Quaternion.Euler(RotatePosition);
            transform.position = StartPosition;
        }


    }
}