using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Estado estado { get; private set; }

    public GameObject obstaculo;
    public float espera;
    public float tempoDestruicao;


    public static GameController instancia = null;

    void Awake() {
        if (instancia == null) {
            instancia = this;
        }
        else if (instancia != null) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        estado = Estado.AguardandoComecar;
    }

    IEnumerator GerarObstaculos() {
        while (GameController.instancia.estado == Estado.Jogando) {
            Vector3 pos = new Vector3(0.87f, Random.Range(0.321f, -1.222f), 0f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.identity) as GameObject;
            Destroy(obj, tempoDestruicao);
            yield return new WaitForSeconds(espera);
        }
    }

    public void PlayerComecou() {
        estado = Estado.Jogando;
        StartCoroutine(GerarObstaculos());
    }

    public void PlayerMorreu() {
        estado = Estado.GameOver;
    }
}


