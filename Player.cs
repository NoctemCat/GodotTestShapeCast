using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	[Export]
	public int BulletsPerFrame = 3;

	public event Action<Vector2> Fire;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector(InputMap.Left, InputMap.Right, InputMap.Up, InputMap.Down);
		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;
		}
		else
		{
			velocity = Velocity.MoveToward(Vector2.Zero, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();

		if (Input.IsActionPressed(InputMap.Fire))
		{
			for (int i = 0; i < BulletsPerFrame; i++)
			{
				Fire?.Invoke(GetGlobalMousePosition());
			}
			// GD.Print("Fire");
		}
	}

	public override void _Process(double delta)
	{

	}
}

public static class InputMap
{
	public static StringName Left { get; } = "left";
	public static StringName Right { get; } = "right";
	public static StringName Up { get; } = "up";
	public static StringName Down { get; } = "down";
	public static StringName Fire { get; } = "fire";
}