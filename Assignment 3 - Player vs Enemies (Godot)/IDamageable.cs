using Godot;
using System;

public interface IDamageable
{
    int Health { get; set; }
    void ReceiveDamage();
}
