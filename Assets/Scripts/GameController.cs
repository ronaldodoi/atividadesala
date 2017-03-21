using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

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
		StartCoroutine(GerarObstaculos());
	}
	IEnumerator GerarObstaculos()
    {
        while (true)
        {
            Vector3 pos = new Vector3(4.2f, Random.Range(-1.5f, 0.5f), 1.33f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.identity) as GameObject;
            Destroy(obj,tempoDestruicao);
            yield return new WaitForSeconds(espera);
        }
    }

	
	void Update () {
		
	}
}
