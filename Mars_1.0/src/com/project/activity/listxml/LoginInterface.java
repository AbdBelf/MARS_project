package com.project.activity.listxml;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.Handler;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.project.R;
import com.project.horsligne.MainActivityHorsligne;
import com.project.manager.Constants;
import com.project.manager.managerActivity;
import com.project.webservice.AppelService;

public class LoginInterface extends Activity {

	final Context ctx = this;

	EditText userName;
	EditText password;
	protected ProgressDialog mProgressDialog;

	public static final String MY_PREF = "preferences";

	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_NO_TITLE);

		setContentView(R.layout.logininteface);

		Button loginbutton = (Button) findViewById(R.id.buttonOk);
		Button horsLignebutton = (Button) findViewById(R.id.buttonHorsligne);
		userName = (EditText) findViewById(R.id.editTextId);
		password = (EditText) findViewById(R.id.editTextPassword);

		loginbutton.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {

				Authentification();

				// mProgressDialog = ProgressDialog.show(ctx, "Please wait",
				// "Long operation starts...", true, true);
				//
				// new Thread(new Runnable() {
				//
				// @Override
				// public void run() {
				//
				//
				//
				// handler.sendEmptyMessage(0);
				// }
				//
				// }).start();

			}

		});

		horsLignebutton.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {

				Intent intent = new Intent(ctx, MainActivityHorsligne.class);
				startActivity(intent);

				// finish();
			}

		});

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {

		// ajout au submenu
		menu.add(0, 100, 0, "Quit");
		menu.add(0, 102, 0, "Parametres").setIcon(R.drawable.spanner32);
		getMenuInflater().inflate(R.menu.activity_main, menu);

		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {

		switch (item.getItemId()) {
		case 100:
			System.exit(0);
			break;

		case 102: {
			/**
			 * Demande Aide Expert : Affichage du Login
			 */

			Intent intent = new Intent(LoginInterface.this,
					managerActivity.class);

			this.startActivityForResult(intent, 102);

			// finish();
			break;
		}

		}
		return super.onOptionsItemSelected(item);
	}

	private void Authentification() {
		// TODO Auto-generated method stub
		Boolean result = false;

		String user = "";

		user = userName.getText().toString();
		String pass = password.getText().toString();

		if (!(pass.equals("") || user.equals(""))) {

			/*
			 * VERIFICATION DE LA CONNECTION AVEC UN ENVOI SIMPLE POUR SAVOIR SI
			 * LE WEB SERVICE EST TROUVE OU NON
			 */
			try {

				SoapObject request = new SoapObject(Constants.NAMESPACE,
						"loginTechnicien");
				SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
						SoapEnvelope.VER11);

				envelope.dotNet = true;
				envelope.setOutputSoapObject(request);
				HttpTransportSE androidHttpTransport = new HttpTransportSE(
						Constants.IP + "service1.asmx");
				androidHttpTransport.call("http://tempuri.org/loginTechnicien",
						envelope);

			} catch (Exception e) {

				Toast.makeText(ctx, "Vérifiez votre Connexion à internet.",
						Toast.LENGTH_LONG).show();

				return;
			}

			result = AppelService.AppelServLoginTech(user, pass);

			if (result) {

				Constants.nomTechnicien = AppelService.AppelServNomTech();

				// Intent intent = new
				// Intent(ctx,DescriptionAide.class);

				/**
				 * SAVE PREFERENCES (IP , NAME TECHNICIAN ) ...
				 */

				SharedPreferences sp = getSharedPreferences(MY_PREF, 0);

				// Getting the Editor
				SharedPreferences.Editor edit = sp.edit();

				// Modifying Editor

				edit.putString("IP", Constants.IP);
				edit.putString("nomTechnicien", Constants.nomTechnicien);

				// Save the Editor value
				edit.commit();

				Intent intent = new Intent(ctx, MainActivity.class);
				startActivity(intent);

				finish();

			} else {
				Toast.makeText(ctx, "Vous n'avez pas le droit d'accéder !",
						Toast.LENGTH_LONG).show();

			}

		} else {
			Toast.makeText(ctx,
					"Veuillez entrer l'identifiant et le mot de passe SVP.",
					Toast.LENGTH_LONG).show();

		}

	}

	private Handler handler = new Handler() {

		public void handleMessage(android.os.Message msg) {

			if (msg.what == 0) {

				mProgressDialog.dismiss();

			}

		};

	};

}