package com.project.expert;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.project.R;
import com.project.manager.Constants;
import com.project.webservice.AppelService;

public class DescriptionAide extends Activity {

	Context context = this;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		requestWindowFeature(Window.FEATURE_NO_TITLE);

		setContentView(R.layout.description_aide);

		TextView nomTechnicien = (TextView) findViewById(R.id.NameTechnicien);
		nomTechnicien.setText("Bienvenue  " + Constants.nomTechnicien);

		TextView tvTypePanne = (TextView) findViewById(R.id.TypePanneDescript2);
		tvTypePanne.setText(Constants.typePanne);

		TextView tvPanne = (TextView) findViewById(R.id.PanneDescript2);
		tvPanne.setText(Constants.panne);

//		TextView tvProcedureM = (TextView) findViewById(R.id.ProcDescript2);
//		tvProcedureM.setText(Constants.procedureMaintenance);

		Button btnOk = (Button) findViewById(R.id.btnOk);

		/**
		 * Ok
		 */
		btnOk.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {

				EditText edit = (EditText) findViewById(R.id.DiagnosticText);

				String result = edit.getText().toString();
				if (!result.equals("")) {
					Constants.DiagnosticPanne = edit.getText().toString();

					Intent intent = new Intent(context,
							ConnectedExpertsActivity.class);

				
					intent.putExtra("NomTech", Constants.nomTechnicien);
					intent.putExtra("PrenomTech", Constants.nomTechnicien);
					intent.putExtra("Panne", Constants.panne);
					intent.putExtra("TypePanne", Constants.typePanne);
					intent.putExtra("Diagnostic", Constants.DiagnosticPanne);

					
					startActivity(intent);

					
					
					//Liste des Experts !!!!
		//			AppelService.setDescriptionAide();
					finish();

				} else {
					Toast.makeText(context,
							"Veuillez remplir le champs \n Diagnostic !",
							Toast.LENGTH_LONG).show();
				}

			}
		});

	}

}
