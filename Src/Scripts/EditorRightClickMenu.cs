using Godot;
using System;

public class EditorRightClickMenu : PopupMenu
{
    private Global Global; 
    private PackedScene Node; 
    private Vector2 MousePos; 

    public override void _Ready()
    {
        Connect("id_pressed", this, "OptionPressed"); 
        Connect("popup_hide", this, "AboutToHide"); 

        AddItem("Add Input", 0); 
        AddItem("Add Output", 1); 

        Node = GD.Load<PackedScene>("res://Src/Node.tscn"); 
        Global = (Global)GetTree().Root.GetNode("Global"); 
    }

    //Setting Input
    public override void _Input(InputEvent Event)
    {
        if (Event.IsActionPressed("RightClick"))
        {
            SetPosition(GetGlobalMousePosition()); 
            Popup_();
            for (int IONodesIdx = 0; IONodesIdx < Global.Nodes.GetChildren().Count; IONodesIdx++)
            {
                ColorRect IONode = (ColorRect) Global.Nodes.GetChild(IONodesIdx);
                IONode.MouseFilter = Control.MouseFilterEnum.Ignore;
            }
            MousePos = GetViewport().GetMousePosition(); 
        }
    }
    public void OptionPressed(int id)
    {
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
            case 3:
                AutoSort();
                break;
        }
    }

    public void AboutToHide()
    {
        GD.Print("CHANGED");
        for (int IONodesIdx = 0; IONodesIdx < Global.Nodes.GetChildren().Count; IONodesIdx++)
        {
            ColorRect IONode = (ColorRect) Global.Nodes.GetChild(IONodesIdx);
            IONode.MouseFilter = Control.MouseFilterEnum.Stop;
        }
    }

    private void AddNode(bool input = false)
    {
        IONode NewNode = (IONode)Node.Instance();
        NewNode.SetPosition(-(new Vector2(1550, 1080)/2) * Global.Camera.Zoom +
                            Global.Camera.Offset + MousePos * Global.Camera.Zoom);
        NewNode.Input = input;
        Global.Nodes.AddChild(NewNode);
    }

    private void DeleteSelected()
    {
        for (int ChildIndex = 0; ChildIndex < Global.SelectedNodes.Count; ChildIndex++)
            Global.SelectedNodes[ChildIndex].QueueFree();
        Global.SelectedNodes.Clear();
        Global.Selected = false;
        Global.RightClickMenu.ChangeItem();
    }

    private void AutoSort()
    {
        Vector2 FirstPosition = new Vector2(100000, 100000);
        int Input = 0;
        int Output = 0;
        Vector2 IODistance = new Vector2(100, 60);
        for (int ChildIndex = 0; ChildIndex < Global.SelectedNodes.Count; ChildIndex++)
        {
            IONode Child = Global.SelectedNodes[ChildIndex];
            if (FirstPosition == new Vector2(100000, 100000))
                FirstPosition = Child.GetPosition();
            Child.SetPosition(Child.Input ? new Vector2(FirstPosition.x, FirstPosition.y + IODistance.y * Input)
                              : new Vector2(FirstPosition.x + IODistance.x, FirstPosition.y + IODistance.y * Output));
            Input += Convert.ToInt16(Child.Input);
            Output += Convert.ToInt16(!Child.Input);
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
