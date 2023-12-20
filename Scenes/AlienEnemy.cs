using Godot;
using System;

public partial class AlienEnemy : CharacterBody2D
{
    //[Export] public NavigationAgent2D navigationAgent2D;
    public Node2D targetEntity;
    public Vector2 velocity = Vector2.Zero;
    public float SPEED = 1.0f;
    public Vector2 direction = Vector2.Zero;
    public Vector2 targetPosition;
    public AlienEnemy alienEnemy;
    public Node navigationAgent2DNode;
    public NavigationAgent2D navigationAgent2D;

    public override void _Ready()
    {
        var alienEnemy = this;
        var childrenNodes = alienEnemy.GetChildren();
        
        foreach (var child in childrenNodes)
        {
           // var navigationAgent2DCached = child;
            GD.Print(child);
            if (child.Name == "NavigationAgent2D")
                navigationAgent2DNode = child;
                navigationAgent2D = child as NavigationAgent2D;                    
        }







    //       public NavigationAgent2D navigationAgent2D;

    //public override void _Ready()
    //{
    //    var alienEnemy = this;
    //    var childrenNodes = alienEnemy.GetChildren();

    //    foreach (var child in childrenNodes)
    //    {
    //        // var navigationAgent2DCached = child;
    //        GD.Print(child);
    //        if (child.Name == "NavigationAgent2D")
    //            navigationAgent2D = child as NavigationAgent2D;
    //    }



















        //NavigationAgent2D.targetPosition = new Vector2(100, 100);
        //CallDeferred("SetUpNavigationAgent");
        Callable.From(SetUpNavigationAgent).CallDeferred();
        
    }

    public async void SetUpNavigationAgent()
    {

        //Wait for the first physics frame so the NavigationServer can sync. 
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
        // navigationAgent2D = GetNode<NavigationAgent2D>("NavigationAgent2D");
        //navigationAgent2D = GetTree().Root.GetNode("AlienEnemy/NavigationAgent2D") as NavigationAgent2D;
        // navigationAgent2D.TargetPosition = GetGlobalMousePosition();
        // targetEntity = GetTree().Root.GetNode<CharacterBody2D>("Player");

        targetEntity = GetParent().GetNode<CharacterBody2D>("Player");
        // navigationAgent2D = GetNode<NavigationAgent2D>("NavigationAgent2D");

       // navigationAgent2D.TargetPosition = targetEntity.GlobalPosition;


        // Now that the navigation map is no longer empty, set the movement target. 

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        //base._PhysicsProcess(delta);

        // navigationAgent2D.TargetPosition = targetPosition;
        CreatePath();
        // MoveAndSlide(navigationAgent2D.Velocity);
        MoveAndSlide();

    }

	public void CreatePath()
    {
        //navigationAgent2D.TargetPosition = targetEntity.Position;
       // var direction = ToLocal(navigationAgent2D.GetNextPathPosition().Normalized());
       // velocity = direction * SPEED;
     //   Velocity = velocity;

    }


    //public void OnTimerTimeOut()
    //{
    //	CreatePath();
    //}


}
