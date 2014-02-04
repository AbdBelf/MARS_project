package com.project.procedure.parser;

import java.util.ArrayList;
import android.util.Log;
import com.project.typepanne.parser.XMLGettersSetters;


public class ProcedureXMLGettersSetters extends XMLGettersSetters {

	private ArrayList<String> panne = new ArrayList<String>();

	public ArrayList<String> getpanne() {
		return panne;
	}

	public void setpanne(String panne) {
		this.panne.add(panne);
		Log.i("ProcedureXMLGS: This is the panne:", panne);
	}

}