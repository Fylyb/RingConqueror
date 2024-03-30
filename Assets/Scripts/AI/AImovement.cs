using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;

public class AImovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    public float attackDistance = 2f;
    public float stopDistance = 2f;

    public float strafeDistance = 5f;

    public int lowerThanForLeftStep;
    public int higherThanForRightStep;

    private bool _isMovingAway = false;

    private bool _isRotating = false;

    private bool isRandomNumberAndMoveInProgress = false;


    private bool sideL = false;
    private bool sideR = false;

    private Transform _player;
    private CharacterController _characterController;
    private Animator _animator;
    private Rigidbody _rigidbody;

    public AIhealth aiHealth;
    public PlayerHealth playerHealth;
    private RigidbodyConstraints _originalConstraints;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        aiHealth = GetComponent<AIhealth>();
        //playerHealth = GetComponent<PlayerHealth>();

        _originalConstraints = _rigidbody.constraints;
    }

    void Update()
    {
        if (playerHealth.kO == false)
        {
            if (_isMovingAway)
            {
                MoveAwayFromPlayer();
            }
            else if (aiHealth.kO == true)
            {
                StartCoroutine(FreezePosition(5f));
            }
            else if (sideL == true)
            {
                LeftMovement();
                StartCoroutine(SideFalseAfterSeconds(1f));
            }
            else if (sideR == true)
            {
                RightMovement();
                StartCoroutine(SideFalseAfterSeconds(1f));
            }
            else
            {
                StartCoroutine(ChangeOfSideStep());
                MoveTowardsPlayer();
            }
        }
    }

    private IEnumerator SideFalseAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        sideL = false;
        sideR = false;
    }

    private IEnumerator ChangeOfSideStep()
    {
        if (isRandomNumberAndMoveInProgress)
        {
            yield break;
        }

        isRandomNumberAndMoveInProgress = true;

        // P�ed vygenerov�n�m nov�ho n�hodn�ho ��sla vy�kejte n�jak� �as (nap�. 5 sekund)
        yield return new WaitForSeconds(1f);

        int randomValue = Random.Range(0, 100);
        Debug.Log(randomValue);

        // Zde m��ete p�idat va�e podm�nky nebo akce na z�klad� n�hodn� hodnoty
        if (randomValue > higherThanForRightStep) //bylo 90
        {
            sideR = true;
        }
        else if (randomValue < lowerThanForLeftStep) //bylo 10
        {
            sideL = true;
        }
        // Po�kejte n�jak� dal�� �as (nap�. 5 sekund)
        yield return new WaitForSeconds(1f);

        isRandomNumberAndMoveInProgress = false;
    }

    public void MoveTowardsPlayer()
    {
        _animator.SetTrigger("StepForward");

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        if (distanceToPlayer > attackDistance)
        {
            Vector3 targetPosition = _player.position;
            targetPosition.y = transform.position.y;

            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            _characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            //StartCoroutine(WaitForSeconds(1f));
        }

    }

    public void MoveAwayFromPlayer()
    {
        _animator.SetTrigger("StepBackward");

        Vector3 directionToPlayer = (_player.position - transform.position).normalized;
        Vector3 moveDirection = -directionToPlayer; // Inverze sm�ru k hr��i pro couv�n�

        Vector3 targetPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        targetPosition.y = transform.position.y;

        Collider ringCollider = GameObject.FindGameObjectWithTag("BoxingRing").GetComponent<Collider>();
        if (ringCollider.bounds.Contains(targetPosition))
        {
            // Pohyb AI Opponenta sm�rem od hr��e
            _characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            // Nasm�rov�n� AI Opponenta sm�rem k hr��i (nez�visl� na sm�ru pohybu)
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            _isMovingAway = false;
        }
    }

    private void LeftMovement()
    {
        _animator.SetTrigger("LeftStep");
        MoveSideways(-1);
    }

    private void RightMovement()
    {
        _animator.SetTrigger("RightStep");
        MoveSideways(1);
    }

    private void MoveSideways(int direction)
    {
        // Z�sk�n� sm�ru od AI Oponenta ke hr��i
        Vector3 directionToPlayer = (_player.position - transform.position).normalized;

        // Kolm� sm�r pro pohyb do stran
        Vector3 strafeDirection = new Vector3(directionToPlayer.z, 0, -directionToPlayer.x).normalized;

        // V�po�et c�lov� pozice pro pohyb do stran
        Vector3 targetPosition = transform.position + strafeDirection * strafeDistance * Time.deltaTime;

        // Kontrola, zda AI Opponent nevyjde ze h�i�t�
        Collider ringCollider = GameObject.FindGameObjectWithTag("BoxingRing").GetComponent<Collider>();
        if (ringCollider.bounds.Contains(targetPosition))
        {
            // Pohyb do stran pomoc� CharacterController
            _characterController.Move(strafeDirection * moveSpeed * direction * Time.deltaTime);

            // Oto�en� AI sm�rem k hr��i
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            _isRotating = false;
            // AI Opponent by vy�el ze h�i�t�, tak�e p�estane prov�d�t pohyb
            // M��ete zde p�idat dal�� logiku nebo zavolat jin� metody podle pot�eby
        }
    }

    public IEnumerator FreezePosition(float seconds)
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        Debug.Log("zacatek");
        yield return new WaitForSeconds(seconds);
        Debug.Log("konec");
        _rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BoxingRing")
        {
            Debug.Log("boxingring");
        }
    }

    public void StartMovingAway()
    {
        _isMovingAway = true;
    }

    public void StopMovingAway()
    {
        _isMovingAway = false;
    }
}
