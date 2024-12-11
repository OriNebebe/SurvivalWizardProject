using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] CinemachineBlenderSettings interpolatedTransmission;
    [SerializeField] CinemachineBlenderSettings instantTransmission;
    [SerializeField] CinemachineBrain camBrain;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] CinemachineVirtualCamera activeCam;
    public bool SwitchCamera(CinemachineVirtualCamera selectedPlayer)
    {
        if (selectedPlayer == null)
        {
            //Debug.Log("wtf");
            return false;
        }
        if (activeCam == selectedPlayer)
        {

            //Debug.Log("dont need to switch");
        }
        else
        {
            
            //Debug.Log("shot ray: "+ CheckForObstackle(activeCam.transform.position,selectedPlayer.transform.position));
           // Debug.Log("switch");
           CheckForObstackle(activeCam.transform.position,selectedPlayer.transform.position);
            activeCam.Priority = 5;
            activeCam = selectedPlayer;
            activeCam.Priority = 10;
        }
        return true;
    }

    bool CheckForObstackle(Vector3 a,Vector3 b)
    {
        Debug.DrawRay(a,b-a,Color.yellow,1f);
        float rayLenght = Vector3.Distance(a,b);
        RaycastHit hit;
        if(Physics.SphereCast(a , 0.5f, b-a, out hit, rayLenght, groundLayer))
        {
            camBrain.m_CustomBlends = instantTransmission;
            return true;
        }
        camBrain.m_CustomBlends = interpolatedTransmission;
        return false;
    }
}
