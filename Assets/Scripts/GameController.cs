using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    public GameObject gameOverPanel;
    public GameObject pontosPanel;
    public Text txtMaiorPontuacao;
    private List<GameObject> obstaculos;



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
        obstaculos = new List<GameObject>();
        estado = Estados.AguardoComecar;
        PlayerPrefs.SetInt("HighScore", 0);
        menuCamera.SetActive(true);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pontosPanel.SetActive(false);

    }
    IEnumerator GerarObstaculos()
    {
        while (GameController.instancia.estado == Estados.jogando)  
        {
            Vector3 pos = new Vector3(11f, Random.Range(-2.5f, -4f), 2.05f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.Euler(0f,0f,0f)) as GameObject;
            obstaculos.Add(obj);
            StartCoroutine(DestruirObstaculo(obj));
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

    IEnumerator DestruirObstaculo(GameObject obj) {
        yield return new WaitForSeconds(tempoDestruicao);
        if (obstaculos.Remove(obj)) {
            Destroy(obj);
        }
    }


    public void PlayerComecou()
    {
        estado = Estados.jogando;
        menuCamera.SetActive(false);
        menuPanel.SetActive(false);
        pontosPanel.SetActive(true);
        atualizarPontos(0);
        StartCoroutine(GerarObstaculos());
    }

    public void PlayerVoltou()
    {
        while (obstaculos.Count > 0)
        {
            GameObject obj = obstaculos[0];
            if (obstaculos.Remove(obj))
            {
                Destroy(obj);
            }
        }
        estado = Estados.AguardoComecar;
        menuCamera.SetActive(true);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pontosPanel.SetActive(false);
        GameObject.Find("micro_zombie").GetComponent<PlayerController>().recomecar();
    }


    public void PlayerMorreu()
    {
        estado = Estados.GameOver;
        if (pontos > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", pontos);
            txtMaiorPontuacao.text = "" + pontos;
        }
        gameOverPanel.SetActive(true);

    }
    
}
