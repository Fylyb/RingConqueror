using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayerHealth : MonoBehaviour
{
    public AIcollision collisionScript;

    public GameManager gameManager;

    public Animator animator; // Animator komponenta hráèe

    public AIcollision aiCollision;

    public AudienceBehavier audienceBehavier;

    public float maxHealth = 100f;
    public float currentHealth;

    public int numberOfPressedSpaceToGetUp;
    public int numberOfPressedSpaceToGetUp2;

    public Slider healthSlider;
    public ReviveBar reviveBar;

    public AudioManager audioManager;

    public Image overlay;
    public float duration;
    public float fadeSpeed;

    public int spacebarCount;
    public int playerGetkoCount;
    public bool kO;

    private float _durationTimer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        collisionScript = GetComponent<AIcollision>();

        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (overlay.color.a > 0)
        {
            _durationTimer += Time.deltaTime;
            if (_durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public float HealPlayer()
    {
        return currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        print("Player Health: " + currentHealth);

        _durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);

        healthSlider.value = currentHealth / maxHealth;

        if (currentHealth <= 0 && playerGetkoCount == 0)
        {
            playerGetkoCount++;
            Die();
            currentHealth = Mathf.CeilToInt(maxHealth * 0.75f);//75% ze základu
            Debug.Log(currentHealth);
        }
        else if (currentHealth <= 0 && playerGetkoCount == 1)
        {
            playerGetkoCount++;
            Die();
            currentHealth = Mathf.CeilToInt(maxHealth * 0.5f);//50% ze základu
            Debug.Log(currentHealth);
        }
        else if (currentHealth <= 0 && playerGetkoCount == 2)
        {
            playerGetkoCount++;
            Die();
            currentHealth = Mathf.CeilToInt(maxHealth * 0.25f);//25% ze základu
            Debug.Log(currentHealth);
        }
    }

    private void Die()
    {
        // Zde provedete akce, které se mají stát po smrti hráèe, napøíklad zastavení hry, zobrazení game over obrazovky atd.
        aiCollision.hitAI += 10;
        audienceBehavier.WhichAnimation(3);
        Debug.Log("playerKOcount: " + playerGetkoCount);
        StartCoroutine(koTime(4.4f, 7f, 10f));
        print("Hráè KO");
    }
    private IEnumerator koTime(float koSeconds, float gettingUpSeconds, float reviveTime)
    {
        Debug.Log(playerGetkoCount);
        Debug.Log("zacatek pocitani");
        kO = true;
        animator.Play("Knocked Out");
        yield return new WaitForSeconds(koSeconds);
        Time.timeScale = 0f;
        gameManager.PauseGame();


        if (playerGetkoCount == 1)
        {
            float startTime = Time.realtimeSinceStartup;
            spacebarCount = 0;
            while (Time.realtimeSinceStartup - startTime < reviveTime && spacebarCount < numberOfPressedSpaceToGetUp)
            {
                gameManager.pressSpaceText.enabled = true;
                // Zde sledujeme stisknutí mezerníku
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    spacebarCount++;
                    Debug.Log("Mezerník stisknut: " + spacebarCount);
                    // Aktualizace hodnoty slideru v tøídì ReviveBar
                    reviveBar.spaceBarPressCount = spacebarCount;
                    reviveBar.UpdateSliderValue(numberOfPressedSpaceToGetUp);
                }

                yield return null; // Poèkáme na další frame
            }
            if (spacebarCount >= numberOfPressedSpaceToGetUp)
            {
                // Hráè stiskl mezerník alespoò 10krát
                gameManager.pressSpaceText.enabled = false;
                reviveBar.successfulRevive = true;
                Debug.Log("Revive successful");
                Time.timeScale = 1f;
                gameManager.ResumeGame();
                animator.Play("GettingUp");
                yield return new WaitForSeconds(gettingUpSeconds);
                kO = false;
                Debug.Log("konec pocitani");
                spacebarCount = 0;
                reviveBar.spaceBarPressCount = 0;
                reviveBar.UpdateSliderValue(numberOfPressedSpaceToGetUp);
                gameManager.reviveBar.interactable = false;
                gameManager.reviveBar.enabled = false;
                gameManager.reviveBar.gameObject.SetActive(false);

                healthSlider.value = currentHealth / maxHealth;

                audioManager.RingBellStart();
                audienceBehavier.WhichAnimation(4);
            }
            else if (spacebarCount < numberOfPressedSpaceToGetUp && Time.realtimeSinceStartup - startTime > reviveTime)
            {
                gameManager.LostByThirdKO();
            }
        }
        else if (playerGetkoCount == 2)
        {
            Debug.Log("revivetrueorfalse: " + reviveBar.successfulRevive);
            reviveBar.successfulRevive = false;
            gameManager.currentTimeCountdown = gameManager.countdown;

            gameManager.countdownText.enabled = true;

            gameManager.reviveBar.interactable = true;
            gameManager.reviveBar.enabled = true;
            gameManager.reviveBar.gameObject.SetActive(true);

            float startTime = Time.realtimeSinceStartup;
            spacebarCount = 0;
            while (Time.realtimeSinceStartup - startTime < reviveTime && spacebarCount < numberOfPressedSpaceToGetUp2)
            {
                gameManager.pressSpaceText.enabled = true;
                // Zde sledujeme stisknutí mezerníku
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    spacebarCount++;
                    Debug.Log("Mezerník stisknut: " + spacebarCount);
                    // Aktualizace hodnoty slideru v tøídì ReviveBar
                    reviveBar.spaceBarPressCount = spacebarCount;
                    reviveBar.UpdateSliderValue(numberOfPressedSpaceToGetUp2);
                }

                yield return null; // Poèkáme na další frame
            }
            if (spacebarCount >= numberOfPressedSpaceToGetUp2)
            {
                // Hráè stiskl mezerník alespoò 10krát
                gameManager.pressSpaceText.enabled = false;
                reviveBar.successfulRevive = true;
                Debug.Log("Revive successful");
                Time.timeScale = 1f;
                gameManager.ResumeGame();
                animator.Play("GettingUp");
                yield return new WaitForSeconds(gettingUpSeconds);
                kO = false;
                Debug.Log("konec pocitani");
                spacebarCount = 0;
                reviveBar.spaceBarPressCount = 0;
                reviveBar.UpdateSliderValue(numberOfPressedSpaceToGetUp2);
                gameManager.reviveBar.interactable = false;
                gameManager.reviveBar.enabled = false;
                gameManager.reviveBar.gameObject.SetActive(false);

                healthSlider.value = currentHealth / maxHealth;

                audioManager.RingBellStart();
                audienceBehavier.WhichAnimation(5);
            }
            else if (spacebarCount < numberOfPressedSpaceToGetUp2 && Time.realtimeSinceStartup - startTime > reviveTime)
            {
                gameManager.LostByThirdKO();
            }
        }
        else
        {
            gameManager.reviveBar.interactable = false;
            gameManager.reviveBar.enabled = false;
            gameManager.reviveBar.gameObject.SetActive(false);
        }
    }
}
