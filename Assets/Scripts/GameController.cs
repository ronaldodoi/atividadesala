using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Estados estado { get; private set; }

    public GameObject obstaculo;
    public float espera;
    public float tempoDestruicao;
    public static GameController instancia = null;
    public GameObject menu;
    public GameObject canvas;
    public GameObject menuCamera;
    public GameObject menuPanel;
    private int pontos;
    public Text txtPontos;
    private void Awake()
    {
        if(instancia == null)
        {
            instancia = this;
        }
        else if (instancia != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
    }
            
    void Start () {
        estado = Estados.AguardoComecar;
	}
	IEnumerator GerarObstaculos()
    {
        while (GameController.instancia.estado == Estados.jogando)  
        {
            Vector3 pos = new Vector3(11f, Random.Range(-1.76f, -5.55f), 0f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.Euler(0f,-90f,0f)) as GameObject;
            Destroy(obj,tempoDestruicao);
            yield return new WaitForSeconds(espera);
        }
    }

    private void atualizarPontos(int x)
    {
        pontos = x;
        txtPontos.text = "" + x;
    }

    public void incrementarPontos(int x)
    {
        atualizarPontos(pontos + x);
    }
    
    public void PlayerComecou()
    {
        estado = Estados.jogando;
        menuCamera.SetActive(false);
        menuPanel.SetActive(false);
        atualizarPontos(0);
        StartCoroutine(GerarObstaculos());
    }

    public void PlayerMorreu()
    {
        estado = Estados.GameOver;
        
    }
    
}
