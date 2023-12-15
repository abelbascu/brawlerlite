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
    public AnimatedSprite2D animatedSprite2D;
    public PlayerArea playerArea;
    public Action AnimationFinished;
    public PlayerStates playerState = new PlayerStates();

    public enum PlayerStates
    {
        Idle,
        Moving,
        Attacking
    };

    public override void _Ready()
    {
        animatedSprite2D = this.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        playerArea = this.GetNode<Area2D>("PlayerArea") as PlayerArea;

        //initialize movementComponent
        movementComponent = GetNode<PlayerMovementComponent>("PlayerMovementComponent");
        movementComponent.characterBody = this;
        movementComponent.animatedSprite = animatedSprite2D;

        //inizitialize attackComponent
        attackComponent = GetNode<PlayerAttackComponent>("PlayerAttackComponent");
        attackComponent.characterBody = this;
        attackComponent.animatedSprite = animatedSprite2D;
       
        //animatedSprite.AnimationFinished is a Godot internal signal, we subscribe to it to put player to idle after attack
        attackComponent.animatedSprite.AnimationFinished += attackComponent.OnAttackAnimationFinished;
        attackComponent.AttackAnimationEnded += OnAttackAnimationEnded;
        attackComponent.playerArea = playerArea;

        //attack needs to know where we are facing, both when moving or if idle
        attackComponent.movementComponent = movementComponent;

        //initialize state machine, get reference to input manager
        playerState = PlayerStates.Idle;
        inputManagerComponent = GetNode<InputManagerComponent>("InputManagerComponent");
        
    }

    //execute state machine transitions and corresponding actions
    public override void _Process(double delta)
    {
        if (inputManagerComponent.GetMovementInput(out Vector2 movementDirection) == true && playerState != PlayerStates.Attacking)
        {
            playerState = PlayerStates.Moving;
            movementComponent.UpdateDirection(movementDirection);
        }

        if (inputManagerComponent.GetAttackInput() == true && playerState != PlayerStates.Attacking)
        {
            playerState = PlayerStates.Attacking;
            attackComponent.Attack(movementDirection);
        }

        if (playerState != PlayerStates.Attacking && playerState != PlayerStates.Moving)
            playerState = PlayerStates.Idle;
    }


    public void OnAttackAnimationEnded()
    {
        playerState = PlayerStates.Idle;
    }

    public void OnMovementAnimationEnded()
    {
        playerState = PlayerStates.Idle;
    }


}
