using Godot;
using System;

public class IONode : ColorRect
{
    [Export]
    public Color SelectColor;
    [Export]
    public Color UnSelectColor;


    public bool Input = false;
}
