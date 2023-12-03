using Godot;
using System;

public partial class Attacker : Node
{

    public int Damage { get; set; } = 1;
    public Area2D enemyArea;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        enemyArea = GetNode<Area2D>("EnemyArea");
    }



    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (enemyArea != null)
        {
            //enemyArea.AreaShapeEntered() += OnAreaEntered(enemyArea.AreaShapeEntered().);
        }
    }

    public void Attack()
    {

    }
}
