using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Basa
{
    [RequireComponent(typeof(RayCastScript))] //requerimento obrigatorio de outro script para funcionar
    public class AcoesRaycast : MonoBehaviour
    {

        /// ///////////////////acesso a outros scrits/////////////////////////////////////////

        RayCastScript rayCastScript; //armazenando o script dentro de uma variavel
        public GameControllerScript gameControllerScript;
        public GerenciadorPortasChaves gerenciadorPortaChavesScript;
        Chave chaveScript;
        //--------------------------------------------------------------------------------------------   

        public bool pegou;
        float distancia;
        public GameObject salvaObjeto;

        /// ////////////////////////////////////////
        
        public GameObject botaoSlot1;
       

        

        



        void Start()
        {
            
            
            rayCastScript = GetComponent<RayCastScript>();
            

            pegou = false;
            
            
            

        }


        void Update()
        {
            distancia = rayCastScript.distanciaAlvo;
            if (distancia <= 3)
            {
                if (Input.GetKeyDown(KeyCode.E) && rayCastScript.objPega != null)
                {
                    Pegar();

                }
                if (Input.GetKeyDown(KeyCode.E) && rayCastScript.objArrasta != null)
                {
                    if (!pegou)
                    {
                        pegou = true;
                        Arrastar();

                    }
                    else
                    {
                        pegou = false;
                        Soltar();

                    }



                }

            }
        }
           
            void Arrastar()
            {
               rayCastScript.objArrasta.GetComponent<Rigidbody>().isKinematic = true;
               rayCastScript.objArrasta.GetComponent<Rigidbody>().useGravity = false;
               rayCastScript.objArrasta.transform.SetParent(transform);
               rayCastScript.objArrasta.transform.localPosition = new Vector3(0, 0, 3);
               rayCastScript.objArrasta.transform.localRotation = Quaternion.Euler(0, 0, 0);

            }
            void Soltar()
            {
            rayCastScript.objArrasta.GetComponent<Rigidbody>().isKinematic = false;
            rayCastScript.objArrasta.GetComponent<Rigidbody>().useGravity = true;
            rayCastScript.objArrasta.transform.localPosition = new Vector3(0, 0, 3);
            rayCastScript.objArrasta.transform.SetParent(null);
            


            }
            void Pegar()
            {

             
             gerenciadorPortaChavesScript.ContagemDeChave();








            Destroy(rayCastScript.objPega);

            }
    }
}

