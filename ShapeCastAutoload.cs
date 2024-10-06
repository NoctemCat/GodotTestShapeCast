using Godot;
using System;
using System.Collections.Generic;

public partial class ShapeCastAutoload : Node2D
{
	public static ShapeCastAutoload Instance { get; private set; }
	[Export] ShapeCast2D ShapeCast { get; set; }
	public List<GodotObject> Results { get; private set; } = [];
	public override void _EnterTree()
	{
		Instance = this;

		ShapeCast.TargetPosition = Vector2.Zero;

		ShapeCast.CollideWithBodies = true;
		ShapeCast.CollisionMask = 1;

		ShapeCast.Enabled = false;
	}

	public List<GodotObject> IntersectShape(Transform2D transform)
	{
		Results.Clear();

		ShapeCast.Transform = transform;
		ShapeCast.ForceShapecastUpdate();

		int count = ShapeCast.GetCollisionCount();
		for (int i = 0; i < count; i++)
		{
			Results.Add(ShapeCast.GetCollider(i));
		}

		return Results;
	}
}
