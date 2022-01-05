using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Basa 
{ 
public class pressioneE : MonoBehaviour
{
    ////////Outros Scripts/////////////
    public portaMadeira portaMadeiraScript;
   ///////////////////////////////////

    void OnTriggerEnter(Collider col)//verificando se o player esta dentro do collisor para abrir porta
    {
        if (col.gameObject.CompareTag("Player"))
        {


            GetComponent<MeshRenderer>().enabled = true;


        }
           


    }

    void OnTriggerExit(Collider col)//fechando a porta
    {
        portaMadeiraScript.animator.SetTrigger("Fechar");
        GetComponent<MeshRenderer>().enabled = false;
        
    }
}
}