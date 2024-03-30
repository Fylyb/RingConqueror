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
            Debug.Log("boxovac� ring m� Collider komponentu.");
        }
        else
        {
            // Collider neexistuje
            Debug.LogError("Chyb� Collider komponenta na boxovac�m ringu.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
