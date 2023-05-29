package com.androidlearning.practice_8;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import androidx.appcompat.app.AppCompatActivity;

public class MainActivity extends AppCompatActivity
{
    private Button gotoFrameAnimationButton;
    private Button gotoTweenAnimationButton;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main_activity);

        gotoFrameAnimationButton = findViewById(R.id.gotoFrameAnimButton);
        gotoTweenAnimationButton = findViewById(R.id.gotoTweenAnimationButton);

        gotoFrameAnimationButton.setOnClickListener(view -> {
            Intent intent = new Intent(this, FrameAnimationActivity.class);
            startActivity(intent);
        });

        gotoTweenAnimationButton.setOnClickListener(view -> {
            Intent intent = new Intent(this, TweenAnimationActivity.class);
            startActivity(intent);
        });
    }
}
