package com.project.xmlmanager;

import java.net.URL;
import java.util.ArrayList;
import java.util.HashMap;

import javax.xml.parsers.SAXParser;
import javax.xml.parsers.SAXParserFactory;

import org.xml.sax.InputSource;
import org.xml.sax.XMLReader;

import android.content.res.Resources;
import android.util.Log;

import com.project.R;
import com.project.etape_objet.parser.Etape_ObjetXMLGettersSetters;
import com.project.etape_objet.parser.Etape_ObjetXMLHandler;
import com.project.manager.Constants;

public class Etape_ObjetXMLManager {

	private Resources _resources;
	private int EtapeId;

	public Etape_ObjetXMLManager(int EtapeID,Resources resources) {
		
		this.EtapeId = EtapeID;
		this._resources = resources;
		
	}

	public ArrayList<HashMap<String, Object>> getObjets() {
		/**
		 * Parsing avec SAX : Plus Rapid !
		 */
		
		// Création de la ArrayList qui nous permettra de remplire la listView
		ArrayList<HashMap<String, Object>> listItem = new ArrayList<HashMap<String, Object>>();

		Etape_ObjetXMLGettersSetters data;

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
			Etape_ObjetXMLHandler myXMLHandler = new Etape_ObjetXMLHandler();
			xmlR.setContentHandler(myXMLHandler);

		//	xmlR.parse(new InputSource(this._resources.openRawResource(R.raw.etape_objet)));
			
			xmlR.parse(new InputSource(new URL(Constants.IP+"etape_objet.xml").openStream())); 


		} catch (Exception e) {
			System.out.println(e);
		}

		// Récupération des valeurs !!

		data = Etape_ObjetXMLHandler.data;
		
		if(data == null)Log.i("Etape_ObjetXMLManager","data is null");

		Log.i("Etape_ObjetManager:dataSize", data.getObjet_Points().size() + "");
		
		for (int i = 0; i < data.getId().size(); i++) {
			if (Integer.parseInt(data.getetape().get(i)) == EtapeId) {
				HashMap<String, Object> map = new HashMap<String, Object>();

				map.put("Id", data.getId().get(i).toString());
				map.put("Etape", data.getetape().get(i));
				map.put("Objet", data.getobjet().get(i));
				map.put("Points3d", data.getObjet_Points().get(i));
				listItem.add(map);
			}

		}
		return listItem;
	}


}
