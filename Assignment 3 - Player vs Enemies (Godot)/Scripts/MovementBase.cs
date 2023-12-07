using Godot;
using System;

public partial class MovementBase : Node
{
    public AnimatedSprite2D animatedSprite = new AnimatedSprite2D();
  
    public Action AnimationEnded;
    public Vector2 direction;
    public bool isWalking { get; set; } = false;
    public string animationName;
    public float Speed { get; set; } = 50;

    public override void _Process(double delta)
    {
        UpdateDirection(GetDirection());
    }

    public virtual Vector2 GetDirection()
    {
        direction = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");
        return direction;
    }

    public virtual void UpdateDirection(Vector2 direction)
    {
    }

    public virtual void UpdateDirectionAnimations(Vector2 direction)
    {
         
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

    //public void OnAnimationFinished()
    //{
    //    animatedSprite.FlipH = false;
    //}
}
