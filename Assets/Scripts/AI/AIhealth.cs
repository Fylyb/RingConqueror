using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class AIhealth : MonoBehaviour
{
    public Collision collisionScript;

    public Animator animator; // Animator komponenta hr·Ëe
    public AImovement aiMovement;
    public GameManager gameManager;
    public Slider aiHealthSlider;
    public Collision collision;
    public AudienceBehavier audienceBehavier;
    public AudioManager audioManager;

    public int aiGetkoCount;
    public bool kO;

    public int chanceGettingUpFirstKO;
    public int chanceGettingUpSecondKO;

    public float maxHealth = 100f;

    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        collisionScript = GetComponent<Collision>();
        aiMovement = GetComponent<AImovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public float HealOpponent()
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
        //print("AI Health: " + _currentHealth);

        aiHealthSlider.value = currentHealth / maxHealth;

        if (currentHealth <= 0 && aiGetkoCount == 0)
        {
            aiGetkoCount++;
            Die();
            currentHealth = Mathf.CeilToInt(maxHealth * 0.75f);//75% ze z·kladu
            Debug.Log(currentHealth);
        }
        else if (currentHealth <= 0 && aiGetkoCount == 1)
        {
            aiGetkoCount++;
            Die();
            currentHealth = Mathf.CeilToInt(maxHealth * 0.5f);//50% ze z·kladu
            Debug.Log(currentHealth);
        }
        else if (currentHealth <= 0 && aiGetkoCount == 2)
        {
            aiGetkoCount++;
            Die();
            currentHealth = Mathf.CeilToInt(maxHealth * 0.25f);//25% ze z·kladu
            Debug.Log(currentHealth);
        }
    }

    private void Die()
    {
        audienceBehavier.WhichAnimation(3);
        collision.hitPlayer += 10;
        print("AI KO");
        StartCoroutine(koTime(4.4f, 7f));
    }

    private IEnumerator koTime(float koSeconds, float gettingUpSeconds)
    {
        Debug.Log("zacatek pocitani");
        kO = true;
        animator.Play("Knocked Out");
        yield return new WaitForSeconds(koSeconds);

        int randomNumber = Random.Range(0, 100);
        if (randomNumber < chanceGettingUpFirstKO && aiGetkoCount == 1)//p¯edtÌm bylo 75
        {
            animator.Play("GettingUp");
            yield return new WaitForSeconds(gettingUpSeconds);
            kO = false;
            Debug.Log("konec pocitani");
            aiHealthSlider.value = currentHealth / maxHealth;
            audioManager.RingBellStart();
            audienceBehavier.WhichAnimation(4);
        }
        else if (randomNumber < chanceGettingUpSecondKO && aiGetkoCount == 2)//p¯edtÌm bylo 50
        {
            animator.Play("GettingUp");
            yield return new WaitForSeconds(gettingUpSeconds);
            kO = false;
            Debug.Log("konec pocitani");
            aiHealthSlider.value = currentHealth / maxHealth;
            audioManager.RingBellStart();
            audienceBehavier.WhichAnimation(5);
        }
        else
        {
            gameManager.WonByThirdKO();
        }
    }
}
