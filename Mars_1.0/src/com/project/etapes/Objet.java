package com.project.etapes;

import com.project.manager.Position;
import com.project.manager.Rotation;
import com.project.manager.Scale;

public class Objet {

	private int id;
	private String name;
	private String type;
	private Position position;
	private Rotation rotation;
	private Scale scale;
	
	public Objet(int id, String name,String type, Position position, Rotation rotation,Scale scale){
		
		this.id = id;
		this.name = name;
		this.type = type;
		this.position = position;
		this.rotation = rotation;
		this.scale = scale;
		
	}
	
	
	
	public Position getPosition() {
		return position;
	}

	public void setPosition(Position position) {
		this.position = position;
	}

	public Rotation getRotation() {
		return rotation;
	}

	public void setRotation(Rotation rotation) {
		this.rotation = rotation;
	}

	public Scale getScale() {
		return scale;
	}

	public void setScale(Scale scale) {
		this.scale = scale;
	}



	public int getId() {
		return id;
	}



	public void setId(int id) {
		this.id = id;
	}



	public String getName() {
		return name;
	}



	public void setName(String name) {
		this.name = name;
	}



	public String getType() {
		return type;
	}



	public void setType(String type) {
		this.type = type;
	}


}
