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
        //    // Detekce stisku mezern�ku
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        // Inkrementace po�tu stisk� mezern�ku
        //        //spaceBarPressCount++;
        //        //Debug.Log(spaceBarPressCount);
        //        // Aktualizace hodnoty slideru
        //        //UpdateSliderValue();

        //        // Kontrola, zda dos�hl po�adovan�ho po�tu stisk� mezern�ku
        //        if (spaceBarPressCount >= 10)
        //        {
        //            successfulRevive = true;
        //        }
        //    }
        //}
    }
    public void UpdateSliderValue(float max)
    {
        // Aktualizace hodnoty slideru na z�klad� po�tu stisk� mezern�ku
        slider.value = Mathf.Clamp01((float)spaceBarPressCount / max);
    }
}
