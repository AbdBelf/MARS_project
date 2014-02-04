package com.project.panne.parser;

import java.util.ArrayList;

import com.project.typepanne.parser.XMLGettersSetters;

import android.util.Log;

public class PanneXMLGettersSetters extends XMLGettersSetters {

	private ArrayList<String> type_panne = new ArrayList<String>();

	public ArrayList<String> getType_panne() {
		return type_panne;
	}

	public void setType_panne(String type_panne) {
		this.type_panne.add(type_panne);
		Log.i("PanneXMLGS: This is the type_panne:", type_panne);
	}

}
