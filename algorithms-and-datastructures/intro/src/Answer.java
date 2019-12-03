import java.util.*;

public class Answer {

   public static void main (String[] param) {

       try{
            System.out.println(Double.toString(34));
        }catch (NumberFormatException e){
            e.printStackTrace();
        }
      // conversion double -> String

       System.out.println(Integer.parseInt("1"));
      // conversion String -> int
       //long t0 = System.nanoTime();
       //try {
        //   Thread.sleep(3000);
       //} catch (InterruptedException e) {
        //   e.printStackTrace();
       //}
       //long t1 = System.nanoTime();
       //System.out.println("Vahe on: " + (t1/t0)/1000000 + " ms");
       // "hh:mm:ss"

       System.out.println(Math.cos(Math.toDegrees(45)));
      // cos 45 deg

      // table of square roots

      String firstString = "ABcd12";
      String result = reverseCase (firstString);
      System.out.println ("\"" + firstString + "\" -> \"" + result + "\"");

      // reverse string

      String s = "How  many	 words   here";
      int nw = countWords (s);
      System.out.println (s + "\t" + String.valueOf (nw));

      // pause. COMMENT IT OUT BEFORE JUNIT-TESTING!

      final int LSIZE = 100;
      ArrayList<Integer> randList = new ArrayList<Integer> (LSIZE);
      Random generaator = new Random();
      for (int i=0; i<LSIZE; i++) {
         randList.add (Integer.valueOf (generaator.nextInt(1000)));
      }

      // minimal element

      // HashMap tasks:
      //    create
      //    print all keys
      //    remove a key
      //    print all pairs

      System.out.println ("Before reverse:  " + randList);
      reverseList (randList);
      System.out.println ("After reverse: " + randList);

      System.out.println ("Maximum: " + maximum (randList));
   }

   /** Finding the maximal element.
    * @param a Collection of Comparable elements
    * @return maximal element.
    * @throws NoSuchElementException if <code> a </code> is empty.
    */
   static public <T extends Object & Comparable<? super T>>
         T maximum (Collection<? extends T> a) 
            throws NoSuchElementException {
       return Collections.max(a);
   }//maximum

   /** Counting the number of words. Any number of any kind of
    * whitespace symbols between words is allowed.
    * @param text text
    * @return number of words in the text
    */
   public static int countWords (String text) {

       return new StringTokenizer(text).countTokens();

   } //countWords

   /** Case-reverse. Upper -> lower AND lower -> upper.
    * @param s string
    * @return processed string
    */
   public static String reverseCase (String s) {

      if (s.length() != 0){

          StringBuilder result = new StringBuilder();

          for(int i = 0; i < s.length(); i++){

              char character = s.charAt(i);
              if(Character.isUpperCase(character)){
                    result.append(String.valueOf(character).toLowerCase());
              }else if(Character.isLowerCase(character)){
                  result.append(String.valueOf(character).toUpperCase());
              }
          }//for

          return result.toString();
      }//if

      return s;
   }//reverseCase

   /** List reverse. Do not create a new list.
    * @param list list to reverse
    */
   public static <T extends Object> void reverseList (List<T> list)
      throws UnsupportedOperationException {
       Collections.reverse(list);
   }//reverseList
}
