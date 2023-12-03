using Godot;
using System;
using System.Threading;

public partial class HealthBar : ProgressBar, IDamageable
{
	private const int MAX_HEALTH = 10;
	public int Health { get; set; } = MAX_HEALTH;
	public Color color = new Color();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.MaxValue = MAX_HEALTH;
		this.Value = Health;

		color = Color.Color8(255, 45, 1, 1);

	}

	public void ReceiveDamage(int damage) => Health -= damage;


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Health--;
		this.Value = Health;
		Thread.Sleep(500);

	}
}
