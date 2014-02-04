package com.project.horsligne;

import java.io.FileInputStream;
import java.util.ArrayList;
import java.util.HashMap;

import javax.xml.parsers.SAXParser;
import javax.xml.parsers.SAXParserFactory;

import org.xml.sax.InputSource;
import org.xml.sax.XMLReader;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
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
import com.project.manager.Constants;
import com.project.typepanne.parser.XMLGettersSetters;
import com.project.typepanne.parser.XMLHandler;

public class MainActivityHorsligne extends Activity {

	// data

	private ListView maListViewPerso;
	private Context context;
	private MainActivityHorsligne _this;

	public ArrayList<HashMap<String, String>> listItem;

	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		requestWindowFeature(Window.FEATURE_NO_TITLE);

		setContentView(R.layout.liste);
		context = this;
		_this = this;

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

//			xmlR.parse(new InputSource(new URL(Constants.IP + "typepanne.xml")
//					.openStream()));

			
			FileInputStream fis = context.openFileInput(Constants.FICHIER_TYPE_PANNE );
			    
			    
			xmlR.parse(new InputSource(fis));
			

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
					map.put("img", String.valueOf(R.drawable.voyant_temperature_eau));
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

					Intent intent = new Intent(context, PanneActivityHorsligne.class);
					intent.putExtra("type", map.get("Id"));


					// Log.i("xxx",map.get("Id"));

					_this.startActivityForResult(intent, 101);
				}
			});

		} catch (Exception e) {
			System.out.println(e);
			Toast.makeText(
					context,
					"Base de données non sauvgardée ! ",
					Toast.LENGTH_LONG).show();

			// finish();

		}

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {

		// ajout au submenu
		menu.add(0, 100, 0, "Quit");
		// menu.add(0, 102, 0, "Parametres").setIcon(R.drawable.spanner32);
		getMenuInflater().inflate(R.menu.activity_main, menu);

		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {

		switch (item.getItemId()) {
		case 100:
			System.exit(0);
			break;

		

		/*
		 * case 102: {
		 * 
		 * Intent intent = new Intent(context,managerActivity.class);
		 * 
		 * this.startActivityForResult(intent, 102);
		 * 
		 * finish();
		 * 
		 * break; }
		 */

		}
		return super.onOptionsItemSelected(item);
	}

}
