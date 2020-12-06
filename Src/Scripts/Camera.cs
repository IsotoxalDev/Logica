using Godot; // Including Godot Namespace
using System; // Including System Namespace

public class Camera : Godot.Camera2D
{
    private Tween zoomTween; //Variable for importing zoom tween

    private bool drag = false; //Variable to check if dragging/panning is allowed

    private Vector2 minZoom = new Vector2(0.1f, 0.1f); //Minimum threshold for zooming
    private Vector2 maxZoom = new Vector2(1.5f, 1.5f); //Maximum threshold for zooming

    public override void _Ready()//_Ready function
    {
        zoomTween = (Tween) GetNode("zoomTween"); //Getting Node zoomTween
    }

    public override void _Input(InputEvent Event)//_Input function
    {

        if (Event.IsAction("ZoomIn")) //Checking if action is zoom in
            _zoom(-1); //Calling the _zoom function

        else if (Event.IsAction("ZoomOut")) // Checking if action is zoom out
            _zoom(1); //Calling the _zoom function

        //Checking if space or middle mouse button is pressed
        if (Event.IsActionPressed("Space") || Event.IsActionPressed("MiddleMouseButton"))
            drag = true; // If space or middle mouse button pressed then set drag to true

        //Checking if space or middle mouse button is released
        else if (Event.IsActionReleased("Space") || Event.IsActionReleased("MiddleMouseButton"))
            drag = false; // If space or middle mouse button released then set drag to false

        //checking if drag is true and mouse is moving
        if (drag && Event.GetType() == typeof(InputEventMouseMotion))
        {
            //Setting newEvent to event by explicitly convertong it to Input Event Mouse Motion
            InputEventMouseMotion NewEvent = (InputEventMouseMotion) Event;
            Offset -= NewEvent.Relative * Zoom; //Setting offset relative to the mouse motion
        }
    }

    private void _zoom(int Dir) //_zoom function
    {
        var NewZoom = Zoom + Zoom * Dir/10; //Setting var newZoom to a new zoom value
        if (NewZoom > minZoom && NewZoom < maxZoom) //Checking if zoom is under the threshold
        {
        // interpolating the zoom property
        zoomTween.InterpolateProperty(this, "zoom", Zoom, NewZoom, 0.05f, 0, 0);
        zoomTween.Start(); //starting zoom
        }
    }
}
