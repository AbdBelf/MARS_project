package com.project.panne.parser;

import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;

import android.util.Log;


public class PanneXMLHandler extends DefaultHandler {

	String elementValue = null;
	Boolean elementOn = false;
	public static PanneXMLGettersSetters data = null;
	

	public static PanneXMLGettersSetters getXMLData() {
		return data;
	}

	public static void setXMLData(PanneXMLGettersSetters data) {
		PanneXMLHandler.data = data;
	}

	/** 
	 * This will be called when the tags of the XML starts.
	 **/
	@Override
	public void startElement(String uri, String localName, String qName,Attributes attributes) throws SAXException {

		elementOn = true;

		Log.i("localName", localName);
			if(localName.equals("Pannes"))
			{
				data = new PanneXMLGettersSetters();
			}
			else
				if(localName.equals("panne")){			
					String attributeValue = attributes.getValue("id");
					data.setId(Integer.parseInt(attributeValue));
					}
	}

	/** 
	 * This will be called when the tags of the XML end.
	 **/
	@Override
	public void endElement(String uri, String localName, String qName)
			throws SAXException {

		elementOn = false;

		/** 
		 * Sets the values after retrieving the values from the XML tags
		 * */
		Log.i("PanneXMLHandler","localName: "+localName);
			if (localName.equalsIgnoreCase("libelle"))
				data.setlibelle(elementValue);
		else
			if (localName.equalsIgnoreCase("description"))
				data.setdescription(elementValue);
			else
				if (localName.equalsIgnoreCase("type_panne"))
					data.setType_panne(elementValue);

	}

	/** 
	 * This is called to get the tags value
	 **/
	@Override
	public void characters(char[] ch, int start, int length) throws SAXException {

		if (elementOn) {
			elementValue = new String(ch, start, length);
			elementOn = false;
		}

	}

}
