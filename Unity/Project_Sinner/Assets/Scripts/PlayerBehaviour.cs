using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public Camera fpsCam;
    public float forwardWalkSpeed;
    public float sideWalkSpeed;
    public float backWalkSpeed;
    public float runSpeed;
    public bool isRunning;
    public bool isCrouching;
    public float jumpForce;
    public bool isGrounded;
    public float crouchSpeed;
    public float crouchHeight;
    private PlayerMouseLook m_MouseLook;
    private CapsuleCollider CapsuleCollider;
    private float startPlayerHeight;
    void Start () {
        m_MouseLook = GetComponent<PlayerMouseLook>();
        m_MouseLook.Init(transform, fpsCam.transform);
        CapsuleCollider = GetComponent<CapsuleCollider>();
        fpsCam = GetComponentInChildren<Camera>();
        startPlayerHeight = CapsuleCollider.height;
    }
	
	void Update () {
        RotateView();

        //Movement \/
        if (Input.GetKey(KeyCode.W))
        {
            isRunning = false;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!isCrouching)
                {
                    transform.Translate(Vector3.forward * runSpeed / 100);
                    isRunning = true;
                }
            }
            else
            {
                transform.Translate(Vector3.forward * forwardWalkSpeed / 100);
            }
        }
        else
        {
            isRunning = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * backWalkSpeed / 100);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * sideWalkSpeed / 100);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * sideWalkSpeed / 100);
        }
        //

        //Crouch \/
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            if (CapsuleCollider.height > crouchHeight)
            {
                CapsuleCollider.height = CapsuleCollider.height - Time.deltaTime * crouchSpeed;
                if (CapsuleCollider.height < crouchHeight)
                {
                    CapsuleCollider.height = crouchHeight;
                }
            }
        }
        else
        {
            isCrouching = false;
            if (CapsuleCollider.height < startPlayerHeight)
            {
                CapsuleCollider.height = CapsuleCollider.height + Time.deltaTime * crouchSpeed;
                if (CapsuleCollider.height > startPlayerHeight)
                {
                    CapsuleCollider.height = startPlayerHeight;
                }
            }
        }
        //

        //Jump \/
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);
            }
        }
        //
    }
    private void RotateView()
    {
        m_MouseLook.LookRotation(transform, fpsCam.transform);
    }
}
