
using UnityEngine;

public class BlackDragonTest : MonoBehaviour
{
    private Animator animator;
    int IdleSimple;
    int IdleAgressive;
    int IdleRestless;
    int Walk;
    int BattleStance;
    int Bite;
    int Drakaris;
    int FlyingFWD;
    int FlyingAttack;
    int Hover;
    int Lands;
    int Die;


    int TakeOff;
    void Start()
    {
        animator = GetComponent<Animator>();
        IdleSimple = Animator.StringToHash("IdleSimple");
        IdleAgressive = Animator.StringToHash("IdleAgressive");
        IdleRestless = Animator.StringToHash("IdleRestless");
        Walk = Animator.StringToHash("Walk");
        BattleStance = Animator.StringToHash("BattleStance");
        Bite = Animator.StringToHash("Bite");
        Drakaris = Animator.StringToHash("Drakaris");
        FlyingFWD = Animator.StringToHash("FlyingFWD");
        FlyingAttack = Animator.StringToHash("FlyingAttack");
        Hover = Animator.StringToHash("Hover");
        Lands = Animator.StringToHash("Lands");
        TakeOff = Animator.StringToHash("TakeOff");
        Die = Animator.StringToHash("Die");
    }

    private bool isFlying = false;
    private void FlyMode(int durationTime)
    {
        isFlying = true;
        animator.SetTrigger(TakeOff);  
        animator.SetTrigger(FlyingFWD);
    }

    private void CalibratePosition()
    {
        if (isFlying)
        {
            if (!animator.GetBool(FlyingFWD))
            {
                Debug.Log(transform.GetChild(1).position);
                isFlying = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) FlyMode(30);
        CalibratePosition();
    }
}
