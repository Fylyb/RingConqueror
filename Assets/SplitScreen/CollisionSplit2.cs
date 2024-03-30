using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static CombatSplit2;

public class CollisionSplit2 : MonoBehaviour
{
    private GameManager2 _gameManager;
    public bool hitRegistered = false; // Nová promìnná pro sledování, zda se úder již zaregistroval
    public CombatSplit2 _combatScript; //combat skript který je v základu správnì pro hráèe 2 - odkazuje na CombatSplit2
    public CombatSplit1 aiCombat; //combat skript, odkazuje na hráèe1 - odkazuje na CombatSplit1
    public PlayerHealthSplit1 aiHealth; //playerhealth skript, odkazuje na hráèe 1 - odkazuje na PlayerHealthSplit1
    public AudioManager audioManager;

    public int hitPlayer = 0;

    private void Start()
    {
        _combatScript = GetComponent<CombatSplit2>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerTorso" && !hitRegistered && _combatScript.currentAttack != AttackType.None)
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
        else if (other.gameObject.tag == "PlayerHead" && !hitRegistered && _combatScript.currentAttack != AttackType.None)
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
        if (other.gameObject.tag == "PlayerTorso")
        {
            _combatScript.currentAttack = AttackType.None;
            print("EXIT, torso");
            // Resetování hitRegistered na false, aby se mohl znovu registrovat úder
            hitRegistered = false;
        }
        else if (other.gameObject.tag == "PlayerHead")
        {
            _combatScript.currentAttack = AttackType.None;
            print("EXIT, hlava");
            // Resetování hitRegistered na false, aby se mohl znovu registrovat úder
            hitRegistered = false;
        }
    }
}
