/* 
 * PROJECT: NyARToolkit for Android SDK
 * --------------------------------------------------------------------------------
 * This work is based on the original ARToolKit developed by
 *   Hirokazu Kato
 *   Mark Billinghurst
 *   HITLab, University of Washington, Seattle
 * http://www.hitl.washington.edu/artoolkit/
 *
 * NyARToolkit for Android SDK
 *   Copyright (C)2010 NyARToolkit for Android team
 *   Copyright (C)2010 R.Iizuka(nyatla)
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * For further information please contact.
 *  http://sourceforge.jp/projects/nyartoolkit-and/
 *  
 * This work is based on the NyARToolKit developed by
 *  R.Iizuka (nyatla)
 *    http://nyatla.jp/nyatoolkit/
 * 
 * contributor(s)
 *  noritsuna
 */

package com.project.mars;

import javax.microedition.khronos.egl.EGLConfig;
import javax.microedition.khronos.opengles.GL10;

import jp.androidgroup.nyartoolkit.view.GLSurfaceView;
import jp.nyatla.kGLModel.KGLException;
import jp.nyatla.kGLModel.KGLModelData;
import jp.nyatla.nyartoolkit.detector.NyARDetectMarker;
import android.content.res.AssetManager;
import android.opengl.GLU;
import android.opengl.Matrix;
import android.os.Handler;
import android.os.SystemClock;
import android.util.Log;

import com.project.etapes.Etape;
import com.project.etapes.Objet;
import com.project.manager.Constants;
//import android.opengl.GLSurfaceView;



public class ModelRenderer implements GLSurfaceView.Renderer
{
	
	private static final int MARKER_MAX = Constants.MARKER_MAX;

	private int found_markers;
	private int [] ar_code_index = new int[MARKER_MAX];
	private float [][] resultf = new float[MARKER_MAX][16];
	private float [] cameraRHf = new float[16];
	private boolean useRHfp = false;

	private boolean drawp = false;

	
	private boolean reloadTexturep;
	private boolean modelChangep;

	private boolean existeModel = false; 
	
	// metaseq
	private KGLModelData[] model = new KGLModelData[Constants.nbObjProcedure];
	private AssetManager am;
	private String[] modelName = new String[Constants.nbObjProcedure];
	private float[] modelScale = new float[Constants.nbObjProcedure];

	public int mWidth;
	public int mHeight;

	public static final int MODEL_FLAG = 0x01;
	public static final int BG_FLAG = 0x02;
	public static final int ALL_FLAG = MODEL_FLAG | BG_FLAG;
	public int deleteFlags = MODEL_FLAG | BG_FLAG;

	
	
	/**
	 * 
	 * 
	 * My Code
	 * 
	 */
	
	public double bestConfidence = 0.0f;
	public int bestMarkerIndex = -1;
	
	private NyARDetectMarker nya ;
	

	
	public Etape etape;
	
	//Pour l'envoyer apres EtapeSuivante car on a pas son instance
	GL10 gl;
	
	float[] transl = {0.0f,0.0f,0.0f,0};
	
	float[] rota = {90.0f,0.0f,0.0f,0.0f};

	public boolean checkDeleteAll() {
		return deleteFlags == ALL_FLAG;
	}
	


	public ModelRenderer(boolean useTranslucentBackground, AssetManager am, String[] modelName, float[] modelScale) 
	{
		model = new KGLModelData[Constants.nbObjProcedure];
		Log.e("OnModelRender", "*************************************************");

		
		mTranslucentBackground = useTranslucentBackground;
     
        //Pour parcourir ls fichier et les lire
        this.am = am;
        
        //Initialisation de la camera d'openGL
		cameraReset();

		Log.i("MODELR", "NbModel "+Constants.nbObjProcedure+ " This.ModName" +this.modelName.length + " ModName "+modelName.length);
		
		for (int i = 0; i < Constants.nbObjProcedure; i++) {
			
			
	        this.modelName[i] = modelName[i];
			this.modelScale[i] = modelScale[i];
	        loadModel(modelName[i], modelScale[i]);
		}
    }
	
	public void reloadTexture() {
		reloadTexturep = true;
      
	}

	public void loadModel(String fname, float scale) {
		modelChangep = true;
	}
	
	
	private Handler mainHandler;
	public void setMainHandler(Handler handler) {
		mainHandler = handler;
	}

	
	public void initModel(GL10 gl) {
		if (mainHandler != null) {
			mainHandler.sendMessage
				(mainHandler.obtainMessage
						(MarsARActivity.SHOW_LOADING));
		}
		for (int i = 0; i < Constants.nbObjProcedure; i++) {
			
		

		
				if (model[i] != null) {
					model[i].Clear(gl);
					model[i] = null;
					deleteFlags |= MODEL_FLAG;
				
			}
		
			
			
			
			if (modelName[i] != null) {
				try {
					
					Log.i("ModelRenderer", "chargement du "+modelName[i]);
					
					//Rechargement et Création des Modele envoyés
					model[i] = KGLModelData.createGLModel
						(gl, null, am, modelName[i], modelScale[i]);
					

				} catch (KGLException e) {
					Log.e("ModelRenderer", "KGLModelData error", e);
				}
				deleteFlags &= ~MODEL_FLAG;
			}
		}
		if (mainHandler != null) {
			mainHandler.sendMessage
				(mainHandler.obtainMessage
						(MarsARActivity.HIDE_LOADING));
		}
		modelChangep = false;
	}

	
	public void objectClear() {
		drawp = false;
	}

	
	
	public float [] zoomV = new float[4];
	public float [] upOriV = { 0.0f, 1.0f, 0.0f, 0.0f };
	public float [] lookV = new float[4];
	public float [] camRmtx = new float[16];

	public float [] camV = new float[4];
	public float [] upV = new float[4];
	public float ratio;

	// Temporary
	private float [] mtx = new float[16];

	private boolean cameraChangep;
	
	private void cameraSetup(GL10 gl) {
		gl.glMatrixMode(GL10.GL_PROJECTION);
		gl.glLoadIdentity();
		
	
		//Au cas ou l'ecran change d'orientation, le ratio change, et on modifie la surface d'OpenGL
		
		//gl.glFrustumf(-ratio, ratio, -1, 1, 1, 1000);
		
		//Meme chose que Frustumf , juste une declaration différente avec un angle 
		GLU.gluPerspective(gl, 45, ratio, 1.0f, 2000.0f);
		
		GLU.gluLookAt(gl,
					  camV[0], camV[1], camV[2],
					  lookV[0], lookV[1], lookV[2], 
					  upV[0], upV[1], upV[2]);
		cameraChangep = false;
		
		
		/**
		 * Parameters LookAt
		 void gluLookAt(	GLdouble  	eyeX,
	 	GLdouble  	eyeY,
	 	GLdouble  	eyeZ,
	 	GLdouble  	centerX,
	 	GLdouble  	centerY,
	 	GLdouble  	centerZ,
	 	GLdouble  	upX,
	 	GLdouble  	upY,
	 	GLdouble  	upZ);
	Parameters

	eyeX, eyeY, eyeZ
	Specifies the position of the eye point.

	centerX, centerY, centerZ
	Specifies the position of the reference point.

	upX, upY, upZ
	Specifies the direction of the up vector. 
		 */
	}


	private void cameraMake() {
		Matrix.setIdentityM(mtx, 0);
		Matrix.translateM(mtx, 0, lookV[0], lookV[1], lookV[2]);
		Matrix.multiplyMM(mtx, 0, camRmtx, 0, mtx, 0);
		Matrix.multiplyMV(camV, 0, mtx, 0, zoomV, 0);
		Matrix.multiplyMV(upV, 0, camRmtx, 0, upOriV, 0);
		cameraChangep = true;
	}

	public void cameraReset() {
		zoomV[0] = zoomV[1] = camV[0] = camV[1] = 0.0f;
		zoomV[2] = camV[2] = -500.0f;
		lookV[0] = lookV[1] = lookV[2] = 0.0f;
		upV[0] = upV[2] = 0.0f;
		upV[1] = 1.0f;
		Matrix.setIdentityM(camRmtx, 0);
		cameraChangep = true;
	}

	public void cameraRotate(float rot, float x, float y, float z,float [] sMtx) {
		float [] vec = { x, y, z, 0 };
		Matrix.setIdentityM(mtx, 0);
		Matrix.rotateM(mtx, 0, rot, vec[0], vec[1], vec[2]);
		Matrix.multiplyMM(camRmtx, 0, sMtx, 0, mtx, 0);
		cameraMake();
	}

	public void cameraZoom(float z) {
		zoomV[2] += z;
		cameraMake();
	}

	public void cameraMove(float x, float y, float z) {
		float [] vec = { x, y, z, 0 };
		Matrix.multiplyMV(vec, 0, camRmtx, 0, vec, 0);
		for (int i = 0; i < 3; i++) {
			lookV[i] += vec[i];
		}
		cameraMake();
	}
	

    public void objectPointChanged(int found_markers, int [] ar_code_index, float[][] resultf, 
    		float[] cameraRHf, NyARDetectMarker nya) {
    	
    	
    	 this.nya = nya;

    	/**
    	 * section critique Car cette méthode est appelé chaque 0.5s depuis ArtoolkitDrawer 
    	 * Pour éviter l'entrelacement des threads, on ajoute Synchronized(this)
    	 */

		synchronized (this) {
			this.found_markers = found_markers;
			for (int i = 0; i < MARKER_MAX; i++) {
				
				//Patterns Detecté
				this.ar_code_index[i] = ar_code_index[i];
				
				//GL_MODELVIEW concernant chaque patterns
				System.arraycopy(resultf[i], 0, this.resultf[i], 0, 16);
			}
			System.arraycopy(cameraRHf, 0, this.cameraRHf, 0, 16);
		}
		
		
		useRHfp = true;
		drawp = true;
		
    }

	public void setDrawp(boolean dp) {
		drawp = dp;
	}

	// Light
	public boolean lightCamp = false;
	public boolean lightp = true;
	public boolean speLightp = false;
	
	float[] lightPos0 = { 1000, 1000, 1000, 0 };
	float[] lightPos1 = { 1000, 1000, 1000, 0 };
	float[] lightPos2 = { 1000, 1000, 1000, 0 };
	float[] lightDif = { 0.6f, 0.6f, 0.6f, 1 };
	float[] lightSpe = { 1.0f, 1.0f, 1.0f, 1 };
	float[] lightAmb = { 0.01f, 0.01f, 0.01f, 1 };

	private void lightSetup(GL10 gl) {
		if (lightCamp) {
			gl.glLightfv(GL10.GL_LIGHT0, GL10.GL_POSITION, camV, 0);
			gl.glLightfv(GL10.GL_LIGHT0, GL10.GL_DIFFUSE, lightDif, 0);
			gl.glLightfv(GL10.GL_LIGHT1, GL10.GL_POSITION, camV, 0);
			gl.glLightfv(GL10.GL_LIGHT1, GL10.GL_AMBIENT, lightAmb, 0);
			if (speLightp) {
				gl.glLightfv(GL10.GL_LIGHT2, GL10.GL_POSITION, camV, 0);
				gl.glLightfv(GL10.GL_LIGHT2, GL10.GL_SPECULAR, lightSpe, 0);
			}
		} else {
			gl.glLightfv(GL10.GL_LIGHT0, GL10.GL_POSITION, lightPos0, 0);
			gl.glLightfv(GL10.GL_LIGHT0, GL10.GL_DIFFUSE, lightDif, 0);
			gl.glLightfv(GL10.GL_LIGHT1,GL10.GL_POSITION, lightPos2, 0);
			gl.glLightfv(GL10.GL_LIGHT1, GL10.GL_AMBIENT, lightAmb, 0);
			if (speLightp) {
				gl.glLightfv(GL10.GL_LIGHT2, GL10.GL_POSITION, lightPos1, 0);
				gl.glLightfv(GL10.GL_LIGHT2, GL10.GL_SPECULAR, lightSpe, 0);
			}
		}
		gl.glEnable(GL10.GL_LIGHTING);
		gl.glEnable(GL10.GL_LIGHT0);
		gl.glEnable(GL10.GL_LIGHT1);
		if (speLightp)
			gl.glEnable(GL10.GL_LIGHT2);
	}
	private void lightCleanup(GL10 gl) {
		gl.glDisable(GL10.GL_LIGHTING);
		gl.glDisable(GL10.GL_LIGHT0);
		gl.glDisable(GL10.GL_LIGHT1);
		gl.glDisable(GL10.GL_LIGHT2);
	}
	
	
	

	    
	/**
	 * Méthode exécutée en boucle
	 */
	public void onDrawFrame(GL10 gl) {

		this.gl = gl;
		// Si y'a des Nouveaux Modeles détéctés, on initialise la vue d'openGL
		// et on les crées

		if (modelChangep) {

			existeModel = true;
			initModel(gl);
			reloadTexturep = false;

		} else if (reloadTexturep) {
			// Rechargement des Textures des modèles ajoutées s'ils existent
			Log.d("ModelRenderer", "in reloadTexturep:");
			for (int i = 0; i < Constants.nbObjProcedure; i++) {
				if (model[i] != null)
					model[i].reloadTexture(gl);
			}
			reloadTexturep = false;
		}

		/*
		 * Usually, the first thing one might want to do is to clear the screen.
		 * The most efficient way of doing this is to use glClear().
		 */

		// gl.glClearColor(bgColor[0], bgColor[1], bgColor[2], bgColor[3]);
		gl.glClear(GL10.GL_COLOR_BUFFER_BIT | GL10.GL_DEPTH_BUFFER_BIT);

		// Si les pattern et les matrices ont été envoyées
		if (drawp) {

			// si la matrice de projection a changé, on modifie
			if (useRHfp) {
				gl.glMatrixMode(GL10.GL_PROJECTION);
				gl.glLoadMatrixf(cameraRHf, 0);
			} else

			if (cameraChangep) {

				// La camera d'openGL a changer, on modifie
				cameraSetup(gl);

			}

			if (existeModel) {
				// Y'a des modeles qui ont été chargé

				/**
				 * Detecter le Meilleur marqueurs ayant la meilleure confidence
				 * 
				 */
				
		
				//On initialise la confidence afin de le revérifier a chaque étape 
				//Sinon l'affichage ne sera fait que lors d'une confidence meilleur que les anciennes execution
				
				bestConfidence = 0;
				bestMarkerIndex = -1;
				
				for (int i = 0; i < found_markers; i++) {
					
					
					double best = nya.getConfidence(i);
					if(nya.getConfidence(i)>bestConfidence){
						
						bestConfidence = best;
						bestMarkerIndex = i;
					}
					
				//	Log.d("ModelRenderer", "BestConfidence: " + best + ", Marker: "+ bestMarkerIndex +" Confidence"+ best);

					Log.d("ModelRenderer", "BestConfidence: 1 " + nya.getConfidence(0));
					if (found_markers > 1 )
					{
						Log.d("ModelRenderer", "BestConfidence: 2 " + nya.getConfidence(1));

					}
	
				}
					gl.glMatrixMode(GL10.GL_MODELVIEW);
					if (useRHfp) {

						// Chargement de la GL_ModelView car GL_PROJECTION a
						// changée
						
						
						
						//	Le probleme si on reinitialise pas , il sera tjrs fixé au best marqueur ancien !!

						gl.glLoadMatrixf(resultf[bestMarkerIndex], 0);
						
						

						/**
						 * 
						 * réglage de la position par rapport au marquers
						 * detecté
						 */

				
					// OpenGL座標系→ARToolkit座標系
					// Coordonnées OpenGL →Coordonnées ARToolkit

					gl.glRotatef(90.0f, 1.0f, 0.0f, 0.0f);
					} else {
						gl.glLoadIdentity();
					}

					/**
					 * Changement de repère va s'effectuer ici !!car il dépend
					 * du marqueur trouvé
					 * 
					 */
					
					switch (ar_code_index[bestMarkerIndex]) {
					case 1:
					{
						//U
						gl.glTranslatef(14.0f, 0, 0);
						
						break;
					}
					case 2:{
						//S
						gl.glTranslatef(4.75f, -5.0f, 0);
						
						
						break;

					}
					case 3:{
						//T
						gl.glTranslatef(0, 0, 0);
						
						
						break;

					}
					case 4:{
						//H
						gl.glTranslatef(-7.25f, 4.25f, 0);
						
						
						break;

					}
					case 5:{
						//B
						gl.glTranslatef(-9.0f, 0, 2.5f);
						
						
						break;

					}
						
						
					default:
						break;
					}
					
					Log.d("ModelRenderer", "onDrawFrame: " + bestMarkerIndex + ",model: "
							+ ar_code_index[bestMarkerIndex]);

					// Log.d("ModelRenderer", "i : " + i + ",ar_code_index: "+
					// ar_code_index[i]);

					if (lightp)
						lightSetup(gl);

					/**
					 * Les modele a afficher avec la translation qui leurs
					 * convient sera mise ici !!
					 * 
					 */

					 etape = Constants.etapes.get(Constants.etapeActuelle);
					 
					 //Nbre de model a sauter avant dafficher ceux souhaité 
					 int tmp = 0;
					 
					 for (int j=0 ; j< Constants.etapeActuelle; j++){
						 
						 tmp = tmp + Constants.etapes.get(j).getObjets().size();
						 
						 
					 }
					 
					 int tmp2 = Constants.etapes.get(Constants.etapeActuelle).getObjets().size();
					 tmp2 = tmp2 + tmp;
					 //Pour afficher les objets qui sont entre tmp et tmp2 uniquement 
					 
					 int diff;
					for (int i=tmp ; i< tmp2 ; i++){
						
						gl.glPushMatrix();

						//Pour que la liste puisse commencer a partir du 0
						i = i-tmp;
						Objet obj = etape.getObjets().get(i);
						
						float x = obj.getPosition().getX();
						float y = obj.getPosition().getY();
						float z = obj.getPosition().getZ();

						
						gl.glTranslatef(x, y, z);
						

						 x = obj.getRotation().getX();
						 y = obj.getRotation().getY();
						 z = obj.getRotation().getZ();

						gl.glRotatef(obj.getRotation().getAngle(), x, y, z);
						
						
						i = i+tmp;
						
						Log.i("Draw", "Activation du  "+modelName[i]);
						
						Log.i("Draw", "Etape Actuelle  "+Constants.etapeActuelle + "Objet "+Constants.etapes.get(Constants.etapeActuelle).getObjets().get(0).getName());

					
						//Application du translate manuel !!!
					//	if (i == transl[3]) {

					//	this.transl = GetTranslation();
						
//						gl.glRotatef(transl[0], 1.0f, 0.0f, 0.0f);
//						gl.glRotatef(transl[1], 0.0f, 1.0f, 0.0f);
						
			//			gl.glTranslatef(transl[0] , transl[1], transl[2]);
					
						//gl.glRotatef(rota[0], rota[1], rota[2], rota[3]);
						
						
						//gl.glTranslatef(0, transl[1], 0);
					
				//		Log.i("Draw", "Translate!!  x , y , z" + transl[0] + " " + transl[1] + transl[2]);
						

					//	gl.glTranslatef(transl[0], transl[1], transl[2]);
				
						//}

					
						model[i].enables(gl, 1.0f);
						model[i].draw(gl);
						model[i].disables(gl);					
						
						gl.glPopMatrix();
						
						//i = i+tmp;

					}
				

					if (lightp)
						lightCleanup(gl);

					/**
					 * On ajoute Break car si on trouve un marqueurs c'est bon,
					 * pa la peine de continuer car on a deja le changement de
					 * repre, on ajoute seulement les objets demandé ...
					 */


			}
		} else {
			gl.glMatrixMode(GL10.GL_PROJECTION);
			gl.glLoadMatrixf(cameraRHf, 0);
			gl.glEnable(GL10.GL_DEPTH_TEST);
		}
		makeFramerate();
	}

/**
 * 
 * Translation
 */
    public void SetTranslation(int i,float tr1 , float tr2 , float tr3) {

    	transl[0] = tr1;
    	transl[1] = tr2;
    	transl[2] = tr3;
    	
    }

    public float[] GetTranslation() {

		return transl;
	}

	private int mFrames = 0;
    private float mFramerate;
    private long mStartTime;


    private void makeFramerate() {
        long time = SystemClock.uptimeMillis();

		synchronized (this) {
			mFrames++;
			if (mStartTime == 0) {
				mStartTime = time;
			}
			if (time - mStartTime >= 1) {
				mFramerate = (float)(1000 * mFrames) / (float)(time - mStartTime);
				
				Log.d("ModelRenderer", "OpenGL Framerate: " + mFramerate + " (" + (time - mStartTime) + "ms)");
				mFrames = 0;
				mStartTime = time;
			}
		}
    }

    public void onSurfaceChanged(GL10 gl, int width, int height) {
		mWidth = width;
		mHeight = height;
		
		gl.glViewport(0, 0, width, height);

		/*
		 * Set our projection matrix. This doesn't have to be done
		 * each time we draw, but usually a new projection needs to
		 * be set when the viewport is resized.
		 */
		ratio = (float) width / height;
		cameraChangep = true;
    }

    public void onSurfaceCreated(GL10 gl, EGLConfig config) {
        /*
         * By default, OpenGL enables features that improve quality
         * but reduce performance. One might want to tweak that
         * especially on software renderer.
         */
         gl.glDisable(GL10.GL_DITHER);

        /*
         * Some one-time OpenGL initialization can be made here
         * probably based on features of this particular context
         */
		gl.glHint(GL10.GL_PERSPECTIVE_CORRECTION_HINT,
				  GL10.GL_FASTEST);

		if (mTranslucentBackground) {
            gl.glClearColor(0,0,0,0);
        } else {
            gl.glClearColor(1,1,1,1);
        }

        for (int i = 0; i < Constants.nbObjProcedure; i++) {
        	if (model[i] != null) {
        		model[i].resetTexture();
        	}
        }
		reloadTexture();
		cameraChangep = true;
    }
    
    private boolean mTranslucentBackground;
    
    
	}
