using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjuntoInterfaceCreator : MonoBehaviour
{
    public GameObject conjuntoPrefab, pecaGamePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void inicializaDeConjuntoLogico(Conjunto conjLogico, ArrayList pecasInterface)
    {
        GameObject tabuleiro = GameObject.FindGameObjectWithTag("Tabuleiro");
        GameObject conjunto = Instantiate(conjuntoPrefab, tabuleiro.transform);
        ConjuntoInterface conjInt = conjunto.GetComponent<ConjuntoInterface>();
        conjInt.inicializa();
        conjInt.setaConjLogico(conjLogico);
        conjInt.transform.position = conjLogico.calculaPosPorPecas();
        //conjLogico.printaPecas();
        foreach(GameObject p in pecasInterface)
        {
            p.transform.parent = conjunto.transform;
            conjInt.addPecaInterface(p, false);
        }
        //foreach (Peca p in conjLogico.getPecas())
        //{
        //    GameObject pecaGame = Instantiate(pecaGamePrefab);
        //    PecaGame criadorPecaGame = pecaGame.GetComponent<PecaGame>();
        //    criadorPecaGame.criaPeca(p);
        //    conjInt.addPecaInterface(pecaGame);
        //}
        //conjInt.inserePeca(gameObjet);
        //conjuntoDono = conj;
        tabuleiro.GetComponent<TabuleiroInterface>().insereConjInt(conjunto);
        tabuleiro.GetComponent<TabuleiroInterface>().ativaColisores();
    }
}
