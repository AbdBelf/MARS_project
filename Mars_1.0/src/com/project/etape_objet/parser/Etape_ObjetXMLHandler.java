package com.project.etape_objet.parser;

import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;

import android.util.Log;

import com.project.manager.Point3d;
import com.project.manager.Position;
import com.project.manager.Rotation;
import com.project.manager.Scale;


public class Etape_ObjetXMLHandler extends DefaultHandler {

	String elementValue = null;
	Boolean elementOn = false;
	public static Etape_ObjetXMLGettersSetters data = null;
	
	private Point3d[] points3d = null;
	

	public static Etape_ObjetXMLGettersSetters getXMLData() {
		return data;
	}

	public static void setXMLData(Etape_ObjetXMLGettersSetters data) {
		Etape_ObjetXMLHandler.data = data;
	}

	/** 
	 * This will be called when the tags of the XML starts.
	 **/
	@Override
	public void startElement(String uri, String localName, String qName,Attributes attributes) throws SAXException {

		elementOn = true;

		Log.i("Etape_ObjetXMLHandler:localName", localName);
			if(localName.equals("Etapes_Objets"))
			{
				data = new Etape_ObjetXMLGettersSetters();
			}
			else
				if(localName.equals("etape_objet")){			
					String attributeValue = attributes.getValue("id");
					data.setId(Integer.parseInt(attributeValue));
					}
				else
					if(localName.equals("etape")){
						String attributeValue = attributes.getValue("id");
						data.setetape(attributeValue);
						}
				else
					if(localName.equals("objet")){
						points3d = new Point3d[3];
						String attributeValue = attributes.getValue("id");
						data.setobjet(attributeValue);
						}
					else
						if(localName.equals("position")){
							Position pos = new Position();
							//get x position
							String attValue = attributes.getValue("x");
							
							Log.i("Etape_ObjetXMLHandler:value2BCasted",attValue);
							pos.setX(Float.parseFloat(attValue));
							
							Log.i("Etape_ObjetXMLHandler","after first casting");
							
							//get y position
							attValue = attributes.getValue("y");							
							pos.setY(Float.parseFloat(attValue));
							
							//get z position
							attValue = attributes.getValue("z");							
							pos.setZ(Float.parseFloat(attValue));
							
							points3d[0] = pos;				
							Log.i("Etape_ObjetXMLHandler","leaving position");
							}
						else
							if(localName.equals("rotation")){
								Rotation rot = new Rotation();
								//get rotation angle
								String attValue = attributes.getValue("angle");
								rot.setAngle(Float.parseFloat(attValue));
								//get x rotation
								attValue = attributes.getValue("x");
								rot.setX(Float.parseFloat(attValue));
								
								//get y rotation
								attValue = attributes.getValue("y");							
								rot.setY(Float.parseFloat(attValue));
								
								//get z rotation
								attValue = attributes.getValue("z");							
								rot.setZ(Float.parseFloat(attValue));
								
								points3d[1] =rot;							
								}
							else
								if(localName.equals("scale")){
									Scale scl = new Scale();
									//get x scale
									String attValue = attributes.getValue("x");
									scl.setX(Float.parseFloat(attValue));
									
									//get y scale
									attValue = attributes.getValue("y");							
									scl.setY(Float.parseFloat(attValue));
									
									//get z scale
									attValue = attributes.getValue("z");							
									scl.setZ(Float.parseFloat(attValue));
									
									points3d[2] =scl;			
									
									//data.setObjet_Points(points3d);
									String str = "x: "+points3d[0]+" y: "+points3d[1]+" z: "+points3d[2];
									Log.i("Et_ObjXMLHandler_inScale:Point",str);
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
		Log.i("Etape_ObjetXMLHandler","localName: "+localName);
			
			if (localName.equalsIgnoreCase("objet")){
				data.setObjet_Points(points3d);
				String str = "x: "+points3d[0]+" y: "+points3d[1]+" z: "+points3d[2];
				Log.i("Et_ObjXMLHandler:Point",str);
			}
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
