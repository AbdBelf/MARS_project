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
import com.project.manager.Constants;
import com.project.panne.parser.PanneXMLGettersSetters;
import com.project.panne.parser.PanneXMLHandler;

public class PanneActivityHorsligne extends Activity {
	
	// data

	 private ListView maListViewPanne;
	 private int typePanneId;
	 private Context context;
	 private PanneActivityHorsligne _this;

	 public ArrayList<HashMap<String, String>> listItem;
	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		 requestWindowFeature(Window.FEATURE_NO_TITLE);

		setContentView(R.layout.liste);

		
		ImageView imV = (ImageView)findViewById(R.id.imageViewLayoutManager);
		imV.setImageResource(R.drawable.panneslogo);
	
		
		context = this;
		_this =this;
		if (this.getIntent().getExtras() != null)
			typePanneId = Integer.parseInt(this.getIntent().getExtras().getString("type"));
	//	Toast.makeText(this, this.getIntent().getExtras().getString("type"), Toast.LENGTH_SHORT).show();
        //Log.d("Xpath", "initialize");
        
        
		// Récupération de la listview créée dans le fichier main.xml
		maListViewPanne = (ListView) findViewById(R.id.custom_list_view);

		// Création de la ArrayList qui nous permettra de remplire la listView
		 listItem = new ArrayList<HashMap<String, String>>();

		// On déclare la HashMap qui contiendra les informations pour un item
		// Création d'une HashMap pour insérer les informations du premier item
		// de notre listView
				
		/**
		 * Parsing avec SAX : Plus Rapid !
		 */
		
		
		PanneXMLGettersSetters data;

		
		
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
			PanneXMLHandler myXMLHandler = new PanneXMLHandler();
			xmlR.setContentHandler(myXMLHandler);
			
		
			FileInputStream fis = this.openFileInput(Constants.FICHIER_PANNE );
			
			xmlR.parse(new InputSource(fis));
			
			fis.close();

			
			
		} catch (Exception e) {
			System.out.println(e);
		}

		
//Récupération des valeurs !!
		
		data = PanneXMLHandler.data;
		
	
		
		
		Log.i("PanneActivity",data.getId().size()+"");
		
		
		for (int i=0; i<data.getlibelle().size(); i++){
			
			if (Integer.parseInt(data.getType_panne().get(i)) == typePanneId) {
				
				HashMap<String, String> map = new HashMap<String, String>();

				map.put("Id", data.getId().get(i).toString());
				map.put("Libelle", data.getlibelle().get(i));
				map.put("Description", data.getdescription().get(i));
				map.put("Type_panne", data.getType_panne().get(i));

				int q = Integer.parseInt(data.getType_panne().get(i));
				switch (q) {
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
					// map.put("img", "batterie");
					Log.i("IMAGE", String.valueOf(map.get("img")));
					break;
				}
				listItem.add(map);
			}
			
		}
			
		// Création d'un SimpleAdapter qui se chargera de mettre les items
		// présent dans notre list (listItem) dans la vue affichageitem
		SimpleAdapter mSchedule = new SimpleAdapter(this.getBaseContext(),listItem, R.layout.custom_layout, new String[] { "img",
						"Libelle", "Description" }, new int[] { R.id.img,R.id.Libelle, R.id.Description });

		// On attribut à notre listView l'adapter que l'on vient de créer
		maListViewPanne.setAdapter(mSchedule);

		// Enfin on met un écouteur d'évènement sur notre listView
		maListViewPanne.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> a, View v, int position, long id) {
				// on récupère la HashMap contenant les infos de notre item
				// (titre, description, img)
				
				Log.i("on Click","ON CLICK");
				@SuppressWarnings("unchecked")
				HashMap<String, String> map = (HashMap<String, String>) maListViewPanne.getItemAtPosition(position);
				
				Intent intent = new Intent(context,ProcedureActivityHorsligne.class);
				intent.putExtra("panne", map.get("Id"));
				
				
			
				
				//Log.i("xxx",map.get("Id"));
				
				_this.startActivityForResult(intent, 101);
			}
		});

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
