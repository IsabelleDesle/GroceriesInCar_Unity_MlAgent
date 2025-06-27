//Put this script on cars

using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;
using System.Threading;
using System.Numerics;

public class CarTraffic3 : MonoBehaviour
{

    void Update()
    {
        UnityEngine.Vector3 StartPosition = new UnityEngine.Vector3(169, 0, -800);
        UnityEngine.Vector3 RotatePosition = new UnityEngine.Vector3(0, 0, 0);

        float Speed = 70f;
        // move the car forward
        transform.position += transform.forward * Time.deltaTime * Speed;

        //when car arrives at corner of street, turn left
        if (transform.position.z > 800)
        {
            transform.rotation = UnityEngine.Quaternion.Euler(RotatePosition);
            transform.position = StartPosition;
        }
    }
}