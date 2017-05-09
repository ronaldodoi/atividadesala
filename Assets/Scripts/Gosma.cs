using System.Collections;
using UnityEngine;

public class Gosma : MonoBehaviour
{

    public float velocidadeh;
    public float velocidadev;
    public float min;
    public float max;
    public float espera;
    private GameObject player;
    private bool pontuou = false;


    void Start()
    {
        float destino = (Random.Range(0f, 1f) < 0.5) ? max : min;
        StartCoroutine(Move(destino));
    }

    IEnumerator Move(float destino)
    {
        while (Mathf.Abs(destino - transform.position.y) > 0.2f)
        {
            Vector3 direcaov = (destino == max) ? Vector3.up : Vector3.down;
            Vector3 velocidadeVetorial = direcaov * velocidadev;
            transform.position = transform.position + velocidadeVetorial * Time.deltaTime;

            yield return null;

        }


        yield return new WaitForSeconds(espera);

        destino = (destino == max) ? min : max;
        StartCoroutine(Move(destino));
    }

    void Update()
    {

        Vector3 direcaoh = Vector3.left * velocidadeh;
        transform.position = transform.position + direcaoh * Time.deltaTime;
        if (!pontuou && GameController.instancia.estado == Estados.jogando)
        {
            if (transform.position.x < player.transform.position.x)
            {
                GameController.instancia.incrementarPontos(1);
                pontuou = true;
            }
        }
    }
    private void Awake()
    {
        player = GameObject.Find("micro_zombie");
    }

}
