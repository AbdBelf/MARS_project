package com.project.etape_objet.parser;

import java.util.ArrayList;

import android.util.Log;

import com.project.manager.Point3d;

public class Etape_ObjetXMLGettersSetters {

	private ArrayList<Integer> id = new ArrayList<Integer>();
	private ArrayList<String> etape = new ArrayList<String>();
	private ArrayList<String> objet = new ArrayList<String>();
	private ArrayList<Point3d[]> objet_Points = new ArrayList<Point3d[]>();
	
	
		public ArrayList<Integer> getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id.add(id);
		Log.i("Etape_ObjetXMLGettersSetters:This is the id:", id.toString());
	}

		public ArrayList<String> getetape() {
		return etape;
	}

	public void setetape(String etape) {
		this.etape.add(etape);
		Log.i("Etape_ObjetXMLGettersSetters:This is the etape:", etape);
	}

	public ArrayList<String> getobjet() {
		return objet;
	}

	public void setobjet(String objet) {
		this.objet.add(objet);
		Log.i("Etape_ObjetXMLGettersSetters:This is the objet:", objet);
	}

	public ArrayList<Point3d[]> getObjet_Points() {
		return objet_Points;
	}

	public void setObjet_Points(Point3d[] objet_Points) {
		this.objet_Points.add(objet_Points);
	}
}
