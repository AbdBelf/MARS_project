package com.project.typepanne.parser;

import java.util.ArrayList;

import android.util.Log;

/**
 *  This class contains all getter and setter methods to set and retrieve data.
 *  
 **/
public class XMLGettersSetters {

	private ArrayList<Integer> id = new ArrayList<Integer>();
	private ArrayList<String> libelle = new ArrayList<String>();
	private ArrayList<String> description = new ArrayList<String>();
	
		public ArrayList<Integer> getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id.add(id);
		Log.i("This is the id:", id.toString());
	}

		public ArrayList<String> getlibelle() {
		return libelle;
	}

	public void setlibelle(String libelle) {
		this.libelle.add(libelle);
		Log.i("This is the libelle:", libelle);
	}

	public ArrayList<String> getdescription() {
		return description;
	}

	public void setdescription(String description) {
		this.description.add(description);
		Log.i("This is the description:", description);
	}

	

}
