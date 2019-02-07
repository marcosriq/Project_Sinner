using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour {

    // Use this for initialization
    public float torque;
    public Rigidbody rb;
    public Transform player;
    public float playerDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        player = obj.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        Vector3 playerRelativePos = transform.InverseTransformPoint(player.position);
        playerDistance = playerRelativePos.y;
        if(playerDistance >= -0.005 || playerDistance <= 0.005)
        {
            float turn = Input.GetAxis("Vertical");
            rb.AddForce(transform.up * torque * turn);
        }
    }
    public void Open()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
