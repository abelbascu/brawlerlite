using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerArea : Godot.Area2D
{
    public Player player;
    //public Action<Area2D> EnemyEnteredPlayerArea;
    public bool IsOverlappingEnemy { get; set; } = false;
    public List<Area2D> EnemiesOverlapping = new List<Area2D>();

    public override void _Ready()
    {
        this.AreaEntered += OnAreaEntered;
        this.AreaExited += OnAreaExited;
    }


    public void OnAreaExited(Area2D area)
    {
        if (EnemiesOverlapping.Contains(area))
        {
            EnemiesOverlapping.Remove(area);
        }
    }

    public void OnAreaEntered(Area2D area)
    {
        var entityGroups = area.GetParent().GetGroups();
        foreach (var group in entityGroups)
        {
            if (group != null && group == "Enemies")
            {
                //EnemyEnteredPlayerArea?.Invoke(area);
                IsOverlappingEnemy = true;
                EnemiesOverlapping.Add(area);

            }
        }
    }

}
