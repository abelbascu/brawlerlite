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
        attackComponent = GetNode<PlayerAttackComponent>("PlayerAttackComponent");

        movementComponent.attackComponent = attackComponent;
        attackComponent.movementComponent = movementComponent;
        movementComponent.isAttacking = attackComponent.IsAttacking;

        attackComponent.direction = movementComponent.direction;


    }

   


}
