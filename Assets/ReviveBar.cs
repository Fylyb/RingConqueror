using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ReviveBar : MonoBehaviour
{
    private Slider slider;
    public int spaceBarPressCount;

    public bool successfulRevive = false;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        //if (playerHealth.kO == true)
        //{
        //    // Detekce stisku mezerníku
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        // Inkrementace poètu stiskù mezerníku
        //        //spaceBarPressCount++;
        //        //Debug.Log(spaceBarPressCount);
        //        // Aktualizace hodnoty slideru
        //        //UpdateSliderValue();

        //        // Kontrola, zda dosáhl požadovaného poètu stiskù mezerníku
        //        if (spaceBarPressCount >= 10)
        //        {
        //            successfulRevive = true;
        //        }
        //    }
        //}
    }
    public void UpdateSliderValue(float max)
    {
        // Aktualizace hodnoty slideru na základì poètu stiskù mezerníku
        slider.value = Mathf.Clamp01((float)spaceBarPressCount / max);
    }
}
