using Godot;
using System.Threading;
using static Godot.TextServer;

public abstract partial class CharacterBase : CharacterBody2D
{
    public AnimatedSprite2D animatedSprite = new AnimatedSprite2D(); 
    public CharacterBody2D characterBody = new CharacterBody2D();
    public bool isAttacking = false;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        characterBody = this as CharacterBody2D;

        var movementComponent = ResourceLoader.Load<PackedScene>("res://MovementComponent.tscn").Instantiate() as MovementComponent;
        AddChild(movementComponent);

        movementComponent.animatedSprite = animatedSprite;
        movementComponent.characterBody = characterBody;

        var attackComponent = ResourceLoader.Load<PackedScene>("res://AttackComponent.tscn").Instantiate() as AttackComponent;
        AddChild(attackComponent);

        attackComponent.movementComponent = movementComponent;
        attackComponent.animatedSprite = animatedSprite;

        movementComponent.attackComponent = attackComponent;
    }
}

