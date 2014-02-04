package com.project.xmlmanagerHorsligne;

import java.io.FileInputStream;
import java.util.ArrayList;
import java.util.HashMap;

import javax.xml.parsers.SAXParser;
import javax.xml.parsers.SAXParserFactory;

import org.xml.sax.InputSource;
import org.xml.sax.XMLReader;

import android.content.Context;
import android.content.res.Resources;
import android.util.Log;

import com.project.etape.parse.EtapeXMLGettersSetters;
import com.project.etape.parse.EtapeXMLHandler;
import com.project.manager.Constants;

public class EtapeXMLManagerHorsligne {

	private Resources _resources;
	private int ProcedureId;
	private Context context;
	

	public EtapeXMLManagerHorsligne(int procedureID,Resources resources , Context context) {
		
		this.ProcedureId = procedureID;
		this._resources = resources;
		this.context = context;
	}

	public ArrayList<HashMap<String, Object>> getEtapes() {
		/**
		 * Parsing avec SAX : Plus Rapid !
		 */
		
		// Création de la ArrayList qui nous permettra de remplire la listView
		ArrayList<HashMap<String, Object>> listItem = new ArrayList<HashMap<String, Object>>();

		EtapeXMLGettersSetters data;

		EtapeXMLHandler myXMLHandler = new EtapeXMLHandler();
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
			
			xmlR.setContentHandler(myXMLHandler);

			//xmlR.parse(new InputSource(this._resources.openRawResource(R.raw.etape)));
			
			FileInputStream fis = 	context.openFileInput(Constants.FICHIER_ETAPE );
			
			
			xmlR.parse(new InputSource(fis));
			

		} catch (Exception e) {
			System.out.println(e);
		}

		// Récupération des valeurs !!

		data = myXMLHandler.getXMLData();
		
		Log.i("EtapeManager","EtapeManagerSize :"+ data.getId().size());
		
		if(data == null) Log.i("EtapeXMLManager","data is null");

		
		
		for (int i = 0; i < data.getId().size(); i++) {
			
			if (Integer.parseInt(data.getprocedure().get(i)) == ProcedureId) {
				
				HashMap<String, Object> map = new HashMap<String, Object>();

				map.put("Id", data.getId().get(i).toString());
				map.put("Libelle", data.getlibelle().get(i));
				map.put("Description", data.getdescription().get(i));
				map.put("procedure", data.getprocedure().get(i));
				map.put("Objet", data.getobjet().get(i));
				map.put("Points3d", data.getObjet_Points().get(i));
				
				Log.i("EtapeXMLManager","OKETAPE "+data.getlibelle().get(i));
				
				listItem.add(map);
			}

		}
		return listItem;
	}

}



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
//import android.content.Context;
//import android.content.res.Resources;
//import android.util.Log;
//
//import com.project.etape.parse.EtapeXMLGettersSetters;
//import com.project.etape.parse.EtapeXMLHandler;
//import com.project.manager.Constants;
//
//public class EtapeXMLManagerHorsligne {
//
//	private Resources _resources;
//	private int ProcedureId;
//	
//	Context context;
//
//	public EtapeXMLManagerHorsligne(int procedureID,Resources resources , Context context) {
//		
//		this.ProcedureId = procedureID;
//		this._resources = resources;
//		this.context = context;
//	}
//
//	public ArrayList<HashMap<String, String>> getEtapes() {
//		/**
//		 * Parsing avec SAX : Plus Rapid !
//		 */
//		
//		// Création de la ArrayList qui nous permettra de remplire la listView
//		ArrayList<HashMap<String, String>> listItem = new ArrayList<HashMap<String, String>>();
//
//		EtapeXMLGettersSetters data;
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
//			/**
//			 * Create the Handler to handle each of the XML tags.
//			 **/
//			EtapeXMLHandler myXMLHandler = new EtapeXMLHandler();
//			xmlR.setContentHandler(myXMLHandler);
//
//		
//			FileInputStream fis = 	context.openFileInput(Constants.FICHIER_ETAPE );
//			
//			
//			xmlR.parse(new InputSource(fis));
//			
//			//xmlR.parse(new InputSource(new URL(Constants.IP+"etape.xml").openStream())); 
//			
//
//		} catch (Exception e) {
//			System.out.println(e);
//		}
//
//		// Récupération des valeurs !!
//
//		data = EtapeXMLHandler.data;
//		
//		if(data == null)Log.i("EtapeXMLManager","data is null");
//
//		Log.i("EtapeManager", data.getId().size() + "");
//		
//		for (int i = 0; i < data.getlibelle().size(); i++) {
//			if (Integer.parseInt(data.getprocedure().get(i)) == ProcedureId) {
//				HashMap<String, String> map = new HashMap<String, String>();
//
//				map.put("Id", data.getId().get(i).toString());
//				map.put("Libelle", data.getlibelle().get(i));
//				map.put("Description", data.getdescription().get(i));
//				map.put("procedure", data.getprocedure().get(i));
//				listItem.add(map);
//			}
//
//		}
//		return listItem;
//	}
//
//}
