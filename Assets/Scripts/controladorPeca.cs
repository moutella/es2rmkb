using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorPeca : MonoBehaviour
{
    // Start is called before the first frame update
    private seguradorDePecas seguraPecas;
    float distance = 10; //mudar para pegar a distancia toda frame
    public bool pecaMovimentada, pecaSolta;
    public bool inseridaNesteTurno;
    public GameObject pecaNaUi;
    private Collider2D colisao;
    private void OnMouseDown()
    {
       
        //seguraPecas.limpaArrays(gameObject);
        //colisao.enabled = false;
    }
    private void OnMouseDrag()
    {
        Debug.Log("PRINTOU PEÇA");
        if (inseridaNesteTurno)
        {
            pecaNaUi.GetComponent<pecaDragUI>().movimentando = true;
            pecaNaUi.GetComponent<pecaDragUI>().setou = false;
            pecaNaUi.transform.position = Input.mousePosition;
            pecaNaUi.GetComponent<Collider2D>().enabled = true;
            Debug.Log("ue");
            //pecaNaUi.GetComponent<>
        }
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
        if (inseridaNesteTurno)
        {
            pecaNaUi.GetComponent<pecaDragUI>().movimentando = false;

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
    private void FixedUpdate()
    {
        distance = -Camera.main.transform.position.z;
    }
    void Start()
    {
        //seguraPecas = GameObject.FindGameObjectsWithTag("SeguraPecaController")[0].GetComponent<seguradorDePecas>();
        //inseridaNesteTurno = false;
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

}
