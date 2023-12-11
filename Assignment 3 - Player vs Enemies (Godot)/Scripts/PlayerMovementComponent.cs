using Godot;
using System;
using System.Threading;


public partial class PlayerMovementComponent : Node
{
    public CharacterBody2D characterBody = new CharacterBody2D();
    public PlayerAttackComponent attackComponent;
    public bool isAttacking = false;
    public AnimatedSprite2D animatedSprite = new AnimatedSprite2D();
    public Action AnimationEnded;
    public Vector2 direction;
    public bool isWalking { get; set; } = false;
    public string animationName;
    public float Speed { get; set; } = 50;

    public void UpdateDirection(Vector2 direction)
    {
        Vector2 velocity = characterBody.Velocity; //Velocity is the internal property of the RigidBody2D
        velocity.Y = direction.Y * Speed;
        velocity.X = direction.X * Speed;
        characterBody.Velocity = velocity;

        if (characterBody != null && isAttacking == false)
        {
            characterBody.MoveAndSlide();
            UpdateDirectionAnimations(direction);
        }
    }

    public void UpdateDirectionAnimations(Vector2 direction)
    {
        animationName = (direction != Vector2.Zero) ? "walk_" + ReturnedDirection(direction) : "idle";
        if (attackComponent.IsAttacking == false)
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
        return "left";
    }

    public void OnAnimationFinished()
    {
        if (direction == Vector2.Right)
            animatedSprite.FlipH = false;
    }

}
