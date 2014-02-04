package com.project.manager;

public class Rotation extends Point3d {

	private float angle = 0.0f;
	public Rotation(){
		this.angle = 0.0f;
	}
	public Rotation(float x, float y, float z) {
		super(x, y, z);
		// TODO Auto-generated constructor stub
	}
	
	public Rotation(float angle,float x, float y, float z){
		super(x,y,z);
		this.setAngle(angle);
	}

	public float getAngle() {
		return angle;
	}

	public void setAngle(float angle) {
		this.angle = angle;
	}

}
