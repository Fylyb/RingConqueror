using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static CombatSplit1;

public class CollisionSplit1 : MonoBehaviour
{
    private GameManager2 _gameManager;// dát gamemanager2
    public bool hitRegistered = false; // Nová promìnná pro sledování, zda se úder již zaregistroval
    public CombatSplit1 _combatScript; //combat skript který je v základu správnì pro hráèe 1 - odkazuje na CombatSplit1
    public CombatSplit2 aiCombat; //combat skript, odkazuje na hráèe2 - odkazuje na CombatSplit2
    public PlayerHealthSplit2 aiHealth; //playerhealth skript, odkazuje na hráèe 2 - odkazuje na PlayerHealthSplit2
    public AudioManager audioManager;

    public int hitPlayer = 0;

    private void Start()
    {
        _combatScript = GetComponent<CombatSplit1>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AItorso" && !hitRegistered && _combatScript.currentAttack != AttackType.None)
        {
            if (aiCombat.isBlocking == false || aiHealth.kO == false)
            {
                if (aiHealth != null && _combatScript.currentAttack != AttackType.None || _combatScript.currentAttack != AttackType.Blocking)
                {
                    int damage = 0;

                    switch (_combatScript.currentAttack)
                    {
                        case AttackType.LeftHook:
                            damage = 30;//15
                            break;
                        case AttackType.JabBody:
                            damage = 10;//10
                            break;
                        case AttackType.CrossBody:
                            damage = 15;//10
                            break;
                    }
                    aiHealth.TakeDamage(damage);
                    audioManager.SoundWhenPunch();

                    //Debug.Log($"Player dal {hitPlayer} zasahu");
                    hitPlayer++;
                    hitRegistered = true;
                }
            }
            else if (aiCombat.isBlocking == true)
            {
                audioManager.BlockSound();
            }
        }
        else if (other.gameObject.tag == "AIhead" && !hitRegistered && _combatScript.currentAttack != AttackType.None)
        {
            if (aiCombat.isBlocking == false || aiHealth.kO == false)
            {
                if (aiHealth != null && _combatScript.currentAttack != AttackType.None || _combatScript.currentAttack != AttackType.Blocking)
                {
                    int damage = 0;

                    switch (_combatScript.currentAttack)
                    {
                        case AttackType.JabHead:
                            damage = 15;//20
                            break;
                        case AttackType.LeftHook:
                            damage = 30;//25
                            break;
                        case AttackType.CrossHead:
                            damage = 20;//20
                            break;
                    }
                    aiHealth.TakeDamage(damage);
                    audioManager.SoundWhenPunch();

                    //Debug.Log($"Player dal {hitPlayer} zasahu");
                    hitPlayer++;
                    hitRegistered = true;
                }
            }
            else if (aiCombat.isBlocking == true)
            {
                audioManager.BlockSound();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AItorso")
        {
            _combatScript.currentAttack = AttackType.None;
            print("EXIT, torso");
            // Resetování hitRegistered na false, aby se mohl znovu registrovat úder
            hitRegistered = false;
        }
        else if (other.gameObject.tag == "AIhead")
        {
            _combatScript.currentAttack = AttackType.None;
            print("EXIT, hlava");
            // Resetování hitRegistered na false, aby se mohl znovu registrovat úder
            hitRegistered = false;
        }
    }
}
