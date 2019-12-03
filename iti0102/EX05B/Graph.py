"""Savings graph."""


import calendar
import locale
import turtle
import random


def color_bars():
    """Fill bars with random colors from list."""
    colors_list = ["Hot Pink", "Blue", "Red", "Light Cyan", "Orange", "Purple", "Thistle", "Misty Rose", "Maroon"]
    color = random.choice(colors_list)
    return color


def get_month_name(month_no):
    """Convert a month's numeric value into its Estonian name as a string."""
    locale.setlocale(locale.LC_TIME, "et-EE")
    return calendar.month_name[month_no].capitalize()


def draw_graph(pencil: turtle.Turtle, posx, posy, data_list):
    """
    Draw a bar chart-style graph and a legend describing the data.

    Arguments:
    pencil -- the turtle object used for drawing
    posx, posy -- start coordinates of the drawing
    data_list -- the data shown on the graph as a list of numeric values
    """
    pencil.up()
    pencil.goto(posx, posy)
    pencil.down()
    draw_legend(pencil, data_list)
    pencil.goto(posx, posy)
    pencil.down()
    draw_bars(pencil, data_list)
    pencil.goto(posx, posy)


def draw_legend(pencil: turtle.Turtle, data_list):
    """
    Draw a legend box for a graph.

    The legend box contains text describing the data.
    Arguments:
    pencil -- the turtle object used for drawing
    data_list -- the data as a list of numeric values
    """
    for i in range(4):
        pencil.fd(250)
        pencil.left(90)
        if i == 3:
            pencil.up()
            pencil.goto(pencil.xcor() + 10, pencil.ycor() + 230)
    for i in range(13):
        if i == 0 or data_list[i] == 0:
            continue
        else:
            pencil.write(f"{get_month_name(i)}: {data_list[i]}€")
            pencil.goto(pencil.xcor(), pencil.ycor() - 17)
    pencil.write(f"Kokku: {sum(data_list)}")
    if sum(data_list) > 480:
        pencil.goto(pencil.xcor(), pencil.ycor() - 17)
        pencil.write("Saad minna kontserdile!")
    else:
        pencil.write("Kontserdile minna ei saa!")


def draw_bars(pencil: turtle.Turtle, data_list):
    """
    Draw a bar chart.

    Arguments:
    pencil -- the turtle object used for drawing
    data_list -- the data as a list of numeric values
    """
    i = 0
    pencil.backward(400)
    while i != len(data_list):
        if data_list[i] == 0:
            i += 1
            continue
        else:
            pencil.fillcolor(color_bars())
            pencil.begin_fill()
            pencil.left(90)
            pencil.fd(data_list[i])
            pencil.right(90)
            pencil.fd(30)
            pencil.right(90)
            pencil.fd(data_list[i])
            pencil.left(90)
            pencil.end_fill()
            i += 1


def main():
    """Set up the turtle window and start the drawing process."""
    saving_list = [0, 100, 50, 150, 300, 200, 100, 50, 150, 300, 200.09, 200, 100]
    john_the_turtle = turtle.Turtle()
    john_the_turtle.speed(0)
    draw_graph(john_the_turtle, 50, 0, saving_list)
    turtle.done()


main()
