import java.util.*;

//Reference: http://enos.itcollege.ee/~ylari/I231/Huffman.java

/**
 * Prefix codes and Huffman tree.
 * Tree depends on source data.
 */
public class Huffman {

   private Map<Byte, Leaf> encodingTable;

   private int length = 0;


   /** Constructor to build the Huffman code for a given bytearray.
    * @param original source data
    */
   Huffman (byte[] original) {
      int[] frequencies = new int[256]; // all ASCII characters
      encodingTable = new HashMap<>();

      // count occurrences
      for (byte aByte : original){
         frequencies[aByte]++;
      }

      // queue of all leaves
      PriorityQueue<Tree> priorityQueue = new PriorityQueue<>();
      for (int i = 0; i < frequencies.length; i++){
         if (frequencies[i] > 0){
            priorityQueue.add(new Leaf(frequencies[i], (byte) i));
         }
      }

      // create one tree from all the leaves in queue
      Tree root;
      // if size is 1 then the only leaf is the root
      if (priorityQueue.size() == 1){
         root = priorityQueue.poll();
      } else {
         while (priorityQueue.size() > 1){
               priorityQueue.add(new Node(priorityQueue.poll(), priorityQueue.poll()));
         }
         root = priorityQueue.poll();
      }
      // fill encodingTable with byte(character), leaf pairs
      code(root, new StringBuilder());
   }

   /** Length of encoded data in bits. 
    * @return number of bits
    */
   public int bitLength() {
      return length;
   }


   /** Encoding the byte array using this prefixcode.
    * @param origData original data
    * @return encoded data
    */
   public byte[] encode (byte [] origData) {
      StringBuilder stringBuilder = new StringBuilder();

      //create 010010.. string according to encodingTable values and given byte array
      for (byte b : origData)
         stringBuilder.append(encodingTable.get(b).code);

      length = stringBuilder.length(); // stringBuilder holds given byte array in string of bits form
      List<Byte> bytes = new ArrayList<>();

      while (stringBuilder.length() > 0) {
         while (stringBuilder.length() < 8)
            stringBuilder.append('0'); // append needed amount of 0's to allow another byte creation
         String str = stringBuilder.substring(0, 8); // take first 8 characters(0101..) -> 8 bits = 1 byte
         bytes.add((byte) Integer.parseInt(str, 2)); // bits to byte
         stringBuilder.delete(0, 8);
      }

      byte[] ret = new byte[bytes.size()]; // List of bytes to byte array
      for (int i = 0; i < bytes.size(); i++)
         ret[i] = bytes.get(i);
      return ret;
   }

   /** Decoding the byte array using this prefixcode.
    * @param encodedData encoded data
    * @return decoded data (hopefully identical to original)
    */
   public byte[] decode (byte[] encodedData) {
      StringBuilder stringBuilder = new StringBuilder();

      // bytes to bits
      for (byte anEncodedData : encodedData)
         stringBuilder.append(String.format("%8s", Integer.toBinaryString(anEncodedData & 0xFF)).replace(' ', '0'));

      String str = stringBuilder.substring(0, length); // cut trailing zeros

      List<Byte> bytes = new ArrayList<>();
      stringBuilder.setLength(0);

      while (str.length() > 0) {
         stringBuilder.append(str.substring(0, 1));
         str = str.substring(1); // remove taken bit from other bits
         // check encodingTable for bit sequence and find according byte that is bound with the bit sequence
         for (Leaf leaf : encodingTable.values()) {
            if (leaf.code.equals(stringBuilder.toString())) {
               bytes.add(leaf.value);
               stringBuilder.setLength(0);
               break;
            }
         }
      }
      byte[] ret = new byte[bytes.size()]; // list of bytes to byte array
      for (int i = 0; i < bytes.size(); i++)
         ret[i] = bytes.get(i);
      return ret;
   }

   /** Finds the code for each character in Huffman tree and adds it to encodingTable. */
   private void code(Tree tree, StringBuilder prefix) {
      assert tree != null;

      if (tree instanceof Leaf) {
         Leaf leaf = (Leaf) tree;
         leaf.code = (prefix.length() > 0) ? prefix.toString() : "0"; // prefix length 0 indicates that the leaf has no nodes | 1 leaf tree
         encodingTable.put(leaf.value, leaf); // add byte and according leaf to encodingTable
      } else if (tree instanceof Node) {
         Node node = (Node) tree;
         prefix.append('0');
         code(node.left, prefix); // iteration over left subtree
         prefix.deleteCharAt(prefix.length() - 1); // step out of left subtree
         prefix.append('1');
         code(node.right, prefix); // iteration over right subtree
         prefix.deleteCharAt(prefix.length() - 1); // step out of right subtree
      }
   }

   /** Represents Huffman tree. */
   abstract class Tree implements Comparable<Tree> {

      /** The frequency of this tree. */
      public final int frequency;

      public Tree(int frequency) {
         this.frequency = frequency;
      }

      /**
       * This is an overriding method.
       * @see java.lang.Comparable#compareTo(java.lang.Object)
       * {@inheritDoc}
       */
      @Override
      public int compareTo(Tree tree) {
         return frequency - tree.frequency;
      }

   }

   /** Represents Huffman leaf. */
   class Leaf extends Tree {

      /** The byte this leaf represents. */
      public final byte value;

      /** Code that the byte will be encoded by. */
      public String code;

      /**
       * Constructor to create a leaf.
       * @param frequency amount of occurrences of byte
       * @param value byte value
       */
      public Leaf(int frequency, byte value) {
         super(frequency);
         this.value = value;
      }

   }

   /** Represents Huffman node. */
   class Node extends Tree {

      public final Tree left, right; // subtrees

      /**
       * Constructor to create a tree node.
       * @param left left subtree
       * @param right right subtree
       */
      public Node(Tree left, Tree right) {
         super(left.frequency + right.frequency);
         this.left = left;
         this.right = right;
      }

   }

   /** Main method. */
   public static void main (String[] params) {
      String tekst = "ABCD";
      byte[] orig = tekst.getBytes();
      Huffman huf = new Huffman (orig);
      byte[] kood = huf.encode (orig);
      byte[] orig2 = huf.decode (kood);
      // must be equal: orig, orig2
      System.out.println (Arrays.equals (orig, orig2));
      int lngth = huf.bitLength();
      System.out.println ("Length of encoded data in bits: " + lngth);
   }


}

