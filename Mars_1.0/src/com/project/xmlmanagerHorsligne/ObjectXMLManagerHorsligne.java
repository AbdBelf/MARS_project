package com.project.xmlmanagerHorsligne;

import java.io.FileInputStream;

import javax.xml.xpath.XPath;
import javax.xml.xpath.XPathConstants;
import javax.xml.xpath.XPathFactory;

import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.InputSource;

import android.content.Context;
import android.content.res.Resources;
import android.util.Log;
import android.widget.Toast;

import com.project.R;
import com.project.manager.Constants;

public class ObjectXMLManagerHorsligne {
	Context context;
	private Resources _resources;

	
	// private Integer objetId;

	public ObjectXMLManagerHorsligne(/* Integer ObjetId, */Context context,
			Resources resources) {

		// this.objetId = ObjetId;
		this.context = context;
		this._resources = resources;

	}

	public String getObjectName(Integer ObjectId) {
		String result = "";

		try {
			// parseData

			// create input source
			// InputSource input = new
			// InputSource(this._resources.openRawResource(R.raw.objet3d));

			FileInputStream fis = 	context.openFileInput(Constants.FICHIER_OBJET3D );
			
			
			InputSource input = new InputSource(fis);


			// create xPath object
			XPath xpath = XPathFactory.newInstance().newXPath();

			// expression: recuperer le nom de lobjet dont l'id = ObjectId
			// /objet[@id='']/@nom
			String exp = "//objet[@id='" + ObjectId + "']/@nom";
			Log.i("ObjXMLManager:exp: ", exp);
			NodeList nodes = (NodeList) xpath.evaluate(exp, input,
					XPathConstants.NODESET);

			// Toast.makeText(
			// _context,
			// "ObjXMLManager:count: " + String.valueOf(nodes.getLength()),
			// Toast.LENGTH_SHORT).show();

			Node node = nodes.item(0);
			result = node.getTextContent();

		} catch (Exception ex) {
			Toast.makeText(this.context, "Exception: " + ex.getMessage(),
					Toast.LENGTH_LONG).show();
		}

		return result;
	}

	public String getObjectType(Integer ObjectId) {
		String result = "";

		try {
			
			FileInputStream fis = 	context.openFileInput(Constants.FICHIER_OBJET3D );
			
			// create input source
			InputSource input = new InputSource(fis);

			// create xPath object
			XPath xpath = XPathFactory.newInstance().newXPath();

			// expression: recuperer le type de l'objet dont l'id = ObjectId
			// /objet[@id='']/@nom
			String exp = "//objet[@id='" + ObjectId + "']/@type";
			Log.i("ObjXMLManager:exp: ", exp);
			NodeList nodes = (NodeList) xpath.evaluate(exp, input,
					XPathConstants.NODESET);

			// Toast.makeText(
			// _context,
			// "ObjXMLManager:count: " + String.valueOf(nodes.getLength()),
			// Toast.LENGTH_SHORT).show();

			Node node = nodes.item(0);
			result = node.getTextContent();

		} catch (Exception ex) {
			Toast.makeText(this.context, "Exception: " + ex.getMessage(),
					Toast.LENGTH_LONG).show();
		}

		return result;
	}

}
