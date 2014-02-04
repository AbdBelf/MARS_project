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

import com.project.R;
import com.project.etapes.Etape;
import com.project.etapes.Objet;
import com.project.manager.Constants;
import com.project.manager.Point3d;
import com.project.manager.Position;
import com.project.manager.Rotation;
import com.project.manager.Scale;
import com.project.mars.MarsARActivity;
import com.project.procedure.parser.ProcedureXMLGettersSetters;
import com.project.procedure.parser.ProcedureXMLHandler;
import com.project.xmlmanagerHorsligne.EtapeXMLManagerHorsligne;
import com.project.xmlmanagerHorsligne.ObjectXMLManagerHorsligne;

public class ProcedureActivityHorsligne extends Activity {

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

			FileInputStream fis = this.openFileInput(Constants.FICHIER_PROCEDURE );
			
			xmlR.parse(new InputSource(fis));
			
			fis.close();

		} catch (Exception e) {
			System.out.println(e);
		}

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

				// Vider la liste au cas ou on entre une 2eme fois
				Constants.etapes.clear();

				// on récupère la HashMap contenant les infos de notre item
				// (titre, description, img)
				HashMap<String, String> map = (HashMap<String, String>) maListViewProc
						.getItemAtPosition(position);

			
				
				// recuperer toute les etapes pour la procedure selectionee
				EtapeXMLManagerHorsligne etapeManager = new EtapeXMLManagerHorsligne(Integer.parseInt(map.get("Id").toString()), getResources(), context);

				
				
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
					
					ObjectXMLManagerHorsligne objectmanager = new ObjectXMLManagerHorsligne( ProcedureActivityHorsligne.this, getResources());

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

				Intent intent = new Intent(ProcedureActivityHorsligne.this,
						MarsARActivity.class);

				((Activity) context).startActivityForResult(intent, 1000);

				// context.startActivity(intent);

			}
		});

	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {

	
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {

		// ajout au submenu
		menu.add(0, 100, 0, "Quit");
		
		getMenuInflater().inflate(R.menu.activity_main, menu);

		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {

		switch (item.getItemId()) {
		case 100:
			System.exit(0);
			break;

		
		}
		return super.onOptionsItemSelected(item);
	}
}


//package com.project.horsligne;
//
//import java.io.FileInputStream;
//import java.util.ArrayList;
//import java.util.HashMap;
//
//import javax.xml.parsers.SAXParser;
//import javax.xml.parsers.SAXParserFactory;
//
//import org.xml.sax.InputSource;
//import org.xml.sax.XMLReader;
//
//import android.app.Activity;
//import android.content.Context;
//import android.content.Intent;
//import android.os.Bundle;
//import android.util.Log;
//import android.view.Menu;
//import android.view.MenuItem;
//import android.view.View;
//import android.view.Window;
//import android.widget.AdapterView;
//import android.widget.AdapterView.OnItemClickListener;
//import android.widget.ImageView;
//import android.widget.ListView;
//import android.widget.SimpleAdapter;
//
//import com.project.R;
//import com.project.etapes.Etape;
//import com.project.etapes.Objet;
//import com.project.manager.Constants;
//import com.project.manager.Point3d;
//import com.project.manager.Position;
//import com.project.manager.Rotation;
//import com.project.manager.Scale;
//import com.project.mars.MarsARActivity;
//import com.project.procedure.parser.ProcedureXMLGettersSetters;
//import com.project.procedure.parser.ProcedureXMLHandler;
//import com.project.xmlmanagerHorsligne.EtapeXMLManagerHorsligne;
//import com.project.xmlmanagerHorsligne.Etape_ObjetXMLManagerHorsligne2;
//import com.project.xmlmanagerHorsligne.ObjectXMLManagerHorsligne;
//
//public class ProcedureActivityHorsligne extends Activity{
//
//	
//	// data
//
//	 private ListView maListViewProc;
//	 private int PanneId;
//	 Context context = this;
//
//	/** Called when the activity is first created. */
//	@Override
//	public void onCreate(Bundle savedInstanceState) {
//		super.onCreate(savedInstanceState);
//
//
//		requestWindowFeature(Window.FEATURE_NO_TITLE);
//
//		setContentView(R.layout.liste);
//
//		ImageView imV = (ImageView) findViewById(R.id.imageViewLayoutManager);
//		imV.setImageResource(R.drawable.procedurelogo);
//
//		
//
//		if (this.getIntent().getExtras() != null)
//			PanneId = Integer.parseInt(this.getIntent().getExtras().getString("panne"));
//		//Toast.makeText(this, this.getIntent().getExtras().getString("panne"), Toast.LENGTH_SHORT).show();
//        
//        
//        
//		// Récupération de la listview créée dans le fichier main.xml
//		maListViewProc = (ListView) findViewById(R.id.custom_list_view);
//
//		// Création de la ArrayList qui nous permettra de remplire la listView
//		ArrayList<HashMap<String, String>> listItem = new ArrayList<HashMap<String, String>>();
//
//		// On déclare la HashMap qui contiendra les informations pour un item
//		// Création d'une HashMap pour insérer les informations du premier item
//		// de notre listView
//				
//		/**
//		 * Parsing avec SAX : Plus Rapid !
//		 */
//		
//		
//		ProcedureXMLGettersSetters data;
//
//		
//		
//		try {
//			
//			/**
//			 * Create a new instance of the SAX parser
//			 **/
//			SAXParserFactory saxPF = SAXParserFactory.newInstance();
//			SAXParser saxP = saxPF.newSAXParser();
//			XMLReader xmlR = saxP.getXMLReader();
//
//			
//			
//			/** 
//			 * Create the Handler to handle each of the XML tags. 
//			 **/
//			ProcedureXMLHandler myXMLHandler = new ProcedureXMLHandler();
//			xmlR.setContentHandler(myXMLHandler);
//			
//			FileInputStream fis = this.openFileInput(Constants.FICHIER_PROCEDURE );
//			
//			xmlR.parse(new InputSource(fis));
//			
//			fis.close();
//			
//			
//		
//			
//		} catch (Exception e) {
//			System.out.println(e);
//		}
//
//		
////Récupération des valeurs !!
//		
//		data = ProcedureXMLHandler.data;
//		
//	
//		
//		
//		Log.i("ProcedureActivity",data.getId().size()+"");
//		for (int i=0; i<data.getlibelle().size(); i++){
//			if (Integer.parseInt(data.getpanne().get(i)) == PanneId) {
//				HashMap<String, String> map = new HashMap<String, String>();
//
//				map.put("Id", data.getId().get(i).toString());
//				map.put("Libelle", data.getlibelle().get(i));
//				map.put("Description", data.getdescription().get(i));
//				map.put("panne", data.getpanne().get(i));
//
//				switch (i) {
//				case 0:
//					map.put("img", String.valueOf(R.drawable.batterie));
//					break;
//				case 1:
//					map.put("img", String.valueOf(R.drawable.frein));
//					break;
//				case 2:
//					map.put("img", String.valueOf(R.drawable.echapp));
//					break;
//				case 3:
//					map.put("img", String.valueOf(R.drawable.moteur));
//					break;
//
//				default:
//					map.put("img", String.valueOf(R.drawable.maintenance));
//					// map.put("img", "batterie");
//					Log.i("IMAGE", String.valueOf(map.get("img")));
//					break;
//				}
//				listItem.add(map);
//			}
//			
//		}
//			
//		// Création d'un SimpleAdapter qui se chargera de mettre les items
//		// présent dans notre list (listItem) dans la vue affichageitem
//		SimpleAdapter mSchedule = new SimpleAdapter(this.getBaseContext(),listItem, R.layout.custom_layout, new String[] { "img",
//						"Libelle", "Description" }, new int[] { R.id.img,R.id.Libelle, R.id.Description });
//
//		// On attribut à notre listView l'adapter que l'on vient de créer
//		maListViewProc.setAdapter(mSchedule);
//
//		// Enfin on met un écouteur d'évènement sur notre listView
//		maListViewProc.setOnItemClickListener(new OnItemClickListener() {
//			@Override
//			@SuppressWarnings("unchecked")
//			public void onItemClick(AdapterView<?> a, View v, int position, long id) {
//				
//				
//				//Vider la liste au cas ou on entre une 2eme fois 
//				Constants.etapes.clear();
//				
//				
//				// on récupère la HashMap contenant les infos de notre item
//				// (titre, description, img)
//				HashMap<String, String> map = (HashMap<String, String>) maListViewProc.getItemAtPosition(position);
//				
//				//recuperer toute les etapes pour la procedure selectionee
//				EtapeXMLManagerHorsligne etapeManager = new EtapeXMLManagerHorsligne(Integer.parseInt(map.get("Id").toString()), getResources() , context);
//
//				ArrayList<HashMap<String, String>> etapesdeProcedure = etapeManager.getEtapes();
//
//				//simple affichage des etapes recuperees pour tester l'execution
//				String mString = "";
//				for (int i = 0; i < etapesdeProcedure.size(); i++) {
//					mString += "Id: " + etapesdeProcedure.get(i).get("Id")+ "    Libellé: "+ etapesdeProcedure.get(i).get("Libelle") + "\n";
//				}
//			//	Toast.makeText(ProcedureActivity.this, mString,Toast.LENGTH_LONG).show();
//				
//
//				Constants.nbObjProcedure = 0;
//				Constants.NB_MODEL = 0;
//				Constants.etapeActuelle = 0;
//				
//				
//				//recuperation des objets 3d pour chaque etape
//				for (int i = 0; i < etapesdeProcedure.size(); i++) {
//					//pour chaque etape(i) recuperer les objets..
//					//TODO
//					
//					//recuperer toute les objets pour chaque etape
//					HashMap<String, String> mMap = etapesdeProcedure.get(i);
//					Etape_ObjetXMLManagerHorsligne2 etape_objectManager = new Etape_ObjetXMLManagerHorsligne2(Integer.parseInt(mMap.get("Id").toString()), getResources(),context);
//
//					ArrayList<HashMap<String, Object>> objetsdEtape = etape_objectManager.getObjets();
//
//					mString = "";
//					
//					Log.i("ProcedureActivity:etapesdeProcedure.size",
//							objetsdEtape.size() + "");
//					
//					//ce manager nous permet de recuperer les noms des objets a partir de leurs ids
//					ObjectXMLManagerHorsligne objectmanager = new ObjectXMLManagerHorsligne(ProcedureActivityHorsligne.this, getResources());
//					
//					Etape mEtape = new Etape(Integer.parseInt(mMap.get("Id")), mMap.get("Libelle"), mMap.get("Description"));
//					
//					for (int j = 0; j < objetsdEtape.size(); j++) {
//						
//						Point3d[] pnts = (Point3d[]) (objetsdEtape.get(j).get("Points3d"));
//						String mid = objetsdEtape.get(j).get("Objet").toString();
//						Position mposition = (Position) pnts[0];
//						Rotation mrotation = (Rotation)pnts[1];
//						Scale mscale = (Scale)pnts[2];
//						
//						Objet obj = new Objet(Integer.parseInt(mid),objectmanager.getObjectName(Integer.parseInt(mid)) 
//								, objectmanager.getObjectType(Integer.parseInt(mid)), 
//								mposition, mrotation, mscale);
//						
//						mEtape.addObjet(obj);
//						
//						Constants.nbObjProcedure ++;
//						
//						/*String str = "x: " + pnts[0].getX() + " y: "
//								+ pnts[0].getY() + " z: " + pnts[0].getZ()
//								+ "angle: " + ((Rotation) pnts[1]).getAngle()
//								+ "x: " + pnts[1].getX() + " y: "
//								+ pnts[1].getY() + " z: " + pnts[1].getZ()
//								+ "x: " + pnts[2].getX() + " y: "
//								+ pnts[2].getY() + " z: " + pnts[2].getZ();
//
//						mString += "Id: " + objetsdEtape.get(j).get("Id")
//								+ "    etape: "
//								+ objetsdEtape.get(j).get("Etape")
//								+ "    objet: "
//								+ objetsdEtape.get(j).get("Objet") + str + "\n";*/
//						
////						Toast.makeText(
////								ProcedureActivity.this,
////								"Object N°" + j + ": "
////										+ objectmanager.getObjectName(Integer.parseInt(objetsdEtape.get(j).get("Objet").toString())),
////								Toast.LENGTH_LONG).show();
////						
//						
//					}
//					
//					
//					com.project.manager.Constants.etapes.add(mEtape);
//					 
//					
//					Log.i("Etape "+i, "Etapes.size() :"+Constants.etapes.size());
//					
//					//Toast.makeText(ProcedureActivity.this, mString,Toast.LENGTH_LONG).show();
//					
//					//apres la detection des etapes et leurs objets ossociés on peut lancer l'augmentation
//					
//					
//				}
//				
//			
//				
//				int xy = Constants.etapes.get(Constants.etapeActuelle).getObjets().size();
//				Constants.NB_MODEL = xy;
//				
//				
//				
//				Log.i("ProcedureActivity","etape.size"+xy+" nbModel "+Constants.NB_MODEL);
//
//				
//				Intent intent = new Intent(ProcedureActivityHorsligne.this, MarsARActivity.class);
//				
//				((Activity) context).startActivityForResult(intent, 1000);
//
//				
//				//context.startActivity(intent);
//				
//			}
//		});
//
//	}
//	
//	@Override
//	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
//
////		data = null;
////
////		Log.i("ProcedureActivity", "Nbre etape " + Constants.etapes.size());
////
////		if (Constants.etapeActuelle < Constants.etapes.size()) {
////
////			Constants.etapeActuelle = Constants.etapeActuelle + 1;
////
////			Log.i("ProcedureActivity", "etape suivante "+ Constants.etapeActuelle);
////
////			Intent intent = new Intent(ProcedureActivity.this,
////					MarsARActivity.class);
////			this.startActivityForResult(intent, 1000);
////
////		}
//
//	}
//	
//
//	@Override
//	public boolean onCreateOptionsMenu(Menu menu) {
//
//		// ajout au submenu
//		menu.add(0, 100, 0, "Quit");
//		getMenuInflater().inflate(R.menu.activity_main, menu);
//
//		return true;
//	}
//    
//    @Override
//	public boolean onOptionsItemSelected(MenuItem item) {
//
//		switch (item.getItemId()) {
//		case 100:
//			System.exit(0);
//			break;
//
//
//		}
//		return super.onOptionsItemSelected(item);
//	}
//}
