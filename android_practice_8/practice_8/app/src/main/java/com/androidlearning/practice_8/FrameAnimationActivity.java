package com.androidlearning.practice_8;

import android.graphics.drawable.AnimationDrawable;
import android.os.Bundle;
import android.widget.Button;
import android.widget.ImageView;

import androidx.appcompat.app.AppCompatActivity;

public class FrameAnimationActivity extends AppCompatActivity
{
    private ImageView framedWhatAnimation;
    private Button framedWhatStartButton;
    private Button framedWhatPauseButton;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.frame_animation_activity);

        framedWhatAnimation = findViewById(R.id.framedWhatAnimation);
        framedWhatStartButton = findViewById(R.id.framedWhatStartButton);
        framedWhatPauseButton = findViewById(R.id.frameWhatStopButton);

        framedWhatStartButton.setOnClickListener(view -> {
            ((AnimationDrawable) framedWhatAnimation.getDrawable()).start();
        });

        framedWhatPauseButton.setOnClickListener(view -> {
            ((AnimationDrawable) framedWhatAnimation.getDrawable()).stop();
        });
    }
}
