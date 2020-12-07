tool
extends Node2D

export var color = Color()
export var size = 16
export var zoom = 0
export var offset = Vector2(0, 0)
export var grid_size = Vector2()

func _draw():
    if size == 0:
        size = 1

    var temp_size = size + zoom

    var wrap_offset = Vector2(wrapf(offset.x, 0, temp_size), wrapf(offset.y, 0, temp_size))

    var ceil_x = grid_size.x / temp_size
    var ceil_y = grid_size.y / temp_size

    for i in ceil_y:
        var start_x = Vector2(0, (i * temp_size) + wrap_offset.y)
        var end_x = Vector2(grid_size.x, (i * temp_size) + wrap_offset.y)
        draw_line(start_x, end_x, color, 2)

    for i in ceil_x:
        var start_y = Vector2((i * temp_size) + wrap_offset.x, 0)
        var end_y = Vector2((i * temp_size) + wrap_offset.x + 0.01, grid_size.y)
        draw_line(start_y, end_y, color, 2)
