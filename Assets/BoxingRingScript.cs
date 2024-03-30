using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingRingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Collider boxCollider = GetComponent<Collider>();

        // Kontrola, zda Collider existuje
        if (boxCollider != null)
        {
            // Collider existuje
            Debug.Log("boxovací ring má Collider komponentu.");
        }
        else
        {
            // Collider neexistuje
            Debug.LogError("Chybí Collider komponenta na boxovacím ringu.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
