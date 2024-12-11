using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float[] position;

    public PlayerData (Vector3 playerOne)
    {
        position = new float[3];
        position[0] = playerOne.x;
        position[1] = playerOne.y;
        position[2] = playerOne.z;
    }
}
