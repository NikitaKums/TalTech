package tdd.greeting;

import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class GreeterTest {

    Greeter greeter;

    @Before
    public void setUp(){
        greeter = new Greeter();
    }

    @Test
    public void should_return_hello_name_when_name_is_given(){
        String name = "Nikita";
        // act
        String greeting = greeter.greet(name);
        // assert
        assertEquals("Hello, " + name + ".", greeting);
    }

    @Test
    public void should_return_hello_my_friend_when_name_is_null(){
        String name = null;
        String greeting = greeter.greet(name);
        assertEquals("Hello, my friend.", greeting);
    }

    @Test
    public void should_return_uppercase_when_name_is_uppercase(){
        String name = "NIKITA";
        String greeting = greeter.greet(name);
        assertEquals("HELLO, NIKITA!", greeting);
    }

    @Test
    public void should_return_hello_name_and_name_when_name_is_array(){
        String[] name = new String[]{"Jill", "Jane"};
        String greeting = greeter.greet(name);
        assertEquals("Hello, Jill and Jane.", greeting);
    }

    @Test
    public void should_support_arbitrary_number_of_names_in_array(){
        String[] name = new String[]{"Jill", "Jane", "Charlotte"};
        String greeting = greeter.greet(name);
        assertEquals("Hello, Jill, Jane, and Charlotte.", greeting);
    }

    @Test
    public void should_support_arbitrary_number_of_names_in_array_with_uppercase_shouting(){
        String[] name = new String[]{"Amy", "BRIAN", "Charlotte"};
        String greeting = greeter.greet(name);
        assertEquals("Hello, Amy and Charlotte. AND HELLO BRIAN!", greeting);
    }

    @Test
    public void should_split_names_containing_comma(){
        String[] name = new String[]{"Bob", "Charlie, Dianne"};
        String greeting = greeter.greet(name);
        assertEquals("Hello, Bob, Charlie, and Dianne.", greeting);
    }

    @Test
    public void should_not_split_intentional_comma_in_name(){
        String[] name = new String[]{"Bob", "\"Charlie, Dianne\""};
        String greeting = greeter.greet(name);
        assertEquals("Hello, Bob and Charlie, Dianne.", greeting);
    }
}