using Godot;
using System;
using System.Threading;

public partial class PlayerAttackComponent : Node
{
    public AnimatedSprite2D animatedSprite;
    public CharacterBody2D characterBody;
    public int Damage { get; set; } = 1;
    public Area2D enemyArea;
    public Action AttackAnimationEnded;
    //public Vector2 direction;
    public string animationName;
    public PlayerMovementComponent movementComponent;
    public Vector2 cachedDirection;


    public void Attack(Vector2 direction)
    {
        ShowAttackAnimation(direction);
    }

    public void ShowAttackAnimation(Vector2 direction)
    {

        cachedDirection = movementComponent.cachedDirectionBeforeIdle;
        animationName = "attack_" + ReturnedDirection(direction);

        if (animationName == "attack_idle") // if player is idle, we check his previous direction to know where he is facing
            animationName = "attack_" + ReturnedDirection(cachedDirection);

        if ((direction == Vector2.Left) || (cachedDirection == Vector2.Left))   
            animatedSprite.FlipH = false; //we reuse attack_left animation
        
        if (direction == Vector2.Right || (cachedDirection == Vector2.Right))    
            animatedSprite.FlipH = true; //we reuse attack_left animation
        
        animatedSprite.Play(animationName);

    }

    public string ReturnedDirection(Vector2 direction)
    {
        var normalizedDirection = direction.Normalized();

        if (normalizedDirection.Y > 0)
            return "down";
        if (normalizedDirection.Y < 0)
            return "up";
        if (normalizedDirection.X > 0)
            return "right";
        if (normalizedDirection.X < 0)
            return "left";
        return "idle";
    }

    public void OnAttackAnimationFinished()
    {
        AttackAnimationEnded.Invoke();
    }
}
