using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasmaMove : MonoBehaviour {

    public float velocidadeVertical;
    public float velocidadeHorizontal;
    public float min;
    public float max;
    public float espera;

    void Start() {
        float direcao = (Random.Range(0f, 1f) < 0.5) ? min : max;
        StartCoroutine(Move(direcao));
    }

    IEnumerator Move(float destino) {
        while (Mathf.Abs(destino - transform.position.y) > 0.1f) {
            Vector3 direcao = (destino == max) ? Vector3.up : Vector3.down;
            Vector3 velocidadeVetorial = direcao * velocidadeVertical;
            transform.position = transform.position + velocidadeVetorial * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(espera);

        destino = (destino == max) ? min : max;
        StartCoroutine(Move(destino));
    }

    void Update() {
        Vector3 velocidadeVetorial = Vector3.left * velocidadeHorizontal;
        transform.position = transform.position + velocidadeVetorial * Time.deltaTime;

    }
}


