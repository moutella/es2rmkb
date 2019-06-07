using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pecaDragUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public bool movimentando;
    float distance = 10; //mudar para pegar a distancia toda frame
    public Collider2D colisor;
    public GameObject pecaGamePrefab;
    private GameObject pecaGame;
    private PecaGame criadorPecaGame;
    private controladorPeca controlaPeca;
    private maoUI maoPlayer;
    public GameObject slotAtual;
    public bool setou;
    public Image imagem;
    public TMPro.TextMeshProUGUI texto;
    public GameObject tabuleiro;
    private bool jaExistePecaWorld;
    public ControladorJogo Controlador;
    public void OnDrag(PointerEventData eventData)
    {
        if(Controlador.getTurno(ControladorJogo.JOGADOR)){
            transform.position = Input.mousePosition;
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 pecaPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (pecaGame != null) { 
                pecaGame.transform.position = pecaPos;
                controlaPeca.pecaMovimentada = true;
                controlaPeca.pecaSolta = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Controlador.isBotandoPeca = true;
        tabuleiro.GetComponent<TabuleiroInterface>().ativaColisores();
        if (slotAtual != null) {
            slotAtual.GetComponent<slotMao>().libera();
            slotAtual = null;
        }
        if (pecaGame == null)
        {
            pecaGame = Instantiate(pecaGamePrefab);
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 pecaPos = Camera.main.ScreenToWorldPoint(mousePos);
            pecaGame.transform.position = pecaPos;
            criadorPecaGame = pecaGame.GetComponent<PecaGame>();
            controlaPeca = pecaGame.GetComponent<controladorPeca>();
            criadorPecaGame.criaPeca(GetComponent<pecaGameUI>().pecaLogica);
            criadorPecaGame.setInvisivel();
            controlaPeca.setaPecaUI(gameObject);
            colisor.enabled = true;
            controlaPeca.contaColisao = 0;
            controlaPeca.pecaMovimentada = true;
            controlaPeca.pecaSolta = false;
            controlaPeca.GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            jaExistePecaWorld=true;
        }
        movimentando = true;
        colisor.enabled = true;
        setou = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //tabuleiro.GetComponent<TabuleiroInterface>().desativaColisores();
        movimentando = false;
        if (pecaGame != null) { 
            controlaPeca.pecaSolta = true;
        }
        if (pecaGame != null & texto.enabled == false)
        {
            pecaGame.GetComponent<controladorPeca>().pecaMovimentada = false;
            //Debug.Log("CONTA COLISAO:  " + controlaPeca.contaColisao);
            if (controlaPeca.contaColisao == 0 & !jaExistePecaWorld)
            {
                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                Vector3 pecaPos = Camera.main.ScreenToWorldPoint(mousePos);
                controlaPeca.criaConjuntoNovo(pecaPos);
            }
        }
        //Debug.Log("soltou");
    }


    void Start()
    {
        jaExistePecaWorld = false;
        tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
        distance = -Camera.main.transform.position.z;
        maoPlayer = GameObject.FindGameObjectWithTag("SeguraPecaUi").GetComponent<maoUI>();
        movimentando = false;

        Controlador = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (movimentando) { 
        if(col.tag == "SeguraPecaUi")
            {
                GetComponent<Image>().enabled = true;
                texto.enabled = true;
                criadorPecaGame.setInvisivel();
                if (!maoPlayer.pecaUIObjects.Contains(gameObject)) { 
                    maoPlayer.inserePeca(gameObject);
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "SeguraPecaUi")
        {
            
            maoPlayer.removePeca(gameObject);
            GetComponent<Image>().enabled = false;
            texto.enabled = false;
            criadorPecaGame.setVisivel();
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(!movimentando)
        { 
            if (col.tag == "Slot" && !setou)
            {
                if (col.GetComponent<slotMao>().vazio())
                {
                    transform.SetPositionAndRotation(col.transform.position, Quaternion.identity);
                    col.GetComponent<slotMao>().preenche(gameObject);
                    slotAtual = col.gameObject;
                    colisor.enabled = false;
                    setou = true;
                }
                else
                {
                    GameObject slot = maoPlayer.getPrimeiroVazio();
                    transform.SetPositionAndRotation(slot.gameObject.transform.position, Quaternion.identity);
                    slot.GetComponent<slotMao>().preenche(gameObject);
                    slotAtual = slot;
                    colisor.enabled = false;
                    setou = true;
                }
            }
        }
    }
}
