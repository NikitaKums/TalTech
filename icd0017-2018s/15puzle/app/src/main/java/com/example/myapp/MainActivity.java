package com.example.myapp;

import android.os.Bundle;
import android.os.CountDownTimer;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;


public class MainActivity extends AppCompatActivity {
    static private final String TAG = MainActivity.class.getSimpleName();

    private int timerTime = 0;
    private boolean timerRunning;
    private TimeCounter timer;

    private boolean winner;

    private double score = 1000;
    private int movesDone = 0;

    private HelperMethods helperMethods;

    private TextView mTextViewScoreCount;
    private TextView mTextViewTimeCount;
    private TextView mTextViewMovesCount;
    private TextView mTextViewWinMessage;
    private Button mButtonStart;
    private Button mButtonRestart;

    private Button mTableButton1;
    private Button mTableButton2;
    private Button mTableButton3;
    private Button mTableButton4;
    private Button mTableButton5;
    private Button mTableButton6;
    private Button mTableButton7;
    private Button mTableButton8;
    private Button mTableButton9;
    private Button mTableButton10;
    private Button mTableButton11;
    private Button mTableButton12;
    private Button mTableButton13;
    private Button mTableButton14;
    private Button mTableButton15;
    private Button mTableButton16;

    List<Button> buttons;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Log.d(TAG, "onCreate");

        super.onCreate(savedInstanceState);
        setContentView(R.layout.gameui_container);

        helperMethods = new HelperMethods();
        buttons = new ArrayList<Button>();
        timer = new TimeCounter(3600 * 1000, 1000);

        initializeUIElements(savedInstanceState);
        UpdateUI(C.REFRESH_STATISTICS_PART);
    }

    // ============================== LIFECYCLE EVENTS ===============================
    @Override
    protected void onStart() {
        Log.d(TAG, "onStart");
        super.onStart();

    }

    @Override
    protected void onResume() {
        Log.d(TAG, "onResume");
        super.onResume();
        if (timerRunning){
            timer.onStart();
        }
    }

    @Override
    protected void onPause() {
        Log.d(TAG, "onPause");
        super.onPause();
    }


    @Override
    protected void onStop() {
        Log.d(TAG, "onStop");
        super.onStop();
    }

    @Override
    protected void onDestroy() {
        Log.d(TAG, "onDestroy");
        super.onDestroy();
    }


    @Override
    protected void onRestart() {
        Log.d(TAG, "onRestart");
        super.onRestart();
    }

    @Override
    protected void onSaveInstanceState(Bundle outState) {
        Log.d(TAG, "onSaveInstanceState");
        outState.putBoolean(C.IS_TIMER_RUNNING, timerRunning);
        outState.putInt(C.TIMER_CURRENT_SECONDS, timerTime);
        outState.putDouble(C.SCORE, score);
        outState.putInt(C.MOVES_DONE, movesDone);
        outState.putString(C.BUTTON_TEXTS, getButtonTextJson());
        outState.putBoolean(C.WINNER, winner);
        timer.cancel();
        super.onSaveInstanceState(outState);
    }

    @Override
    protected void onRestoreInstanceState(Bundle savedInstanceState) {
        Log.d(TAG, "onRestoreInstanceState");
        super.onRestoreInstanceState(savedInstanceState);

        if(savedInstanceState != null) {
            timerRunning = savedInstanceState.getBoolean(C.IS_TIMER_RUNNING);
            score = savedInstanceState.getDouble(C.SCORE);
            movesDone = savedInstanceState.getInt(C.MOVES_DONE);
            timerTime = savedInstanceState.getInt(C.TIMER_CURRENT_SECONDS);
            winner = savedInstanceState.getBoolean(C.WINNER);
            try {
                JSONObject buttonTexts = new JSONObject(savedInstanceState.getString(C.BUTTON_TEXTS));
                for (Button button : buttons) {
                    if (!buttonTexts.getString(button.getTag().toString()).equals("#")){
                        button.setText(buttonTexts.getString(button.getTag().toString()));
                        continue;
                    }
                    button.setText(" ");
                }
            } catch (JSONException e) {
                e.printStackTrace();
            }
        }
        UpdateUI(C.REFRESH_STATISTICS_PART);
        if (winner) {
            timer.onWin();
        }
    }

    public String getButtonTextJson(){
        JSONObject jsonObject = new JSONObject();
        String buttonText;
        for (Button button : buttons){
            try {
                buttonText = button.getText().toString().equals(" ") ? "#" : button.getText().toString();
                jsonObject.put(button.getTag().toString(), buttonText);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        }
        return jsonObject.toString();
    }

    public void startButtonOnClick(View view) {
        Log.d(TAG, "startButtonOnClick");
        timer.onStart();
    }

    public void resetButtonOnClick(View view) {
        Log.d(TAG, "resetButtonOnClick");
        score = 1000;
        timer.onRestart();
    }

    public void UpdateUI(String data){
        Log.d(TAG, "UpdateUI");
        switch (data){
            case C.RESTART_BUTTON_CLICKED:
                mButtonStart.setEnabled(true);
                mButtonRestart.setEnabled(false);
                break;
            case C.DISABLE_START_BUTTON_ON_WIN:
                helperMethods.makeButtonsClickableOrNot(buttons, false);
            case C.START_BUTTON_CLICKED:
                mButtonStart.setEnabled(false);
                mButtonRestart.setEnabled(true);
                break;
            case C.REFRESH_STATISTICS_PART:
                mTextViewTimeCount.setText(helperMethods.getStringTimeFromSeconds(timerTime));
                mTextViewScoreCount.setText(String.valueOf(score));
                mTextViewMovesCount.setText(String.valueOf(movesDone));
                break;
        }
    }

    public void initializeUIElements(Bundle save){
        Log.d(TAG, "initializeUIElements");
        mTextViewScoreCount = (TextView) findViewById(R.id.textViewScoreCount);
        mTextViewTimeCount = (TextView) findViewById(R.id.textViewTimeCount);
        mTextViewMovesCount = (TextView) findViewById(R.id.textViewMovesCount);
        mTextViewWinMessage = (TextView) findViewById(R.id.textViewWinMessage);
        mButtonRestart = (Button) findViewById(R.id.buttonRestart);
        mButtonStart = (Button) findViewById(R.id.buttonStart);
        mButtonRestart.setEnabled(false);

        mTableButton1 = (Button) findViewById(R.id.button1);
        mTableButton2 = (Button) findViewById(R.id.button2);
        mTableButton3 = (Button) findViewById(R.id.button3);
        mTableButton4 = (Button) findViewById(R.id.button4);
        mTableButton5 = (Button) findViewById(R.id.button5);
        mTableButton6 = (Button) findViewById(R.id.button6);
        mTableButton7 = (Button) findViewById(R.id.button7);
        mTableButton8 = (Button) findViewById(R.id.button8);
        mTableButton9 = (Button) findViewById(R.id.button9);
        mTableButton10 = (Button) findViewById(R.id.button10);
        mTableButton11 = (Button) findViewById(R.id.button11);
        mTableButton12 = (Button) findViewById(R.id.button12);
        mTableButton13 = (Button) findViewById(R.id.button13);
        mTableButton14 = (Button) findViewById(R.id.button14);
        mTableButton15 = (Button) findViewById(R.id.button15);
        mTableButton16 = (Button) findViewById(R.id.button16);

        buttons.add(mTableButton1);
        buttons.add(mTableButton2);
        buttons.add(mTableButton3);
        buttons.add(mTableButton4);
        buttons.add(mTableButton5);
        buttons.add(mTableButton6);
        buttons.add(mTableButton7);
        buttons.add(mTableButton8);
        buttons.add(mTableButton9);
        buttons.add(mTableButton10);
        buttons.add(mTableButton11);
        buttons.add(mTableButton12);
        buttons.add(mTableButton13);
        buttons.add(mTableButton14);
        buttons.add(mTableButton15);
        buttons.add(mTableButton16);

        helperMethods.makeButtonsClickableOrNot(buttons, false);
        helperMethods.setButtonText(buttons);

        if (save == null) {
            helperMethods.shuffle(buttons);
        }
    }

    public void tableButtonOnClick(View view) {
        Log.d(TAG, "tableButtonOnClick");

        if (timerRunning){

            if(helperMethods.parseMove((Button) view, buttons)){
                movesDone++;
                mTextViewMovesCount.setText(String.valueOf(movesDone));
                if (helperMethods.isSolved(buttons)){
                    timer.onWin();
                    winner = true;
                }
            }
        }
    }


    public class TimeCounter extends CountDownTimer {

        public TimeCounter(long millisInFuture, long countDownInterval) {
            super(millisInFuture, countDownInterval);
        }

        @Override
        public void onTick(long l) {
            Log.d(TAG, "onTick");

            if (timerTime > 3599) timer.cancel();
            timerTime = timerTime + 1;
            mTextViewTimeCount.setText(helperMethods.getStringTimeFromSeconds(timerTime));
            score = helperMethods.calculateScore(score, movesDone);
            mTextViewScoreCount.setText(String.valueOf(score));
        }

        @Override
        public void onFinish() {
            Log.d(TAG, "onFinish");
        }

        public void onWin(){
            timerRunning = false;
            this.cancel();
            mTextViewWinMessage.setText(C.WINNER);
            UpdateUI(C.DISABLE_START_BUTTON_ON_WIN);

        }

        public void onRestart(){
            Log.d(TAG, "onRestart");
            timerTime = 0;
            movesDone = 0;
            timerRunning = false;
            this.cancel();
            UpdateUI(C.REFRESH_STATISTICS_PART);
            UpdateUI(C.RESTART_BUTTON_CLICKED);
            helperMethods.setButtonText(buttons);
            helperMethods.shuffle(buttons);
            helperMethods.makeButtonsClickableOrNot(buttons, false);
            mTextViewWinMessage.setText(" ");
            winner = false;
        }

        public void onStart(){
            Log.d(TAG, "onStart");
            this.start();
            timerRunning = true;
            helperMethods.makeButtonsClickableOrNot(buttons, true);
            UpdateUI(C.START_BUTTON_CLICKED);
        }
    }

     /*@Override
    public boolean onCreateOptionsMenu(Menu menu) {
        Log.d(TAG, "onCreateOptionsMenu");
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        Log.d(TAG, "onOptionsItemSelected");
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }*/

}
