using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tpp;
    public GameObject shooting;

    void Start()
    {
        tpp.SetActive(true);
        shooting.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            tpp.SetActive(false);
            shooting.SetActive(true);
        }
        else
        {
            tpp.SetActive(true);
            shooting.SetActive(false);
        }
    }
}
