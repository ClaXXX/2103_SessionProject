using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Uncap the roll speed of our ball
 */
public class UncapPhysicSpeed : MonoBehaviour
{
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on " + gameObject.name);
            return;
        }

        rb.maxAngularVelocity = Mathf.Infinity;
        
        Destroy(this); // the script will remove itself after having change the rolling speed
    }
}
