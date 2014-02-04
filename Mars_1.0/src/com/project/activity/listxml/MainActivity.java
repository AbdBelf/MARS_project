package com.project.activity.listxml;

import java.io.InputStream;
import java.io.OutputStream;
import java.net.URL;
import java.util.ArrayList;
import java.util.HashMap;

import javax.xml.parsers.SAXParser;
import javax.xml.parsers.SAXParserFactory;

import org.xml.sax.InputSource;
import org.xml.sax.XMLReader;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.Window;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Toast;

import com.project.R;
import com.project.expert.DescriptionAide;
import com.project.manager.Constants;
import com.project.typepanne.parser.XMLGettersSetters;
import com.project.typepanne.parser.XMLHandler;

public class MainActivity extends Activity {

	// data

	private ListView maListViewPerso;
	private Context context;
	private MainActivity _this;

	public ArrayList<HashMap<String, String>> listItem;

	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		requestWindowFeature(Window.FEATURE_NO_TITLE);

		setContentView(R.layout.liste);
		context = this;
		_this = this;
		
		

		SharedPreferences s = getSharedPreferences("preferences", 0);

		Log.e("IP", "IP :" + s.getString("IP", ""));

		Constants.IP = s.getString("IP", "");
		
		Constants.nomTechnicien = s.getString("nomTechnicien", "");

		

	
	        
		
		
		

		ImageView imV = (ImageView) findViewById(R.id.imageViewLayoutManager);
		imV.setImageResource(R.drawable.typepannelogo);

		// Log.d("Xpath", "initialize");

		// Récupération de la listview créée dans le fichier main.xml
		maListViewPerso = (ListView) findViewById(R.id.custom_list_view);

		// Création de la ArrayList qui nous permettra de remplire la listView
		listItem = new ArrayList<HashMap<String, String>>();

		// On déclare la HashMap qui contiendra les informations pour un item
		// Création d'une HashMap pour insérer les informations du premier item
		// de notre listView

		/*
		 * 
		 * REMETTRE LES VALEURS DES CONSTANTES A VIDE AU CAS OU LE CACHE N EST
		 * PAS DETRUIT APRES LA
		 * 
		 * 2EME REUTILISATION DE L'APPLICATION
		 */
		Constants.typePanne = "";
		Constants.panne = "";
		// Constants.procedureMaintenance = "";
		Constants.DiagnosticPanne = "";

		/**
		 * Parsing avec SAX : Plus Rapid !
		 */

		XMLGettersSetters data;

		try {

			/**
			 * Create a new instance of the SAX parser
			 **/
			SAXParserFactory saxPF = SAXParserFactory.newInstance();
			SAXParser saxP = saxPF.newSAXParser();
			XMLReader xmlR = saxP.getXMLReader();

			/**
			 * Create the Handler to handle each of the XML tags.
			 **/
			XMLHandler myXMLHandler = new XMLHandler();
			xmlR.setContentHandler(myXMLHandler);

			xmlR.parse(new InputSource(new URL(Constants.IP
					+ "data/typepanne.xml").openStream()));

			// xmlR.parse(new InputSource(new URL(Constants.IP +
			// "typepanne.xml")
			// .openStream()));

			// xmlR.parse(new
			// InputSource(getResources().openRawResource(R.raw.typepanne)));

			// Récupération des valeurs !!

			data = XMLHandler.data;

			for (int i = 0; i < data.getlibelle().size(); i++) {

				HashMap<String, String> map = new HashMap<String, String>();

				map.put("Id", data.getId().get(i).toString());
				map.put("Libelle", data.getlibelle().get(i));
				map.put("Description", data.getdescription().get(i));

				switch (i) {
				case 0:
					map.put("img", String.valueOf(R.drawable.moteur));
					break;
				case 1:
					map.put("img", String.valueOf(R.drawable.icone_volant_ptit));
					break;
				case 2:
					map.put("img",
							String.valueOf(R.drawable.voyant_temperature_eau));
					break;

				default:
					map.put("img", String.valueOf(R.drawable.maintenance));
					Log.i("IMAGE", String.valueOf(map.get("img")));
					break;
				}

				listItem.add(map);

			}

			// Création d'un SimpleAdapter qui se chargera de mettre les items
			// présent dans notre list (listItem) dans la vue affichageitem
			SimpleAdapter mSchedule = new SimpleAdapter(this.getBaseContext(),
					listItem, R.layout.custom_layout, new String[] { "img",
							"Libelle", "Description" }, new int[] { R.id.img,
							R.id.Libelle, R.id.Description });

			// On attribut à notre listView l'adapter que l'on vient de créer
			maListViewPerso.setAdapter(mSchedule);

			// Enfin on met un écouteur d'évènement sur notre listView
			maListViewPerso.setOnItemClickListener(new OnItemClickListener() {
				@Override
				@SuppressWarnings("unchecked")
				public void onItemClick(AdapterView<?> a, View v, int position,
						long id) {
					// on récupère la HashMap contenant les infos de notre item
					// (titre, description, img)

					Log.i("on Click", "ON CLICK");
					HashMap<String, String> map = (HashMap<String, String>) maListViewPerso
							.getItemAtPosition(position);

					Intent intent = new Intent(context, PanneActivity.class);
					intent.putExtra("type", map.get("Id"));

					Constants.typePanne = listItem.get(position).get("Libelle");
					Constants.panne = "";
					// Constants.procedureMaintenance = "";
					Constants.DiagnosticPanne = "";

					// Log.i("xxx",map.get("Id"));

					_this.startActivityForResult(intent, 101);
				}
			});

		} catch (Exception e) {

			Log.e("Error", e.toString());
			System.out.println(e);
			Toast.makeText(
					context,
					"Veuillez vérifier votre connexion à internet !  L'application ne pourra pas démarrer ",
					Toast.LENGTH_LONG).show();

			// finish();

		}

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {

		// ajout au submenu
		menu.add(0, 100, 0, "Quit");
		menu.add(0, 101, 0, "Aide Expert Type panne").setIcon(
				R.drawable.spanner32);
		menu.add(0, 102, 0, "Enregistrer Hors Ligne").setIcon(
				R.drawable.spanner32);

		getMenuInflater().inflate(R.menu.activity_main, menu);

		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {

		switch (item.getItemId()) {
		case 100:
			System.exit(0);
			break;

		case 101: {
			/**
			 * Demande Aide Expert : Affichage du Login
			 */
			/*
			 * Intent intent = new Intent(context,LoginDialog.class);
			 * 
			 * this.startActivityForResult(intent, 101);
			 */

			Intent intent = new Intent(context, DescriptionAide.class);

			startActivity(intent);

			break;
		}

		case 102: {

			SauvgarderFichierHorsLigne();
			Toast.makeText(context, "Mode Hors Ligne mit à jour.",
					Toast.LENGTH_LONG).show();

			break;
		}

		}
		return super.onOptionsItemSelected(item);
	}

	private void SauvgarderFichierHorsLigne() {
		// TODO Auto-generated method stub

		try {

			/* FICHIER TYPE PANNE */
			InputStream in = new URL(Constants.IP + Constants.repertoire
					+ Constants.FICHIER_TYPE_PANNE).openStream();

			byte[] buffer = new byte[4096];

			OutputStream out = openFileOutput(Constants.FICHIER_TYPE_PANNE,
					Context.MODE_PRIVATE);

			int n = in.read(buffer, 0, buffer.length);
			while (n >= 0) {
				out.write(buffer, 0, n);
				n = in.read(buffer, 0, buffer.length);
			}

			in.close();
			out.close();

			/* FICHIER PANNE */
			InputStream in2 = new URL(Constants.IP + Constants.repertoire
					+ Constants.FICHIER_PANNE).openStream();

			byte[] buffer2 = new byte[4096];

			OutputStream out2 = openFileOutput(Constants.FICHIER_PANNE,
					Context.MODE_PRIVATE);

			int n2 = in2.read(buffer2, 0, buffer2.length);
			while (n2 >= 0) {
				out2.write(buffer2, 0, n2);
				n2 = in2.read(buffer2, 0, buffer2.length);
			}

			in2.close();
			out2.close();

			/* FICHIER PROCEDURE */
			InputStream in3 = new URL(Constants.IP + Constants.repertoire
					+ Constants.FICHIER_PROCEDURE).openStream();

			byte[] buffer3 = new byte[4096];

			OutputStream out3 = openFileOutput(Constants.FICHIER_PROCEDURE,
					Context.MODE_PRIVATE);

			int n3 = in3.read(buffer3, 0, buffer3.length);
			while (n3 >= 0) {
				out3.write(buffer3, 0, n3);
				n3 = in3.read(buffer3, 0, buffer3.length);
			}

			in3.close();
			out3.close();

			/* FICHIER ETAPE */
			InputStream in4 = new URL(Constants.IP + Constants.repertoire
					+ Constants.FICHIER_ETAPE).openStream();

			byte[] buffer4 = new byte[4096];

			OutputStream out4 = openFileOutput(Constants.FICHIER_ETAPE,
					Context.MODE_PRIVATE);

			int n4 = in4.read(buffer4, 0, buffer4.length);
			while (n4 >= 0) {
				out4.write(buffer4, 0, n4);
				n4 = in4.read(buffer4, 0, buffer4.length);
			}

			in4.close();
			out4.close();

			/* FICHIER OBJET3D */
			InputStream in6 = new URL(Constants.IP + Constants.repertoire
					+ Constants.FICHIER_OBJET3D).openStream();

			byte[] buffer6 = new byte[4096];

			OutputStream out6 = openFileOutput(Constants.FICHIER_OBJET3D,
					Context.MODE_PRIVATE);

			int n6 = in6.read(buffer6, 0, buffer6.length);
			while (n6 >= 0) {
				out6.write(buffer6, 0, n6);
				n6 = in6.read(buffer6, 0, buffer6.length);
			}

			in6.close();
			out6.close();

		} catch (Exception e) {

			Log.e("Error", e.toString());
		}

	}

}
