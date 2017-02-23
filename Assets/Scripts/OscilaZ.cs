using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscilaZ : MonoBehaviour {

    public float Velocidade, min, max, espera;

	// Use this for initialization
	void Start () {
        StartCoroutine(Move(max));
	}
	
    IEnumerator Move(float destino)
    {
        while(Mathf.Abs(destino-transform.localPosition.z) > 0.2f)
        {
            Vector3 direcao = (destino == max) ? Vector3.forward : Vector3.back;
            Vector3 velocidadevetorial = direcao * Velocidade;
            transform.localPosition = transform.localPosition + velocidadevetorial * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(espera);

        destino = (destino == max) ? min : max;
        StartCoroutine(Move(destino));
    }
	
}
