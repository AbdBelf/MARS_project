package com.project.etapes;

import java.util.ArrayList;

public class Etape {
	
	private int id;
	private String Libelle;
	private String Description;
	private ArrayList<Objet> objets = new ArrayList<Objet>() ;
	
	public Etape(int id,String Libelle,String Description){
		
		this.id = id;
		this.Libelle = Libelle;
		this.Description = Description;
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public String getLibelle() {
		return Libelle;
	}

	public void setLibelle(String libelle) {
		Libelle = libelle;
	}

	public String getDescription() {
		return Description;
	}

	public void setDescription(String description) {
		Description = description;
	}

	public ArrayList<Objet> getObjets() {
		return objets;
	}

	public void setObjets(ArrayList<Objet> objets) {
		this.objets = objets;
	}
	
	public void addObjet(Objet obj){
		
		this.objets.add(obj);
	}

}
