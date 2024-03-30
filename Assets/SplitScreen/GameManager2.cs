using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
//using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEditor;

public class GameManager2 : MonoBehaviour
{
    public GameObject pauseMenu;

    private float _playerStartStamina;
    private float _aiStartStamina;

    public CollisionSplit1 collisionPlayerScript; // nastav� se CollisionSplit1
    public CollisionSplit2 collisionAIScript; // nastav� se CollisionSplit2

    public PlayerHealthSplit1 playerHealth; // nastav� se PlayerHealthSplit1
    public GameObject playerHitbox;

    public GameObject respawnPointPlayer;
    public GameObject respawnPointEnemy;

    public PlayerHealthSplit2 aiHealth; // nastav� se PlayerHealth2
    public GameObject opponentHitbox;
    public GameObject opponentHitboxHead;

    public AudioManager audioManager;

    public Animator animator;
    public Animator animatorPlayer;

    //public Transform player;

    public float rotationSpeed = 5f;

    private int _playerScore = 0;
    private int _aiScore = 0;

    public PauseMenu pauseMenuScript;

    public StaminaManager playerStaminaManager; // P�eta�en� va�eho StaminaManageru pro hr��e
    public StaminaManager aiOponentStaminaManager; // P�eta�en� va�eho StaminaManageru pro AI Oponenta

    public TextMeshProUGUI playerScoreText; // Text pro zobrazen� sk�re hr��e
    public TextMeshProUGUI aiScoreText; // Text pro zobrazen� sk�re AI Oponenta

    public GameObject playerCharacter; // Drag & drop hr��e do Unity Inspectoru
    public GameObject aiOponent; // Drag & drop UI AI Oponenta
    public TextMeshProUGUI roundText; // Drag & drop UI textov�ho prvku pro zobrazen� kola
    public TextMeshProUGUI timerText; // Drag & drop UI textov�ho prvku pro zobrazen� �asu
    public TextMeshProUGUI countdownText;
    public Button nextRoundButton; // Drag & drop UI tla��tka pro dal�� kolo
    public Button nextLevelButton;
    public Button resetLevel;
    public GameObject damageOverlay1;
    public GameObject damageOverlay2;
    public TextMeshProUGUI wonText;
    public TextMeshProUGUI lostText;
    public TextMeshProUGUI drawText;
    public TextMeshProUGUI pressSpaceText;
    public TextMeshProUGUI pressNum0Text;
    public TextMeshProUGUI gameIsPausedText;
    public TextMeshProUGUI playerNeutralCornerText;
    public TextMeshProUGUI opponentNeutralCornerText;
    public TextMeshProUGUI fightText;

    public AudienceBehavier audienceBehavier;

    public ReviveBar reviveBar1;

    public Slider reviveBar;

    public SkinnedMeshRenderer headRenderer;

    //public SplitScreenOption splitScreenOption;

    private float _originalRoundTime; // Nov� prom�nn� pro ukl�d�n� p�vodn� hodnoty _roundTime
    private bool _isTimerPaused = false; // P�id�na prom�nn� pro sledov�n�, zda je �asova� pozastaven

    public bool gamePaused = false;

    private bool _ringBellStarted = false;

    private bool _lastRound;

    private int _currentRound = 1;
    private int _totalRounds = StaticData.valueOfRoundsToKeep;
    private float _roundTime = StaticData.valueOfRoundsToKeep * 60; // 2 minuty  20f = 20 sekund

    public float currentTimeCountdown = 0f;
    public float countdown = 10f;

    private enum GameState
    {
        RoundStart,
        RoundInProgress,
        RoundEnd
    }
    private GameState currentState;

    [SerializeField] TextMeshProUGUI countdownTextSerialize;

    // Start is called before the first frame update
    void Start()
    {
        

        //// Z�sk�n� hodnot z statick�ch prom�nn�ch ve SplitScreenOption
        //int selectedRounds = SplitScreenOption.SelectedRounds;
        //float selectedMinutes = SplitScreenOption.SelectedMinutes;

        //// Vol�n� metody SetGameParameters s hodnotami
        //SetGameParameters(selectedRounds, selectedMinutes);

        //_totalRounds = splitScreenOption._selectedRounds;
        //_roundTime = splitScreenOption._selectedMinutes * 60;

        //DOD�LAT HITBOX NA HLAVU A �IVOTY Z HLAVY A TORSA JAKO JEDNO
        playerHitbox = GameObject.FindWithTag("Player");
        playerHealth = playerHitbox.GetComponent<PlayerHealthSplit1>();

        opponentHitbox = GameObject.FindWithTag("AI");
        //opponentHitboxHead = GameObject.FindWithTag("AIhead");
        aiHealth = opponentHitbox.GetComponent<PlayerHealthSplit2>();

        _playerStartStamina = playerStaminaManager.maxStamina;
        _aiStartStamina = aiOponentStaminaManager.maxStamina;



        //MUS� B�T ZAKOMENTOVAN� JINAK P�I SPU�T�N�, SKRIPTY Z GAMEMANAGERU, COLLIONPLAYERSCRIPT A COLLISIONAISCRIPT ZM�Z� A MUS� SE VE H�E NAHODIT MANU�LN�
        //collisionPlayerScript = GetComponent<Collision>();
        //collisionAIScript = GetComponent<AIcollision>();

        currentState = GameState.RoundStart;
        UpdateRoundText();
        UpdateTimerUI();

        nextRoundButton.interactable = false;
        nextRoundButton.gameObject.SetActive(false);

        nextLevelButton.interactable = false;
        nextLevelButton.gameObject.SetActive(false);

        resetLevel.interactable = false;
        resetLevel.gameObject.SetActive(false);

        reviveBar.interactable = false;
        reviveBar.enabled = false;
        reviveBar.gameObject.SetActive(false);

        wonText.enabled = false;
        lostText.enabled = false;
        drawText.enabled = false;
        pressSpaceText.enabled = false;
        pressNum0Text.enabled = false;
        gameIsPausedText.enabled = false;

        playerNeutralCornerText.enabled = false;
        opponentNeutralCornerText.enabled = false;

        countdownText.enabled = false;

        currentTimeCountdown = countdown;

        playerHealth.kO = false;
        aiHealth.kO = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.RoundStart:
                // Zde by moblo b�t zpr�va pro hr��e, �e za��n� kolo
                // ...
                audioManager.AudienceSound();
                currentState = GameState.RoundInProgress;
                currentTimeCountdown = countdown;
                break;

            case GameState.RoundInProgress:

                gameIsPaused();

                if (!_ringBellStarted)
                {
                    audioManager.RingBellStart();
                    _ringBellStarted = true;
                }

                audienceBehavier.WhichAnimation(1);
                audienceBehavier.WhichAnimation(6);

                _roundTime -= Time.deltaTime;
                _roundTime = Mathf.Max(_roundTime, 0f); // Zajist�, �e �as nebude pod nulou
                UpdateTimerUI();

                if (_currentRound == _totalRounds)
                {
                    _lastRound = true;
                    Debug.Log("posledni kolo");
                }

                if (aiHealth.playerGetkoCount == 3)
                {
                    Invoke("WonByThirdKO", 4f);
                    playerNeutralCornerText.enabled = false;
                }
                if (playerHealth.playerGetkoCount == 3)
                {
                    Invoke("LostByThirdKO", 4f);
                    opponentNeutralCornerText.enabled = false;
                }

                if (playerHealth.kO == true || aiHealth.kO == true)
                {
                    //countdownText.enabled = true;

                    if (reviveBar1.successfulRevive == false && gamePaused == true)
                    {
                        //if (playerHealth.playerGetkoCount != 3/* && playerHealth.playerGetkoCount == 1*/)
                        //{
                        countdownText.enabled = true;
                        reviveBar.enabled = true;
                        reviveBar.interactable = true;
                        reviveBar.gameObject.SetActive(true);

                        currentTimeCountdown -= 1 * Time.unscaledDeltaTime;
                        currentTimeCountdown = Mathf.Max(currentTimeCountdown, 0); // Ujist�me se, �e currentTimeCountdown nen� men�� ne� 0
                        countdownTextSerialize.text = currentTimeCountdown.ToString("0");
                        //}
                    }
                    if (currentTimeCountdown <= 0)
                    {
                        LostByThirdKO();
                        opponentNeutralCornerText.enabled = false;
                    }

                    headRenderer.enabled = true;
                    //opponentNeutralCornerText.enabled = true;
                    StartCoroutine(AiNeutralCorner());
                }
                else
                {
                    countdownText.enabled = false;
                }

                if (aiHealth.kO == true)
                {
                    PlayerNeutralCornera();
                    //playerNeutralCornerText.enabled = true;
                    //StartCoroutine(PlayerNeutralCorner());
                }

                if(playerHealth.kO == true)
                {
                    AiNeutralCorner();
                }

                if (playerHealth.kO == false && aiHealth.kO == false)
                {
                    playerNeutralCornerText.enabled = false;
                    opponentNeutralCornerText.enabled = false;
                }

                if (_roundTime <= 0)
                {
                    currentState = GameState.RoundEnd;
                    nextRoundButton.gameObject.SetActive(true);
                    nextRoundButton.interactable = true;

                    audioManager.RingBellEnd();

                    damageOverlay1.SetActive(false);
                    damageOverlay2.SetActive(false);

                    HandleRoundResult();

                    // Zde m��ete p�idat k�d na zastaven� hry, nap��klad:
                    Time.timeScale = 0f; // Zastav� b�h hry (�asov� �daj = 0)
                }
                break;

            case GameState.RoundEnd:

                break;
        }
    }
    //public void SetGameParameters(int rounds, float minutes)
    //{
    //    _totalRounds = rounds;
    //    _roundTime = minutes * 60;
    //    _originalRoundTime = _roundTime; // Aktualizace p�vodn� hodnoty _roundTime

    //    UpdateRoundText();
    //    UpdateTimerUI();
    //}
    IEnumerator AiNeutralCorner()
    {
        yield return new WaitForSeconds(1.5f);
        animator.Play("MoveAwayFromPlayer");
        Vector3 target = new Vector3(5.27f, 1.03f, 4.99f);
        aiOponent.transform.position = Vector3.MoveTowards(aiOponent.transform.position, target, Time.deltaTime * 2);

        //Vector3 directionToPlayer = (player.position - transform.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        //transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void PlayerNeutralCornera()
    {
        //yield return new WaitForSeconds(1.5f);
        Debug.Log("vola sa");
        Vector3 target1 = new Vector3(-5.34f, 1.03f, -5.57f);
        playerCharacter.transform.position = Vector3.MoveTowards(playerCharacter.transform.position, target1, Time.deltaTime * 2);
        Physics.SyncTransforms();
    }

    public void WonByThirdKO()
    {
        Time.timeScale = 0f;
        wonText.enabled = true;
        nextLevelButton.interactable = true;
        nextLevelButton.gameObject.SetActive(true);
        audioManager.RingBellEnd();
    }
    public void LostByThirdKO()
    {
        Time.timeScale = 0f;
        lostText.enabled = true;
        resetLevel.interactable = true;
        resetLevel.gameObject.SetActive(true);
        audioManager.RingBellEnd();
    }
    public void Draw()
    {
        Time.timeScale = 0f;
        drawText.enabled = true;
        audioManager.RingBellEnd();
    }

    void UpdateScore()
    {
        playerScoreText.text = _playerScore.ToString();
        aiScoreText.text = _aiScore.ToString();
    }

    void UpdateRoundText()
    {
        roundText.text = $"Round {_currentRound} / {_totalRounds}";
    }

    void UpdateTimerUI()
    {
        if (aiHealth.kO == true || playerHealth.kO == true)
        {
            if (!_isTimerPaused)
            {
                _isTimerPaused = true; // Nastav�me indik�tor, �e �asova� byl pozastaven

                // Ulo��me p�vodn� hodnotu _roundTime
                _originalRoundTime = _roundTime;

                // Pozastav�me hru na ur�it� �as
                StartCoroutine(PauseGameForSeconds(0.5f));
            }
        }
        else
        {
            // Pokud �asova� nebyl pozastaven, aktualizujeme ho norm�ln�
            if (!_isTimerPaused)
            {
                int minutes = Mathf.FloorToInt(_roundTime / 60);
                int seconds = Mathf.FloorToInt(_roundTime % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
    }

    IEnumerator PauseGameForSeconds(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds); // Po�kej ur�en� �as

        // Po uplynut� �asu obnov�me p�vodn� hodnotu _roundTime a znovu spust�me �asova�
        _isTimerPaused = false;
        _roundTime = _originalRoundTime;
    }

    void HandleRoundResult()
    {
        if (_lastRound)
        {
            //vyhodnocen� na body
            if (_playerScore > _aiScore)
            {
                wonText.enabled = true;
            }
            else if (_playerScore < _aiScore)
            {
                lostText.enabled = true;
            }
            else if (_playerScore == _aiScore)
            {
                drawText.enabled = true;
                resetLevel.interactable = true;
                resetLevel.gameObject.SetActive(true);
            }
        }

        if (collisionPlayerScript.hitPlayer > collisionAIScript.hitPlayer)
            _playerScore++;
        else if (collisionPlayerScript.hitPlayer < collisionAIScript.hitPlayer)
            _aiScore++;

        Debug.Log("player hits " + collisionPlayerScript.hitPlayer);
        Debug.Log("ai hits " + collisionAIScript.hitPlayer);

        // Aktualizuje sk�re po ka�d�m kole
        UpdateScore();
        playerHealth.HealPlayer();
        aiHealth.HealPlayer();
    }

    public void gameIsPaused()
    {
        bool isButtonPressed = PlayerInputEvents.PisPressed();
        bool isEscPressed = PlayerInputEvents.EscPressed();

        if (isButtonPressed && gamePaused == true && playerHealth.kO == false)
        {
            gameIsPausedText.enabled = false;
            Time.timeScale = 1f;
            gamePaused = false;
        }
        else if (isButtonPressed && gamePaused == false && playerHealth.kO == false)
        {
            gameIsPausedText.enabled = true;
            Time.timeScale = 0f;
            gamePaused = true;
        }

        if (isEscPressed && gamePaused == true)
        {
            Time.timeScale = 1f;
            gamePaused = false;
            pauseMenu.SetActive(false);
        }
        else if (isEscPressed && gamePaused == false)
        {
            Time.timeScale = 0f;
            gamePaused = true;
            pauseMenu.SetActive(true);
        }

        if (pauseMenuScript.resumePressed == true)
        {
            Time.timeScale = 1f;
            gamePaused = false;
        }
    }

    public void NextRound()
    {
        if (_currentRound < _totalRounds)
        {
            currentState = GameState.RoundStart;

            aiHealth.healthSlider.value = aiHealth.currentHealth / aiHealth.maxHealth;
            playerHealth.healthSlider.value = playerHealth.currentHealth / playerHealth.maxHealth;

            reviveBar1.successfulRevive = false;

            collisionPlayerScript.hitPlayer = 0;
            collisionAIScript.hitPlayer = 0;

            Respawn();

            playerHealth.kO = false;
            aiHealth.kO = false;

            _ringBellStarted = false;

            _currentRound++;
            UpdateRoundText();

            aiHealth.playerGetkoCount = 0;
            playerHealth.playerGetkoCount = 0;
            playerHealth.spacebarCount = 0;

            Time.timeScale = 1f; // Nastav� norm�ln� rychlost b�hu hry (�asov� �daj = 1)

            // Resetuje �as a nastavuje re�im na RoundStart
            _roundTime = StaticData.valueOfMinutesToKeep * 60;
            currentState = GameState.RoundStart; // Nastavte stav zp�t na RoundStart
            nextRoundButton.interactable = false;
            nextRoundButton.gameObject.SetActive(false);


            damageOverlay1.SetActive(true);
            damageOverlay2.SetActive(true);

            // Resetov�n� stamin hr��e a AI Oponenta
            playerStaminaManager.ResetStamina();


            //aiOponentStaminaManager.ResetStamina();
        }
        else
        {
            // Dos�hli jste maxim�ln�ho po�tu kol


        }
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("GameMenu");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    // Tato metoda se zavol�, kdy� sc�na je �sp�n� na�tena
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Nastav� Time.timeScale na 1f po na�ten� sc�ny
        Time.timeScale = 1f;
    }

    public void Respawn()
    {
        playerCharacter.transform.position = respawnPointPlayer.transform.position;
        aiOponent.transform.position = respawnPointEnemy.transform.position;
        //mus� to tu b�t napsan�, proto�e od verze 2018.n�co to nen� automatick�, tudi� to mus� b�t napsan� manu�ln�
        Physics.SyncTransforms();
    }

    public void PauseGame()
    {
        gamePaused = true;
    }
    public void ResumeGame()
    {
        gamePaused = false;
    }
}
