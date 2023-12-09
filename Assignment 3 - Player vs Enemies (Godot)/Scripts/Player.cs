using Godot;
using System;
using System.Collections;
using System.Threading;
using static Godot.TextServer;

public partial class Player : CharacterBody2D
{
    public PlayerMovementComponent movementComponent;
    public PlayerAttackComponent attackComponent;

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
        attackComponent.IsAttackingAction += movementComponent.OnIsAttacking;

    }
}
