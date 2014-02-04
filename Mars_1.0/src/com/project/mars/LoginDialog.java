package com.project.mars;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.SurfaceHolder;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.project.R;
import com.project.activity.listxml.MainActivity;
import com.project.manager.Constants;
import com.project.webservice.AppelService;

public class LoginDialog extends Activity {

	final private static int DIALOG_LOGIN = 1;
	final Context ctx = this;
	AlertDialog dialogDetails = null;

	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		setContentView(R.layout.vierge);

		showDialog(DIALOG_LOGIN);
	}

	protected Dialog onCreateDialog(int id) {

		switch (id) {
		case DIALOG_LOGIN: {
			LayoutInflater inflater = LayoutInflater.from(this);
			View dialogview = inflater.inflate(R.layout.dialog_layout, null);

			AlertDialog.Builder dialogbuilder = new AlertDialog.Builder(this);

			dialogbuilder.setTitle("Login");
			dialogbuilder.setIcon(android.R.drawable.ic_dialog_alert);

			dialogbuilder.setView(dialogview);
			dialogDetails = dialogbuilder.create();

			break;
		}
		}

		return dialogDetails;
	}

	@Override
	protected void onPrepareDialog(int id, Dialog dialog) {

		switch (id) {
		case DIALOG_LOGIN:

			final AlertDialog alertDialog = (AlertDialog) dialog;
			Button loginbutton = (Button) alertDialog
					.findViewById(R.id.btn_login);
			Button cancelbutton = (Button) alertDialog
					.findViewById(R.id.btn_cancel);
			final EditText userName = (EditText) alertDialog
					.findViewById(R.id.user);
			final EditText password = (EditText) alertDialog
					.findViewById(R.id.password);

			/**
			 * Login Button
			 */
			loginbutton.setOnClickListener(new View.OnClickListener() {

				@Override
				public void onClick(View v) {

					

						Boolean result = false;
						int user = -1;

						try {
						user = Integer.parseInt(userName.getText().toString());

						} catch (Exception e) {

							Toast.makeText(ctx,
									"Veuillez entrer l'identifiant !",
									Toast.LENGTH_LONG).show();

							return;
						}
						
						String pass = password.getText().toString();

						if (!pass.equals("")) {
							
							
							//result = AppelService.AppelServLoginTech(id, pass);
									
							
							
							if (result) {

								Constants.nomTechnicien = AppelService
										.AppelServNomTech();
								
							//	Intent intent = new Intent(ctx,DescriptionAide.class);

								Intent intent = new Intent(ctx,MainActivity.class);
								startActivity(intent);

								alertDialog.dismiss();

								finish();

							} else {
								Toast.makeText(ctx,
										"Vous n'avez pas le droit d'accéder !",
										Toast.LENGTH_LONG).show();

								showDialog(DIALOG_LOGIN);
							}

						} else {
							Toast.makeText(ctx,
									"Veuillez entrer le mot de passe.",
									Toast.LENGTH_LONG).show();

							showDialog(DIALOG_LOGIN);
						}

				
				}
			});

			/**
			 * Cancel
			 */
			cancelbutton.setOnClickListener(new View.OnClickListener() {

				@Override
				public void onClick(View v) {
					alertDialog.dismiss();

					finish();
				}
			});
			break;
		}
	}

	public void surfaceChanged(SurfaceHolder holder, int format, int w, int h) {

		dialogDetails.dismiss();

		// showDialog(DIALOG_LOGIN);

	}

}