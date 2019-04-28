using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class seguradorDePecas : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] locais;
    public GameObject[] pecas;
    public bool[] livre;
    public int numeroDeLocais;
    void Start()
    {
        pecas = new GameObject[numeroDeLocais];
        livre = new bool[numeroDeLocais];
       for (int i = 0; i < numeroDeLocais; i++)
        {
            livre[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private int primeiroVazio()
    {
        int x = 0;
        foreach(bool local in livre){
            if (local)
            {
                return x;
            }
            x++;
        }
        return -1;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        print(other.gameObject.GetComponent<controladorPeca>().pecaSolta);
        if (other.gameObject.tag == "peca" && other.gameObject.GetComponent<controladorPeca>().pecaSolta)
        {
            other.gameObject.GetComponent<controladorPeca>().pecaSolta = false;
            print(primeiroVazio());
            other.gameObject.transform.position = locais[primeiroVazio()].position + new Vector3(0,0,-1);
            pecas[primeiroVazio()] = other.gameObject;
            livre[primeiroVazio()] = false;
        }
    }
    public void limpaArrays(GameObject peca)
    {
        print(peca);
        print("ue");
        int x = 0;
        foreach (GameObject pecaSalva in pecas)
        {
            if(pecaSalva == peca)
            {
                livre[x] = true;
                pecas[x] = null;
            }
            x++;
        }
    }
}