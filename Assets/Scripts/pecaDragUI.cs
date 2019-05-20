using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pecaDragUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Collider2D colisor;
    public maoUI maoPlayer;
    public GameObject slotAtual;
    private bool setou;
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (slotAtual != null) {
            Debug.Log("Liberou:  " + slotAtual);
            slotAtual.GetComponent<slotMao>().libera();
            slotAtual = null;
        }
        setou = false;
        //colisor.enabled = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        colisor.enabled = true;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //Vector3 pecaPos = Camera.main.ScreenToWorldPoint(mousePos);
        //transform.position = pecaPos;
        //pecaMovimentada = true;
        //pecaSolta = false;
        transform.position = mousePos;
    }

    private void OnMouseUp()
    {
        
        //StartCoroutine("mudaParaSolta");
    }

    void Start()
    {
        maoPlayer = GameObject.FindGameObjectWithTag("SeguraPecaUi").GetComponent<maoUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
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
