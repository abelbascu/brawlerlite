using Godot;
using System;
using System.Threading;

public partial class PlayerAttackComponent : Node
{
    public AnimatedSprite2D animatedSprite;
    public CharacterBody2D characterBody;
    public bool IsAttacking { get; set; } = false;
    public int Damage { get; set; } = 1;
    public Area2D enemyArea;
    public Action AttackAnimationEnded;
    public Vector2 direction;
    public string animationName;
    public PlayerMovementComponent movementComponent;

    public override void _Ready()
    {
        IsAttacking = false;
    }

    public override void _Process(double delta)
    {
        CheckIfAttackActionPressed();

        if (IsAttacking)
        {
            direction = movementComponent.direction;
            ShowAttackAnimation(direction);
        }

        if (enemyArea != null)
        {
            //enemyArea.AreaShapeEntered() += OnAreaEntered(enemyArea.AreaShapeEntered().);
        }
    }

    public void Attack()
    {

    }

    public void CheckIfAttackActionPressed()
    {
        if (Input.IsActionPressed("attack"))
        {
            IsAttacking = true;
            //IsAttackingAction.Invoke();
        }

        //else IsAttacking = false;    this sentence was preventing the attack animation to be completed, when you released spacebar this was set to false before the animation finished so the movement code was enabled. 
    }

    public void ShowAttackAnimation(Vector2 direction)
    {
        {
            animationName = "attack_" + ReturnedDirection(direction);

            if (direction == Vector2.Right)
            {
                animatedSprite.FlipH = true;
            }
            animationName = "attack_" + ReturnedDirection(direction);
            if (direction == Vector2.Right)
            {
                animatedSprite.FlipH = true; //we reuse attack_left animation
            }
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

    public void OnAttackAnimationFinished()
    {
        IsAttacking = false;
        if (animationName == "attack_right")
              animatedSprite.FlipH = false;

    }
}
