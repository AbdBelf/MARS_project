package com.project.etape.parse;

import java.util.ArrayList;

import android.util.Log;

import com.project.manager.Point3d;
import com.project.typepanne.parser.XMLGettersSetters;

public class EtapeXMLGettersSetters extends XMLGettersSetters {

	private ArrayList<String> procedure = new ArrayList<String>();

	private ArrayList<ArrayList<String>> objet = new ArrayList<ArrayList<String>>();
	private ArrayList<ArrayList<Point3d[]>> objet_Points = new ArrayList<ArrayList<Point3d[]>>();

	public ArrayList<String> getprocedure() {
		return procedure;
	}

	public void setprocedure(String procedure) {
		this.procedure.add(procedure);
		Log.i("EtapeXMLGS: This is the procedure:", procedure);
	}

	
	public ArrayList<ArrayList<String>> getobjet() {
		return objet;
	}

	public void setobjet(ArrayList<String> objet) {
		this.objet.add(objet);
		//Log.i("Etape_ObjetXMLGettersSetters:This is the objet:", objet);
	}

	public ArrayList<ArrayList<Point3d[]>> getObjet_Points() {
		return objet_Points;
	}

	public void setObjet_Points(ArrayList<Point3d[]> objet_Points) {
		this.objet_Points.add(objet_Points);
	}

}
