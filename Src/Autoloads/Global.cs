using Godot;
using System;
using System.Collections.Generic;

public class Global : Node
{
    public bool OnEditor = false;
    public bool Selected = false;
    public List<IONode> SelectedNodes = new List<IONode>();

    // Nodes
    public Camera2D Camera;
    public ViewportContainer EditorContainer;
    public Control Nodes;
    public EditorRightClickMenu RightClickMenu;

    public override void _Ready()
    {
        Node root = (Node)GetTree().Root;
        Camera = (Camera2D)FindByName(root, "CameraMan");
        EditorContainer = (ViewportContainer)FindByName(root, "Editor");
        Nodes = (Control)FindByName(EditorContainer, "Nodes");
        RightClickMenu = (EditorRightClickMenu)FindByName(root, "EditorRightClickMenu");
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

    public void Select(Vector2 Size, Vector2 Position)
    {
        Vector2[] SelectArea = new Vector2[2];
        SelectArea[0] = -EditorContainer.RectSize / 2 * Camera.Zoom + Camera.Offset + Position * Camera.Zoom;
        SelectArea[1] = -EditorContainer.RectSize / 2 * Camera.Zoom + Camera.Offset + Position * Camera.Zoom + (Size * 40) * Camera.Zoom;
        SelectedNodes.Clear();
        Selected = false;
        for (int ChildIndex = 0; ChildIndex < Nodes.GetChildCount(); ChildIndex++)
        {
            var Child = (IONode)Nodes.GetChild(ChildIndex);
            var Big = SelectArea[0] < SelectArea[1];
            Child.Modulate = Child.UnSelectColor;
            if (Child.RectPosition.x < SelectArea[Convert.ToInt16(Big)].x &&
                Child.RectPosition.x > SelectArea[Convert.ToInt16(!Big)].x)
            {
                if (Child.RectPosition.y < SelectArea[Convert.ToInt16(Big)].y &&
                    Child.RectPosition.y > SelectArea[Convert.ToInt16(!Big)].y)
                {
                    Selected=true;
                    SelectedNodes.Add(Child);
                    Child.Modulate = Child.SelectColor;
                }
            }
        }
        RightClickMenu.ChangeItem();
    }

}
