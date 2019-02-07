using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerMechanics : MonoBehaviour {

    private Camera camera;
    private FirstPersonController fpsController;
    float default_WalkSpeed;
    float default_RunSpeed;
	// Use this for initialization
	void Start () {
        fpsController = GetComponent<FirstPersonController>();
        camera = GetComponentInChildren<Camera>();
        default_WalkSpeed = fpsController.m_WalkSpeed;
        default_RunSpeed = fpsController.m_RunSpeed;
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            if (hit.transform.tag == "Door")
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    hit.transform.GetComponent<DoorBehaviour>().Open();
                    print("Door Open!");
                }
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Door")
        {
            fpsController.m_WalkSpeed /= 2;
            fpsController.m_RunSpeed /= 2;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
        {
            fpsController.m_WalkSpeed = default_WalkSpeed;
            fpsController.m_RunSpeed = default_RunSpeed;
        }
    }
}
