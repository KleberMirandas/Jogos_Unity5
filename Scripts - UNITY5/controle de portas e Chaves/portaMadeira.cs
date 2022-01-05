using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Basa 
{ 
public class portaMadeira : MonoBehaviour
{

/// ///////////////////acesso a outros scrits/////////////////////////////////////////
    public GameControllerScript gameController;//acesso ao Script  GameControllerScript
    public AcoesRaycast acoesRaycast;
    public GerenciadorPortasChaves gerenciadorPortaChavesScript;
    public Chave chaveScript;
//-------------------------------------------------------------------------------------

 ///////////////////////Variaveis porta um/////////////////////////////////////////////        
    public Animator animator;
    public bool trancada;
    public bool idPortaUm;
    private bool colidindo;
    private bool portaAberta = false;
 //--------------------------------------------------------------------------------------
   
        

 
    void Start()
    {
            //gerenciadorPortaChavesScript.ContagemDeChave();

    }

    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && trancada == true && colidindo)//verifica se aporta esta trancada
        {

                if (gerenciadorPortaChavesScript.contaChaveUm == 0)
                {        
                          ///////////////////////////////////////////
                         //COLOCAR SOM PRECISO ENCONTRAR AS CHAVE//
                         //////////////////////////////////////////
                         animator.SetTrigger("Trancado");

                }
                else
                {

                    if (gerenciadorPortaChavesScript.idChaveUm == true && idPortaUm == true)
                    {
                        //////////////////////////////
                        ///Colocar um som de destrancar a porta
                        ///////////////////////////
                        gerenciadorPortaChavesScript.ContagemDeChaveNega();
                        animator.SetTrigger("Abrir");
                        trancada = false;

                    }
                    else 
                    {
                        //////////////////////////////
                        ///Colocar informação "PRECISO ACHAR AS CHAVES CERTAS DAS PORTAS CERTAS"/////
                        ///////////////////////////
                        animator.SetTrigger("Trancado");

                    }





                }
                
        }
        else if (Input.GetKeyDown(KeyCode.E) && trancada == false && colidindo)
        {
           animator.SetTrigger("Abrir");

        }
            
            
             
            

    }

    void OnTriggerEnter(Collider col)//verificando se o player esta dentro do collisor para abrir porta
    {
        if (col.gameObject.CompareTag("Player"))
        {
            colidindo = true;
          

        }

    }

    void OnTriggerExit(Collider col)//fechando a porta
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (portaAberta)
            {
                animator.SetTrigger("Fechar");

            }
            colidindo = false;

          


        }

    }
}
}