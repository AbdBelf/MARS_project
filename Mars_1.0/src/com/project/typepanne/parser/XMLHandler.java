package com.project.typepanne.parser;

import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;

import android.util.Log;

public class XMLHandler extends DefaultHandler {

	String elementValue = null;
	Boolean elementOn = false;
	public static XMLGettersSetters data = null;
	

	public static XMLGettersSetters getXMLData() {
		return data;
	}

	public static void setXMLData(XMLGettersSetters data) {
		XMLHandler.data = data;
	}

	/** 
	 * This will be called when the tags of the XML starts.
	 **/
	@Override
	public void startElement(String uri, String localName, String qName,Attributes attributes) throws SAXException {

		elementOn = true;

		Log.i("localName", localName);
		
		if (localName.equals("TypePanne"))
		{
			data = new XMLGettersSetters();
		}
		else
			if(localName.equals("Pannes"))
			{
				data = new XMLGettersSetters();
			}
			else
				if(localName.equals("Procedures"))
				{
					data = new XMLGettersSetters();
				}
				else
					if(localName.equals("Etapes"))
					{
						data = new XMLGettersSetters();
					}
					else
						if(localName.equals("type")){
							String attributeValue = attributes.getValue("id");
							data.setId(Integer.parseInt(attributeValue));
							}
						else
							if(localName.equals("panne")){
								String attributeValue = attributes.getValue("id");
								data.setId(Integer.parseInt(attributeValue));
								}
							else
								if(localName.equals("procedure")){
									String attributeValue = attributes.getValue("id");
									data.setId(Integer.parseInt(attributeValue));
									}
								else
									if(localName.equals("etape")){
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
		Log.i("XMLHandler","localName: "+localName);
			if (localName.equalsIgnoreCase("libelle"))
				data.setlibelle(elementValue);
		else
			if (localName.equalsIgnoreCase("description"))
				data.setdescription(elementValue);

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
