using Godot;
using System;
using System.Collections;
using System.Threading;
using static Godot.TextServer;

public partial class Player : CharacterBody2D
{
    public PlayerMovementComponent movementComponent;
    public PlayerAttackComponent attackComponent;
    public InputManagerComponent inputManagerComponent;
    PlayerStates playerState = new PlayerStates();

    enum PlayerStates
    {
        Idle,
        Moving,
        Attacking
    };

    public override void _Ready()
    {

        movementComponent = GetNode<PlayerMovementComponent>("PlayerMovementComponent");
        movementComponent.characterBody = this;
        movementComponent.animatedSprite = this.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        movementComponent.animatedSprite.AnimationFinished += movementComponent.OnAnimationFinished;

        attackComponent = GetNode<PlayerAttackComponent>("PlayerAttackComponent");
        attackComponent.characterBody = this;
        attackComponent.animatedSprite = this.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        attackComponent.animatedSprite.AnimationFinished += attackComponent.OnAttackAnimationFinished;

        movementComponent.attackComponent = attackComponent;
        attackComponent.movementComponent = movementComponent;
        //attackComponent.IsAttackingAction += movementComponent.OnIsAttacking;

        playerState = PlayerStates.Idle;

        inputManagerComponent = GetNode<InputManagerComponent>("InputManagerComponent");
    }

    public override void _Process(double delta)
    {
        if (inputManagerComponent.GetMovementInput(out Vector2 movementDirection) && playerState != PlayerStates.Attacking)
        {
            playerState = PlayerStates.Moving;
            movementComponent.UpdateDirection(movementDirection);
        }
        else
            playerState = PlayerStates.Idle;

        if (inputManagerComponent.GetAttackInput())
        {
            playerState = PlayerStates.Attacking;
            attackComponent.Attack(movementDirection);
        }
    }

    //centralizsr el input en player el check de ataque y llamar a Attack(), lo mismo con Movement
}
