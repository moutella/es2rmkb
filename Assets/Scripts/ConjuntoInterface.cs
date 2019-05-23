using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjuntoInterface : MonoBehaviour
{
    float distance;
    private static float tamanhoPeca = 2 / 3f, distanciaPecas = 0.7f;
    private Conjunto conjuntoLogico;
    public GameObject conjuntoPrefab, pecaGamePrefab;
    public ArrayList pecasObjFilho;
    private BoxCollider2D colisor;
    private bool conjuntoEmMovimento, conjuntoSolto;
    private int contaCol;
    ControladorJogo Controlador;




    void Start()
    {
        conjuntoSolto = false;
        conjuntoEmMovimento = true;
    }
    void flipaColisor()
    {
        GetComponent<Collider2D>().enabled = !GetComponent<Collider2D>().enabled;
    }
    void FixedUpdate()
    {
        distance = -Camera.main.transform.position.z;
        if (Controlador.isBotandoPeca)
        {
            GetComponent<Collider2D>().enabled = true;
        }
        else { 
            GetComponent<Collider2D>().enabled = Controlador.modoConjunto;
        }

    }
    public void LateUpdate()
    {
        if (gameObject.name.Equals("ConjuntoInterface(Clone)(Clone)"))
        {
            Destroy(gameObject);
        }
        //transform.localPosition = recalculaPosition();
        if (transform.childCount == 0)
        {
            GameObject tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
            tabuleiro.GetComponent<TabuleiroInterface>().removeConjInt(gameObject);
            Destroy(gameObject);
        }
    }


    private void OnMouseDown()

    {
        //Inserir código que vai ser executado quando um player clicar num conjunto[com a tecla shift]
    }
    private void OnMouseDrag()
    {
        conjuntoSolto = false;
        conjuntoEmMovimento = true;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnMouseUp()
    {
        if (conjuntoEmMovimento & contaCol != 0)
        {
            conjuntoSolto = true;
        }
        mudaPosPecasFilho();
        //if (contaCol == 0)
        //{
        //    conjuntoSolto = falso;
        //}
    }
    /// <summary>
    /// FIM DAS FUNÇÕES CHAMADAS PELO UNITY
    /// </summary>
    public void inicializa()
    {
        this.colisor = GetComponent<BoxCollider2D>();
        this.conjuntoLogico = new Conjunto();
        this.pecasObjFilho = new ArrayList();
        GameObject tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
        Controlador = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>();
        Tabuleiro tabAtual = Controlador.getTabuleiroAtual();
        GetComponent<Collider2D>().enabled = Controlador.modoConjunto;
        tabAtual.insereConjunto(conjuntoLogico);
        tabuleiro.GetComponent<TabuleiroInterface>().desativaColisores();
//        conjuntoLogico.setPos(transform.localPosition);
    }
    public void addPecaInterface(GameObject peca, bool mudapos)
    {
        pecasObjFilho.Add(peca);
        peca.GetComponent<controladorPeca>().setaConjuntoDono(gameObject);
        peca.transform.parent = transform;
        if (pecasObjFilho.Count > 1 & mudapos)
        {
            transform.localPosition += new Vector3(0.35f, 0, 0);
        }
        mudaPosPecasFilho();
        colisor.size = new Vector2(tamanhoPeca * transform.childCount, 1);
    }
    public void inserePeca(GameObject peca, bool mudaPos)
    {
        Peca p = peca.GetComponent<PecaGame>().getPecaLogica();
        if (!conjuntoLogico.getPecas().Contains(p)) {
            //Debug.Log(tamanhoPeca);
            if (!pecasObjFilho.Contains(peca)) {
                pecasObjFilho.Add(peca);
            }
            //Debug.Log(pecasObjFilho.Count);
            conjuntoLogico.inserePeca(peca.GetComponent<PecaGame>().getPecaLogica());
            //Debug.Log(tamanhoPeca * pecasObjFilho.Count);
            if (pecasObjFilho.Count > 1 & mudaPos) {
                transform.localPosition += new Vector3(0.35f, 0, 0);
            }
            peca.transform.parent = transform;
            mudaPosPecasFilho();
            colisor.size = new Vector2(tamanhoPeca * transform.childCount, 1);
        }
    }
    public void insereOutroConjunto(ArrayList pecas)
    {
        foreach (GameObject peca in pecas)
        {
            inserePeca(peca, true);
        }
    }
    public void inserePecaAntes(GameObject peca)
    {
        Peca p = peca.GetComponent<PecaGame>().getPecaLogica();
        if (!conjuntoLogico.getPecas().Contains(p))
        {
            if (!pecasObjFilho.Contains(peca))
            {
                pecasObjFilho.Insert(0, peca);
            }
            conjuntoLogico.inserePecaAntes(peca.GetComponent<PecaGame>().getPecaLogica());
            if (pecasObjFilho.Count > 1)
            {
                transform.localPosition -= new Vector3(0.35f, 0, 0);
            }
            peca.transform.parent = transform;

            mudaPosPecasFilho();
            colisor.size = new Vector2(tamanhoPeca * transform.childCount, 1);
        }
    }
    public void insereOutroConjuntoAntes(ArrayList pecas)
    {
        foreach (GameObject peca in pecas)
        {
            inserePecaAntes(peca);
        }
    }
    public void removePeca(GameObject peca)
    {
        //Debug.ClearDeveloperConsole();
        //Debug.Log("--------------COMECOU UM SPLIT----------");
        int x = pecasObjFilho.Count;
        Peca p = peca.GetComponent<PecaGame>().getPecaLogica();
        Conjunto extra = conjuntoLogico.divide(p);
        int index = pecasObjFilho.IndexOf(peca);
        pecasObjFilho.Remove(peca);
        if (extra != null)
        {

            GameObject tab = GameObject.FindGameObjectWithTag("Tabuleiro");
            ConjuntoInterfaceCreator inicializador = tab.GetComponent<ConjuntoInterfaceCreator>();

            Debug.Log("TOTAL LENGHT: " + (pecasObjFilho.Count + 1));

            ArrayList pecasEsq = pecasObjFilho.GetRange(0, conjuntoLogico.getNumPecas());
            Debug.Log("pecas esq length: " + pecasEsq.Count);

            ArrayList pecasDir = pecasObjFilho.GetRange(index, extra.getNumPecas());
            Debug.Log("pecas dir length: " + pecasDir.Count);

            inicializador.inicializaDeConjuntoLogico(conjuntoLogico, pecasEsq);
            inicializador.inicializaDeConjuntoLogico(extra, pecasDir);
            Tabuleiro tabAtual = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>().getTabuleiroAtual();
            tabAtual.removeConjunto(conjuntoLogico);
            //Debug.Break();
            Destroy(gameObject);
            //Debug.Break();
        }
        else
        {
            GameObject tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
            tabuleiro.GetComponent<TabuleiroInterface>().ativaColisores();

            if (transform.childCount == 0)
            {
                Tabuleiro tabAtual = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>().getTabuleiroAtual();
                tabAtual.removeConjunto(conjuntoLogico);
                Destroy(gameObject);
            }
            if (conjuntoLogico.getPecas().Count == 0)
            {
                Tabuleiro tabAtual = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>().getTabuleiroAtual();
                tabAtual.removeConjunto(conjuntoLogico);
                Destroy(gameObject);
            }
            else
            {
                transform.localPosition = recalculaPosition();
                conjuntoLogico.setPos(transform.localPosition);
                mudaPosPecasFilho();
            }
            colisor.size = new Vector2((tamanhoPeca * (x - 1)), 1);
        }
    }

    public Vector3 recalculaPosition() //Quando retirar vai setar a nova raiz do conjunto no meio do conjunto restante
    {
        Vector3 posicao = Vector3.zero;
        //Debug.Log("NUMERO DE ELEMENTOS: " + pecasObjFilho.Count + "NUMERO PECAS: " + conjuntoLogico.getPecas().Count);
        foreach (GameObject p in pecasObjFilho)
        {
            //Debug.Log(p.transform.position);
            posicao += p.transform.position;
            
        }
        //Debug.Log(posicao);
        posicao = posicao / pecasObjFilho.Count;
        return posicao;
    }
    
    
    public void setaConjLogico(Conjunto conj)
    {

        Tabuleiro tabAtual = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>().getTabuleiroAtual();
        tabAtual.removeConjunto(conjuntoLogico);
        this.conjuntoLogico = conj;
        tabAtual.insereConjunto(conjuntoLogico);
    }
    public void mudaPosPecasFilho()    //Após a inserção ou remoção de uma peça, relativo a posição do conjunto
    {
        //Debug.Log("QUANTIDADE DE PECAS: " + pecasObjFilho.Count);
        //conjuntoLogico.printaPecas();
            if (conjuntoLogico.getNumPecas() % 2 == 0) //numero par de peças
            {
                int numeroPecasExtrasPorlado = pecasObjFilho.Count / 2 - 1;
            float offset = -0.35f - numeroPecasExtrasPorlado * distanciaPecas;
                foreach(GameObject peca in pecasObjFilho)
                {
                    //Debug.Log(offset);
                    peca.GetComponent<PecaGame>().setaPosicao(offset);
                    offset += distanciaPecas;
                    
                }
            }
            else //numero impar de peças
            {

                float offset = - ((pecasObjFilho.Count-1)/2) * distanciaPecas;
                
                foreach (GameObject peca in pecasObjFilho)
                {
                    //Debug.Log(offset);
                    peca.GetComponent<PecaGame>().setaPosicao(offset);
                    offset += distanciaPecas;
                }
            }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Conjunto" & conjuntoEmMovimento)
        {
            contaCol++;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Conjunto" & conjuntoEmMovimento)
        {
            contaCol--;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {

        //Debug.Log("BIRL COLIDIU COM: " + other.gameObject.tag);

        if (conjuntoSolto & other.gameObject.tag == "Conjunto")
        {
            Debug.Log("ENTROU");
            conjuntoSolto = false;
            if (other.gameObject.transform.position.x < transform.position.x)
            {
                other.gameObject.GetComponent<ConjuntoInterface>().insereOutroConjunto(pecasObjFilho);
                Debug.Log("BOTOU DEPOIS");
                Tabuleiro tabAtual = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>().getTabuleiroAtual();
                tabAtual.removeConjunto(conjuntoLogico);
                Destroy(gameObject);
            }
            else
            {
                other.gameObject.GetComponent<ConjuntoInterface>().insereOutroConjuntoAntes(pecasObjFilho);
                Debug.Log("BOTOU ANTES");
                Tabuleiro tabAtual = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>().getTabuleiroAtual();
                tabAtual.removeConjunto(conjuntoLogico);
                Destroy(gameObject);
            }
            ;
        }
    }
    
}
