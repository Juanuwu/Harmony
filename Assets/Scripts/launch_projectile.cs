using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launch_projectile : MonoBehaviour
{
   public GameObject projectile;
   public float launchVelocity = 700f;
 
   void Update()
   { 
   }

    public void shoot()
    {
        GameObject ball = Instantiate(projectile, transform.position,  
                                                     transform.rotation);
           BoxCollider gameObjectsBoxCollider= ball.AddComponent<BoxCollider>();
           gameObjectsBoxCollider.size = new Vector3(1f, 1.311829f, 1.208954f);
           gameObjectsBoxCollider.center = new Vector3(0f, 0.6176605f, 0.004940033f);
           ball.GetComponent<launch_projectile>().enabled = false;
           ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 
                                                (1000, launchVelocity,0));
    }


}
