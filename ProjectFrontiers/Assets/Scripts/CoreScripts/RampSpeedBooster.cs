using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampSpeedBooster : MonoBehaviour
{
    [SerializeField] private float speedBoostAmount;
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision!");
        if (collision.tag == "Player")
        {
            Rigidbody rb = collision.transform.root.gameObject.GetComponent<Rigidbody>();
            CarControl carControl = collision.transform.root.gameObject.GetComponent<CarControl>();

            //rb.AddForce(collision.transform.forward * speedBoostAmount);

            foreach(var wheel in carControl.wheels) if (wheel.motorized) wheel.WheelCollider.motorTorque = speedBoostAmount;

            Debug.Log("Done!");
        }
    }
}
