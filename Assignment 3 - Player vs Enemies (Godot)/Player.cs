using Godot;
using System;
using System.Collections;
using System.Threading;
using static Godot.TextServer;

public partial class Player : CharacterBody2D
{
    public AnimatedSprite2D animatedSprite = new AnimatedSprite2D();
    public CharacterBody2D characterBody = new CharacterBody2D();
    public bool isAttacking { get; set; } = false;
    
    public override void _Ready()
    {
        var movementHandler = ResourceLoader.Load<PackedScene>("res://MovementHandler.tscn").Instantiate() as MovementHandler;
        AddChild(movementHandler);     
        isAttacking = false;
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        characterBody = this as CharacterBody2D;

        movementHandler.SetAnimatedSprite(animatedSprite);
        movementHandler.SetCharacterBody(this);
        
    }
}
