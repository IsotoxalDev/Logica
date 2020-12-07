using Godot;
using System;

public class Global : Node
{
    public bool OnEditor = false;
    public bool Selected = false;
    public int SelectedNodeNo = 0;
    
    // Nodes
    public Camera2D Camera;
    public ViewportContainer EditorContainer;
    public Control Nodes;
    public CanvasRightClickMenu RightClickMenu;

    public override void _Ready()
    {
        Node root = (Node) GetTree().Root;
        Camera = (Camera2D) FindByName(root, "CameraMan");
        EditorContainer = (ViewportContainer) FindByName(root, "Editor");
        Nodes = (Control) FindByName(EditorContainer, "Nodes");
        RightClickMenu = (CanvasRightClickMenu) FindByName(root, "CanvasRightClickMenu");
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
        SelectArea[1] = -EditorContainer.RectSize / 2 * Camera.Zoom + Camera.Offset + Position * Camera.Zoom + Size * 40;
        for (int ChildIndex = 0; ChildIndex < Nodes.GetChildCount(); ChildIndex++)
        {
            var Child = (IONode)Nodes.GetChild(ChildIndex);
            var Big = SelectArea[0] < SelectArea[1];
            Child.Deselect();
            if (Child.RectPosition < SelectArea[Convert.ToInt16(Big)] && Child.RectPosition > SelectArea[Convert.ToInt16(!Big)])
            {
                Child.Select();
            }
        }
    }
    
}
