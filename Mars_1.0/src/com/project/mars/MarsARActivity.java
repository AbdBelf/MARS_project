package com.project.mars;

import java.io.InputStream;
import java.util.ArrayList;

import jp.androidgroup.nyartoolkit.hardware.CameraIF;
import jp.androidgroup.nyartoolkit.hardware.HT03ACamera;
import jp.androidgroup.nyartoolkit.view.GLSurfaceView;
import android.app.Activity;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.PixelFormat;
import android.hardware.Camera;
import android.hardware.Camera.PreviewCallback;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;
import android.view.ViewGroup.LayoutParams;
import android.view.Window;
import android.view.WindowManager;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.SeekBar;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.SeekBar.OnSeekBarChangeListener;

import com.project.R;
import com.project.etapes.Etape;
import com.project.manager.Constants;
import com.project.manager.Rotation;
import com.project.webservice.AppelService;

//import android.opengl.GLSurfaceView;

public class MarsARActivity extends Activity implements View.OnClickListener,
		SurfaceHolder.Callback {

	/**
	 * 
	 * Declaration de variables
	 */

	private int nbModel = Constants.NB_MODEL;

	private ModelRenderer mRenderer;

	private boolean mTranslucentBackground = true;
	private boolean isYuv420spPreviewFormat = false;
	private boolean isUseSerface = false;

	private Handler mHandler = new MainHandler();

	private CameraIF mCameraDevice;

	private GLSurfaceView mGLSurfaceView;
	public SurfaceView mSurfaceView;

	// Width and height of surfaceView : Orientation : Landscape
	private int surf_width = 320;
	private int surf_height = 240;

	/* Pour le Main Handler , Differents Type de message */

	/* Message de retour lors de la fin d'une Activity Fille */
	public static final int CROP_MSG = 1;

	/**
	 * Message du Handler , Pour l'affichage
	 */
	public static final int FIRST_TIME_INIT = 2;
	public static final int RESTART_PREVIEW = 3;
	public static final int CLEAR_SCREEN_DELAY = 4;
	public static final int SHOW_LOADING = 5;
	public static final int HIDE_LOADING = 6;
	public static final int KEEP = 7;

	public static final int MSG_PERSO = 8;

	public static final int SHOW_LOGIN = 9;

	private static final int SCREEN_DELAY = 2 * 60 * 1000;

	private PreviewCallback mPreviewCallback = new JpegPreviewCallback();

	public static final String TAG = "MARS-ARToolkitAndroid";

	private ARToolkitDrawer arToolkit = null;

	/**
	 * Allows you to control the surface size and format, edit the pixels in the
	 * surface, and monitor changes to the surface.
	 */
	private SurfaceHolder mSurfaceHolder = null;

	String[] modelName;

	float[] modelScale;

	public Context context;

	public static float density;

	/*
	 * POUR LA SEEK BAR - TAUX DE BINARISATION
	 */

	public Boolean showSeekBar = false;

	public SeekBar seekBarCompression;
	public LinearLayout CompressionLayout;

	public TextView endLabel;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		context = this;
		/**
		 * Chargement des modèles et leurs Scales
		 */

		getWindow().clearFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);

		final DisplayMetrics displayMetrics = new DisplayMetrics();
		getWindowManager().getDefaultDisplay().getMetrics(displayMetrics);
		density = displayMetrics.density;

		int nbObj = Constants.nbObjProcedure;

		modelName = new String[nbObj];

		modelScale = new float[nbObj];

		ArrayList<Etape> liste = Constants.etapes;

		int tmp = 0;

		int nb = liste.size();
		for (int j = 0; j < nb; j++) {

			for (int i = 0; i < liste.get(j).getObjets().size(); i++) {

				modelName[tmp] = liste.get(j).getObjets().get(i).getName();
				modelScale[tmp] = liste.get(j).getObjets().get(i).getScale()
						.getX();

				Log.d(TAG, "SCAAAAALE : " + modelName[tmp]);
				Log.d(TAG, "SCAAAAALE : " + modelScale[tmp]);

				tmp++;

			}

		}

		// String[] modelName = new String[nbModel];
		// modelName[0] = "tournevis.mqo";
		// modelName[1] = "fleche.mqo";
		//
		// modelScale = new float[] {0.01f,0.5f};

		/**
		 * Instanciation de Model de rendu GLSurfaceView
		 */
		mRenderer = new ModelRenderer(mTranslucentBackground, getAssets(),
				modelName, modelScale);

		/**
		 * Reception des message des autres classes afin de les afficher dans
		 * l'activité principale
		 */
		mRenderer.setMainHandler(mHandler);

		/**
		 * Demande de la barre de progression
		 */
		requestWindowFeature(Window.FEATURE_PROGRESS);

		/**
		 * Pour ne pas éteindre l'ecran au cas d'innactivité
		 */
		Window win = getWindow();
		win.addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);

		/**
		 * SEEK BAR
		 */

		/*
		 * SEEK BAR
		 */

		seekBarCompression = new SeekBar(context);

		CompressionLayout = new LinearLayout(this);

		CompressionLayout.setOrientation(LinearLayout.VERTICAL);
		CompressionLayout.setPadding(0, 5, 5, 5);

		endLabel = new TextView(this);
		endLabel.setText("Taux de Binarisation : "
				+ Constants.valeurBinarisation);

		LinearLayout.LayoutParams CompressionTextParams = new LinearLayout.LayoutParams(
				LayoutParams.WRAP_CONTENT, LayoutParams.WRAP_CONTENT);

		CompressionLayout.addView(endLabel, CompressionTextParams);

		seekBarCompression.setMax(255);
		seekBarCompression.setProgress(Constants.valeurBinarisation);
		seekBarCompression.setOnSeekBarChangeListener(seekBarChangeListener);

		LinearLayout.LayoutParams seekBarParams = new LinearLayout.LayoutParams(
				LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT);

		seekBarParams.gravity = Gravity.CENTER_HORIZONTAL;
		CompressionLayout.addView(seekBarCompression, seekBarParams);

		FrameLayout.LayoutParams frameLayoutParams = new FrameLayout.LayoutParams(
				400, LayoutParams.WRAP_CONTENT, Gravity.RIGHT);

		CompressionLayout.setVisibility((showSeekBar) ? LinearLayout.VISIBLE
				: LinearLayout.GONE);

		/**
		 * Camera Initialisation and Set Content View as GLSurfaceView
		 */
		// init Camera.
		if (getString(R.string.camera_name).equals("HT03ACamera")) {

			isUseSerface = true;
			isYuv420spPreviewFormat = true;

			mGLSurfaceView = new GLSurfaceView(this);

			// mGLSurfaceView.destroyDrawingCache();

			/**
			 * 
			 * public void setEGLConfigChooser (int redSize, int greenSize, int
			 * blueSize, int alphaSize, int depthSize, int stencilSize)
			 * 
			 * 
			 */

			mGLSurfaceView.setEGLConfigChooser(8, 8, 8, 8, 16, 0);

			/**
			 * 
			 * the view will choose an RGB_888 surface with a depth buffer of at
			 * least 16 bits.
			 */

			mGLSurfaceView.setRenderer(mRenderer);

			/**
			 * Pour avoir un back transparent
			 */
			mGLSurfaceView.getHolder().setFormat(PixelFormat.TRANSLUCENT);

			mSurfaceView = new SurfaceView(this);

			mCameraDevice = new HT03ACamera(this, mSurfaceView);

			/* Initialiser le surfaceView avec celui de GLSurfaceView */

			setContentView(mSurfaceView);

			/* Transposé la surface de la camera sur le GLSurfaceView */
			addContentView(mGLSurfaceView, new LayoutParams(
					LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT));

			addContentView(CompressionLayout, frameLayoutParams);

			mGLSurfaceView.setZOrderMediaOverlay(true);

			// mGLSurfaceView.setRenderMode(GLSurfaceView.RENDERMODE_WHEN_DIRTY);
		}

		/**
		 * Ou le résultat obtenu par la camera sera envoyé
		 */
		mCameraDevice.setPreviewCallback(mPreviewCallback);

		/**
		 * Initialisation des parametres de la camera pour ArToolkit a partir du
		 * fichier "camera_param" du rep "raw" Matrice de projection , width ,
		 * height, distortion , scale factor
		 */

		InputStream camePara = getResources()
				.openRawResource(R.raw.camera_para);

		/**
		 * 
		 * Chargement des pattern dans la BDD
		 */

		ArrayList<InputStream> patt = new ArrayList<InputStream>();

		patt.add(getResources().openRawResource(R.raw.patthiro));
		patt.add(getResources().openRawResource(R.raw.markeru));
		patt.add(getResources().openRawResource(R.raw.markers));
		patt.add(getResources().openRawResource(R.raw.markert));
		patt.add(getResources().openRawResource(R.raw.markerh));
		patt.add(getResources().openRawResource(R.raw.markerb));
		// patt.add(getResources().openRawResource(R.raw.pattkanji));

		/**
		 * Initialisation de la class de traitement sur les images de
		 * CallBackPreview elle sera appelé avec la méthode .Draw lors de la
		 * réception d'une image de puis la camera dans JPEGPreviewCallBack
		 */
		arToolkit = new ARToolkitDrawer(camePara, patt, mRenderer,
				mTranslucentBackground, isYuv420spPreviewFormat);

	}

	@Override
	public void onStart() {
		super.onStart();
		mCameraDevice.onStart();
	}

	@Override
	public void onResume() {
		super.onResume();

		mGLSurfaceView.setZOrderMediaOverlay(true);
		mGLSurfaceView.onResume();
		Log.d("Main", "onResume");

		mCameraDevice.onResume();

		/**
		 * Sends a Message containing only the what value, to be delivered after
		 * the specified amount of time elapses.
		 */

		mHandler.sendEmptyMessageDelayed(CLEAR_SCREEN_DELAY, SCREEN_DELAY);
	}

	@Override
	public void onStop() {
		mCameraDevice.onStop();
		// finish();
		super.onStop();
	}

	@Override
	protected void onPause() {

		// mOrientationListener.disable();
		mCameraDevice.onPause();
		mGLSurfaceView.onPause();
		super.onPause();
	}

	@Override
	protected void onDestroy() {
		mCameraDevice.onDestroy();
		mRenderer = null;
		arToolkit = null;

		// SUPPRIMER LE CACHE D'OPEN GL APRES CHAQUE SORTIE !!
		mGLSurfaceView.destroyDrawingCache();

		finish();

		Log.i("OnDestroy", "CACHE ***************");
		super.onDestroy();
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.activity_main, menu);

		menu.add(0, 99, 0, "Etape Precedente").setIcon(R.drawable.leftarrow32);
		menu.add(0, 100, 0, "Etape Suivante").setIcon(R.drawable.arrowright32);
		menu.add(0, 98, 0, "Information").setIcon(R.drawable.lightbulb32);

		menu.add(0, 106, 0, "Taux de binarisation").setIcon(
				android.R.drawable.ic_menu_manage);

		menu.add(0, 101, 0, "Exit").setIcon(R.drawable.stop32);
		menu.add(0, 103, 0, "Translate x");
		menu.add(0, 104, 0, "Translate y");
		menu.add(0, 105, 0, "Translate z");
		menu.add(0, 107, 0, "Rotate X");
		menu.add(0, 108, 0, "Rotate Y");

		return true;

		/**
		 * Constants.etapes.get(Constants.etapeActuelle).getDescription()
		 */
	}

	byte[] data;
	public boolean firstTime = true;

	public boolean transx;
	public boolean transy;
	public boolean transz;
	public boolean rotatex;
	public boolean rotatey;

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {

		switch (item.getItemId()) {

		case 98: {

			Toast.makeText(
					context,
					Constants.etapes.get(Constants.etapeActuelle)
							.getDescription(), Toast.LENGTH_LONG).show();

			break;
		}

		case 99: {

			if (Constants.etapeActuelle > 0) {

				Constants.etapeActuelle--;
			} else {
				Constants.msg_affiche = "Vous etes à la 1ere étape.";
				mHandler.sendMessage(mHandler.obtainMessage(MSG_PERSO));
			}

			break;
		}

		case 100: {

			if (Constants.etapeActuelle < Constants.etapes.size() - 1) {

				Constants.etapeActuelle++;

			} else {

				Constants.msg_affiche = "Fin des Etapes de Maintenance";
				mHandler.sendMessage(mHandler.obtainMessage(this.MSG_PERSO));
			}

			break;

		}

		case 101: {

			System.exit(0);

			// if (firstTime & mSurfaceView != null & mGLSurfaceView != null) {
			// /* Initialiser le surfaceView avec celui de GLSurfaceView */
			// setContentView(mSurfaceView);
			//
			// /* Transposé la surface de la camera sur le GLSurfaceView */
			// addContentView(mGLSurfaceView , new
			// LayoutParams(LayoutParams.FILL_PARENT,
			// LayoutParams.FILL_PARENT));
			//
			// firstTime = false;
			// }

			// Constants.msg_affiche = data.toString();
			// mHandler.sendMessage(mHandler.obtainMessage(MarsARActivity.MSG_PERSO));

			break;
		}

		// Translate manuellement !!!
		case 103: {

			transx = true;
			transy = false;
			transz = false;

			rotatex = false;
			rotatey = false;

			break;
		}

		case 104: {

			transx = false;
			transy = true;
			transz = false;
			rotatex = false;
			rotatey = false;
			break;
		}
		case 105: {

			transx = false;
			transy = false;
			transz = true;
			rotatex = false;
			rotatey = false;
			break;
		}

		// rotatex
		case 107: {

			transx = false;
			transy = false;
			transz = false;
			rotatex = true;
			rotatey = false;
			break;
		}

		// rotatey
		case 108: {

			transx = false;
			transy = false;
			transz = false;
			rotatex = false;
			rotatey = true;
			break;
		}
		case 106: {

			showSeekBar = !showSeekBar;
			CompressionLayout
					.setVisibility((showSeekBar) ? LinearLayout.VISIBLE
							: LinearLayout.GONE);
			break;
		}
		}
		return super.onOptionsItemSelected(item);
	}

	public Etape etape;

	/**
	 * 
	 * SurfaceHolder.Callback ( for camera )
	 * 
	 * SurfaceChanged : quand on met le phone du portrait vers paysage ou le
	 * contraire
	 */

	@Override
	public void surfaceChanged(SurfaceHolder holder, int format, int w, int h) {
		// TODO Auto-generated method stub

		Log.d(TAG, "surfaceChanged");

		// Make sure we have a surface in the holder before proceeding.
		if (holder.getSurface() == null) {
			Log.d(TAG, "holder.getSurface() == null");
			return;
		}

		/**
		 * On affecte ce holder au cas ou la camera n'est pas encore instanciée
		 * et on a changer la position du téléphone avant le démarrage de
		 * l'application
		 * 
		 */
		mSurfaceHolder = holder;

		if (!isUseSerface) {

			// Pas la peine de faire des traitement puisque l'application n'est
			// pas encore chargée
			// Le holder sera crée apres instantiation du GLSurfaceView
			return;
		}

		if (mCameraDevice instanceof HT03ACamera) {
			HT03ACamera cam = (HT03ACamera) mCameraDevice;
			cam.surfaceChanged(holder, format, w, h);
		}

		surf_width = w;
		surf_height = h;

	}

	@Override
	public void surfaceCreated(SurfaceHolder holder) {
		// TODO Auto-generated method stub

		if (!isUseSerface) {
			return;
		}
		if (mCameraDevice instanceof HT03ACamera) {
			HT03ACamera cam = (HT03ACamera) mCameraDevice;
			cam.surfaceCreated(holder);
		}

	}

	@Override
	public void surfaceDestroyed(SurfaceHolder holder) {
		// TODO Auto-generated method stub

		if (!isUseSerface) {
			return;
		}
		if (mCameraDevice instanceof HT03ACamera) {
			HT03ACamera cam = (HT03ACamera) mCameraDevice;
			cam.surfaceDestroyed(holder);
		}

		finish();

		mSurfaceHolder = null;
	}

	/**
	 * View.OnClickListener
	 * 
	 * @see android.view.View.OnClickListener#onClick(android.view.View)
	 */
	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub

	}

	public static float mPreviousX = 0;
	public static float mPreviousY = 0;

	// @Override
	// public boolean onTouchEvent(MotionEvent event) {
	//
	// switch (event.getAction()) {
	// case MotionEvent.ACTION_DOWN:
	// break;
	//
	// case MotionEvent.ACTION_MOVE:
	// {
	// float x = event.getX();
	// float y = event.getY();
	// float deltaX = (x - mPreviousX) / density / 2f;
	// float deltaY = (y - mPreviousY) / density / 2f;
	//
	//
	// mRenderer.SetTranslation(/*mRenderer.transl[0] */+ x ,/*
	// mRenderer.transl[1] +*/ y , 0);
	//
	// mPreviousX = x;
	// mPreviousY = y ;
	//
	//
	// break;
	// }
	//
	// case MotionEvent.ACTION_UP:
	// break;
	// }
	//
	// return true;
	// }

	private final float TOUCH_SCALE_FACTOR = 480.0f / 640.0f;
	private float previousX;
	private float previousY;

	public boolean onTouchEvent(final MotionEvent evt) {
		float currentX = evt.getX();
		float currentY = evt.getY();
		float deltaX, deltaY;
		switch (evt.getAction()) {
		case MotionEvent.ACTION_MOVE:
			// Modify rotational angles according to movement
			deltaX = currentX - previousX;
			deltaY = currentY - previousY;

			if (transx) {
				if (currentX > previousX) {
					mRenderer.transl[0] += 0.2; // deltaX * TOUCH_SCALE_FACTOR;
					// mRenderer.transl[1] = 0; // deltaY * TOUCH_SCALE_FACTOR;
					// mRenderer.transl[2] = 0;
				} else {
					mRenderer.transl[0] -= 0.2; // deltaX * TOUCH_SCALE_FACTOR;
					// mRenderer.transl[1] = 0; // deltaY * TOUCH_SCALE_FACTOR;
					// mRenderer.transl[2] = 0;

				}
			}

			if (transy) {
				if (currentX > previousX) {
					// mRenderer.transl[0] = 0; // deltaX * TOUCH_SCALE_FACTOR;
					mRenderer.transl[1] += 0.2; // deltaY * TOUCH_SCALE_FACTOR;
					// mRenderer.transl[2] = 0;
				} else {
					// mRenderer.transl[0] = 0; // deltaX * TOUCH_SCALE_FACTOR;
					mRenderer.transl[1] -= 0.2; // deltaY * TOUCH_SCALE_FACTOR;
					// mRenderer.transl[2] = 0;

				}
			}

			if (transz) {
				if (currentX > previousX) {
					// mRenderer.transl[0] = 0; // deltaX * TOUCH_SCALE_FACTOR;
					// mRenderer.transl[1] = 0; // deltaY * TOUCH_SCALE_FACTOR;
					mRenderer.transl[2] += 0.2;
				} else {
					// mRenderer.transl[0] = 0; // deltaX * TOUCH_SCALE_FACTOR;
					// mRenderer.transl[1] = 0; // deltaY * TOUCH_SCALE_FACTOR;
					mRenderer.transl[2] -= 0.2;

				}
			}

			if (rotatex) {

				if (currentX > previousX) {
					mRenderer.rota[1] += 2.0f;
				} else {

					mRenderer.rota[1] -= 2.0f;
				}

			}

			if (rotatey) {

				if (currentX > previousX) {

					mRenderer.rota[2] += 2.0f;

				} else {
					mRenderer.rota[2] -= 2.0f;

				}

			}

		}
		// Save current x, y
		previousX = currentX;
		previousY = currentY;
		return true; // Event handled
	}

	/**
	 * Lors de l'utilisation de cette activity apres un ListeMenu Elle permet de
	 * retourner les données obtenues lors de l'execution de cette activity
	 * 
	 */

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		switch (requestCode) {
		case CROP_MSG: {
			Intent intent = new Intent();
			if (data != null) {
				Bundle extras = data.getExtras();
				if (extras != null) {
					intent.putExtras(extras);
				}
			}
			setResult(resultCode, intent);
			finish();
			break;
		}
		}
	}

	Camera cam;

	/**
	 * CallBack Camera : Les Images reçu de la camera sont envoyées vers cette
	 * méthode
	 * 
	 */
	private final class JpegPreviewCallback implements PreviewCallback {

		@Override
		public void onPreviewFrame(byte[] jpegData, Camera camera) {
			Log.d(TAG, "JpegPictureCallback.onPreviewFrame");

			if (jpegData != null) {
				Log.d(TAG, "data exist ");

				/**
				 * Debut des traitement de ARToolkit a partir de l'image obtenue
				 * 
				 */
				// cam = camera;
				data = jpegData;

				arToolkit.draw(jpegData /* , surf_width , surf_height */);

			} else {
				try {
					// The measure against over load.
					Thread.sleep(500);
				} catch (InterruptedException e) {
					;
				}
			}
		}

	};

	/**
	 * 
	 * Haldler Part : Pour le renvoi des message à MainActivity et les Afficher
	 * ici
	 */

	// ---------------------------- Handler classes
	// ---------------------------------

	/**
	 * This Handler is used to post message back onto the main thread of the
	 * application
	 */
	private class MainHandler extends Handler {
		@Override
		public void handleMessage(Message msg) {
			mCameraDevice.handleMessage(msg);
			switch (msg.what) {
			case KEEP: {
				if (msg.obj != null) {
					mHandler.post((Runnable) msg.obj);
				}
				break;
			}

			case CLEAR_SCREEN_DELAY: {
				/**
				 * Window flag: as long as this window is visible to the user,
				 * keep the device's screen turned on and bright.
				 */
				getWindow().clearFlags(
						WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
				break;
			}

			case FIRST_TIME_INIT: {
				// initializeFirstTime();
				break;
			}

			case SHOW_LOADING: {
				showDialog(DIALOG_LOADING);
				break;
			}

			case HIDE_LOADING: {
				try {
					dismissDialog(DIALOG_LOADING);
					removeDialog(DIALOG_LOADING);
				} catch (IllegalArgumentException e) {
				}
				break;
			}

			case MSG_PERSO: {

				Toast.makeText(context, Constants.msg_affiche, 1000).show();

				break;
			}
			}
		}
	}

	public Handler getMessageHandler() {
		return this.mHandler;
	}

	/**
	 * Progress Dialog Function
	 * 
	 */

	private static final int DIALOG_LOADING = 0;
	final private static int DIALOG_LOGIN = 1;

	@Override
	protected Dialog onCreateDialog(int id) {

		switch (id) {
		case DIALOG_LOADING: {
			ProgressDialog dialog = new ProgressDialog(this);
			dialog.setMessage("Chargement des Parametres ...");
			dialog.setCancelable(false);
			dialog.getWindow().setFlags(
					WindowManager.LayoutParams.FLAG_BLUR_BEHIND,
					WindowManager.LayoutParams.FLAG_BLUR_BEHIND);
			return dialog;
		}

		default:
			return super.onCreateDialog(id);
		}
	}

	@Override
	public void onBackPressed() {
	}

	// Listener SeekBar Compression JPEG
	private OnSeekBarChangeListener seekBarChangeListener = new OnSeekBarChangeListener() {
		public void onProgressChanged(SeekBar seekBar, int progress,
				boolean fromUser) {

			Constants.valeurBinarisation = progress;

			endLabel.setText("Taux de Binarisation : "
					+ Constants.valeurBinarisation);
		}

		public void onStartTrackingTouch(SeekBar seekBar) {
			// Not used
		}

		public void onStopTrackingTouch(SeekBar seekBar) {
		}
	};

}
