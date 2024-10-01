
using UnityEngine;
using System.IO;

public class PinkShirtGuy : MonoBehaviour, ICharacterMovement
{
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
        float speedBootingConstant = 3.0f;
        Vector3 newVector = targerPosition - transform.position;
        transform.forward = newVector;
        transform.position += wanderingSpeed * Time.deltaTime * newVector.normalized * speedBootingConstant;
        animator.SetBool("isWalking", true);
    }

    private string[] animationArray = new string[1] {"GreatTrigger"};
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
            animator.SetTrigger(animationArray[0]);
            LookTowardPlayer();
            currentAnimationMovementIndex = FileFetcher.GetCurrentFileIndex();
        }
    }

    //wandering function does not complete in interface
    //'longestWalkingTime' and 'currentWalkingTime' trace for the longest time character walk per time

    //'walkingTimerCount' and 'walkingTimer' is the time interval between two wandering of character 
    int audioFileIndex = -1;
    int currentAudioFileIndex = -1;

    float walkingTimerCount = 0.0f;
    int walkingTimer = 25;

    private float currentWalkingTime = 0.0f;
    Vector3 targetPosition;
    [SerializeField] private float longestWalkingTime = 20f;
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
            //two condition for stop wandering :
            //1. distance between character and the destination is less then 2
            //2. character walking for too long that the time exceed the settings (maybe character is being blocked)
            if (distance <= 2 || currentWalkingTime > longestWalkingTime)
            {
                walkingTimerCount = 0.0f;
                currentWalkingTime = 0.0f;
                animator.SetBool("isWalking", false);
            }
            else
            {
                //keep tracking how's the wandering goes
                float wanderingSpeedBoost = 0.5f;
                wanderingSpeed += acceleration * Time.deltaTime * wanderingSpeedBoost;
                currentWalkingTime += Time.deltaTime;
                WalkTowardPosition(targetPosition);
            }
        }
        else
        {
            //timer for the next wandering term
            //decrease the speed if the character is not walking
            walkingTimerCount += Time.deltaTime;
            wanderingSpeed -= acceleration * Time.deltaTime * 10;

        }
        //limit the speed and set the animator
        if (wanderingSpeed >= 1) wanderingSpeed = 1.0f;
        if (wanderingSpeed <= 0) wanderingSpeed = 0.0f;
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
        //temp force to stay function
        if (GameSettings.NPCForceToStay) ForceToStay();
    }

    //extended function for fliming video
    void ForceToStay()
    {
        wanderingSpeed = 0;
        transform.position = new Vector3(0, 0, -16);
        walkingTimerCount = -100;
        transform.GetComponent<Rigidbody>().isKinematic = true;
    }
}
