using Godot;
using System;

public class Select : ColorRect
{
    private bool selecting = false; 
    private Global Global; 
    private Vector2 MousePosition;

    public override void _Ready()
    {
        Global = (Global) GetTree().Root.GetNode("Global"); 
    }
    
    public override void _Input(InputEvent Event)
    {
        if (Event.IsActionPressed("Click"))
        {
            selecting = false;
            Hide();
            bool OnNode = false;
            Vector2 MousePos = GetGlobalMousePosition();
            for (int NodeIndex = 0; NodeIndex < Global.SelectedNodes.Count ; NodeIndex++)
            {
                IONode node = Global.SelectedNodes[NodeIndex];
                if (node.RectPosition.x < MousePos.x &&
                    (node.RectPosition.x + node.RectSize.x) > MousePos.x)
                {
                    if (node.RectPosition.y < MousePos.y &&
                        (node.RectPosition.y + node.RectSize.y) > MousePos.y)
                    {
                        OnNode = true;
                    }
                }
            }
            if (!OnNode){
                Global.Deselect();
            }
            if (!selecting && !Global.RightClickMenu.Visible)
            {
                (Material as ShaderMaterial).SetShaderParam("mousepos", new Vector2(0, 0));
                SetPosition(GetGlobalMousePosition());
                Show();
                selecting = true;
            }
        }
        
        else if (Event.IsActionReleased("Click"))
        {
            if (selecting)
            {
                Hide();
                selecting = false;
                Global.Select(MousePosition, RectPosition);
            }
        }

        else if (selecting && Event.GetType() == typeof (InputEventMouseMotion) && !Global.OnNodeClick)
            (Material as ShaderMaterial).SetShaderParam("mousepos", GetLocalMousePosition());
            MousePosition = GetLocalMousePosition();
    }
}
