using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    public Slider LoadingBarFill;
    public Button ContinueBtn;
    public TextMeshProUGUI LoadingText;

    private AsyncOperation operation;

    void Start()
    {
        // Deaktivuj tlaèítko a progress bar na zaèátku
        ContinueBtn.gameObject.SetActive(false);
        LoadingBarFill.gameObject.SetActive(false);
        LoadingText.gameObject.SetActive(true);

        // Nastav funkci pro stisknutí tlaèítka
        ContinueBtn.onClick.AddListener(OnContinueButtonClick);

        // Spus asynchronní naèítání scény
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        if (SceneManager.GetActiveScene().name == "FinishScreen")
        {
            //string currentSceneName = SceneManager.GetActiveScene().name;

            // Zaèni asynchronní naèítání scény s názvem aktualniScenaName + "GameMenu"
            operation = SceneManager.LoadSceneAsync("GameMenu");
            operation.allowSceneActivation = false; // Vypni automatické pøecházení naètené scény
        }
        else if(SceneManager.GetActiveScene().name == "LoadingScreenSplitScreen")
        {
            operation = SceneManager.LoadSceneAsync("SplitScreen");
            operation.allowSceneActivation = false;
        }
        else
        {
            // Zaèni asynchronní naèítání scény
            operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            operation.allowSceneActivation = false; // Vypni automatické pøecházení naètené scény
        }

        // Aktivuj progress bar
        LoadingBarFill.gameObject.SetActive(true);

        // Èekej, dokud scéna není naètena do 90%
        while (operation.progress < 0.9f)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.value = progressValue;
            yield return null;
        }

        // Scéna byla naètena do 90%, deaktivuj progress bar a aktivuj tlaèítko
        ContinueBtn.gameObject.SetActive(true);
        LoadingBarFill.gameObject.SetActive(false);
        LoadingText.gameObject.SetActive(false);


    }

    void OnContinueButtonClick()
    {
        // Pøi stisknutí tlaèítka povol automatické pøecházení naètené scény
        operation.allowSceneActivation = true;
    }
}
