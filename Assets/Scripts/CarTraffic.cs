//Put this script on cars

using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;
using System.Threading;
using System.Numerics;

public class CarTraffic : MonoBehaviour
{

    void Update()
    {



        float Speed = 70f;
        // move the car forward
        transform.position += transform.forward * Time.deltaTime * Speed;

        // when the car goes out of bounds, reset
        if (transform.position.x < -400)

        {
            // reset the car to the starting position
            transform.position = new UnityEngine.Vector3(370, 0.5f, 120);
        }



    }
}