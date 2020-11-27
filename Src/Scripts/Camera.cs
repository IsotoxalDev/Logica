using Godot;
using System;

public class Camera : Godot.Camera2D
{
	private Tween zoomTween;

	private bool drag = false;
	private Vector2 pos = new Vector2(0, 0);

	private Vector2 zoomStep = new Vector2(0.05f, 0.05f);
	private Vector2 minZoom = new Vector2(0.1f, 0.1f);
	private Vector2 maxZoom = new Vector2(2, 2f);

	public override void _Ready()
	{
		zoomTween = (Tween) GetNode("zoomTween");
	}

	public override void _Input(InputEvent Event)
	{

		if (Event.IsAction("zoom_in"))
			_zoom(-1);

		else if (Event.IsAction("zoom_out"))
			_zoom(1);

		if (Event.IsActionPressed("space") || Event.IsActionPressed("middle_mouse_button"))
		{
			drag = true;
		}

		else if (Event.IsActionReleased("space") || Event.IsActionReleased("middle_mouse_button"))
		drag = false;

		if (drag && Event.GetType() == typeof(InputEventMouseMotion))
		{
			InputEventMouseMotion NewEvent = (InputEventMouseMotion) Event;
			Offset -= NewEvent.Relative * Zoom;
		}
	}

	private void _zoom(int Dir)
	{
		var NewZoom = Zoom + Zoom * Dir/10;
		if (NewZoom > minZoom && NewZoom < maxZoom)
		{
		zoomTween.InterpolateProperty(this, "zoom", Zoom, NewZoom, 0.05f, 0, 0);
		zoomTween.Start();
		}
	}
}
