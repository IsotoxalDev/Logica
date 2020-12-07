using Godot;
using System;

public class Editor : ViewportContainer
{
    private Global Global;
    
    public override void _Ready()
    {
        Connect("mouse_entered", this, "OnEditor");
        Connect("mouse_exited", this, "NotOnEditor");

        Global = (Global)GetTree().Root.GetNode("Global");
    }
    
    private void OnEditor() {Global.OnEditor = true;}    
    private void NotOnEditor() {Global.OnEditor = false;}    
}










