import java.util.LinkedList;
import java.util.StringTokenizer;

/**
 * Calculation of expression in Polish Reversed Notation using linked list.
 */

public class LongStack {

    /** Linked list. */
    private LinkedList<Long> linkedList;

    /**
     * Test class
     */
    public static void main (String[] argum) {
        System.out.println(LongStack.interpret("-234 -"));
    }

    /**
     * Constructor.
     */
    LongStack() {

        linkedList = new LinkedList<>();
    }

    /**
     * Makes a copy of current stack.
     * @return Copy of current stack
     * @throws CloneNotSupportedException
     */
    @Override
    public Object clone() throws CloneNotSupportedException {
        LongStack temp = new LongStack();
        int size = linkedList.size();
        if(size > 0){
            for (int i = 0; i < size; i++){
                temp.linkedList.add(i, linkedList.get(i));
            }
        }
        return temp;
    }//clone

    /**
     * Checks if the stack is empty.
     * @return true
     *              if stack is empty
     */
    public boolean stEmpty() {
        return linkedList.size() < 1;
    }//stEmpty

    /**
     * Adds long value to the stack.
     * @param a
     *          value to add to the stack.
     */
    public void push (long a) {
        linkedList.add(a);
    }//push

    /**
     * Gets topmost value in the stack.
     * @return topmost value from the stack
     */
    public long pop() {
        if (stEmpty()){
            throw new RuntimeException("Stack underflow");
        }
        long temp = linkedList.getLast();
        linkedList.removeLast();
        return temp;
    } // pop

    /**
     * Does an arithmetic operation between two topmost value of the stack.
     * @param s arithmetic operation
     */
    public void op (String s) {
        if (!s.equals("+") && !s.equals("-") && !s.equals("*") && !s.equals("/")){
            throw new RuntimeException("Invalid operation: " + s);
        }
        if(linkedList.size() < 1)
        {
            throw new RuntimeException("Too few arguments for calculation. Operation: " + s);
        }

        long secondNumber;
        long firstNumber;

        try {
            secondNumber = pop();
            firstNumber = pop();
        } catch (RuntimeException e){
            throw new RuntimeException(e.getMessage() + ". Not enough elements to perform: " + s + " ");
        }

        switch (s){
            case "+":
                push(firstNumber + secondNumber);
                break;
            case "-":
                push(firstNumber - secondNumber);
                break;
            case "*":
                push(firstNumber * secondNumber);
                break;
            case "/":
                push(firstNumber / secondNumber);
                break;
        }//switch
    }//op

    /**
     * Reads topmost element of the stack without removing it.
     * @return topmost element of the stack.
     */
    public long tos() {
        if (stEmpty()){
            throw new RuntimeException("Stack underflow");
        }
        return linkedList.getLast();
    }//tos

    /**
     * Checks whether two stacks are equal.
     * @param o Object whose stack is compared.
     * @return true
     *              if two stack are equal.
     */
    @Override
    public boolean equals (Object o) {
        if(((LongStack)o).linkedList.size() != linkedList.size()){
            return false;
        }
        for(int i = 0; i < linkedList.size(); i++){
            if (((LongStack)o).linkedList.get(i) != linkedList.get(i)){
                return false;
            }
        }
        return true;
    }//equals

    /**
     * Converts stack to a string.
     * @return stack values as string.
     */
    @Override
    public String toString() {
        if (stEmpty()){
            return " ";
        }
        StringBuilder stringBuilder = new StringBuilder();
        for(int i = 0; i < linkedList.size(); i++){
            stringBuilder.append(linkedList.get(i));
        }
        return stringBuilder.toString();
    }//toString

    /**
     * Calculates value of arithmetic expression in Polish Reversed Notation.
     * @param pol arithmetic expression in Polish Reversed Notation.
     * @return result of the calculation.
     */
    public static long interpret (String pol) {
        if (pol.trim().isEmpty()){
            throw new RuntimeException("Arithmetic expression is missing/empty");
        }
        LongStack longStack = new LongStack();
        StringTokenizer stringTokenizer = new StringTokenizer(pol);
        try {
            while(stringTokenizer.hasMoreTokens()) {
                String current = stringTokenizer.nextToken();
                try {
                    longStack.push(Long.parseLong(current));
                } catch (NumberFormatException e) {
                    longStack.op(current);
                }//try-catch
            }//while
        }catch (RuntimeException e){
            throw new RuntimeException(e.getMessage() + ". Invalid string: " + pol);
        }//try-catch

        if(longStack.linkedList.size() > 1){
            throw new RuntimeException("Too many elements leftover in stack." + " Invalid string: " + pol);
        }

        return longStack.tos();
    }//interpret

}//LongStack