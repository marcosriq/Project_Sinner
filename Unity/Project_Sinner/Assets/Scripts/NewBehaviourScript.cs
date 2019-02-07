using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    public float torque;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float turn = Input.GetAxis("Horizontal");
        rb.AddForce(transform.up * torque * turn);
    }
}
