"""Animal drawing."""


import turtle


def body():
    """Function to draw shape of the body of an animal."""
    turtle.home()
    turtle.circle(120, 180)
    turtle.right(45)
    turtle.fd(30)
    for i in range(2):
        turtle.left(90)
        turtle.fd(30)
        turtle.right(90)
        turtle.fd(30)
    turtle.left(90)
    turtle.fd(30)
    turtle.right(45)
    turtle.circle(120, 180)


def legs():
    """Function to draw legs for an animal."""
    turtle.home()
    turtle.fd(120)
    turtle.circle(50, 180)
    turtle.up()
    turtle.fd(365)
    turtle.down()
    turtle.circle(50, 180)
    turtle.fd(100)


def eyes_nose_mouth():
    """Function to draw eyes, nose and mouth for an animal."""
    #  Eye drawing
    turtle.up()  # left eye
    turtle.goto(0, 170)
    turtle.down()
    turtle.dot(size=25)
    turtle.goto(-5, 170)
    turtle.dot(size=15)
    turtle.dot("white")

    turtle.up()  # right eye
    turtle.goto(-130, 170)
    turtle.down()
    turtle.dot(size=35)
    turtle.goto(-135, 170)
    turtle.dot("white")

    #  Nose drawing
    turtle.up()
    turtle.goto(-60, 120)
    turtle.down()
    turtle.fillcolor("pink")
    turtle.begin_fill()
    turtle.circle(15)
    turtle.end_fill()
    turtle.up()

    #  Mouth drawing
    turtle.goto(-45, 80)
    turtle.down()
    turtle.circle(50, 75)
    turtle.up()


def ears():
    """Function to draw ears for an animal."""
    turtle.right(50)  # left ear
    turtle.fd(118)
    turtle.down()
    turtle.circle(76, 244)
    turtle.right(90)
    turtle.up()
    turtle.fd(128)  # right ear
    turtle.right(90)
    turtle.fd(5)
    turtle.down()
    turtle.circle(50, 233)
    turtle.up()


def tail():
    """Function to draw the tail for an animal."""
    turtle.home()
    turtle.fillcolor("Grey")
    turtle.fd(120)
    turtle.left(90)
    turtle.fd(100)
    turtle.down()
    turtle.right(90)
    turtle.circle(150, 90)
    turtle.begin_fill()
    turtle.circle(30)
    turtle.right(90)
    turtle.end_fill()
    turtle.up()
    turtle.home()


def main():
    """Draw the complete picture of the animal by using all the functions."""
    turtle.speed(0)
    turtle.pencolor("black")
    body()
    legs()
    eyes_nose_mouth()
    ears()
    tail()
    turtle.done()


main()
