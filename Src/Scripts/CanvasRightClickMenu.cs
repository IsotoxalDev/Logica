using Godot;
using System;

public class CanvasRightClickMenu : PopupMenu
{
    //Declaring Global
    private Global Global; 
    //Declaring Nodes
    private PackedScene Node; 
    //Declaring MousePos
    private Vector2 MousePos; 

    public override void _Ready()
    {
        //Connecting id_pressed to OptionPressed
        Connect("id_pressed", this, "OptionPressed"); 

        AddItem("Add Input", 0); 
        AddItem("Add Output", 1); 

        //Loading Node Scene
        Node = GD.Load<PackedScene>("res://Src/Node.tscn"); 
        //Setting Global to Global
        Global = (Global)GetTree().Root.GetNode("Global"); 
    }

    //Setting Input
    public override void _Input(InputEvent Event)
    {
        //Checking if mouse is on workspace
        if (Global.OnEditor) 
            //Checking if right click is pressed
            if (Event.IsActionPressed("RightClick"))
            {
                //Setting Positon to Global Mouse Position
                SetPosition(GetGlobalMousePosition()); 
                Popup_(); //POP UP
                //Setting MousePos to the mouse position
                MousePos = GetViewport().GetMousePosition(); 
            }
    }
    public void OptionPressed(int id) {
        //Setting NewNode to Instance of NodeScene
        ColorRect NewNode = (ColorRect)Node.Instance();
        //Setting NewNode's Positon to the location in the viewport
        NewNode.SetPosition(-(new Vector2(1550, 1080)/2) * Global.Camera.Zoom +
                            Global.Camera.Offset + MousePos * Global.Camera.Zoom);
        //Adding Child to Nodes
        Global.Nodes.AddChild(NewNode);
    }
}
