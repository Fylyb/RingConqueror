using System.Collections;
using UnityEngine;
using UnityEditor;

public class AIalgorithm : MonoBehaviour
{
    public AImovement aiMovement;
    public AIcombat aiCombat;
    public Combat playerCombat;
    public Collision playerCollision;

    public Transform player;

    public float attackDistance = 2f;

    public int lowerChanceThanForMoveAway;
    public int higherChanceThanForBlocking;

    public float maxSecForAttack;

    public int maxAttackCount;

    public int randomAttackCount;

    private bool _hasAttacked = false;
    private bool _isMovingAway = false;

    void Start()
    {
        aiMovement = GetComponent<AImovement>();
        aiCombat = GetComponent<AIcombat>();

        PlayerInputEvents.OnPlayerInput += HandlePlayerInput;

        
    }

    void Update()
    {
        DecideAction();
    }
    
    void DecideAction()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackDistance/* && !hasAttacked*/)
        {
            
            if (randomAttackCount == 0)
            {
                randomAttackCount = Random.Range(1, maxAttackCount); //bylo 4
            }

            HandlePlayerInput();

            //int randomAttackCount = Random.Range(2, 7);
            Debug.Log(randomAttackCount);
            //moûn· d·t random pokolika ˙derech se st·hne t¯eba od 1 do 6 a musÌ to b˝t celÈ ËÌsla, aby mi to neh·zelo 2.1 t¯eba
            if (aiCombat.aiAttackCount == randomAttackCount)
            {         
                StartCoroutine(MoveAwayForSeconds(1f));
                aiCombat.aiAttackCount = 0;

            }
        }
    }
    // Metoda pro obsluhu hr·Ëova vstupu
    void HandlePlayerInput()
    {
        bool isButtonPressed = PlayerInputEvents.IsAnyButtonPressed();

        if (isButtonPressed)
        {
            Debug.Log("Hr·Ë stiskl nÏjakÈ tlaËÌtko. Zde m˘ûete provÈst odpovÌdajÌcÌ akce.");

            float randomValue = Random.Range(0f, 100f);

            if (randomValue <= lowerChanceThanForMoveAway) //bylo 10
            {
                // P¯Ìklad: Spusù pohyb dozadu na AI po hr·ËovÏ stisku tlaËÌtka
                StartCoroutine(MoveAwayForSeconds(0.3f));
            }
            else if(randomValue >= higherChanceThanForBlocking) //bylo 90
            {
                aiCombat.Blocking();
            }

            // P¯Ìklad: Spusù jinÈ akce na z·kladÏ hr·Ëova vstupu
        }
        else
        {
            if (!_hasAttacked) // P¯id·na podmÌnka, aby se zv˝öenÌ neopakovalo, pokud uû AI ˙toËÌ
            {
                StartCoroutine(AttackCooldown());
                aiCombat.aiAttackCount++;
                _hasAttacked = true; // Nastaveno na true, aby se dalöÌ ˙tok nezv˝öil ihned
                Debug.Log("AttackCount: " + aiCombat.aiAttackCount);
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        _hasAttacked = true;

        float randomValue = Random.Range(1f, maxSecForAttack); //bylo 4f
        //Debug.Log(randomValue);

        yield return new WaitForSeconds(0f);

        aiCombat.RandomAttack();
        

        yield return new WaitForSeconds(randomValue);

        _hasAttacked = false;
        
    }

    IEnumerator MoveAwayForSeconds(float seconds)
    {
        if (_isMovingAway)
        {
            yield break;
        }

        _isMovingAway = true;

        // Zapni pohyb dozadu
        aiMovement.StartMovingAway();

        float timer = 0f;
        randomAttackCount = 0;
        while (timer < seconds)
        {
            // AI pohybujÌcÌ se dozadu
            timer += Time.deltaTime;
            yield return null;
        }

        // Vypni pohyb dozadu po uplynutÌ Ëasu
        aiMovement.StopMovingAway();
        _isMovingAway = false;
    }
}
