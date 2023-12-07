using Godot;
using System;

public partial class EnemyAlienMovementComponent : MovementBase
{
    public EnemyAlienAttackComponent attackComponent = new();

    public override Vector2 GetDirection()
    {
        int randomDirection = new Random().Next(1 - 5);

        switch (randomDirection)
        {
            case 1:
            return new Vector2(-1, 0); // "walk_left"                                  
            case 2:
            return new Vector2(1, 0); // "walk_right"
            case 3:
            return new Vector2(0, -1); // "walk_up"
            case 4:
            return new Vector2(0, 1); // "walk_down"
            default:
            return Vector2.Zero;
        }

    }
}

