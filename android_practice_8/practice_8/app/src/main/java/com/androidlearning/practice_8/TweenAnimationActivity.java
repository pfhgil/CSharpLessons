package com.androidlearning.practice_8;

import android.graphics.drawable.AnimationDrawable;
import android.os.Bundle;
import android.util.Log;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.Button;
import android.widget.ImageView;

import androidx.appcompat.app.AppCompatActivity;

public class TweenAnimationActivity extends AppCompatActivity
{
    private ImageView tweenChmonyaAnimation;
    private Button startChmonyaButton;
    private Button stopChmonyaButton;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.tween_animation_activity);

        tweenChmonyaAnimation = findViewById(R.id.tweenChmonyaAnim);
        startChmonyaButton = findViewById(R.id.startChmonyaButton);
        stopChmonyaButton = findViewById(R.id.stopChmonyaButton);

        Animation rotateAnimation = AnimationUtils.loadAnimation(this, R.anim.rotation_anim);

        startChmonyaButton.setOnClickListener(view -> {
            tweenChmonyaAnimation.startAnimation(rotateAnimation);
        });

        stopChmonyaButton.setOnClickListener(view -> {
            tweenChmonyaAnimation.clearAnimation();
        });
    }
}
