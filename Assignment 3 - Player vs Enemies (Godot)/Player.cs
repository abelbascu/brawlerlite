using Godot;
using System;
using System.Collections;
using System.Threading;
using static Godot.TextServer;

public partial class Player : CharacterBody2D
{
    public AnimatedSprite2D animatedSprite = new AnimatedSprite2D();
    public CharacterBody2D characterBody = new CharacterBody2D();
    public bool isAttacking = false;
    
    public override void _Ready()
    {         
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        characterBody = this as CharacterBody2D;

        var movementHandler = ResourceLoader.Load<PackedScene>("res://MovementHandler.tscn").Instantiate() as MovementHandler;
        AddChild(movementHandler);

        movementHandler.animatedSprite = animatedSprite;
        movementHandler.characterBody = characterBody;

        var attackHandler = ResourceLoader.Load<PackedScene>("res://AttackHandler.tscn").Instantiate() as AttackHandler;
        AddChild(attackHandler);

        attackHandler.movementHandler = movementHandler;  
        attackHandler.animatedSprite = animatedSprite;

        movementHandler.attackHandler = attackHandler;
    }
}
