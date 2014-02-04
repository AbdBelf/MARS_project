package com.project.activity.listxml;

import android.app.Activity;
import android.content.Intent;
import android.graphics.PixelFormat;
import android.os.Bundle;
import android.os.Handler;
import android.view.Window;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ImageView;
import android.widget.LinearLayout;

import com.project.R;

public class SpalshScreenActivity extends Activity {

	private static final int SPLASH_TIME = 3 * 1000;// 3 seconds

	
	public void onAttachedToWindow() {
		super.onAttachedToWindow();
		Window window = getWindow();
		window.setFormat(PixelFormat.RGBA_8888);
	}

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		requestWindowFeature(Window.FEATURE_NO_TITLE);
		setContentView(R.layout.activity_spalsh_screen);
		
		

	//	StartAnimations();

		new Handler().postDelayed(new Runnable() {

			@Override
			public void run() {

				// Run next activity
				
				Intent intent = new Intent(SpalshScreenActivity.this,LoginInterface.class);

				//Intent intent = new Intent(SpalshScreenActivity.this,LoginDialog.class);

				startActivity(intent);
				
				
				/*
				Intent intent = new Intent();
				
				intent.setClass(SpalshScreenActivity.this, MainActivity.class);
				startActivity(intent);
				 */
				SpalshScreenActivity.this.finish();

			}
		}, SPLASH_TIME);

		new Handler().postDelayed(new Runnable() {
			@Override
			public void run() {
			}
		}, SPLASH_TIME);

	}

	private void StartAnimations() {
		Animation anim = AnimationUtils.loadAnimation(this, R.anim.alpha);
		anim.reset();
		LinearLayout l = (LinearLayout) findViewById(R.id.lin_lay);
		l.clearAnimation();
		l.startAnimation(anim);

		anim = AnimationUtils.loadAnimation(this, R.anim.translate);
		anim.reset();
		ImageView iv = (ImageView) findViewById(R.id.logo);
		iv.clearAnimation();
		iv.startAnimation(anim);

	}

	@Override
	public void onBackPressed() {
		this.finish();
		super.onBackPressed();
	}
}
