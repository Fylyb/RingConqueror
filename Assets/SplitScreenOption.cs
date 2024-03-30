using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SplitScreenOption : MonoBehaviour
{
    public TextMeshProUGUI roundLabel;
    public TextMeshProUGUI minutesPerRoundLabel;

    public GameManager2 gameManager; // Reference na GameManager2

    private int _selectedRounds;
    private float _selectedMinutes;

    // Start is called before the first frame update
    void Start()
    {
        _selectedRounds = 1;
        _selectedMinutes = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ResLeftRounds()
    {
        _selectedRounds--;
        if (_selectedRounds <= 0)
        {
            _selectedRounds = 1;
        }

        UpdateRoundsLabel();
    }
    public void ResRightRounds()
    {
        _selectedRounds++;
        if (_selectedRounds > 3)
        {
            _selectedRounds = 3;
        }

        UpdateRoundsLabel();
    }
    public void UpdateRoundsLabel()
    {
        roundLabel.text = _selectedRounds.ToString();
    }
    public void ResLeftMinutes()
    {
        _selectedMinutes--;
        if (_selectedMinutes <= 0)
        {
            _selectedMinutes = 1;
        }

        UpdateMinutesLabel();
    }
    public void ResRightMinutes()
    {
        _selectedMinutes++;
        if (_selectedMinutes > 3)
        {
            _selectedMinutes = 3;
        }

        UpdateMinutesLabel();
    }
    public void UpdateMinutesLabel()
    {
        minutesPerRoundLabel.text = _selectedMinutes.ToString();
    }

    public void PlaySplitScreen()
    {
        int roundsToKeep = _selectedRounds;
        float minutesToKeep = _selectedMinutes;

        StaticData.valueOfRoundsToKeep = roundsToKeep;
        StaticData.valueOfMinutesToKeep = minutesToKeep;

        SceneManager.LoadScene("LoadingScreenSplitScreen");
    }
}