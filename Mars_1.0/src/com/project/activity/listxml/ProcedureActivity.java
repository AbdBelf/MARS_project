package com.project.activity.listxml;

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
import com.project.etapes.Etape;
import com.project.etapes.Objet;
import com.project.expert.DescriptionAide;
import com.project.manager.Constants;
import com.project.manager.Point3d;
import com.project.manager.Position;
import com.project.manager.Rotation;
import com.project.manager.Scale;
import com.project.mars.MarsARActivity;
import com.project.procedure.parser.ProcedureXMLGettersSetters;
import com.project.procedure.parser.ProcedureXMLHandler;
import com.project.xmlmanager.EtapeXMLManager;
import com.project.xmlmanager.ObjectXMLManager;

public class ProcedureActivity extends Activity {

	// data

	private ListView maListViewProc;
	private int PanneId;
	Context context = this;

	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		requestWindowFeature(Window.FEATURE_NO_TITLE);

		setContentView(R.layout.liste);

		ImageView imV = (ImageView) findViewById(R.id.imageViewLayoutManager);
		imV.setImageResource(R.drawable.procedurelogo);


		
		SharedPreferences s = getSharedPreferences("preferences", 0);
	
		
		
		Log.e("IP", "IP :" + s.getString("IP", ""));
		
		Constants.IP = s.getString("IP", "");
		

	//	Log.e("IP", "IP :" + s.getString("nomTechnicien", ""));

		Constants.nomTechnicien = s.getString("nomTechnicien", "");
		
		
		
		
		if (this.getIntent().getExtras() != null)
			PanneId = Integer.parseInt(this.getIntent().getExtras()
					.getString("panne"));

		// Récupération de la listview créée dans le fichier main.xml
		maListViewProc = (ListView) findViewById(R.id.custom_list_view);

		// Création de la ArrayList qui nous permettra de remplire la listView
		ArrayList<HashMap<String, String>> listItem = new ArrayList<HashMap<String, String>>();

		// On déclare la HashMap qui contiendra les informations pour un item
		// Création d'une HashMap pour insérer les informations du premier item
		// de notre listView

		/**
		 * Parsing avec SAX : Plus Rapid !
		 */

		ProcedureXMLGettersSetters data;

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
			ProcedureXMLHandler myXMLHandler = new ProcedureXMLHandler();
			xmlR.setContentHandler(myXMLHandler);

			// xmlR.parse(new
			// InputSource(getResources().openRawResource(R.raw.procedure)));

			xmlR.parse(new InputSource(new URL(Constants.IP + "data/procedure.xml")
					.openStream()));

		
		// Récupération des valeurs !!

		data = ProcedureXMLHandler.data;

		Log.i("ProcedureActivity", data.getId().size() + "");
		for (int i = 0; i < data.getlibelle().size(); i++) {
			if (Integer.parseInt(data.getpanne().get(i)) == PanneId) {
				HashMap<String, String> map = new HashMap<String, String>();

				map.put("Id", data.getId().get(i).toString());
				map.put("Libelle", data.getlibelle().get(i));
				map.put("Description", data.getdescription().get(i));
				map.put("panne", data.getpanne().get(i));

					map.put("img", String.valueOf(R.drawable.maintenance));
			
				listItem.add(map);
			}

		}

		// Création d'un SimpleAdapter qui se chargera de mettre les items
		// présent dans notre list (listItem) dans la vue affichageitem
		SimpleAdapter mSchedule = new SimpleAdapter(this.getBaseContext(),
				listItem, R.layout.custom_layout, new String[] { "img",
						"Libelle", "Description" }, new int[] { R.id.img,
						R.id.Libelle, R.id.Description });

		// On attribut à notre listView l'adapter que l'on vient de créer
		maListViewProc.setAdapter(mSchedule);

		// Enfin on met un écouteur d'évènement sur notre listView
		maListViewProc.setOnItemClickListener(new OnItemClickListener() {
			@Override
			@SuppressWarnings("unchecked")
			public void onItemClick(AdapterView<?> a, View v, int position,
					long id) {
				
				try{

				// Vider la liste au cas ou on entre une 2eme fois
				Constants.etapes.clear();

				// on récupère la HashMap contenant les infos de notre item
				// (titre, description, img)
				HashMap<String, String> map = (HashMap<String, String>) maListViewProc
						.getItemAtPosition(position);

			
				
				// recuperer toute les etapes pour la procedure selectionee
				EtapeXMLManager etapeManager = new EtapeXMLManager(Integer.parseInt(map.get("Id").toString()), getResources());

				
				
				ArrayList<HashMap<String, Object>> etapesdeProcedure = etapeManager.getEtapes();

				// simple affichage des etapes recuperees pour tester
				// l'execution
			

				
				
				Constants.nbObjProcedure = 0;
				Constants.NB_MODEL = 0;
				Constants.etapeActuelle = 0;

				
				
				// recuperation des objets 3d pour chaque etape
				for (int i = 0; i < etapesdeProcedure.size(); i++) {
					// pour chaque etape(i) recuperer les objets..
					// TODO
					// recuperer toute les objets pour chaque etape
					
					HashMap<String, Object> mMap = etapesdeProcedure.get(i);
					
			
					
					
				//	Etape_ObjetXMLManager2 etape_objectManager = new Etape_ObjetXMLManager2(Integer.parseInt(mMap.get("Id").toString()),getResources());

				//	HashMap<String, Object> objetsdEtape = etape_objectManager.getObjets();

				
					

					// ce manager nous permet de recuperer les noms des objets a
					// partir de leurs ids
					
					ObjectXMLManager objectmanager = new ObjectXMLManager( ProcedureActivity.this, getResources());

					Etape mEtape = new Etape(Integer.parseInt(mMap.get("Id").toString()), mMap.get("Libelle").toString(), mMap.get("Description").toString());

					ArrayList<String> listeObjet = (ArrayList<String>) mMap.get("Objet");
					ArrayList<Point3d[]> listePoints3d = (ArrayList<Point3d[]>) mMap.get("Points3d");


					
					for (int j = 0; j < listeObjet.size(); j++) {

						Point3d[] pnts = listePoints3d.get(j);
						
						String mid = listeObjet.get(j).toString();
						
					
						
						
						Position mposition = (Position) pnts[0];
						Rotation mrotation = (Rotation) pnts[1];
						Scale mscale = (Scale) pnts[2];

						Objet obj = new Objet(Integer.parseInt(mid),
								objectmanager.getObjectName(Integer
										.parseInt(mid)), objectmanager
										.getObjectType(Integer.parseInt(mid)),
								mposition, mrotation, mscale);

						mEtape.addObjet(obj);

						Constants.nbObjProcedure++;
						
					}

					com.project.manager.Constants.etapes.add(mEtape);

					Log.i("Etape " + i,
							"Etapes.size() :" + Constants.etapes.size());

					
					
					// apres la detection des etapes et leurs objets ossociés on
					// peut lancer l'augmentation

					
				}

				int xy = Constants.etapes.get(Constants.etapeActuelle)
						.getObjets().size();
				Constants.NB_MODEL = xy;

				Log.i("ProcedureActivity", "etape.size" + xy + " nbModel "
						+ Constants.NB_MODEL);

				Intent intent = new Intent(ProcedureActivity.this,
						MarsARActivity.class);

				((Activity) context).startActivityForResult(intent, 1000);

				
				// context.startActivity(intent);

				} catch (Exception e) {

					Log.e("Error", e.toString());
					System.out.println(e);
					Toast.makeText(
							context,
							"Connexion à internet perdue ! ",
							Toast.LENGTH_LONG).show();

					// finish();

				}
			}
		});
		
		} catch (Exception e) {
			System.out.println(e);
			
			Log.e("Error", e.toString());
			//System.out.println(e);
			Toast.makeText(
					context,
					"Veuillez vérifier votre connexion à internet !  ",
					Toast.LENGTH_LONG).show();
		}



	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {

	
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {

		// ajout au submenu
		menu.add(0, 101, 0, "Aide Expert procédure").setIcon(
				R.drawable.spanner32);
		menu.add(0, 100, 0, "Quitter").setIcon(R.drawable.stop32);

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
			 * Intent intent = new Intent(context, LoginDialog.class);
			 * 
			 * this.startActivityForResult(intent, 102);
			 */

			Intent intent = new Intent(context, DescriptionAide.class);

			startActivity(intent);

			break;
		}

		}
		return super.onOptionsItemSelected(item);
	}
}
