using UnityEngine;

public class RockManControllScriptTest : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    int VELOCITY_HASH;
    string GETHITBOOL_HASH = "GetHitToggle";
    string VICTORYBOOL_HASH = "VictoryToggle";
    string DIEBOOL_HASH = "DieToggle";
    private int currentFileIndex = 0;

    [SerializeField] float acceleration = 0.1f;
    string animationMovementSelect;
    void Start()
    {
        animator = GetComponent<Animator>();
        VELOCITY_HASH = Animator.StringToHash("Velocity");
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.T)) velocity += acceleration * Time.deltaTime;
        else velocity -= acceleration * Time.deltaTime;

        if (velocity >= 1) velocity = 1;
        if (velocity <= 0) velocity = 0;

        animator.SetFloat(VELOCITY_HASH, velocity);


        GetInformationFromScript();
        HandleCharacterAnimation();
        Test();
    }

    private void GetInformationFromScript()
    {
        try 
        {
            animationMovementSelect = FileFetcher.GetResponseTxtContent().Substring(0, 1);
        }
        catch{}

        
        if (Input.GetKeyDown(KeyCode.C)) Debug.Log(animationMovementSelect);
    }

    private void HandleCharacterAnimation()
    {
        if (currentFileIndex != FileFetcher.GetCurrentFileIndex())
        {
            if (animationMovementSelect == "1") animator.SetTrigger(GETHITBOOL_HASH);
            else if (animationMovementSelect == "2") animator.SetTrigger(VICTORYBOOL_HASH);
            else if (animationMovementSelect == "3") animator.SetTrigger(DIEBOOL_HASH);
            currentFileIndex = FileFetcher.GetCurrentFileIndex();
        }
        
    }
    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) animator.SetTrigger(GETHITBOOL_HASH);
        if (Input.GetKeyDown(KeyCode.Alpha2)) animator.SetTrigger(VICTORYBOOL_HASH);
        if (Input.GetKeyDown(KeyCode.Alpha3)) animator.SetTrigger(DIEBOOL_HASH);
    }
}
