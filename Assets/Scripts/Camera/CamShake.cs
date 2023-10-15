using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CamShake : MonoBehaviour
{
    public CinemachineVirtualCamera cinamachineVirtualCam;
    private float shakeIntensity = 1f;
    private float shakeTime = 0.2f;
    private float timer;

    public CinemachineBasicMultiChannelPerlin m_MultiChannelPerlin;
    private void Start()
    {
        m_MultiChannelPerlin = cinamachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        StopShakeCam();
    }
    public void ShakeCam()
    {
        m_MultiChannelPerlin.m_AmplitudeGain = shakeIntensity;
        timer = shakeTime;
    }

    public void StopShakeCam()
    {
        m_MultiChannelPerlin.m_AmplitudeGain = 0;
        timer = 0;  
    }
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                StopShakeCam();
            }
        }
    }
}
