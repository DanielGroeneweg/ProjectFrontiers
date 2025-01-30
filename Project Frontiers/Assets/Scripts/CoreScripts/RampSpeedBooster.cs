using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampSpeedBooster : MonoBehaviour
{
    [SerializeField] private float speedBoostAmount;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Rigidbody rb = collision.transform.root.gameObject.GetComponent<Rigidbody>();
            CarControl carControl = collision.transform.root.gameObject.GetComponent<CarControl>();

            rb.AddForce(collision.transform.forward * speedBoostAmount);
        }
    }
}