using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorPeca : MonoBehaviour
{
    // Start is called before the first frame update
    private seguradorDePecas seguraPecas;
    float distance = 10;
    public int valor, tipo;
    public bool pecaMovimentada, pecaSolta;
    private Collider2D colisao;
    private void OnMouseDown()
    {
        seguraPecas.limpaArrays(gameObject);
        //colisao.enabled = false;
    }
    private void OnMouseDrag()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 pecaPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = pecaPos;
        pecaMovimentada = true;
        pecaSolta = false;
    }

    private void OnMouseUp()
    {
        //colisao.enabled = true;
        if (pecaMovimentada)
        {
            pecaMovimentada = false;
            pecaSolta = true;
        }
        StartCoroutine("mudaParaSolta");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (pecaSolta & other.gameObject.tag=="peca") { 
            Vector3 dif = other.gameObject.transform.position - transform.position;
            if(dif.x > 0)
            {
                transform.position = other.gameObject.transform.position - new Vector3(GetComponent<Collider2D>().bounds.size.x*1.1f, 0, 0);
            }
            else
            {
                transform.position = other.gameObject.transform.position + new Vector3(GetComponent<Collider2D>().bounds.size.x * 1.1f, 0, 0);
            }
        }
    }
    void Start()
    {
        seguraPecas = GameObject.FindGameObjectsWithTag("SeguraPecaController")[0].GetComponent<seguradorDePecas>();

        colisao = gameObject.GetComponent<BoxCollider2D>();
        pecaMovimentada = false;
        pecaSolta = false;
    }
    IEnumerator mudaParaSolta()
    {
        yield return new WaitForSeconds(0.2f);
        if (pecaSolta == true)
        {
            pecaSolta = false;
        }
        yield return null;
    }
    void LateUpdate()
    {
        //pecaSolta = false;
    }
}
