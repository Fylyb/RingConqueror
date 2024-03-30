using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static Combat;

public class AIcollision : MonoBehaviour
{
    private bool _hitRegistered = false; // Nov� prom�nn� pro sledov�n�, zda se �der ji� zaregistroval
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
                            Debug.Log("T�lo");
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
                    //zahraje zvuk bloknut�
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
                    //zahraje zvuk bloknut�
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
            // Resetov�n� hitRegistered na false, aby se mohl znovu registrovat �der
            _hitRegistered = false;
        }
        else if (other.gameObject.tag == "PlayerHead")
        {
            //print("EXIT");
            // Resetov�n� hitRegistered na false, aby se mohl znovu registrovat �der
            _hitRegistered = false;
        }
    }
}
