using Godot;
using System;
using System.Collections;
using System.Threading;
using static Godot.TextServer;

public partial class Player : CharacterBody2D
{
    public AnimatedSprite2D animatedSprite = new AnimatedSprite2D();
    public bool isAttacking;
    public Action AnimationEnded;


    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        // animatedSprite.AnimationFinished += OnAnimationFinished; //we can subscribe to signals by code instead of the inspector
        AnimationEnded += OnAnimationFinished; //another way, using indirection to assign first the callback to a custom action
        animatedSprite.AnimationFinished += AnimationEnded; //then subscribing the custom action to the inbuilt signal
    }

    public override void _PhysicsProcess(double delta)
    {
        UpdateDirection(GetDirection());
        UpdateAttackStatus(GetDirection());
    }

    public Vector2 GetDirection()
    {
        Vector2 direction = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");
        return direction;
    }

    public void UpdateDirection(Vector2 direction)
    {
        float Speed = 50;

        Vector2 velocity = Velocity; //Velocity is the internal property of the RigidBody2D
        velocity.Y = direction.Y * Speed;
        velocity.X = direction.X * Speed;
        Velocity = velocity;
        if (!isAttacking)
        {
            MoveAndSlide();
            UpdateDirectionAnimations(direction);
        }
    }

    public void UpdateDirectionAnimations(Vector2 direction)
    {
        string animationName = (direction != Vector2.Zero) ?  "walk_" + ReturnedDirection(direction) : "idle" ;
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

    public void UpdateAttackStatus(Vector2 direction)
    {
        ShowAttackAnimation(direction);
    }

    public void ShowAttackAnimation(Vector2 direction)
    {
        string animationName;
        if (Input.IsActionPressed("attack"))
        {
            isAttacking = true;
            animationName = "attack_" + ReturnedDirection(direction);
            if (direction == Vector2.Right)
            {
                animatedSprite.FlipH = true; //we reuse attack_left animation
            }
            animatedSprite.Play(animationName);
        }
    }

    public void OnAnimationFinished()
    {
        isAttacking = false;
        animatedSprite.FlipH = false;
    }
}
