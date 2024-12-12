using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] CinemachineBlenderSettings interpolatedTransmission;
    [SerializeField] CinemachineBlenderSettings instantTransmission;
    [SerializeField] CinemachineBrain camBrain;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] CinemachineVirtualCamera activeCam;

    [SerializeField] public float SwitchTime;
    public Image m;
    [SerializeField] public AnimationCurve fadeCurve;
    bool isFadin = false;

    public bool SwitchCamera(CinemachineVirtualCamera selectedPlayer)
    {
        if (selectedPlayer == null || isFadin)
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
            if(CheckForObstackle(activeCam.transform.position,selectedPlayer.transform.position))
            {
                StartCoroutine(CamFade(SwitchTime,selectedPlayer));
            }
            else
            {
                camBrain.m_CustomBlends = interpolatedTransmission;
                activeCam.Priority = 5;
                activeCam = selectedPlayer;
                activeCam.Priority = 10;
                
            }
            
                
            
            
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
            //Debug.Log(hit);
            camBrain.m_CustomBlends = instantTransmission;
            
            //StartCoroutine(CamFade(SwitchTime));
            
            return true;
        }
        //camBrain.m_CustomBlends = interpolatedTransmission;
        return false;
    }

    public IEnumerator CamFade(float  _fadetime,CinemachineVirtualCamera _selectedPlayer)
    {
        isFadin = true;
        float fadeTimer = 0;
        bool mid = true;
        Color newC = m.color;
        while(fadeTimer<= SwitchTime)
        {
            //Debug.Log("Skibidiii");
            //Color fadeC = fadeMat;
            newC = m.color;


            newC.a = Mathf.Clamp(fadeCurve.Evaluate(fadeTimer/SwitchTime),0,1);

            m.color = newC;
            fadeTimer+=Time.deltaTime;

            if(mid && fadeTimer >= SwitchTime/3)
            {
                Debug.Log("MidPoint");
                activeCam.Priority = 5;
                activeCam = _selectedPlayer;
                activeCam.Priority = 10;
            }

            yield return null;
        }
        newC.a = 0;
        m.color = newC;
        isFadin = false;
        //yield return null;
    }

    

}
