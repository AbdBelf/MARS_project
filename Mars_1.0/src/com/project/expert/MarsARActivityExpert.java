package com.project.expert;

import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.HashMap;

import jp.androidgroup.nyartoolkit.hardware.CameraIF;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.ImageFormat;
import android.graphics.Rect;
import android.graphics.YuvImage;
import android.hardware.Camera;
import android.hardware.Camera.PreviewCallback;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;
import android.view.ViewGroup.LayoutParams;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.SeekBar;
import android.widget.SeekBar.OnSeekBarChangeListener;
import android.widget.TextView;
import android.widget.Toast;

import com.project.R;
import com.project.etapes.Etape;
import com.project.etapes.Objet;
import com.project.manager.Constants;
import com.project.manager.Point3d;
import com.project.manager.Position;
import com.project.manager.Rotation;
import com.project.manager.Scale;
import com.project.mars.MarsARActivity;
import com.project.mars.PromptDialog;
import com.project.webservice.AppelService;
import com.project.xmlmanager.EtapeXMLManager;
import com.project.xmlmanager.ObjectXMLManager;

public class MarsARActivityExpert extends Activity implements
		View.OnClickListener, SurfaceHolder.Callback {

	/**
	 * 
	 * Declaration de variables
	 */
	private String expertLogin;

	private int nbModel = Constants.NB_MODEL;

	// private ModelRenderer mRenderer;

	private boolean mTranslucentBackground = true;
	private boolean isYuv420spPreviewFormat = false;
	private boolean isUseSerface = false;

	private Handler mHandler = new MainHandler();

	private CameraIF mCameraDevice;

	// private GLSurfaceView mGLSurfaceView;
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
	public static final int MESSAGE_DIALOG = 10;

	public static final int SHOW_MSG = 12;
	public static final int HIDE_MSG = 13;

	public static final int KEEP = 7;

	public static final int MSG_PERSO = 8;

	public static final int SHOW_LOGIN = 9;

	private static final int SCREEN_DELAY = 2 * 60 * 1000;

	private PreviewCallback mPreviewCallback = new JpegPreviewCallback();

	public static final String TAG = "MARS-ARToolkitAndroid";

	// private ARToolkitDrawer arToolkit = null;

	/**
	 * Allows you to control the surface size and format, edit the pixels in the
	 * surface, and monitor changes to the surface.
	 */
	private SurfaceHolder mSurfaceHolder = null;

	// String[] modelName = null ;
	//
	// float[] modelScale = null;

	// Vector<String> modelName = null;

	// Vector<Float> modelScale = null;

	public Context context;

	public InputStream camePara;
	public ArrayList<InputStream> patt = new ArrayList<InputStream>();

	public Boolean showSeekBar = false;

	public SeekBar seekBarCompression;
	public LinearLayout CompressionLayout;

	GetProcedureThread VerificationProcedureThrd;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		context = this;
		/**
		 * Chargement des modèles et leurs Scales
		 */

		if (this.getIntent().getExtras() != null)
			this.expertLogin = this.getIntent().getExtras().getString("login")
					.toString();

		Toast.makeText(
				context,
				"Votre demande a été acceptée par l'expert \n Debut de l'envoi.",
				4000).show();

		/*
		 * SEEK BAR
		 */

		seekBarCompression = new SeekBar(context);

		CompressionLayout = new LinearLayout(this);

		CompressionLayout.setOrientation(LinearLayout.VERTICAL);
		CompressionLayout.setPadding(0, 5, 5, 5);

		TextView endLabel = new TextView(this);
		endLabel.setText("Taux de Compression :");

		LinearLayout.LayoutParams CompressionTextParams = new LinearLayout.LayoutParams(
				LayoutParams.WRAP_CONTENT, LayoutParams.WRAP_CONTENT);

		CompressionLayout.addView(endLabel, CompressionTextParams);

		seekBarCompression.setMax(100);
		seekBarCompression.setProgress(Constants.COMPRESSION);
		seekBarCompression.setOnSeekBarChangeListener(seekBarChangeListener);

		LinearLayout.LayoutParams seekBarParams = new LinearLayout.LayoutParams(
				LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT);

		seekBarParams.gravity = Gravity.CENTER_HORIZONTAL;
		CompressionLayout.addView(seekBarCompression, seekBarParams);

		FrameLayout.LayoutParams frameLayoutParams = new FrameLayout.LayoutParams(
				400, LayoutParams.WRAP_CONTENT, Gravity.RIGHT);

		CompressionLayout.setVisibility((showSeekBar) ? LinearLayout.VISIBLE
				: LinearLayout.GONE);

		// modelName = new String[1];
		// modelName[0] = "tournevis.mqo";

		// modelName = new Vector<String>();
		// modelName.add("tournevis.mqo");
		// modelName.add("Arrows2.mqo");

		// modelScale = new float[] { 0.0f };
		// modelScale = new Vector<Float>();
		// modelScale.add(0.024f);
		// modelScale.add(0.0f);

		// Constants.nbObjProcedure = 1;

		/**
		 * Instanciation de Model de rendu GLSurfaceView
		 */
		// mRenderer = new ModelRenderer(mTranslucentBackground, getAssets(),
		// modelName, modelScale);

		/**
		 * Reception des message des autres classes afin de les afficher dans
		 * l'activité principale
		 */
		// mRenderer.setMainHandler(mHandler);

		/**
		 * Demande de la barre de progression
		 */
		requestWindowFeature(Window.FEATURE_PROGRESS);

		// final ProgressDialog dialog = ProgressDialog.show(this, "",
		// "Loading. Please wait...", true);

		// dialog.show();

		/**
		 * Pour ne pas éteindre l'ecran au cas d'innactivité
		 */
		Window win = getWindow();
		win.addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);

		/**
		 * Camera Initialisation and Set Content View as GLSurfaceView
		 */
		// init Camera.
		if (getString(R.string.camera_name).equals("HT03ACamera")) {

			isUseSerface = true;
			isYuv420spPreviewFormat = true;

			// mGLSurfaceView = new GLSurfaceView(this);

			/**
			 * 
			 * public void setEGLConfigChooser (int redSize, int greenSize, int
			 * blueSize, int alphaSize, int depthSize, int stencilSize)
			 * 
			 * 
			 */

			// mGLSurfaceView.setEGLConfigChooser(8, 8, 8, 8, 16, 0);

			/**
			 * 
			 * the view will choose an RGB_888 surface with a depth buffer of at
			 * least 16 bits.
			 */

			// mGLSurfaceView.setRenderer(mRenderer);

			/**
			 * Pour avoir un back transparent
			 */
			// mGLSurfaceView.getHolder().setFormat(PixelFormat.TRANSLUCENT);

			mSurfaceView = new SurfaceView(this);

			mCameraDevice = new HT03ACameraExpert(this, mSurfaceView);

			/* Initialiser le surfaceView avec celui de GLSurfaceView */
			// setContentView(mGLSurfaceView );

			/* Transposé la surface de la camera sur le GLSurfaceView */
			// addContentView( mSurfaceView , new
			// LayoutParams(LayoutParams.FILL_PARENT,
			// LayoutParams.FILL_PARENT));

			/**
			 * SeekBar Configuration
			 */

		}

		/**
		 * Initialisation des parametres de la camera pour ArToolkit a partir du
		 * fichier "camera_param" du rep "raw" Matrice de projection , width ,
		 * height, distortion , scale factor
		 */

		/**
		 * 
		 * Chargement des pattern dans la BDD
		 */

		camePara = getResources().openRawResource(R.raw.camera_para);

		patt.add(getResources().openRawResource(R.raw.patthiro));
		patt.add(getResources().openRawResource(R.raw.pattkanji));

		/**
		 * Initialisation de la class de traitement sur les images de
		 * CallBackPreview elle sera appelé avec la méthode .Draw lors de la
		 * réception d'une image de puis la camera dans JPEGPreviewCallBack
		 */
		// arToolkit = new ARToolkitDrawer(camePara, patt,
		// mRenderer,mTranslucentBackground, isYuv420spPreviewFormat);

		mHandler.sendMessage(mHandler
				.obtainMessage(MarsARActivityExpert.HIDE_LOADING));

		/**
		 * Ou le résultat obtenu par la camera sera envoyé
		 */
		mCameraDevice.setPreviewCallback(mPreviewCallback);

		setContentView(mSurfaceView);

		addContentView(CompressionLayout, frameLayoutParams);

		// dialog.dismiss();

		// AppelService.StartVideoFlow(expertLogin);

		VerificationProcedureThrd = new GetProcedureThread(expertLogin);
		try {
			VerificationProcedureThrd.start();
		} catch (Exception x) {
			x.printStackTrace();
		}
	}

	// public void changementModel() {
	//
	// synchronized (this) {
	//
	// mCameraDevice.onPause();
	// mHandler.sendMessage(mHandler
	// .obtainMessage(MarsARActivityExpert.SHOW_LOADING));
	//
	// // modelName = new String[2];
	// // modelName[0] = "chair.mqo";
	// // modelName[1] = "droid.mqo";
	// //
	// // modelScale = new float[] { 0.072f , 0.012f };
	// modelName = new Vector<String>(2);
	// modelName.add("chair.mqo");
	// modelName.add("droid.mqo");
	//
	// modelScale = new Vector<Float>();
	// modelScale.add(0.072f);
	// modelScale.add(0.012f);
	//
	// Constants.nbObjProcedure = 2;
	//
	// mRenderer = new ModelRenderer(mTranslucentBackground, getAssets(),
	// modelName, modelScale);
	//
	// arToolkit.changerRender(mRenderer);
	//
	// mRenderer.setMainHandler(mHandler);
	//
	// // init Camera.
	//
	// mGLSurfaceView = new GLSurfaceView(this);
	//
	// mGLSurfaceView.setEGLConfigChooser(8, 8, 8, 8, 16, 0);
	//
	// mGLSurfaceView.setRenderer(mRenderer);
	//
	// mGLSurfaceView.getHolder().setFormat(PixelFormat.TRANSLUCENT);
	//
	// /* Initialiser le surfaceView avec celui de GLSurfaceView */
	// setContentView(mGLSurfaceView);
	//
	// /* Transposé la surface de la camera sur le GLSurfaceView */
	// addContentView(mSurfaceView, new LayoutParams(
	// LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT));
	//
	// mCameraDevice.onResume();
	// mHandler.sendMessage(mHandler
	// .obtainMessage(MarsARActivityExpert.HIDE_LOADING));
	// }
	//
	// }

	@Override
	public void onStart() {
		super.onStart();
		mCameraDevice.onStart();
	}

	@Override
	public void onResume() {
		super.onResume();

		// mGLSurfaceView.onResume();
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

		VerificationProcedureThrd.interrupt();
		VerificationProcedureThrd.stop();
		// VerificationProcedureThrd = null;

		super.onStop();

	}

	@Override
	protected void onPause() {

		// mOrientationListener.disable();
		mCameraDevice.onPause();
		// mGLSurfaceView.onPause();
		super.onPause();
	}

	@Override
	protected void onDestroy() {
		mCameraDevice.onDestroy();
		// mRenderer = null;
		// arToolkit = null;

		// SUPPRIMER LE CACHE D'OPEN GL APRES CHAQUE SORTIE !!
		// mGLSurfaceView.destroyDrawingCache();

		// VerificationProcedureThrd.interrupt();
		VerificationProcedureThrd.stop();
		VerificationProcedureThrd = null;

		finish();

		Log.i("OnDestroy", "CACHE ***************");
		super.onDestroy();
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.activity_main, menu);

		menu.add(0, 101, 0, "Exit").setIcon(R.drawable.stop32);

		// menu.add(0, 104, 0, "Get Object");
		// menu.add(0, 104, 0, "Load miku");
		// menu.add(0, 105, 0, "Load Object");

		menu.add(0, 106, 0, "Compression Video").setIcon(
				android.R.drawable.ic_menu_manage);

		menu.add(0, 108, 0, "Send Message");
		return true;

		/**
         * 
         */
	}

	//
	byte[] data;

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {

		switch (item.getItemId()) {

		case 101: {
			try {
				AppelService.DisconnectTechnician(expertLogin);
			} catch (Exception e) {

				Log.e("TAGExpertLoginExit",
						"TAGExpertLoginExit " + e.toString());
			}

			System.exit(0);

			// Constants.msg_affiche = data.toString();
			//
			// mHandler.sendMessage(mHandler.obtainMessage(MarsARActivity.MSG_PERSO));

			break;
		}
		//
		// case 102: {
		//
		// AppelService.changerUrl(2);
		//
		// // Constants.msg_affiche = data.toString();
		// //
		// mHandler.sendMessage(mHandler.obtainMessage(MarsARActivity.MSG_PERSO));
		// break;
		// }

		// case 104: {
		// // //1. Get a 3d object
		// // Objet obj =
		// // Constants.etapes.get(Constants.etapeActuelle).getObjets().get(0);
		// // Log.i("MarsActivity",obj.getName());
		// // //2. Send the object through the web services
		// // AppelService.sendObject(obj);
		//
		// // Load a new 3d object.
		// modelName.add("miku.mqo");
		// modelScale.add(0.0228f);
		//
		// // Send the object list to mRender.
		// mRenderer.setmodelName(modelName);
		// mRenderer.setmodelScale(modelScale);
		//
		// // Tell onDraw methode to consider the new objects list.
		// mRenderer.setmodelChangep(true);
		//
		// // Increment the number of objects.
		// Constants.nbObjProcedure++;
		//
		// break;
		// }
		// case 105: {
		// Objet obj = AppelService.receiveObject();
		// // Load a new 3d object.
		// modelName.add(obj.getName() + ".mqo");
		// Toast.makeText(this, "name: " + obj.getName() + ".mqo",
		// Toast.LENGTH_SHORT).show();
		// // modelScale.add(obj.getScale().getX());
		// modelScale.add(0.024f);
		// Toast.makeText(this, "scale: " + obj.getScale().getX(),
		// Toast.LENGTH_SHORT).show();
		//
		// // Send the object list to mRender.
		// mRenderer.setmodelName(modelName);
		// mRenderer.setmodelScale(modelScale);
		//
		// // Tell onDraw methode to consider the new objects list.
		// mRenderer.setmodelChangep(true);
		//
		// // Increment the number of objects.
		// Constants.nbObjProcedure++;
		// Toast.makeText(this, "" + mRenderer.getModelSize(),
		// Toast.LENGTH_SHORT).show();
		// break;
		// }

		case 106: {

			showSeekBar = !showSeekBar;
			CompressionLayout
					.setVisibility((showSeekBar) ? LinearLayout.VISIBLE
							: LinearLayout.GONE);
			break;
		}

		case 108:
			// AppelService.SendMessage(expertLogin, "Hello Expert");
			// showDialog(MESSAGE_DIALOG);
			PromptDialog p = new PromptDialog(context, "Les messages", "rien");
			p.SetParams(expertLogin);
			p.show();
			break;

		}
		return super.onOptionsItemSelected(item);
	}

	// Listener SeekBar Compression JPEG
	private OnSeekBarChangeListener seekBarChangeListener = new OnSeekBarChangeListener() {
		public void onProgressChanged(SeekBar seekBar, int progress,
				boolean fromUser) {

			Constants.COMPRESSION = progress;
		}

		public void onStartTrackingTouch(SeekBar seekBar) {
			// Not used
		}

		public void onStopTrackingTouch(SeekBar seekBar) {
		}
	};

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

		if (mCameraDevice instanceof HT03ACameraExpert) {
			HT03ACameraExpert cam = (HT03ACameraExpert) mCameraDevice;
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
		if (mCameraDevice instanceof HT03ACameraExpert) {
			HT03ACameraExpert cam = (HT03ACameraExpert) mCameraDevice;
			cam.surfaceCreated(holder);
		}

	}

	@Override
	public void surfaceDestroyed(SurfaceHolder holder) {
		// TODO Auto-generated method stub

		if (!isUseSerface) {
			return;
		}
		if (mCameraDevice instanceof HT03ACameraExpert) {
			HT03ACameraExpert cam = (HT03ACameraExpert) mCameraDevice;
			cam.surfaceDestroyed(holder);
		}

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

	@Override
	public boolean onTouchEvent(MotionEvent event) {
		switch (event.getAction()) {
		case MotionEvent.ACTION_DOWN:
			break;

		case MotionEvent.ACTION_MOVE:
			break;

		case MotionEvent.ACTION_UP:
			break;
		}
		return true;
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
	boolean videoOn = false;

	private final class JpegPreviewCallback implements PreviewCallback {

		@Override
		public void onPreviewFrame(byte[] jpegData, Camera camera) {
			Log.d(TAG, "JpegPictureCallback.onPreviewFrame");

			if (jpegData != null) {
				Log.d(TAG, "data exist ");

				/**
				 * 
				 * Debut des traitement de ARToolkit a partir de l'image obtenue
				 */
				// cam = camera;

				data = jpegData;

				/**
				 * Transformation du YUV vers JPEG + Compression
				 */

				int w = Constants.parameters.getPreviewSize().width;
				int h = Constants.parameters.getPreviewSize().height;

				YuvImage im = new YuvImage(jpegData, ImageFormat.NV21, w, h,
						null);

				Rect r = new Rect(0, 0, w, h);

				ByteArrayOutputStream out = new ByteArrayOutputStream();

				im.compressToJpeg(r, Constants.COMPRESSION, out);

				byte[] data1 = out.toByteArray();

				AppelService.AppelServ(data1, expertLogin);

				if (!videoOn) {
					AppelService.StartVideoFlow(expertLogin);
					videoOn = true;
				}
				// arToolkit.draw(jpegData /* , surf_width , surf_height */);

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

			case SHOW_MSG: {
				// p = null;
				showDialog(SHOW_MSG);
				break;
			}

			case HIDE_MSG: {
				try {
					// if (p != null)
					// p.dismiss();
				} catch (IllegalArgumentException e) {
				}
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

	PromptDialog p = null;// = new PromptDialog(context, "Les messages", "");

	@Override
	protected Dialog onCreateDialog(int id) {

		switch (id) {
		case DIALOG_LOADING: {
			ProgressDialog dialog = new ProgressDialog(this);
			dialog.setMessage("Chargement des parametres de l'application ...");
			dialog.setCancelable(false);
			dialog.getWindow().setFlags(
					WindowManager.LayoutParams.FLAG_BLUR_BEHIND,
					WindowManager.LayoutParams.FLAG_BLUR_BEHIND);
			return dialog;
		}
		case SHOW_MSG: {

			p = new PromptDialog(context, "Les messages", "");
			p.SetParams(expertLogin);
			p.show();

			return null;
		}
		default:
			return super.onCreateDialog(id);
		}
	}

	public class GetProcedureThread extends Thread {
		String login;

		public GetProcedureThread(String login) {
			this.login = login;
		}

		@SuppressWarnings("unchecked")
		@Override
		public void run() {
			Looper.prepare();

			int idProcedure = AppelService.getProcedure(login);

			while (idProcedure == -1) {
				idProcedure = AppelService.getProcedure(login);

				try {
					Constants.COMPRESSION = AppelService.CompressionRate(login);

					if (AppelService.ExistMessage(login)) {
						String msg = AppelService.ReceiveMessage(login);
						Constants.Messages.add(msg);
						Log.i("ConnectedExp", "Message reçu");
						// PromptDialog p = new PromptDialog(context,
						// "Les messages", "");
						// p.SetParams(login);
						// p.show();

						// mHandler.sendMessage(mHandler.obtainMessage(MarsARActivityExpert.HIDE_MSG));

						if (!Constants.dialogOpen)
 {
							mHandler.sendMessage(mHandler
									.obtainMessage(MarsARActivityExpert.SHOW_MSG));
							Constants.dialogOpen = true;
						}
					}

				} catch (Exception e) {
					Log.i("MarsARActvtExpt.PtocThrd", "Error");
					e.printStackTrace();
				}

				// Log.e("MSG","samarche !!");

			}
			Constants.msg_affiche = "Vous venez de recevoir une solution \n Debut du chargement...";
			mHandler.sendEmptyMessage(MSG_PERSO);

			AppelService.setProcedure(login);

			AfficherProcedure(idProcedure);

			VerificationProcedureThrd.interrupt();
			VerificationProcedureThrd.stop();

			try {

				AppelService.DisconnectTechnician(expertLogin);
			} catch (Exception e) {

				Log.e("TAGExpertLoginExit",
						"TAGExpertLoginExit " + e.toString());
			}

			finish();
		}
	}

	public void AfficherProcedure(int idProcedure) {

		// Vider la liste au cas ou on entre une 2eme fois
		Constants.etapes.clear();

		// recuperer toute les etapes pour la procedure selectionee
		EtapeXMLManager etapeManager = new EtapeXMLManager(idProcedure,
				getResources());

		ArrayList<HashMap<String, Object>> etapesdeProcedure = etapeManager
				.getEtapes();

		// simple affichage des etapes recuperees pour tester
		// l'execution

		Constants.nbObjProcedure = 0;
		Constants.NB_MODEL = 0;
		Constants.etapeActuelle = 0;

		// recuperation des objets 3d pour chaque etape
		for (int i = 0; i < etapesdeProcedure.size(); i++) {
			// pour chaque etape(i) recuperer les objets..
			// TODO
			// recuperer toute les objets pour chaque etape

			HashMap<String, Object> mMap = etapesdeProcedure.get(i);

			// Etape_ObjetXMLManager2 etape_objectManager = new
			// Etape_ObjetXMLManager2(Integer.parseInt(mMap.get("Id").toString()),getResources());

			// HashMap<String, Object> objetsdEtape =
			// etape_objectManager.getObjets();

			// ce manager nous permet de recuperer les noms des objets a
			// partir de leurs ids

			ObjectXMLManager objectmanager = new ObjectXMLManager(context,
					getResources());

			Etape mEtape = new Etape(
					Integer.parseInt(mMap.get("Id").toString()), mMap.get(
							"Libelle").toString(), mMap.get("Description")
							.toString());

			ArrayList<String> listeObjet = (ArrayList<String>) mMap
					.get("Objet");
			ArrayList<Point3d[]> listePoints3d = (ArrayList<Point3d[]>) mMap
					.get("Points3d");

			for (int j = 0; j < listeObjet.size(); j++) {

				Point3d[] pnts = listePoints3d.get(j);

				String mid = listeObjet.get(j).toString();

				Position mposition = (Position) pnts[0];
				Rotation mrotation = (Rotation) pnts[1];
				Scale mscale = (Scale) pnts[2];

				Objet obj = new Objet(Integer.parseInt(mid),
						objectmanager.getObjectName(Integer.parseInt(mid)),
						objectmanager.getObjectType(Integer.parseInt(mid)),
						mposition, mrotation, mscale);

				mEtape.addObjet(obj);

				Constants.nbObjProcedure++;

			}

			com.project.manager.Constants.etapes.add(mEtape);

			Log.i("Etape " + i, "Etapes.size() :" + Constants.etapes.size());

			// apres la detection des etapes et leurs objets ossociés on
			// peut lancer l'augmentation

		}

		int xy = Constants.etapes.get(Constants.etapeActuelle).getObjets()
				.size();
		Constants.NB_MODEL = xy;

		Log.i("ProcedureActivity", "etape.size" + xy + " nbModel "
				+ Constants.NB_MODEL);

		Intent intent = new Intent(context, MarsARActivity.class);

		startActivity(intent);

		finish();
	}

	@Override
	protected void onRestart() {
		// TODO Auto-generated method stub

		VerificationProcedureThrd.interrupt();
		// VerificationProcedureThrd.stop();
		// VerificationProcedureThrd = null;
		super.onRestart();
	}

	@Override
	public void onBackPressed() {
	}

}
