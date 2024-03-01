using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoInvisible : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10))
            {

                IInteractuable interactuable = hit.collider.gameObject.GetComponent<IInteractuable>();

                if (interactuable != null)
                {
                    interactuable.Interact();
                }

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
               
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.white);
                Debug.Log("Did not Hit");
            }
        }
        
       
    }
}
