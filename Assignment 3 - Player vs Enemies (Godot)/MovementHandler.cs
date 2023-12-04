using Godot;
using System;

public partial class MovementHandler : Node
{

    public AnimatedSprite2D animatedSprite = new AnimatedSprite2D();
    public CharacterBody2D characterBody = new CharacterBody2D();
    public AttackHandler attackHandler = new AttackHandler();
    public Action AnimationEnded;
    public Vector2 direction;
    public bool isWalking { get; set; } = false;

    public override void _Ready()
    {
        AnimationEnded += OnAnimationFinished; //another way, using indirection to assign first the callback to a custom action
        animatedSprite.AnimationFinished += AnimationEnded; //then subscribing the custom action to the inbuilt signal
        //animatedSprite.AnimationFinished += OnAnimationFinished;
    }

    public override void _Process(double delta)
    {
        UpdateDirection(GetDirection());
    }

    public Vector2 GetDirection()
    {
        direction = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");
        return direction;
    }

    public void UpdateDirection(Vector2 direction)
    {
        float Speed = 50;

        Vector2 velocity = characterBody.Velocity; //Velocity is the internal property of the RigidBody2D
        velocity.Y = direction.Y * Speed;
        velocity.X = direction.X * Speed;
        characterBody.Velocity = velocity;
        if (characterBody != null && attackHandler.IsAttacking == false)
        {
            characterBody.MoveAndSlide();
            UpdateDirectionAnimations(direction);
        }
    }

    public void UpdateDirectionAnimations(Vector2 direction)
    {
        string animationName = (direction != Vector2.Zero) ? "walk_" + ReturnedDirection(direction) : "idle";
        if (attackHandler.IsAttacking == false)
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
        animatedSprite.FlipH = false;
    }
}
