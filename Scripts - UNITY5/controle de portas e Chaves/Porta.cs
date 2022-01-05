using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[Serializable]
public class SonsDaPorta
{
    public AudioClip somAbrir;
    [Range(0.5f, 3.0f)] public float velSomAbrir = 1;
    [Space(7)]
    public AudioClip somFechar;
    [Range(0.5f, 3.0f)] public float velSomFechar = 1;
    [Space(15)]
    public AudioClip somTrancada;
    public AudioClip somDestrancar;
}

[RequireComponent(typeof(AudioSource))]
public class Porta : MonoBehaviour
{
    public int IDPorta = 0;
    public enum EstadoInic { Aberta90, Fechada00, Trancada };
    public EstadoInic EstadoInicial = EstadoInic.Fechada00;
    public enum TipoDeRotacao { RodarEmX, RodarEmY, RodarEmZ };
    public TipoDeRotacao Rotacao = TipoDeRotacao.RodarEmY;
    public KeyCode TeclaAbrir = KeyCode.E;
    [Range(0.0f, 150.0f)] public float grausDeGiro = 90.0f;
    [Range(0.1f, 10.0f)] public float velocidadeDeGiro = 5, distanciaDaPorta = 3;
    public bool inverterGiro = false;
    public Text TextoTrancado;
    [Range(0.1f, 4.0f)] public float tempoTexto = 1;
    public SonsDaPorta Sons;
    //
    [Header("Ponto de proximidade opcional")]
    [Space(15)]
    public GameObject PontoDeProximidade;
    //
    Vector3 rotacaoInicial;
    float giroAtual, giroAlvo;
    GameObject Jogador;
    bool estaFechada, estaTrancada;
    AudioSource emissorSom;
    Chaves listaDeChaves;

    void Start()
    {
        rotacaoInicial = transform.eulerAngles;
        Jogador = GameObject.FindWithTag("Player");
        if (Jogador != null)
        {
            listaDeChaves = Jogador.GetComponent<Chaves>();
        }
        emissorSom = GetComponent<AudioSource>();
        emissorSom.playOnAwake = false;
        emissorSom.loop = false;
        if (TextoTrancado != null)
        {
            TextoTrancado.enabled = false;
        }
        switch (EstadoInicial)
        {
            case EstadoInic.Fechada00:
                estaFechada = true;
                estaTrancada = false;
                giroAlvo = 0.0f;
                giroAtual = 0.0f;
                break;
            case EstadoInic.Aberta90:
                estaFechada = false;
                estaTrancada = false;
                if (inverterGiro == true)
                {
                    giroAtual = grausDeGiro;
                    giroAlvo = grausDeGiro;
                }
                else
                {
                    giroAtual = -grausDeGiro;
                    giroAlvo = -grausDeGiro;
                }
                break;
            case EstadoInic.Trancada:
                estaFechada = true;
                estaTrancada = true;
                giroAlvo = 0.0f;
                giroAtual = 0.0f;
                break;
        }
    }

    void Update()
    {
        if (Jogador != null && listaDeChaves != null)
        {
            ControlarPorta();
            GirarObjeto();
        }
    }

    void ChecarSeTemAChave()
    {
        bool temAChave = false;
        for (int x = 0; x < listaDeChaves.ChavesDoJogador.Count; x++)
        {
            if (listaDeChaves.ChavesDoJogador[x] == IDPorta)
            {
                temAChave = true;
            }
        }
        if (temAChave == true)
        {
            estaTrancada = false;
            if (Sons.somDestrancar != null)
            {
                emissorSom.pitch = 1;
                emissorSom.clip = Sons.somDestrancar;
                emissorSom.PlayOneShot(emissorSom.clip);
            }
        }
        else
        {
            if (Sons.somTrancada != null)
            {
                emissorSom.pitch = 1;
                emissorSom.clip = Sons.somTrancada;
                emissorSom.PlayOneShot(emissorSom.clip);
            }
            StartCoroutine("MensagemNaTela");
        }
    }

    void ControlarPorta()
    {
        Vector3 localDeChecagem;
        if (PontoDeProximidade != null)
        {
            localDeChecagem = PontoDeProximidade.transform.position;
        }
        else
        {
            localDeChecagem = transform.position;
        }
        if (Vector3.Distance(Jogador.transform.position, localDeChecagem) < distanciaDaPorta)
        {
            if (Input.GetKeyDown(TeclaAbrir) && estaTrancada == false)
            {
                estaFechada = !estaFechada;
                //
                if (estaFechada == false)
                {
                    if (Sons.somAbrir != null)
                    {
                        emissorSom.pitch = Sons.velSomAbrir;
                        emissorSom.clip = Sons.somAbrir;
                        emissorSom.PlayOneShot(emissorSom.clip);
                    }
                    if (inverterGiro == true)
                    {
                        giroAlvo = grausDeGiro;
                    }
                    else
                    {
                        giroAlvo = -grausDeGiro;
                    }
                }
                else
                {
                    if (Sons.somFechar != null)
                    {
                        emissorSom.pitch = Sons.velSomFechar;
                        emissorSom.clip = Sons.somFechar;
                        emissorSom.PlayOneShot(emissorSom.clip);
                    }
                    if (inverterGiro == true)
                    {
                        giroAlvo = 0.0f;
                    }
                    else
                    {
                        giroAlvo = 0.0f;
                    }
                }
            }
            if (Input.GetKeyDown(TeclaAbrir) && estaTrancada == true)
            {
                ChecarSeTemAChave();
            }
        }
        giroAtual = Mathf.Lerp(giroAtual, giroAlvo, Time.deltaTime * velocidadeDeGiro);
    }

    void GirarObjeto()
    {
        switch (Rotacao)
        {
            case TipoDeRotacao.RodarEmX:
                transform.eulerAngles = new Vector3(rotacaoInicial.x + giroAtual, rotacaoInicial.y, rotacaoInicial.z);
                break;
            case TipoDeRotacao.RodarEmY:
                transform.eulerAngles = new Vector3(rotacaoInicial.x, rotacaoInicial.y + giroAtual, rotacaoInicial.z);
                break;
            case TipoDeRotacao.RodarEmZ:
                transform.eulerAngles = new Vector3(rotacaoInicial.x, rotacaoInicial.y, rotacaoInicial.z + giroAtual);
                break;
        }
    }

    IEnumerator MensagemNaTela()
    {
        if (TextoTrancado != null)
        {
            TextoTrancado.enabled = true;
            yield return new WaitForSeconds(tempoTexto);
            TextoTrancado.enabled = false;
        }
    }
}