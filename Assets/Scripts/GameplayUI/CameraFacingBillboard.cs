using UnityEngine;


public class CameraFacingBillboard : Elemental
{
    public Camera m_Camera;
    public bool amActive = false;

    public override void Init()
    {
        m_Camera = FindObjectOfType<GolfCameraController>().GetComponent<Camera>();
        amActive = true;

    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        if (amActive)
        {
            var position = m_Camera.transform.position;
            position.y = transform.position.y;
            transform.LookAt(position);
        }
    }
}