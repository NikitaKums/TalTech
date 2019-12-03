package com.example.myapp;

import android.util.Log;
import android.widget.Button;

import java.text.DecimalFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.List;
import java.util.Random;

public class HelperMethods {
    static private final String TAG = HelperMethods.class.getSimpleName();


    public String getStringTimeFromSeconds(int timerTime){
        Log.d(TAG, "getStringTimeFromSeconds");

        Calendar calendar = Calendar.getInstance();
        calendar.set(Calendar.HOUR_OF_DAY, 0);
        calendar.set(Calendar.MINUTE, 0);
        calendar.set(Calendar.MILLISECOND, 0);
        calendar.set(Calendar.SECOND, timerTime);
        return new SimpleDateFormat("HH:mm:ss").format(calendar.getTime());
    }

    public double calculateScore(double score, double movesDone){
        Log.d(TAG, "calculateScore");

        DecimalFormat df = new DecimalFormat("#.##");
        double temp = score - 0.5 - movesDone / 20;
        score = temp < 0 ? 0 : temp;

        return Double.valueOf(df.format(score));
    }

    public void setButtonText(List<Button> Buttons){
        Log.d(TAG, "setButtonText");

        int i = 1;
        for (Button button : Buttons){
            if (i == 16){
                button.setText(" ");
                break;
            }
            button.setText(String.valueOf(i));
            i++;
        }

    }

    public void makeButtonsClickableOrNot(List<Button> Buttons, boolean clickable){
        Log.d(TAG, "makeButtonsClickableOrNot");

        for (Button button: Buttons) {
            button.setEnabled(clickable);
        }
    }

    public boolean parseMove(Button originalButton, List<Button> buttons){
        Log.d(TAG, "parseMove");

        String[] emptyButtonCoords = getButtonCoords(findEmptyButton(buttons).getTag().toString());

        int emptyButtonXCoordInt = Integer.parseInt(emptyButtonCoords[0]);
        int emptyButtonYCoordInt = Integer.parseInt(emptyButtonCoords[1]);

        String[] clickedButtonCoords = getButtonCoords(originalButton.getTag().toString());

        int clickedButtonXCoordInt = Integer.parseInt(clickedButtonCoords[0]);
        int clickedButtonYCoordInt= Integer.parseInt(clickedButtonCoords[1]);

        int row = Math.abs(emptyButtonXCoordInt - clickedButtonXCoordInt);
        int column = Math.abs(emptyButtonYCoordInt - clickedButtonYCoordInt);

        // checker
        if (row != 0 && column != 0){
            return false;
        }
        if (clickedButtonXCoordInt == emptyButtonXCoordInt && clickedButtonYCoordInt == emptyButtonYCoordInt){
            return false;
        }

        String clickedButtonText = ((Button) originalButton).getText().toString();

        int temp = 1;

        if (column == 0){
            while (row != 0){

                Button buttonByTag;

                if (emptyButtonXCoordInt > clickedButtonXCoordInt){
                    buttonByTag = findButtonByTag(buttons, createTagFromInts(Integer.parseInt(getButtonCoords(originalButton.getTag().toString())[0]) + temp,
                            Integer.parseInt(getButtonCoords(originalButton.getTag().toString())[1])));
                } else{
                    buttonByTag = findButtonByTag(buttons, createTagFromInts(Integer.parseInt(getButtonCoords(originalButton.getTag().toString())[0]) - temp,
                            Integer.parseInt(getButtonCoords(originalButton.getTag().toString())[1])));
                }

                String replayingButtonText = buttonByTag.getText().toString();
                buttonByTag.setText(clickedButtonText);
                clickedButtonText = replayingButtonText;

                temp++;
                row--;
            }
            ((Button) originalButton).setText(" ");
            return true;
        }

        if (row == 0){

            while (column != 0){

                Button buttonByTag;

                if (emptyButtonYCoordInt > clickedButtonYCoordInt){
                    buttonByTag = findButtonByTag(buttons, createTagFromInts(Integer.parseInt(getButtonCoords(originalButton.getTag().toString())[0]),
                            Integer.parseInt(getButtonCoords(originalButton.getTag().toString())[1]) + temp));
                } else{
                    buttonByTag = findButtonByTag(buttons, createTagFromInts(Integer.parseInt(getButtonCoords(originalButton.getTag().toString())[0]),
                            Integer.parseInt(getButtonCoords(originalButton.getTag().toString())[1]) - temp));
                }

                String replayingButtonText = buttonByTag.getText().toString();
                buttonByTag.setText(clickedButtonText);
                clickedButtonText = replayingButtonText;

                temp++;
                column--;
            }
            ((Button) originalButton).setText(" ");
            return true;
        }

        return false;

        //System.out.println("Empty button: " + emptyButtonXCoord + " " + emptyButtonYCoord);
        //System.out.println("Clicked button: " + clickedButtonXCoord + " " + clickedButtonYCoord);

    }

    private String createTagFromInts(int x, int y){
        Log.d(TAG, "createTagFromInts");

        return x + "-" + y;
    }

    private Button findButtonByTag(List<Button> buttons, String tag){
        Log.d(TAG, "findButtonByTag");

        for (Button button : buttons){
            if (button.getTag().toString().equals(tag)){
                return button;
            }
        }

        throw new RuntimeException("No such button with tag");
    }

    private String[] getButtonCoords(String tag){
        Log.d(TAG, "getButtonCoords");

        return tag.split("-");
    }

    private Button findEmptyButton(List<Button> buttons){
        Log.d(TAG, "findEmptyButton");

        for (Button button: buttons) {
            if (button.getText() == " "){
                return button;
            }
        }
        return null;
    }

    public boolean isSolved(List<Button> buttons){
        if (!buttons.get(buttons.size() - 1).getText().toString().equals(" ")){
            return false;
        }
        int i = 1;

        for (Button button : buttons) {
            if (i == 16) {
                return true;
            }
            if (Integer.parseInt(button.getText().toString()) != i ){
                return false;
            }
            i++;
        }
        return false;
    }

    public void shuffle(List<Button> buttons){
        for (int i = 0; i < 100; i++){
            Button emptyButton = findEmptyButton(buttons);
            int emptyButtonX = Integer.parseInt(getButtonCoords(emptyButton.getTag().toString())[0]);
            int emptyButtonY = Integer.parseInt(getButtonCoords(emptyButton.getTag().toString())[1]);

            int randomInt = getRandomNumberInRange(0, 1);
            String newTag;

            if (randomInt == 0){
                newTag = createTagFromInts(getRandomNumberInRange(1, 4), emptyButtonY); // change x
            } else {
                newTag = createTagFromInts(emptyButtonX, getRandomNumberInRange(1, 4)); // change y
            }
            parseMove(findButtonByTag(buttons, newTag), buttons);
        }

    }

    private static int getRandomNumberInRange(int min, int max) {

        if (min >= max) {
            throw new IllegalArgumentException("max must be greater than min");
        }

        Random r = new Random();
        return r.nextInt((max - min) + 1) + min;
    }

}
