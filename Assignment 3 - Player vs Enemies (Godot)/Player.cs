using Godot;
using System;
using System.Collections;
using System.Threading;
using static Godot.TextServer;

public partial class Player : CharacterBody2D
{
    public const float Speed = 50;
    public AnimatedSprite2D animatedSprite = new AnimatedSprite2D();
    public bool isAttacking;
    public Vector2 direction;
    public Vector2 new_direction;
    public delegate void AnimationFinished();
    AnimationFinished animationFinished;



    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animationFinished += OnAnimationFinished;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
       
        
        //if (direction != Vector2.Zero)
        //{
            velocity.Y = direction.Y * Speed;
            velocity.X = direction.X * Speed;
        //}

        Velocity = velocity;
        if (!isAttacking)
        {
            MoveAndSlide();
            //Velocity = Vector2.Zero;
            PlayerAnimations(direction);
        }
    }

    public override void _Input(InputEvent @event)
    {
        string animationName;

        direction = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");

        if (Input.IsActionPressed("attack"))
        {
            isAttacking = true;
            animationName = "attack_" + ReturnedDirection(new_direction);
            if(new_direction == Vector2.Right)
                {
                animatedSprite.FlipH = true;
                }
            animatedSprite.Play(animationName);
            
        }
    }

    
    public void OnAnimationFinished()
    {
        isAttacking = false;
        animatedSprite.FlipH = false;
        //Callable.From
    }

    public void PlayerAnimations(Vector2 direction)
    {
        string animationName;

        if (direction != Vector2.Zero)
        {
            new_direction = direction;
            animationName = "walk_" + ReturnedDirection(new_direction);
            animatedSprite.Play(animationName);
        }
        else
        {
            animationName = "idle";
            animatedSprite.Play(animationName);
        }
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
}
