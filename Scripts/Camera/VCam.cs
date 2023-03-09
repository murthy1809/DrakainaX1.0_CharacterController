using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Diagnostics;

public class VCam : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook vCam;
    public VCameraObject vCO,vZO;
    [SerializeField] Transform LookAt,Follow,ZoomLook;


    void Start()
    {
        
    }

    void Update()
    {
        if(ZoomLook == null || vZO == null)
        {
            return;
        }

        if (Input.GetButton("SecondaryAttack"))
       // if (Input.GetButtonDown("SecondaryAttack"))
        {

            vCam.LookAt = ZoomLook;
            vCam.Follow = ZoomLook;
            vCam.m_Orbits[0].m_Height = vZO.TopRigHeight;
            vCam.m_Orbits[0].m_Radius = vZO.TopRigRadius;
            vCam.m_Orbits[1].m_Height = vZO.MiddleRigHeight;
            vCam.m_Orbits[1].m_Radius = vZO.MiddleRigRadius;
            vCam.m_Orbits[2].m_Height = vZO.BottomRigHeight;
            vCam.m_Orbits[2].m_Radius = vZO.BotttomRigRadius;
            vCam.m_Lens.FieldOfView = vZO.VerticalFOV;
            vCam.m_Heading.m_Bias = 0;
            //vCam.m_RecenterToTargetHeading.m_enabled = true;
            UnityEngine.Debug.Log("zoomed");
        }
        else /*if (Input.GetKeyDown(KeyCode.LeftAlt))*/
        {
            vCam.m_Orbits[0].m_Height = vCO.TopRigHeight;
            vCam.m_Orbits[0].m_Radius = vCO.TopRigRadius;
            vCam.m_Orbits[1].m_Height = vCO.MiddleRigHeight;
            vCam.m_Orbits[1].m_Radius = vCO.MiddleRigRadius;
            vCam.m_Orbits[2].m_Height = vCO.BottomRigHeight;
            vCam.m_Orbits[2].m_Radius = vCO.BotttomRigRadius;

            vCam.LookAt = LookAt;
            vCam.Follow = Follow;

            vCam.m_Lens.FieldOfView = vCO.VerticalFOV;
            vCam.m_Heading.m_Bias = 0;
            UnityEngine.Debug.Log("nozoomed");
            //vCam.m_RecenterToTargetHeading.m_enabled = false;
        }


    }


}
