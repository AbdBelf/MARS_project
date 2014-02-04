package com.project.etape.parse;

import java.util.ArrayList;

import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;

import com.project.manager.Point3d;
import com.project.manager.Position;
import com.project.manager.Rotation;
import com.project.manager.Scale;

import android.util.Log;


public class EtapeXMLHandler extends DefaultHandler {

	String elementValue = null;
	Boolean elementOn = false;
	public static EtapeXMLGettersSetters data = null;
	

	Point3d[]  points3d = new Point3d[3];
	
	 ArrayList<Point3d[]> ListPoint ;
	 
		
	ArrayList<String> Listeobjet ;
	
	public static  int	idObjetArrayListe = 0;


	public static EtapeXMLGettersSetters getXMLData() {
		return data;
	}

	public static void setXMLData(EtapeXMLGettersSetters data2) {
		data = data2;
	}

	/** 
	 * This will be called when the tags of the XML starts.
	 **/
	@Override
	public void startElement(String uri, String localName, String qName,Attributes attributes) throws SAXException {

		elementOn = true;

		Log.i("EtapeXMLHandler:localName","EtapeXMLHandler:localName "+ localName);
			if(localName.equals("Etapes"))
			{
				data = new EtapeXMLGettersSetters();
			}
			else
				if(localName.equals("etape")){			
					String attributeValue = attributes.getValue("id");
					data.setId(Integer.parseInt(attributeValue));
					}
				else
					if(localName.equals("objets")){

						ListPoint= new ArrayList<Point3d[]>();
						Listeobjet = new ArrayList<String>();
						
						Log.i("INFO", "BALISE <OBJETS> trouvé !");
								
					}
					else
						if(localName.equals("objet")){
							points3d = new Point3d[3];
							String attributeValue = attributes.getValue("id");
							
							Listeobjet.add(attributeValue);
							
							//data.setobjet(	idObjetArrayListe, attributeValue);
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
		
		Log.i("EtapeXMLHandler","localNameendElement: "+localName);
		
		
		
		if (localName.equalsIgnoreCase("etape"))
			idObjetArrayListe++;
	else
		
			if (localName.equalsIgnoreCase("libelle"))
				data.setlibelle(elementValue);
		else
			if (localName.equalsIgnoreCase("description"))
				data.setdescription(elementValue);
			else
				if (localName.equalsIgnoreCase("objet")){
					

					ListPoint.add(points3d);
					//data.setObjet_Points(idObjetArrayListe, ListPoint);
					String str = "x: "+points3d[0]+" y: "+points3d[1]+" z: "+points3d[2];
					Log.i("Et_ObjXMLHandler:Point",str);
				}
				else
				if (localName.equalsIgnoreCase("procedure"))
				{
					data.setprocedure(elementValue);
					Log.i("Et_ObjXMLHandler:Point","Et_ObjXMLHandler:Point "+elementValue);
				}
		
				else
					if (localName.equalsIgnoreCase("objets"))
					{
						data.setobjet(Listeobjet);
						data.setObjet_Points(ListPoint);
						
					
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
