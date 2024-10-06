using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Main : Node2D
{
	[Export] public PackedScene BuletSceneNaive { get; set; }
	[Export] public PackedScene BuletSceneCode { get; set; }
	[Export] public PackedScene BuletSceneAutoload { get; set; }
	private PackedScene selectedScene;
	[Export] public Node BuletHolder { get; set; }
	[Export] public Player Player { get; set; }
	[Export] public OptionButton Options { get; set; }
	[Export] public LineEdit BulletsPerFire { get; set; }
	[Export] public Label UiLabel { get; set; }

	public static int BulletCount { get; set; } = 0;
	public static Stack<Node2D> BulletPool { get; set; } = [];

	public override void _Ready()
	{
		BulletsPerFire.Text = Player.BulletsPerFrame.ToString();

		Player.Fire += CreateBullet;
		Options.ItemSelected += OnModeSwitch;
		BulletsPerFire.TextSubmitted += OnCountSubmitted;

		OnModeSwitch(0);
	}

	private void OnCountSubmitted(string newText)
	{
		if (int.TryParse(newText, out int result))
		{
			Player.BulletsPerFrame = result;
			BulletsPerFire.ReleaseFocus();
		}
	}

	private void OnModeSwitch(long index)
	{
		foreach (var child in BuletHolder.GetChildren())
		{
			child.Free();
		}
		BulletCount = 0;
		BulletPool.Clear();

		selectedScene = index switch
		{
			0 => BuletSceneNaive,
			1 => BuletSceneCode,
			2 => BuletSceneAutoload,
			_ => BuletSceneNaive
		};
	}

	public override void _Process(double delta)
	{
		UiLabel.Text = $"Bullets: {BulletCount}\nFPS: {Engine.GetFramesPerSecond()}";
	}

	private void CreateBullet(Vector2 pos)
	{
		Node2D bullet;
		if (BulletPool.Count > 0)
		{
			bullet = BulletPool.Pop();
			bullet.Visible = true;
			bullet.SetPhysicsProcess(true);
		}
		else
		{
			bullet = selectedScene.Instantiate<Node2D>();
			BuletHolder.AddChild(bullet);
		}

		var playerPos = Player.GlobalPosition;
		bullet.GlobalPosition = playerPos;

		bullet.Rotation = playerPos.AngleToPoint(pos) + (30.0f.ToRadians() * (Random.Shared.NextSingle() - 0.5f));

		BulletCount++;
	}

	public static void HideNode(Node2D node)
	{
		node.Visible = false;
		node.SetPhysicsProcess(false);
		BulletCount--;
		BulletPool.Push(node);
	}
}

public static class Foo
{

	public static float ToRadians(this float angle)
	{
		return angle * MathF.PI / 180.0f;
	}

}