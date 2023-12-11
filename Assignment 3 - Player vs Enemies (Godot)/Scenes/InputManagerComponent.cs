using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using static Godot.TextServer;

public partial class InputManagerComponent : Node2D
{

	public bool GetMovementInput(out Vector2 direction)
	{
		direction = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");			
		if(direction != Vector2.Zero)
			return true;
		return false;
    }

	public bool GetAttackInput()
	{
		if (Input.IsActionPressed("attack"))
			return true;
		return false;
    }
}
