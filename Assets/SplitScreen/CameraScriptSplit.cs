using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptSplit : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;

    public PlayerHealthSplit1 playerHealth;
    public PlayerHealthSplit2 playerHealth2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.kO == true || playerHealth2.kO == true)
        {
            CameraTwo();
        }
        else
        {
            CameraOne();
        }
    }

    void CameraOne()
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
    }

    void CameraTwo()
    {
        Camera1.SetActive(false);
        Camera2.SetActive(true);
    }
}
