using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSplit2 : MonoBehaviour
{
    public GameObject characterOBJ;
    Animator controllerANIM;

    public StaminaManager staminaManager; // Pøídáme referenci na StaminaManager

    public PlayerHealthSplit2 playerHealth; // bude odkazovat na PlayerHealthSplit2
    public PlayerHealthSplit1 aiHealth; // bude odkazovat na PlayerHealthSplit1

    public GameManager2 gameManager;

    public bool isBlocking = false;
    public bool isAttackAnimationPlayerng = false;

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
        staminaManager = GetComponent<StaminaManager>();
        playerHealth = GetComponent<PlayerHealthSplit2>();
        //aiHealth = GetComponent<AIhealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // Kontrola, zda animace JabHead, JabBody, CrossBody, CrossHead, LeftHook nebo Upper bìží
        bool isAttackAnimationPlaying = controllerANIM.GetCurrentAnimatorStateInfo(0).IsName("JabHead") ||
                                        controllerANIM.GetCurrentAnimatorStateInfo(0).IsName("JabBody") ||
                                        controllerANIM.GetCurrentAnimatorStateInfo(0).IsName("CrossBody") ||
                                        controllerANIM.GetCurrentAnimatorStateInfo(0).IsName("CrossHead") ||
                                        controllerANIM.GetCurrentAnimatorStateInfo(0).IsName("LeftHook") ||
                                        controllerANIM.GetCurrentAnimatorStateInfo(0).IsName("Upper");

        Debug.Log("more: " + isAttackAnimationPlaying);
        if (gameManager.gamePaused != true)
        {
            if (playerHealth.kO == false && aiHealth.kO == false && isAttackAnimationPlaying == false)
            {
                if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    if (staminaManager.UseStamina(20f))
                        Attack1();

                }
                if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    if (staminaManager.UseStamina(20f))
                        LeftHook();
                }
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    StartBlocking();
                }
                //if (Input.GetKeyDown(KeyCode.C))
                //{
                //    if (staminaManager.UseStamina(30f))
                //        Upper();
                //}
                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    if (staminaManager.UseStamina(10f))
                        JabBody();
                }
                if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    if (staminaManager.UseStamina(10f))
                        CrossBody();
                }
                if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    if (staminaManager.UseStamina(20f))
                        CrossHead();
                }
            }
        }
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
        staminaManager.GetCurrentStamina();
        currentAttack = AttackType.JabHead;
        controllerANIM.SetTrigger("JabHead");
    }
    public void CrossBody()
    {
        staminaManager.GetCurrentStamina();
        currentAttack = AttackType.CrossBody;
        controllerANIM.SetTrigger("CrossBody");
    }
    public void CrossHead()
    {
        staminaManager.GetCurrentStamina();
        currentAttack = AttackType.CrossHead;
        controllerANIM.SetTrigger("CrossHead");
    }
    public void JabBody()
    {
        staminaManager.GetCurrentStamina();
        currentAttack = AttackType.JabBody;
        controllerANIM.SetTrigger("JabBody");
    }
    public void Blocking()
    {
        currentAttack = AttackType.Blocking;
        controllerANIM.SetTrigger("Blocking");
    }
    public void LeftHook()
    {
        staminaManager.GetCurrentStamina();
        currentAttack = AttackType.LeftHook;
        controllerANIM.SetTrigger("LeftHook");
    }
    public void Upper()
    {
        staminaManager.GetCurrentStamina();
        currentAttack = AttackType.Upper;
        controllerANIM.SetTrigger("Upper");
    }
}
