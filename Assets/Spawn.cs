using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] T;
    // Start is called before the first frame update
    void Start()
    {
        NewT();
    }

    // Update is called once per frame
    public void Update()
    {
       
    }

    public void NewT(){
        Instantiate(T[Random.Range(0,T.Length)],transform.position,Quaternion.identity);

    }
}
