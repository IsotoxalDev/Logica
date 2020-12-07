using Godot;
using System;

[Tool]
public class IONode : ColorRect
{

    public bool Selected = false;

    private Global Global;
    
    public override void _Ready()
    {
        Global = (Global) GetTree().Root.GetNode("Global");
    }
    
    public void RectChanged()
    {
        SetSize(new Vector2(RectSize.x, RectSize.x));
    }

    public void Select()
    {
        Selected = true;
        Modulate = new Color(0, 0, 0, 1);
        Global.Selected = true;
        Global.RightClickMenu.ChangeItem();
    }
    public void Deselect()
    {
        Selected = false;
        Modulate = new Color(1, 1, 1, 1);
        Global.Selected = false;
        Global.RightClickMenu.ChangeItem();
    }
}
