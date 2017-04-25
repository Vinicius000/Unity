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
    public Text txtMaiorPontuacao;
    public GameObject gameOverPanel;
    public GameObject pontosPanel;

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
        PlayerPrefs.SetInt("HighScore", 0);
        menuCamera.SetActive(true);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pontosPanel.SetActive(false);
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
        pontosPanel.SetActive(true);
        atualizarPontos(0);
        StartCoroutine(GerarObstaculos());
    }

    public void PlayerMorreu()
    {
        estado = Estado.GameOver;
        if (pontos > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", pontos);
            txtMaiorPontuacao.text = "" + pontos;
        }
        gameOverPanel.SetActive(true);
    }

    public void PlayerVoltou()
    {
        estado = Estado.AguardandoComecar;
        menuCamera.SetActive(true);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pontosPanel.SetActive(false);
        GameObject.Find("micro_zombie_mobile").GetComponent<PlayerController>().recomecar();
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

