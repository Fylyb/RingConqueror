using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static Combat;

public class AIcollision : MonoBehaviour
{
    private bool _hitRegistered = false; // Nová promìnná pro sledování, zda se úder již zaregistroval
    public AIcombat aiCombat;
    public Combat playerCombat;
    public PlayerHealth playerHealth;
    public AIhealth aiHealth;
    public AudioManager audioManager;

    public int damageToHead;
    public int damageToBody;

    public int hitAI = 0;

    public void Start()
    {
        aiCombat = GetComponent<AIcombat>();
        aiHealth = GetComponent<AIhealth>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (playerHealth.kO == false)
        {
            if (other.gameObject.tag == "PlayerTorso" && !_hitRegistered)
            {
                if (playerCombat.isBlocking != true || playerHealth.kO == false)
                {
                    if (aiCombat != null)
                    {
                        if (aiCombat.IsBlocking() != true)
                        {
                            Debug.Log("Tìlo");
                            //PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

                            if (playerHealth != null)
                            {
                                playerHealth.TakeDamage(damageToBody);
                                audioManager.SoundWhenPunch();
                            }
                            Debug.Log($"AI dal {hitAI} zasah");
                            hitAI++;
                            _hitRegistered = true;
                        }
                    }
                }
                else
                {
                    //zahraje zvuk bloknutí
                    audioManager.BlockSound();
                }
            }
            else if (other.gameObject.tag == "PlayerHead" && !_hitRegistered)
            {
                if (playerCombat.isBlocking != true || playerHealth.kO == false)
                {
                    if (aiCombat != null)
                    {
                        if (aiCombat.IsBlocking() != true)
                        {
                            Debug.Log("Hlava");
                            //PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

                            if (playerHealth != null)
                            {
                                playerHealth.TakeDamage(damageToHead);
                                audioManager.SoundWhenPunch();
                            }
                            Debug.Log($"AI dal {hitAI} zasah");
                            hitAI++;
                            _hitRegistered = true;
                        }
                    }
                }
                else
                {
                    //zahraje zvuk bloknutí
                    audioManager.BlockSound();
                }
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerTorso")
        {
            //print("EXIT");
            // Resetování hitRegistered na false, aby se mohl znovu registrovat úder
            _hitRegistered = false;
        }
        else if (other.gameObject.tag == "PlayerHead")
        {
            //print("EXIT");
            // Resetování hitRegistered na false, aby se mohl znovu registrovat úder
            _hitRegistered = false;
        }
    }
}
