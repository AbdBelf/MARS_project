package com.project.manager;

import java.util.ArrayList;

import android.hardware.Camera.Parameters;

import com.project.etapes.Etape;


public class Constants   {
	
	
	


	
	public  static  int PATT_MAX = 6;
	public static  int MARKER_MAX = 8;

	public static  int NB_MODEL = 0;


	public static int nbObjProcedure = 0;
	public static int etapeActuelle = 0;

	public static  String  msg_affiche = "";
	public static ArrayList<Etape> etapes = new ArrayList<Etape>();
	
	public static Parameters parameters;
	
	public static  String NAMESPACE = "http://tempuri.org/";


	public static String IP = "http://192.168.43.96/Android/";
	

	public static int valeurBinarisation = 95;
	
	//43.7
	//.1.227
	//10.0.0.2
	
	public static String nomTechnicien = "";
	
	public static String typePanne = "";
	public static String panne = "";
//	public static String procedureMaintenance = "";
	public static String DiagnosticPanne = "";
	
	public  static  int COMPRESSION = 75;
	
	public static String FICHIER_TYPE_PANNE = "typepanne.xml";
	public static String FICHIER_PANNE = "panne.xml";
	public static String FICHIER_PROCEDURE = "procedure.xml";
	public static String FICHIER_ETAPE = "etape.xml";
	public static String FICHIER_OBJET3D = "objet3d.xml";
	

	public static String repertoire = "Data/";

	public static ArrayList<String> Messages = new ArrayList<String>();
	
	public static boolean dialogOpen = false;
	
      
	
	
}
