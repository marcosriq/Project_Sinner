using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMouseLook : MonoBehaviour{
	public Camera targetCamera;
	public float XSensitivity = 2f;
    public float YSensitivity = 2f;
	public float aimSensibility;
	public bool isAiming;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;
	public float verticalRecoil;
	public float horizontalRecoil;
	public bool applyVerticalRecoil;
	public bool applyHorizontalRecoil;



    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;


    public void Init(Transform character, Transform camera){
        m_CharacterTargetRot = character.localRotation;
		m_CameraTargetRot = targetCamera.transform.localRotation;
    }


    public void LookRotation(Transform character, Transform camera){
		float yRot;
		float xRot;
		if (isAiming) {
			yRot = CrossPlatformInputManager.GetAxis ("Mouse X") * (XSensitivity + aimSensibility);
			xRot = CrossPlatformInputManager.GetAxis ("Mouse Y") * (YSensitivity + aimSensibility);
		} else {
			yRot = CrossPlatformInputManager.GetAxis ("Mouse X") * XSensitivity;
			xRot = CrossPlatformInputManager.GetAxis ("Mouse Y") * YSensitivity;
		}

        
		if (applyVerticalRecoil) {
            Quaternion lateCamRot = m_CameraTargetRot;
			m_CameraTargetRot *= Quaternion.Euler (-xRot - verticalRecoil, 0f, 0f);
        } else {
			m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);
		}
		if (applyHorizontalRecoil) {
			m_CharacterTargetRot *= Quaternion.Euler (0f, yRot + horizontalRecoil, 0f);
		} else {
			m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
		}

        if(clampVerticalRotation)
            m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

        if(smooth){
            character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,smoothTime * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,smoothTime * Time.deltaTime);
        }
        else
        {
            character.localRotation = m_CharacterTargetRot;
            camera.localRotation = m_CameraTargetRot;
        }
    }


    Quaternion ClampRotationAroundXAxis(Quaternion q){
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

        angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

}
