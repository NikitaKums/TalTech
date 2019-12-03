import java.util.*;
import java.util.regex.Pattern;

/** Quaternions. Basic operations. */
public class Quaternion {

   private double a, b, c, d;

   /** Constructor from four double values.
    * @param a real part
    * @param b imaginary part i
    * @param c imaginary part j
    * @param d imaginary part k
    */
   public Quaternion (double a, double b, double c, double d) {
      this.a = a;
      this.b = b;
      this.c = c;
      this.d = d;
   }

   /** Real part of the quaternion.
    * @return real part
    */
   public double getRpart() {
      return this.a;
   }

   /** Imaginary part i of the quaternion. 
    * @return imaginary part i
    */
   public double getIpart() {
      return this.b;
   }

   /** Imaginary part j of the quaternion. 
    * @return imaginary part j
    */
   public double getJpart() {
      return this.c;
   }

   /** Imaginary part k of the quaternion. 
    * @return imaginary part k
    */
   public double getKpart() {
      return this.d;
   }

   /** Conversion of the quaternion to the string.
    * @return a string form of this quaternion: 
    * "a+bi+cj+dk"
    * (without any brackets)
    */
   @Override
   public String toString()
   {
      StringBuilder sb = new StringBuilder();
      sb.append(getRpart());

      if (getIpart() < 0) {
         sb.append(getIpart());
         sb.append("i");
      } else {
         sb.append("+");
         sb.append(getIpart());
         sb.append("i");
      }//i part

      if (getJpart() < 0) {
         sb.append(getJpart());
         sb.append("j");
      } else {
         sb.append("+");
         sb.append(getJpart());
         sb.append("j");
      }//j part

      if (getKpart() < 0) {
         sb.append(getKpart());
         sb.append("k");
      } else {
         sb.append("+");
         sb.append(getKpart());
         sb.append("k");
      }//k part

      //does not return string e.g -1.0+-5.0i+6.0j+-8.0k but returns -1.0-5.0i+6.0j-8.0k
      return sb.toString();
   }

   /** Conversion from the string to the quaternion. 
    * Reverse to <code>toString</code> method.
    * @throws IllegalArgumentException if string s does not represent 
    *     a quaternion (defined by the <code>toString</code> method)
    * @param s string of form produced by the <code>toString</code> method
    * @return a quaternion represented by string s
    */
   public static Quaternion valueOf (String s) {
      //allowed is e.g 1.0-5.0i+7.0j-3.0k
      if (s.trim().isEmpty()){
         throw new RuntimeException("Provided string is empty");
      }
      Pattern pattern = Pattern.compile("-?(\\d+\\.\\d+)[+,-](\\d+\\.\\d+)i[+,-](\\d+\\.\\d+)j[+,-](\\d+\\.\\d+)k");
      boolean matches = pattern.matcher(s).matches();
      if (!matches){
         throw new IllegalArgumentException("String " + s + " does not match the needed format.");
      }
      String[] array = s.split("((?=-)|\\+)");
      double realPart = Double.valueOf(array[0]);
      double iPart = Double.valueOf(array[1].substring(0, array[1].length() - 1));
      double jPart = Double.valueOf(array[2].substring(0, array[2].length() - 1));
      double kPart = Double.valueOf(array[3].substring(0, array[3].length() - 1));
      return new Quaternion(realPart, iPart, jPart, kPart);
   }

   /** Clone of the quaternion.
    * @return independent clone of <code>this</code>
    */
   @Override
   public Object clone() throws CloneNotSupportedException {
      return new Quaternion(getRpart(), getIpart(), getJpart(), getKpart());
   }

   /** Test whether the quaternion is zero. 
    * @return true, if the real part and all the imaginary parts are (close to) zero
    */
   public boolean isZero() {
      double threshold = 0.000001;
      return (Math.abs(0 - this.getRpart()) < threshold) && (Math.abs(0 - this.getIpart()) < threshold) &&
              (Math.abs(0 - this.getJpart()) < threshold) && (Math.abs(0 - this.getKpart()) < threshold);
   }

   /** Conjugate of the quaternion. Expressed by the formula 
    *     conjugate(a+bi+cj+dk) = a-bi-cj-dk
    * @return conjugate of <code>this</code>
    */
   public Quaternion conjugate() {
      return new Quaternion(getRpart(), -1 * getIpart(), -1 * getJpart(), -1 * getKpart());
   }

   /** Opposite of the quaternion. Expressed by the formula 
    *    opposite(a+bi+cj+dk) = -a-bi-cj-dk
    * @return quaternion <code>-this</code>
    */
   public Quaternion opposite() {
      return new Quaternion(-1 * getRpart(), -1 * getIpart(), -1 * getJpart(), -1 * getKpart());
   }

   /** Sum of quaternions. Expressed by the formula 
    *    (a1+b1i+c1j+d1k) + (a2+b2i+c2j+d2k) = (a1+a2) + (b1+b2)i + (c1+c2)j + (d1+d2)k
    * @param q addend
    * @return quaternion <code>this+q</code>
    */
   public Quaternion plus (Quaternion q) {
      if (q != null){
         return new Quaternion(
                 this.getRpart() + q.getRpart(),
                 this.getIpart() + q.getIpart(),
                 this.getJpart() + q.getJpart(),
                 this.getKpart() + q.getKpart());
      }
      throw new RuntimeException("Provided Quaternion is null");
   }

   /** Product of quaternions. Expressed by the formula
    *  (a1+b1i+c1j+d1k) * (a2+b2i+c2j+d2k) = (a1a2-b1b2-c1c2-d1d2) + (a1b2+b1a2+c1d2-d1c2)i +
    *  (a1c2-b1d2+c1a2+d1b2)j + (a1d2+b1c2-c1b2+d1a2)k
    * @param q factor
    * @return quaternion <code>this*q</code>
    */
   public Quaternion times (Quaternion q) {
      if (q != null){
         return new Quaternion(
                 (this.getRpart() * q.getRpart()) - (this.getIpart() * q.getIpart()) - (this.getJpart() * q.getJpart()) - (this.getKpart() * q.getKpart()),
                 (this.getRpart() * q.getIpart()) + (this.getIpart() * q.getRpart()) + (this.getJpart() * q.getKpart()) - (this.getKpart() * q.getJpart()),
                 (this.getRpart() * q.getJpart()) - (this.getIpart() * q.getKpart()) + (this.getJpart() * q.getRpart()) + (this.getKpart() * q.getIpart()),
                 (this.getRpart() * q.getKpart()) + (this.getIpart() * q.getJpart()) - (this.getJpart() * q.getIpart()) + (this.getKpart() * q.getRpart()));
      }
      throw new RuntimeException("Provided Quaternion is empty");
   }

   /** Multiplication by a coefficient.
    * @param r coefficient
    * @return quaternion <code>this*r</code>
    */
   public Quaternion times (double r) {
      return new Quaternion(
              this.getRpart() * r,
              this. getIpart() * r,
              this.getJpart() * r,
              this.getKpart() * r);
   }

   /** Inverse of the quaternion. Expressed by the formula
    *     1/(a+bi+cj+dk) = a/(a*a+b*b+c*c+d*d) + 
    *     ((-b)/(a*a+b*b+c*c+d*d))i + ((-c)/(a*a+b*b+c*c+d*d))j + ((-d)/(a*a+b*b+c*c+d*d))k
    * @return quaternion <code>1/this</code>
    */
   public Quaternion inverse() {
      if (!this.isZero()){
         double a = Math.pow(this.norm(), 2);
         return new Quaternion(
                 this.getRpart() / a,
                 (- this.getIpart()) / a,
                 (- this.getJpart()) / a,
                 (- this.getKpart()) / a);
      }
      throw new RuntimeException("Provided Quaternion is zero");
   }

   /** Difference of quaternions. Expressed as addition to the opposite.
    * @param q subtrahend
    * @return quaternion <code>this-q</code>
    */
   public Quaternion minus (Quaternion q) {
      if (q != null){
         return this.plus(q.opposite());
      }
      throw new RuntimeException("Provided Quaternion is null");
   }

   /** Right quotient of quaternions. Expressed as multiplication to the inverse.
    * @param q (right) divisor
    * @return quaternion <code>this*inverse(q)</code>
    */
   public Quaternion divideByRight (Quaternion q) {
      if (q != null && !q.isZero()){
         return this.times(q.inverse());
      }
      throw new RuntimeException("Provided Quaternion is null.");
   }

   /** Left quotient of quaternions.
    * @param q (left) divisor
    * @return quaternion <code>inverse(q)*this</code>
    */
   public Quaternion divideByLeft (Quaternion q) {
      if (q != null && !q.isZero()){
         return q.inverse().times(this);
      }
      throw new RuntimeException("Provided Quaternion is null.");
   }
   
   /** Equality test of quaternions. Difference of equal numbers
    *     is (close to) zero.
    * @param qo second quaternion
    * @return logical value of the expression <code>this.equals(qo)</code>
    */
   @Override
   public boolean equals (Object qo) {
      if (qo != null){
         return (qo instanceof Quaternion) && this.minus((Quaternion) qo).isZero();
      }
      throw new RuntimeException("Provided object is null");
   }

   /** Dot product of quaternions. (p*conjugate(q) + q*conjugate(p))/2
    * @param q factor
    * @return dot product of this and q
    */
   public Quaternion dotMult (Quaternion q) {
      if (q != null){
         return this.times(q.conjugate()).plus(q.times(this.conjugate())).times(0.5);
      }
      throw new RuntimeException("Provided Quaternion as the argument is null.");
   }

   /** Integer hashCode has to be the same for equal objects.
    * @return hashcode
    */
   @Override
   public int hashCode() {
      return Objects.hash(this.getRpart(), this.getIpart(), this.getJpart(), this.getKpart());
   }

   /** Norm of the quaternion. Expressed by the formula 
    *     norm(a+bi+cj+dk) = Math.sqrt(a*a+b*b+c*c+d*d)
    * @return norm of <code>this</code> (norm is a real number)
    */
   public double norm() {
      return Math.sqrt(
              this.getRpart() * this.getRpart() +
              this.getIpart() * this.getIpart() +
              this.getJpart() * this.getJpart() +
              this.getKpart() * this.getKpart());
   }

   /** Main method for testing purposes. 
    * @param arg command line parameters
    */
   public static void main (String[] arg) {
      Quaternion arv1 = new Quaternion (-1., 1, 2., -2.);
      if (arg.length > 0)
         arv1 = valueOf (arg[0]);
      System.out.println ("first: " + arv1.toString());
      System.out.println ("real: " + arv1.getRpart());
      System.out.println ("imagi: " + arv1.getIpart());
      System.out.println ("imagj: " + arv1.getJpart());
      System.out.println ("imagk: " + arv1.getKpart());
      System.out.println ("isZero: " + arv1.isZero());
      System.out.println ("conjugate: " + arv1.conjugate());
      System.out.println ("opposite: " + arv1.opposite());
      System.out.println ("hashCode: " + arv1.hashCode());
      Quaternion res = null;
      try {
         res = (Quaternion)arv1.clone();
      } catch (CloneNotSupportedException e) {};
      System.out.println ("clone equals to original: " + res.equals (arv1));
      System.out.println ("clone is not the same object: " + (res!=arv1));
      System.out.println ("hashCode: " + res.hashCode());
      res = valueOf (arv1.toString());
      System.out.println ("string conversion equals to original: " 
         + res.equals (arv1));
      Quaternion arv2 = new Quaternion (1., -2.,  -1., 2.);
      if (arg.length > 1)
         arv2 = valueOf (arg[1]);
      System.out.println ("second: " + arv2.toString());
      System.out.println ("hashCode: " + arv2.hashCode());
      System.out.println ("equals: " + arv1.equals (arv2));
      res = arv1.plus (arv2);
      System.out.println ("plus: " + res);
      System.out.println ("times: " + arv1.times (arv2));
      System.out.println ("minus: " + arv1.minus (arv2));
      double mm = arv1.norm();
      System.out.println ("norm: " + mm);
      System.out.println ("inverse: " + arv1.inverse());
      System.out.println ("divideByRight: " + arv1.divideByRight (arv2));
      System.out.println ("divideByLeft: " + arv1.divideByLeft (arv2));
      System.out.println (valueOf("-1.0-2.0i-3.0j-4.0i"));
   }
}
// end of file
