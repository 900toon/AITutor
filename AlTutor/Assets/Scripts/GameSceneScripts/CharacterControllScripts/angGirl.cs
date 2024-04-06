using UnityEngine;
using System.IO;

public class angGirl : MonoBehaviour
{
    public Animator animator { get; set; }
    public AudioSource audioSource;
    public int currentAnimationMovementIndex { get; set; }
    public int EMOTIONSINT_HASH { get; set; }
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
        animator.SetBool("isWalk", true);

    }

    private void StopWalking()
    {
        animator.SetBool("isWalk", false);
        
    }

    [SerializeField] private float characterFacialExpressionTimerLimit = 20.0f;
    float characterFacialExpressionTimer = 0.0f;
    public void HandleCharacterAnimation()
    {
        //keep track of how long the same facial expression showed up
        if (characterFacialExpressionTimer > characterFacialExpressionTimerLimit) 
        {
            animator.SetInteger(EMOTIONSINT_HASH, (Random.Range(0, 6)));
            characterFacialExpressionTimer = 0.0f;
        }
        else
        {
            characterFacialExpressionTimer += Time.deltaTime;

        }

        if (currentAnimationMovementIndex != FileFetcher.GetCurrentFileIndex())
        {   
            animator.SetInteger(EMOTIONSINT_HASH , (Random.Range(0, 6)));
            characterFacialExpressionTimer = 0.0f;
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
    [SerializeField] private float longestWalkingTime = 60f;

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
            targetPosition = new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
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
                StopWalking();
                walkingTimerCount = 0.0f;
                currentWalkingTime = 0.0f;

            }
            else
            {
                //keep tracking how's the wandering goes
                
                currentWalkingTime += Time.deltaTime;
                WalkTowardPosition(targetPosition);
            }
        }
        else
        {
            //timer for the next wandering term
            //decrease the speed if the character is not walking
            walkingTimerCount += Time.deltaTime;

        }
       
        
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
        EMOTIONSINT_HASH = Animator.StringToHash("emotionsInt");
        
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
