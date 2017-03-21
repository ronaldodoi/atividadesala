using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Estados estado { get; private set; }

    public GameObject obstaculo;
    public float espera;
    public float tempoDestruicao;
    public static GameController instancia = null;

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
            Vector3 pos = new Vector3(4.2f, Random.Range(-1.5f, 0.5f), 1.33f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.identity) as GameObject;
            Destroy(obj,tempoDestruicao);
            yield return new WaitForSeconds(espera);
        }
    }
    
    public void PlayerComecou()
    {
        estado = Estados.jogando;
        StartCoroutine(GerarObstaculos());
    }
    public void PlayerMorreu()
    {
        estado = Estados.GameOver;
        
    }
    
}
