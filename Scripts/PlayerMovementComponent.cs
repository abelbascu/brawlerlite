using Godot;
using System;
using System.Threading;


public partial class PlayerMovementComponent : Node
{
    public CharacterBody2D characterBody = new CharacterBody2D();
    public AnimatedSprite2D animatedSprite = new AnimatedSprite2D();
    public Action MovementAnimationEnded;
    public Vector2 cachedDirectionBeforeIdle;
    public bool isWalking { get; set; } = false;
    public string animationName;
    public float Speed { get; set; } = 50;

    public void UpdateDirection(Vector2 direction)
    {
        //Velocity is the internal property of the RigidBody2D
        Vector2 velocity = characterBody.Velocity;
        velocity.Y = direction.Y * Speed;
        velocity.X = direction.X * Speed;
        characterBody.Velocity = velocity;

        if (characterBody != null)
        {
            characterBody.MoveAndSlide();
            UpdateDirectionAnimations(direction);
        }
    }

    public void UpdateDirectionAnimations(Vector2 direction)
    {
        animationName = (direction != Vector2.Zero) ? "walk_" + ReturnedDirection(direction) : "idle";

        //safety check if we come from attack Right, that needs to flip sprite as it's reusing attack_left anim
        if (direction == Vector2.Left && animatedSprite.FlipH == true)
            animatedSprite.FlipH = false;
        GD.Print("moving left");

        if (direction == Vector2.Right && animatedSprite.FlipH == true)
            animatedSprite.FlipH = false;
        //we cache the direction so we know where player is facing before idle, so we can attack in that direction when idle
        if (animationName != "idle")     
            cachedDirectionBeforeIdle = direction;

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

    public void OnMovementAnimationFinished()
    {
        MovementAnimationEnded.Invoke();
    }

}
