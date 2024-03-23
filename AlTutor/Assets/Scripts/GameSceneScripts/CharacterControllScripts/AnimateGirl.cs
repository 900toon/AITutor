using System.IO;
using UnityEngine;

public class AnimateGirl : MonoBehaviour
{
    [SerializeField] private float MovementSpeedBoost = 3f;
    public Animator animator { get; set; }
    public AudioSource audioSource;
    public float wanderingSpeed { get; set; }
    public int currentAnimationMovementIndex { get; set; }
    public int WALKSPEEDFLOAT_HASH { get; set; }
    public void LookTowardPlayer()
    {
        var pos = GameObject.Find("Player").transform.position;
        Vector3 newVector = pos - transform.position;
        transform.forward = newVector;
    }
    public void WalkTowardPosition(Vector3 targerPosition)
    {
        Vector3 newVector = targerPosition - transform.position;
        transform.forward = newVector;
        transform.position += wanderingSpeed * MovementSpeedBoost * Time.deltaTime * newVector.normalized;

    }

    private string[] animationArray = new string[3] { "Hit01Trigger", "JumpTrigger", "WinposeTrigger" };
    public void HandleCharacterAnimation()
    {
        string animationMovementSelect = "";
        try
        {
            animationMovementSelect = FileFetcher.GetResponseTxtContent().Substring(0, 1);
        }
        catch { }
        if (currentAnimationMovementIndex != FileFetcher.GetCurrentFileIndex())
        {
            if (animationMovementSelect == "1") animator.SetTrigger(animationArray[0]);
            else if (animationMovementSelect == "2") animator.SetTrigger(animationArray[1]);
            else if (animationMovementSelect == "3") animator.SetTrigger(animationArray[2]);
            else animator.SetTrigger(animationArray[Random.Range(0, 2)]);
            LookTowardPlayer();
            currentAnimationMovementIndex = FileFetcher.GetCurrentFileIndex();
        }
    }

    //wandering function does not complete in interface
    int audioFileIndex = -1;
    int currentAudioFileIndex = -1;

    float walkingTimerCount = 0.0f;
    int walkingTimer = 10;
    Vector3 targetPosition;
    [SerializeField] private float acceleration = 0.1f;
    public void HandleCharacterWandering()
    {
        //when the character is going to talk
        if (audioFileIndex != currentAudioFileIndex)
        {
            currentAudioFileIndex = audioFileIndex;

            walkingTimerCount = -20.0f;
            LookTowardPlayer();
        }
        //set condition for character wandering while not talking
        if (walkingTimerCount > walkingTimer)
        {
            walkingTimerCount = walkingTimer;
            targetPosition = new Vector3(Random.Range(-24f, 24f), 0, Random.Range(-24f, 24f));
            Debug.Log(targetPosition);

        }
        else if (walkingTimerCount == walkingTimer)
        {
            float distance = Vector3.Distance(transform.position, targetPosition);
            if (distance <= 2)
            {
                walkingTimerCount = 0.0f;
            }
            else
            {
                wanderingSpeed += acceleration * Time.deltaTime;
                WalkTowardPosition(targetPosition);
            }
        }
        else
        {
            walkingTimerCount += Time.deltaTime;
            wanderingSpeed -= acceleration * Time.deltaTime * 10 * MovementSpeedBoost;

        }
        if (wanderingSpeed >= 1) wanderingSpeed = 1.0f;
        if (wanderingSpeed <= 0) wanderingSpeed = 0.0f;
        animator.SetFloat(WALKSPEEDFLOAT_HASH, wanderingSpeed);
    }

    //wandering function is undone

    public void HandleAudio()
    {
        string directoryPath = GameSettings.GetResponseSoundDirectory();

        if (Directory.Exists(directoryPath))
        {
            string[] files = Directory.GetFiles(directoryPath, "*.wav");
            if (files.Length > 0)
            {
                string file = files[0];
                audioSource.clip = ReadWav.ReadWavToAudioClip(file);

                audioFileIndex = file[^5];


                File.Delete(file);
                audioSource.Play();
            }
        }
    }
    public void Initialization()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentAnimationMovementIndex = 0;
        wanderingSpeed = 0.0f;
        WALKSPEEDFLOAT_HASH = Animator.StringToHash("WalkSpeedFloat");
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCharacterAnimation();
        HandleAudio();
        HandleCharacterWandering();
    }
}
