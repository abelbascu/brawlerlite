//using Godot;
//using System;

//public partial class AttackBase : Node
//{

//    public AnimatedSprite2D animatedSprite = new AnimatedSprite2D();
//    //public MovementBase movementComponent;
//    // PONER UN EXPORT DE PLAYERMOVEMENTCOMPONENT PERO EN PLAYERATTACKCOMPONENT NO AQUI

//    // MAYBE I CAN GET THE NORMALIZED DIRECTION SO I CAN REMOVE THE RETURNEDDIRECTION METHOD
//    public bool IsAttacking { get; set; } = false;
//    public int Damage { get; set; } = 1;
//    public Area2D enemyArea;
    
//    public Action AttackAnimationEnded;
//    public Vector2 direction;

//    public override void _Ready()
//    {
//        IsAttacking = false;
//        AttackAnimationEnded += OnAttackAnimationFinished; //then subscribing the custom action to the inbuilt signal     
//        //enemyArea = GetNode<Area2D>("EnemyArea");
//    }

//    public override void _Process(double delta)
//    {
//       // direction = movementComponent.direction;
//       // ShowAttackAnimation(movementComponent.direction);

//        if (enemyArea != null)
//        {
//            //enemyArea.AreaShapeEntered() += OnAreaEntered(enemyArea.AreaShapeEntered().);
//        }
//    }

//    public void Attack()
//    {

//    }

//    public void ShowAttackAnimation(Vector2 direction)
//    {
//        string animationName;
//        if (Input.IsActionPressed("attack"))
//        {
//            IsAttacking = true;
//            animationName = "attack_" + ReturnedDirection(direction);
//            if (direction == Vector2.Right)
//            {
//                animatedSprite.FlipH = true;
//            }
//            animationName = "attack_" + ReturnedDirection(direction);
//            if (direction == Vector2.Right)
//            {
//                animatedSprite.FlipH = true; //we reuse attack_left animation
//            }
//            animatedSprite.AnimationFinished += AttackAnimationEnded; //if i subscribe on ready, animatedSprite doesn't exist yet.
//            animatedSprite.Play(animationName);
//        }
//    }

//    public string ReturnedDirection(Vector2 direction)
//    {
//        var normalizedDirection = direction.Normalized();

//        if (normalizedDirection.Y > 0)
//            return "down";
//        if (normalizedDirection.Y < 0)
//            return "up";
//        if (normalizedDirection.X > 0)
//            return "right";
//        if (normalizedDirection.X < 0)
//            return "left";
//        return "left";
//    }

//    public void OnAttackAnimationFinished()
//    {
//        IsAttacking = false;
//        animatedSprite.FlipH = false;
//    }
//}
