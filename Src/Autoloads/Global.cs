using Godot;
using System;

public class Global : Node
{
    public bool OnEditor = false;
    
    // Nodes
    public Camera2D Camera;
    public Control Nodes;

    public override void _Ready()
    {
        Node root = (Node) GetTree().Root;
        Camera = (Camera2D) FindByName(root, "CameraMan");
        Nodes = (Control) FindByName(root, "Nodes");
    }

    private Node FindByName(Node Parent, String Name)
    {
        Node Found;
        for (int ChildIndex = 0; ChildIndex < Parent.GetChildCount(); ChildIndex++)
        {
            if (Parent.GetChild(ChildIndex).Name == Name)
                return Parent.GetChild(ChildIndex);
            else
                Found = FindByName(Parent.GetChild(ChildIndex), Name);
                if (Found != null)
                    return Found;
        }
        return null;
    }
}
