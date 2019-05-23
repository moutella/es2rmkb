using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controladorPeca : MonoBehaviour
{
    // Start is called before the first frame update
    public int contaColisao;
    private seguradorDePecas seguraPecas;
    float distance = 10; //mudar para pegar a distancia toda frame
    public bool pecaMovimentada, pecaSolta;
    public GameObject ConjuntoPrefab;
    public bool inseridaNesteTurno, ignoraConjunto;
    public GameObject pecaNaUi;
    private Collider2D colisao;
    private Vector3 pecaPos;
    public GameObject tabuleiro;
    private GameObject conjuntoDono;

    public void setaConjuntoDono(GameObject conjuntoInt)
    {
        conjuntoDono = conjuntoInt;
    }
    private void OnMouseDown()
        
    {
        
        if (conjuntoDono != null)
        {
            conjuntoDono.GetComponent<ConjuntoInterface>().removePeca(gameObject);
            contaColisao = 0;
            conjuntoDono = null;
            ignoraConjunto = true;
        }
        contaColisao = 0;
        gameObject.transform.parent = transform.root;
    }
    private void OnMouseDrag()
    {
        if (inseridaNesteTurno & pecaNaUi != null)
        {
            pecaNaUi.GetComponent<pecaDragUI>().movimentando = true;
            pecaNaUi.GetComponent<pecaDragUI>().setou = false;
            pecaNaUi.transform.position = Input.mousePosition;
            colisao.enabled = true;
            //Debug.Log("ue");
            //pecaNaUi.GetComponent<>
        }
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        pecaPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = pecaPos;
        pecaMovimentada = true;
        pecaSolta = false;
    }

    private void OnMouseUp()
    {
        ignoraConjunto = false;
        if (pecaMovimentada)
        {
            pecaMovimentada = false;
            pecaSolta = true;
            tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
            tabuleiro.GetComponent<TabuleiroInterface>().ativaColisores();
        }
        if (inseridaNesteTurno)
        {
            pecaNaUi.GetComponent<pecaDragUI>().movimentando = false;

        }
        if (contaColisao == 0) {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 pecaPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (!pecaNaUi.GetComponent<Image>().enabled) { 
                criaConjuntoNovo(pecaPos);
            }
        }
    }

    private void FixedUpdate()
    {
        distance = -Camera.main.transform.position.z;
        if(colisao.enabled == false)
        {
            Debug.Log("COLISAO DESATIVADA");
        }
    }
    void Start()
    {
        tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
        contaColisao = 0;
        //inseridaNesteTurno = false;
        colisao = gameObject.GetComponent<BoxCollider2D>();
        pecaMovimentada = false;
        pecaSolta = false;
    }
    
    public void setaPecaUI(GameObject pecaUI)
    {
        pecaNaUi = pecaUI;
        inseridaNesteTurno = true;
    }
    public void tiraPecaUi()
    {
        inseridaNesteTurno = false;
        pecaNaUi = null;
        Destroy(pecaNaUi);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log(other.gameObject);
        //Debug.Log("pecaSolta: " + pecaSolta);
        if (pecaSolta & other.gameObject.tag == "Conjunto")
        {
            pecaSolta = false;
            if (other.gameObject.transform.position.x < transform.position.x)
            {
                other.gameObject.GetComponent<ConjuntoInterface>().inserePeca(gameObject, true);
                
            }
            else
            {
                other.gameObject.GetComponent<ConjuntoInterface>().inserePecaAntes(gameObject);
            }
            conjuntoDono = other.gameObject;
            tabuleiro.GetComponent<TabuleiroInterface>().desativaColisores();
            contaColisao = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject);
        if (other.gameObject.tag == "Conjunto")
        {
            contaColisao++;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Conjunto")
        {
            contaColisao--;
        }
    }

    public void criaConjuntoNovo(Vector3 pos)
    {
        GameObject tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
        GameObject conj = Instantiate(ConjuntoPrefab,
                    pos,
                    Quaternion.identity, tabuleiro.transform);
        tabuleiro.GetComponent<TabuleiroInterface>().insereConjInt(conj);
        ConjuntoInterface conjInt = conj.GetComponent<ConjuntoInterface>();
        conjInt.inicializa();
        conjInt.inserePeca(gameObject, true);
        conjuntoDono = conj;
        tabuleiro.GetComponent<TabuleiroInterface>().ativaColisores();
    }
    
}
