using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AIcombat : MonoBehaviour
{
    public GameObject characterOBJ;
    Animator controllerANIM;

    public AIcollision aIcollision;

    public bool isBlocking = false;

    public int aiAttackCount;

    public int damageJabHead;
    public int damageJabBody;
    public int damageCrossHead;
    public int damageCrossBody;
    public int damageLeftHook;

    public AIstaminaManager aiStaminaManager; // Pøídáme referenci na StaminaManager

    public enum AttackType
    {
        None,
        JabHead,
        Blocking,
        LeftHook,
        Upper,
        JabBody,
        CrossBody,
        CrossHead
    }

    public AttackType currentAttack = AttackType.None;


    // Start is called before the first frame update
    void Start()
    {
        controllerANIM = characterOBJ.GetComponent<Animator>();
        //staminaManager = GetComponent<StaminaManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void StartBlocking()
    {
        Debug.Log("zacina blocking");
        isBlocking = true;
        Debug.Log($"blocking nastaveny na: {isBlocking}");
        controllerANIM.SetBool("Blocking", isBlocking);

        StartCoroutine(StopBlockingAfterDelay(1.5f));
    }
    IEnumerator StopBlockingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isBlocking = false;
        Debug.Log("konci blocking");
        controllerANIM.SetBool("Blocking", isBlocking);
        Debug.Log($"blocking nastaveny na: {isBlocking}");

    }

    public bool IsBlocking()
    {
        return isBlocking;
    }

    [ContextMenu("Attack 01")]
    public void Attack1()
    {
        aiStaminaManager.GetCurrentStamina();
        currentAttack = AttackType.JabHead;
        aIcollision.damageToHead = damageJabHead;
        controllerANIM.SetTrigger("JabHead");
    }
    public void CrossBody()
    {
        aiStaminaManager.GetCurrentStamina();
        currentAttack = AttackType.CrossBody;
        aIcollision.damageToBody = damageCrossBody;
        controllerANIM.SetTrigger("CrossBody");
    }
    public void CrossHead()
    {
        aiStaminaManager.GetCurrentStamina();
        currentAttack = AttackType.CrossHead;
        aIcollision.damageToHead = damageCrossHead;
        controllerANIM.SetTrigger("CrossHead");
    }
    public void JabBody()
    {
        aiStaminaManager.GetCurrentStamina();
        currentAttack = AttackType.JabBody;
        aIcollision.damageToBody = damageJabBody;
        controllerANIM.SetTrigger("JabBody");
    }
    public void Blocking()
    {
        currentAttack = AttackType.Blocking;
        controllerANIM.SetTrigger("Blocking");
        StartBlocking();
    }
    public void LeftHook()
    {
        aiStaminaManager.GetCurrentStamina();
        currentAttack = AttackType.LeftHook;
        aIcollision.damageToHead = damageLeftHook;
        controllerANIM.SetTrigger("LeftHook");
    }
    //public void Upper()
    //{
    //    aiStaminaManager.GetCurrentStamina();
    //    currentAttack = AttackType.Upper;
    //    controllerANIM.SetTrigger("Upper");
    //}

    public void RandomAttack()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < 15f) // JabHeadProbability
        {
            if (aiStaminaManager.UseStamina(20f))
                Attack1();
        }
        else if (randomValue > 15f && randomValue < 30f) // LeftHookProbability
        {
            if (aiStaminaManager.UseStamina(20f))
                LeftHook();
        }
        else if (randomValue > 30f && randomValue < 55f) // JabBodyProbability
        {
            if (aiStaminaManager.UseStamina(20f))
                CrossHead();
        }
        else if (randomValue > 55f && randomValue < 70f) // CrossBodyProbability
        {
            if (aiStaminaManager.UseStamina(20f))
                CrossBody();
        }
        else if (randomValue > 70f && randomValue < 100f) // CrossHeadProbability
        {
            if (aiStaminaManager.UseStamina(20f))
                JabBody();
        }   
    }
}
