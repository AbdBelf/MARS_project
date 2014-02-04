package com.project.manager;

public class Point3d {
	private float x;
	private float y;
	private float z;
	
	public Point3d(){
		this.x = 0;
		this.y = 0;
		this.z = 0;
	}
	public Point3d(float x, float y, float z){
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public float getX() {
		return x;
	}

	public void setX(float x) {
		this.x = x;
	}

	public float getY() {
		return y;
	}

	public void setY(float y) {
		this.y = y;
	}

	public float getZ() {
		return z;
	}

	public void setZ(float z) {
		this.z = z;
	}

}
