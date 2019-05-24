using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float sizeObjetivo;
    public float velocidadeZoom;
    //float sizeAtual;
    // Start is called before the first frame update
    void Start()
    {
        sizeObjetivo = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        sizeObjetivo = Mathf.Clamp(sizeObjetivo + -Input.GetAxis("Mouse ScrollWheel"), 4, 7);

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, sizeObjetivo, velocidadeZoom);
        //Debug.Log(sizeObjetivo);
    }
}
