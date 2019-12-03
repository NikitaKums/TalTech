import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.regex.Pattern;
//https://git.wut.ee/i231/home5/raw/master/src/Node.java

public class Node {

   private String name; // Node name.
   private Node firstChild; // Pointer to first child.
   private Node nextSibling; // Pointer to next sibling.

   /**
    * Constructor.
    * @param n Name of the node.
    * @param d Pointer to the first child of the node.
    * @param r Pointer to the next sibling of the node.
    */
   Node (String n, Node d, Node r) {
      name = n;
      firstChild = d;
      nextSibling = r;
   }

   /**
    * Creates a tree using string in postfix representation.
    * @param s Postfix representation as a string of a tree.
    * @return Node
    */
   public static Node parsePostfix (String s) {
      if (s.trim().isEmpty()) {
         throw new RuntimeException("Empty string supplied.");
      }
      else if (Pattern.compile("[(]+[)]+").matcher(s).find()){ // can be multiple brackets
         throw new RuntimeException("String contains empty subtree. Invalid string: " + s);
      }
      else if (Pattern.compile("\\({2}").matcher(s).find() && Pattern.compile("\\){2}").matcher(s).find()){ // 2 or more (( )) brackets
         throw new RuntimeException("String contains multiple brackets for no reason. Invalid string: " + s);
      }
      else if (Pattern.compile("[ ]+").matcher(s).find()){ // one or more whitespace
         throw new RuntimeException("String contains whitespace(s). Invalid string: " + s);
      }
      else if (Pattern.compile(",{2}").matcher(s).find()){ // 2 or more commas together
         throw new RuntimeException("Too many commas in string. Invalid string: " + s);
      }
      else if (s.contains(",") && !s.contains("(") && !s.contains(")")) { // just (a) comma(s), no brackets
         throw new RuntimeException("Comma without brackets. Invalid string: " + s);
      }
      //https://stackoverflow.com/questions/275944/how-do-i-count-the-number-of-occurrences-of-a-char-in-a-string
      else if (s.codePoints().filter(temp -> temp == '(').count() != s.codePoints().filter(temp -> temp == ')').count()){ // bracket amount counter
         throw new RuntimeException("Uneven amount of brackets. Invalid string: " + s);
      }
      else if (Pattern.compile("\\).+[^,]\\(").matcher(s).find()){ // comma between closing and opening bracket missing
         throw new RuntimeException("Brackets closing and opening without comma, which should not be possible. Invalid string: " + s);
      }

      String[] temp = s.split("");

      List<Integer> bracketList = new ArrayList<>();
      List<Integer> commaList = new ArrayList<>();
      int openingBracketCounter = 0;
      int closingBracketCounter = 0;
      boolean flag = false;

      // get indexes that are inside of brackets
      for (int a = 0; a < temp.length; a++){
         if (temp[a].equals(")")){
            bracketList.add(a);
            closingBracketCounter += 1;
            if (openingBracketCounter == closingBracketCounter) flag = false;
         }
         else if (temp[a].equals("(")){
            openingBracketCounter += 1;
            bracketList.add(a);
            flag = true;
         }
         else if (flag) bracketList.add(a);
         if (temp[a].equals(",")){
            commaList.add(a);
         }
      }
      // comma can only be between brackets
      if (!bracketList.containsAll(commaList)){
         throw new RuntimeException("Comma can only be between brackets. Invalid string: " + s);
      }

      Node node = new Node(null, null, null); // actually the root (highest) node
      LinkedList<Node> linkedList = new LinkedList<>();
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < temp.length; i++){
         switch (temp[i]) {

            case "(":
               if (temp[i + 1].equals(",")) { // comma as node name checker
                  throw new RuntimeException("Comma after opening bracket. Should not be possible.  Invalid string: " + s);
               } //throw Exception if
               linkedList.push(node);
               node.firstChild = new Node(null, null, null); //create lower child
               node = node.firstChild;
               break;

            case ",":
               node.nextSibling = new Node(null, null, null); //create right sibling
               node = node.nextSibling;
               break;

            case ")":  //get last inserted node from stack since ) completes the current node
               node = linkedList.pop();
               break;

            default:
               if (node.name == null) {
                  node.name = temp[i]; // set name to the node
               } else {
                  sb.append(node.name); // node has name but the name might not be only one character so append next
                  sb.append(temp[i]);   // char to the current node name
                  node.name = sb.toString();
                  sb.setLength(0);
               }
               break;
         }
      }
      return node;
   }

   /**
    * Turns this tree Node into left parenthetic representation.
    * @return string
    */
   public String leftParentheticRepresentation() {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.append(this.name);
      //go down while node has next child, when no more children then check for right siblings
      if (this.firstChild != null){
         stringBuilder.append("(");
         stringBuilder.append(this.firstChild.leftParentheticRepresentation());
         stringBuilder.append(")");
      }
      if (this.nextSibling != null){
         stringBuilder.append(",");
         stringBuilder.append(this.nextSibling.leftParentheticRepresentation());
      }
      return stringBuilder.toString();
   }

   /**
    * Test class.
    */
   public static void main (String[] param) {
      String test = "((a)1,(ee)f,3,(b)2)c";
      Node g = Node.parsePostfix(test);
      System.out.println(g.leftParentheticRepresentation());
   }
}