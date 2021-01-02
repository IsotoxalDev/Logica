using Godot;
using System;

public class IONode : ColorRect
{
	[Export]
	public Color SelectColor;
	[Export]
	public Color UnSelectColor;

	public bool Input = false;
	public bool First = false;
	public bool Movable = false;

	private Global Global;

	public override void _Ready()
	{
		Connect("gui_input", this, "OnInput");
		Global = GetTree().Root.GetNode<Global>("Global");
	}

	public void OnInput(InputEvent Event)
	{
		if (Event.IsActionPressed("Click"))
		{
			Global.OnNodeClick = true;
			Movable = true;
			for(int NodeIndex = 0; NodeIndex < Global.SelectedNodes.Count; NodeIndex++)
			{
				Global.SelectedNodes[NodeIndex].Movable = true;
			}
		}

		else if (Event.IsActionReleased("Click"))
		{
			Global.OnNodeClick = false;
			Movable = false;
		}

		else if (Movable && Event.GetType() == typeof(InputEventMouseMotion))
		{
			Global.OnNodeClick = true;
			InputEventMouseMotion newEvent = (InputEventMouseMotion) Event;
			for(int NodeIndex = 0; NodeIndex < Global.SelectedNodes.Count; NodeIndex++)
			{
				Global.SelectedNodes[NodeIndex].RectPosition += newEvent.Relative;
			}
		}

	}

	public void Select()
	{
					Global.Selected = true;
					Global.SelectedNodes.Add(this);
					Modulate = SelectColor;
	}
}
