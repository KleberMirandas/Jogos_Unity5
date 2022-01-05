using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Basa
{ 
public class RayCastScript : MonoBehaviour
{
        public float distanciaAlvo;
        public GameObject objArrasta, objPega;
        RaycastHit hit;

    
    void Update()
    {
            if (Time.frameCount % 5 == 0)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 5, Color.red);

                if (Physics.SphereCast(transform.position,0.1f,transform.TransformDirection(Vector3.forward), out hit,5))
                {

                    distanciaAlvo = hit.distance;
                    if (hit.transform.gameObject.tag == "objArrasta")
                    {
                        objArrasta = hit.transform.gameObject;


                    }
                    if (hit.transform.gameObject.tag == "objPega")
                    {
                        objPega = hit.transform.gameObject;


                    }

                }
                else
                {
                    objPega = null;
                    objArrasta = null;

                }
            }

        
    }
}
}