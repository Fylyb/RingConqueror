using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using UnityEngine.Audio;

public class OptionsScreen : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;

    public TextMeshProUGUI resolutionLabel;

    public List<RestItem> resolutions = new List<RestItem>();
    private int _selectedResolution;

    public AudioMixer theMixer;

    public TextMeshProUGUI mastLabel;
    public Slider mastSlider;

    // Start is called before the first frame update
    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }

        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;

                _selectedResolution = i;

                UpdateResLabel();
            }
        }

        if (!foundRes)
        {
            RestItem newRes = new RestItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            _selectedResolution = resolutions.Count -1;

            UpdateResLabel();
        }

        float vol = 0f;
        theMixer.GetFloat("MasterVol", out vol);
        mastSlider.value = vol;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResLeft()
    {
        _selectedResolution--;
        if(_selectedResolution < 0)
        {
            _selectedResolution = 0;
        }

        UpdateResLabel();
    }
    public void ResRight()
    {
        _selectedResolution++;
        if(_selectedResolution > resolutions.Count - 1)
        {
            _selectedResolution = resolutions.Count - 1;
        }

        UpdateResLabel();
    }
    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[_selectedResolution].horizontal.ToString() + " x " + resolutions[_selectedResolution].vertical.ToString();
    }
    public void ApplyGraphics()
    {
        //Screen.fullScreen = fullscreenTog.isOn;

        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[_selectedResolution].horizontal, resolutions[_selectedResolution].vertical, fullscreenTog.isOn);
    }
    public void SetMasterVol(float volume)
    {
        mastLabel.text = Mathf.RoundToInt(mastSlider.value + 80).ToString();

        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}

[System.Serializable]
public class RestItem
{
    public int horizontal, vertical;
}