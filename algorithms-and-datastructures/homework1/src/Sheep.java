import java.util.Arrays;

/** https://stackoverflow.com/questions/25852541/java-rarrange-enum-array */

public class Sheep {

    /** Animals that can be used */
    enum Animal {sheep, goat, cow}

    /** Testing method */
    public static void main(String[] param) {
        Animal animal[] = new Animal[20];
        for (int i = 0; i < 20; i++){
            if (i == 1 || i == 5 || i == 7){
                animal[i] = Animal.sheep;
            }else if (i % 2 == 0){
                animal[i] = Animal.cow;
            }else {
                animal[i] = Animal.goat;
            }
        }
        System.out.println(Arrays.toString(animal));
        System.out.println("reorder");
        reorder(animal);
        System.out.println(Arrays.toString(animal));
    } //main

    /**
     * Rearranges given array so that the goats are at the beginning and sheep at the end
     * @param animals Array to sort
     */
    public static void reorder(Animal[] animals) {

        int beginning = 0;
        int end = animals.length;
        int countCow = 0;
        int countSheep = 0;
        int countGoat = 0;
        do {
            if(animals[beginning] == Animal.sheep){
                countSheep++;
                beginning++;
            } else if(animals[beginning] == Animal.goat){
                countGoat++;
                beginning++;
            }else{
                countCow++;
                beginning++;
            }
        } while (beginning != end); //do while

        int i = 0;
        while(i < countGoat){
            animals[i] = Animal.goat;
            i++;
        }

        int  j = 0;
        while (j < countSheep){
            animals[i] = Animal.sheep;
            i++;
            j++;
        }

        j = 0;
        while(j < countCow){
            animals[i] = Animal.cow;
            i++;
            j++;
        }

    } //reorder

} //Sheep