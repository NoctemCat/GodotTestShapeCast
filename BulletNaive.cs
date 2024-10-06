using Godot;
using System;

public partial class BulletNaive : Area2D
{

    private float maxRange = 1200.0f;
    private float speed = 750.0f;
    private float travelledDistance = 0.0f;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        float distance = (float)(speed * delta);
        var motion = Transform.X * speed * (float)delta;

        Position += motion;
        travelledDistance += distance;
        if (travelledDistance > maxRange)
        {
            Main.HideNode(this);
            travelledDistance = 0;
        }
    }

    private void OnBodyEntered(Node2D body)
    {
        Main.HideNode(this);
        travelledDistance = 0;
    }
}
