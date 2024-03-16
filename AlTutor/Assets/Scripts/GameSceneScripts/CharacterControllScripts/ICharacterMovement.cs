using UnityEngine;
public interface ICharacterMovement 
{
    Animator animator { get; set; }
    float wanderingSpeed { get; set; } 
    int currentAnimationMovementIndex { get; set; }
    public int WALKSPEEDFLOAT_HASH { get; set; }
    void Initialization();
    void HandleCharacterAnimation();
    void HandleCharacterWandering();
    void LookTowardPlayer();

}
