using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugJogo : MonoBehaviour
{
    public GameObject peca;
    float distance = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject.Instantiate(peca, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance)), Quaternion.identity);
        }
    }
}
