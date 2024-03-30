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
        // Deaktivuj tla��tko a progress bar na za��tku
        ContinueBtn.gameObject.SetActive(false);
        LoadingBarFill.gameObject.SetActive(false);
        LoadingText.gameObject.SetActive(true);

        // Nastav funkci pro stisknut� tla��tka
        ContinueBtn.onClick.AddListener(OnContinueButtonClick);

        // Spus� asynchronn� na��t�n� sc�ny
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        if (SceneManager.GetActiveScene().name == "FinishScreen")
        {
            //string currentSceneName = SceneManager.GetActiveScene().name;

            // Za�ni asynchronn� na��t�n� sc�ny s n�zvem aktualniScenaName + "GameMenu"
            operation = SceneManager.LoadSceneAsync("GameMenu");
            operation.allowSceneActivation = false; // Vypni automatick� p�ech�zen� na�ten� sc�ny
        }
        else if(SceneManager.GetActiveScene().name == "LoadingScreenSplitScreen")
        {
            operation = SceneManager.LoadSceneAsync("SplitScreen");
            operation.allowSceneActivation = false;
        }
        else
        {
            // Za�ni asynchronn� na��t�n� sc�ny
            operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            operation.allowSceneActivation = false; // Vypni automatick� p�ech�zen� na�ten� sc�ny
        }

        // Aktivuj progress bar
        LoadingBarFill.gameObject.SetActive(true);

        // �ekej, dokud sc�na nen� na�tena do 90%
        while (operation.progress < 0.9f)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.value = progressValue;
            yield return null;
        }

        // Sc�na byla na�tena do 90%, deaktivuj progress bar a aktivuj tla��tko
        ContinueBtn.gameObject.SetActive(true);
        LoadingBarFill.gameObject.SetActive(false);
        LoadingText.gameObject.SetActive(false);


    }

    void OnContinueButtonClick()
    {
        // P�i stisknut� tla��tka povol automatick� p�ech�zen� na�ten� sc�ny
        operation.allowSceneActivation = true;
    }
}
