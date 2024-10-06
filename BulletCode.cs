using Godot;
using System;

public partial class BulletCode : Node2D
{
	[Export] public Shape2D Shape { get; set; }
	private float maxRange = 1200.0f;
	private float speed = 750.0f;
	private float travelledDistance = 0f;

	private PhysicsShapeQueryParameters2D query;
	private PhysicsDirectSpaceState2D dss;
	public override void _Ready()
	{
		query = new()
		{
			Shape = Shape,
			CollideWithBodies = true,
			CollisionMask = 1,
			Transform = GlobalTransform
		};
		dss = GetWorld2D().DirectSpaceState;
	}

	public override void _PhysicsProcess(double delta)
	{
		float distance = (float)(speed * delta);
		var motion = Transform.X * speed * (float)delta;

		Position += motion;
		travelledDistance += distance;

		query.Transform = GlobalTransform;
		var results = dss.IntersectShape(query, 1);
		if (results.Count > 0 || travelledDistance > maxRange)
		{
			Main.HideNode(this);
			travelledDistance = 0;
		}
	}
}
