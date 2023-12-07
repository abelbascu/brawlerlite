using Godot;
using System;

public partial class PlayerMovementComponent : MovementBase
{
    public CharacterBody2D characterBody = new CharacterBody2D();
    //[Export] NodePath nodePath = new NodePath();
    //[Export] PackedScene PlayerAttackComponent attackComponent;
    //public PackedScene attackComponentScene = GD.Load<PackedScene>("res://Scenes/PlayerAttackComponent.tscn");
    public PlayerAttackComponent attackComponent =  new PlayerAttackComponent();
    //public PlayerAttackComponent attackComponent = ResourceLoader.Load<PackedScene>("res://Scenes/PlayerAttackComponent.tscn").Instantiate() as PlayerAttackComponent;
    public bool isAttacking = false;

    //new PlayerAttackComponent() as Node;


    public override void _Ready()
    {
        //attackComponent = GetParent().GetNode("Player").GetNode<PlayerAttackComponent>("PlayerAttackComponent");

        //var attackComponentNode = attackComponentScene.Instantiate() as PlayerAttackComponent;
        //var attackComponent = ResourceLoader.Load<PackedScene>("res://Scenes/PlayerAttackComponent.tscn").Instantiate() as PlayerAttackComponent;
        //AddChild(attackComponent);
        //attackComponent = attackComponentScene;


       // isAttacking = attackComponent.IsAttacking;

        // attackComponent = attackComponentNode as PlayerAttackComponent;
        //attackComponent = attackComponentNode as PlayerAttackComponent;

        AnimationEnded += OnAnimationFinished; //another way, using indirection to assign first the callback to a custom action
        animatedSprite.AnimationFinished += AnimationEnded; //then subscribing the custom action to the inbuilt signal
        //animatedSprite.AnimationFinished += OnAnimationFinished;
        
        characterBody = GetParent() as CharacterBody2D;
        animatedSprite = characterBody.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        base._Ready();
    }

    //public override void _Process(double delta)
    //{
    //   // base._Process(delta);
    //    //UpdateDirection(GetDirection());
    //}

    public override void UpdateDirection(Vector2 direction)
    {
        base.UpdateDirection(direction);

        //float Speed = 50;

        Vector2 velocity = characterBody.Velocity; //Velocity is the internal property of the RigidBody2D
        velocity.Y = direction.Y * Speed;
        velocity.X = direction.X * Speed;
        characterBody.Velocity = velocity;

        if (characterBody != null && isAttacking == false)
        {
            characterBody.MoveAndSlide();
            UpdateDirectionAnimations(direction);
        }
    }

    public override void UpdateDirectionAnimations(Vector2 direction)
    {
        animationName = (direction != Vector2.Zero) ? "walk_" + ReturnedDirection(direction) : "idle";
        if (isAttacking == false)
        animatedSprite.Play(animationName);
    }

    public void OnAnimationFinished()
    {
        animatedSprite.FlipH = false;
    }
}
