using Godot;
using System;

public class Select : ColorRect
{
    // Declaring selecting bool
    private bool selecting = false; 
    // Declaring Global
    private Global Global; 

    public override void _Ready()
    {
        // Setting Global to Global
        Global = (Global) GetTree().Root.GetNode("Global"); 
    }
    
    public override void _Input(InputEvent Event)
    {
        //Checking if click
        if (Event.IsActionPressed("Click"))
        {
            Hide();
            if (!selecting)
            {
                //Setting select shader mousepos param
                (Material as ShaderMaterial).SetShaderParam("mousepos", new Vector2(0, 0));
                //Setting mouse position to mouseposition 
                SetPosition(GetGlobalMousePosition());
                Show();
            }
            //Setting selecting to not selecting
            selecting = !selecting;
        }
        
        else if (Event.IsActionReleased("Click"))
        {
            Hide();
            selecting = !selecting;
        }

        //Setting select shader mousepos param
        else if (selecting && Event.GetType() == typeof (InputEventMouseMotion))
            (Material as ShaderMaterial).SetShaderParam("mousepos", GetLocalMousePosition());
    }
}
