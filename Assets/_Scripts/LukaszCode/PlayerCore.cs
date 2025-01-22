using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.Serializable]
public class PlayerCore
{
    [SerializeField]CinemachineVirtualCamera connectedCamera;
    [SerializeField] GameObject playerBody;
    [SerializeField] MovementBrain movementCore;
    //nah, dont need constructor
    PlayerCore()
    {
        //skibidi
    }

    public CinemachineVirtualCamera GetCamera()
    {
        return connectedCamera;
    }

    public MovementBrain GetMovementC()
    {
        return movementCore;
    }

}
