using Godot;
using System;

[Tool]
public class IONode : ColorRect
{
    public void RectChanged()
    {
        SetSize(new Vector2(RectSize.x, RectSize.x));
    }
}
