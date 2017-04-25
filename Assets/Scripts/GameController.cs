using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Estado estado { get; private set; }

    public GameObject obstaculo;
    public float espera;
    public float tempoDestruicao;
    public GameObject menuCamera;
    public GameObject menuPanel;
    public GameObject prancha;
    public Text txtPontos;

    private int pontos;

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
            Vector3 pos = new Vector3(3f, Random.Range(1f, 2f), -7.7f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.identity) as GameObject;
            Destroy(obj, tempoDestruicao);
            yield return new WaitForSeconds(espera);
        }
    }

    public void PlayerComecou()
    {
        estado = Estado.Jogando;
        menuCamera.SetActive(false);
        menuPanel.SetActive(false);
        StartCoroutine(GerarObstaculos());
    }

    public void PlayerMorreu() {
        estado = Estado.GameOver;
        prancha.SetActive (false);
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
}

