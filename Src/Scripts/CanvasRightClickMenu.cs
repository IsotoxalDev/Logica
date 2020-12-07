using Godot;
using System;

public class CanvasRightClickMenu : PopupMenu
{
    private Global Global; 
    private PackedScene Node; 
    private Vector2 MousePos; 

    public override void _Ready()
    {
        Connect("id_pressed", this, "OptionPressed"); 

        AddItem("Add Input", 0); 
        AddItem("Add Output", 1); 

        Node = GD.Load<PackedScene>("res://Src/Node.tscn"); 
        Global = (Global)GetTree().Root.GetNode("Global"); 
    }

    //Setting Input
    public override void _Input(InputEvent Event)
    {
        if (Global.OnEditor) 
            if (Event.IsActionPressed("RightClick"))
            {
                SetPosition(GetGlobalMousePosition()); 
                Popup_();
                MousePos = GetViewport().GetMousePosition(); 
            }
    }
    public void OptionPressed(int id) {
        switch(id)
        {
            case 0:
                AddNode(true);
                break;
            case 1:
                AddNode();
                break;
            case 2:
                DeleteSelected();
                break;
        }
    }

    private void AddNode(bool input = false)
    {
        ColorRect NewNode = (ColorRect)Node.Instance();
        NewNode.SetPosition(-(new Vector2(1550, 1080)/2) * Global.Camera.Zoom +
                            Global.Camera.Offset + MousePos * Global.Camera.Zoom);
        Global.Nodes.AddChild(NewNode);
    }

    private void DeleteSelected()
    {
        for (int ChildIndex = 0; ChildIndex < Global.Nodes.GetChildCount(); ChildIndex++)
        {
            IONode Child = (IONode) Global.Nodes.GetChild(ChildIndex);
            if (Child.Selected)
            {
                Child.QueueFree();
                Global.Selected = false;
                ChangeItem();
            }
        }
    }

    public void ChangeItem()
    {
        if(Global.Selected)
        {
            RemoveItem(0);
            RemoveItem(0);
            AddItem("Delete", 2);
            AddItem("Auto Sort", 3);
        }
        else
        {
            RemoveItem(0);
            RemoveItem(0);
            AddItem("Add Input", 0); 
            AddItem("Add Output", 1); 
        }
    }
}
