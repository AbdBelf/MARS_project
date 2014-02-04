package com.project.manager;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.project.R;

public class managerActivity extends Activity {

	Context context = this;

	@Override
	public void onCreate(Bundle savedInstanceState) {

		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_NO_TITLE);

		setContentView(R.layout.manager_layout);
		
		
		
		 final SharedPreferences sp = getSharedPreferences("preferences", 0);



	       
	    //    Constants.IP = sp.getString("IP", "");
	       
	      
	        

		Button btnOk = (Button) findViewById(R.id.btnEnregistrer);

		Button btnAnnuler = (Button) findViewById(R.id.btnAnnuler);

		final EditText MAKER_MAX = (EditText) findViewById(R.id.editTextMarkerMax);
		final EditText NameSpace = (EditText) findViewById(R.id.editTextNameSpace);
		final EditText IP = (EditText) findViewById(R.id.editTextIp);

		MAKER_MAX.setText("" + Constants.MARKER_MAX);
		NameSpace.setText(Constants.NAMESPACE);
		IP.setText(Constants.IP);
		
		
		


		/**
		 * Ok
		 */
		btnOk.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {

				if (!MAKER_MAX.getText().toString().equals(""))
					Constants.MARKER_MAX = Integer.parseInt(MAKER_MAX.getText()
							.toString());

				if (!NameSpace.getText().toString().equals(""))
					Constants.NAMESPACE = NameSpace.getText().toString();

				if (!IP.getText().toString().equals(""))
				{
					Constants.IP = IP.getText().toString();
					
					
					//Getting the Editor
			        SharedPreferences.Editor edit = sp.edit();
			       
				 //Modifying Editor
			       
			        edit.remove("IP");
			        edit.putString("IP", Constants.IP);
			       
			        //Save the Editor value
			        edit.commit();
					
				}
				Toast.makeText(context, "Parametres enregistres avec succès",
						Toast.LENGTH_LONG).show();

		
				
				//ON A ENLEVE CETTE PARTIE CAR LA 1ERE PAGE EST LAUTHENTIFICATION
				// ON A PAS BESOIN D'AFFICHER LA LISTE AU DEBUT 
				
				
//				Intent intent = new Intent(context, LoginInterface.class);
//
//				managerActivity.this.startActivityForResult(intent, 102);

				finish();
				
			}
		});

		btnAnnuler.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {

			//	Intent intent = new Intent(context, MainActivity.class);

			//	managerActivity.this.startActivityForResult(intent, 102);

				finish();
				
			}
		});

	}
	


}
