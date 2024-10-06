using Godot;
using System;

public partial class BulletAutoload : Node2D
{
	private float maxRange = 1200.0f;
	private float speed = 750.0f;
	private float travelledDistance = 0f;

	public override void _Ready()
	{

	}

	public override void _PhysicsProcess(double delta)
	{
		float distance = (float)(speed * delta);
		var motion = Transform.X * speed * (float)delta;

		Position += motion;
		travelledDistance += distance;

		var results = ShapeCastAutoload.Instance.IntersectShape(GlobalTransform);
		if (results.Count > 0 || travelledDistance > maxRange)
		{
			Main.HideNode(this);
			travelledDistance = 0;
		}
	}
}
