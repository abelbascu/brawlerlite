using Godot;
using System;
using System.Threading;

public partial class HealthBar : ProgressBar, IDamageable
{
	
	[Export] private const int MAX_HEALTH = 10;
	public int Health { get; set; } = MAX_HEALTH;
	public Color color = new Color();

	public override void _Ready()
	{
		this.MaxValue = MAX_HEALTH;
		color = Color.Color8(255, 45, 1, 1);
	}

	public void ReceiveDamage(int damage) => Health -= damage;

	public override void _Process(double delta)
	{
		this.Value = Health;
	}
}
