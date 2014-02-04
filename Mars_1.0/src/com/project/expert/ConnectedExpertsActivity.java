package com.project.expert;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Vector;

import android.app.Activity;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.os.Handler;
import android.os.Looper;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Toast;

import com.project.R;
import com.project.manager.Constants;
import com.project.mars.PromptDialog;
import com.project.webservice.AppelService;

public class ConnectedExpertsActivity extends Activity {

	// data

	
	private String techName;
	private String techLastName;
	private String breakDown;
	private String breakDownType;
	private String diagnostic;
	
	public static ListView maListViewPerso;
	private Context context;
	private ConnectedExpertsActivity _this;

	public static int NoNotification = 0;
	public static int NotificationExists = 1;
	public static int NotificationApproved = 2;
	public static int NotificationCanceled = 3;
	
	GetNotificationThread getNotifThrd;
	
	RefreshListThread refreshListThread;

	public static SimpleAdapter mSchedule;
	int notifState;
	int countDown = 30;  // 30 secondes to wait for the web service response.
	CountDownTimer waitTimer;
	String sLogin;
	
	// Création de la ArrayList qui nous permettra de remplire la listView
			 ArrayList<HashMap<String, Object>> listItem = new ArrayList<HashMap<String, Object>>();
			
			
			// get the data from the web service : the connected experts.
			 Vector<Object[]> data; 


	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		setContentView(R.layout.liste);
		context = this;
		_this = this;


		ImageView imV = (ImageView)findViewById(R.id.imageViewLayoutManager);
		imV.setImageResource(R.drawable.logoexperts);

		
		//Fill up the info to send with the notification.
		if (this.getIntent().getExtras() != null){
			techName = this.getIntent().getExtras().getString("NomTech");
			techLastName = this.getIntent().getExtras().getString("PrenomTech");
			breakDown = this.getIntent().getExtras().getString("Panne");
			breakDownType = this.getIntent().getExtras().getString("TypePanne");
			diagnostic = this.getIntent().getExtras().getString("Diagnostic");
			}
		
		
		// Récupération de la listview créée dans le fichier liste.xml
		maListViewPerso = (ListView) findViewById(R.id.custom_list_view);

		data = AppelService.getConnectedExperts();
		
		for (int i = 0; i < data.size(); i++) {

			HashMap<String, Object> map = new HashMap<String, Object>();
			Object[] expert = data.get(i);

			map.put("Login", expert[0].toString());
			map.put("Name", expert[1].toString());
			map.put("LastName", expert[2].toString());
			map.put("IpAddress", expert[3].toString());
			map.put("State", expert[4]);

			// add an image
			if (Integer.parseInt(expert[4].toString()) == NotificationExists || Integer.parseInt(expert[4].toString())  == NotificationApproved)
				map.put("img", String.valueOf(R.drawable.occupied));
			else
				map.put("img", String.valueOf(R.drawable.free));
			Log.i("IMAGE", String.valueOf(map.get("img")));

			// Log.i("ConnectedExpertsActivity.ListLoding",expert[0]);
			listItem.add(map);
		}
		
//		refreshListThread = new RefreshListThread();
//		try {
//			refreshListThread.start();
//		} catch (Exception x) {
//			x.printStackTrace();
//		}

		Log.i("ConnectedExpertsActivity.List.Size", listItem.size() + "");
		// Création d'un SimpleAdapter qui se chargera de mettre les items
		// présent dans notre list (listItem) dans la vue affichageitem
		 mSchedule = new SimpleAdapter(this.getBaseContext(),
				listItem, R.layout.connected_expert_layout, new String[] {"img",
						"Login", "Name", "LastName" }, new int[] { R.id.img, R.id.Login,
						R.id.Name, R.id.LastName });

		// On attribut à notre listView l'adapter que l'on vient de créer
		maListViewPerso.setAdapter(mSchedule);

		// Enfin on met un écouteur d'évènement sur notre listView
		maListViewPerso.setOnItemClickListener(new OnItemClickListener() {
			@Override
			@SuppressWarnings("unchecked")
			public void onItemClick(AdapterView<?> a, View v, int position,
					long id) {
				// on récupère la HashMap contenant les infos de notre item
				// (titre, description, img)

				Log.i("ConnectedExpertActivity.onClick", "ON CLICK");
				HashMap<String, String> map = (HashMap<String, String>) maListViewPerso
						.getItemAtPosition(position);
				// Send a notification and wait for reply
				String login = map.get("Login");

				if (Integer.parseInt(map.get("State").toString()) == NotificationExists
						|| Integer.parseInt(map.get("State").toString()) == NotificationApproved) {
					Toast.makeText(context, "Cet expert est occupé",
							Toast.LENGTH_SHORT).show();
				} else {

				
					// Send a notification and the coresponding data: tech name
					// and lasttname, breackdown(panne) and break down type and
					// diagnostic.
					AppelService.SendNotification(login, techName,
							techLastName, breakDown, breakDownType, diagnostic);

					Log.i("ConnectedExpertsActivity.onClick",
							"notif after sending; "
									+ AppelService
											.getNotificationState("login"));
					// Wait for response: start a timer to check for the
					// approval.
					showDialog(0);

					getNotifThrd = new GetNotificationThread(login);
					try {
						getNotifThrd.start();
					} catch (Exception x) {
						x.printStackTrace();
					}
					
					

					// Thread used to calculate the time to end the connection
					// with the webservice if no response to be gotten.
					Log.i("ConnectedEXpertActivity.onClick",
							"try.CountDownThread");
					
					waitTimer = new CountDownTimer(countDown * 1000,
							1000) {

						public void onTick(long millisUntilFinished) {
							// called every 300 milliseconds, which
							// could be used to
							// send messages or some other action
							Log.e("ConnectedEcpertsActivity.CountDownThread.onTick", Thread.currentThread().toString());
							Log.e("ConnectedEcpertsActivity.CountDownThread.onTick",
									millisUntilFinished / 1000 + "s left");
						}

						public void onFinish() {
							// After 60000 milliseconds (60 sec)
							// finish
							// current
							// if you would like to execute
							// something
							// when time finishes
							Log.i("ConnectedEcpertsActivity.CountDownThread.onFinish","dismiss dialog");
							try {

								dismissDialog(0);
								removeDialog(0);
								
								Toast.makeText(context,
										"Expert absent, veuillez choisir un autre.",
										Toast.LENGTH_LONG).show();
								
								getNotifThrd.suspend();
								getNotifThrd.stop();
								getNotifThrd = null;
							} catch (Exception x) {
							}
						}
					};
					waitTimer.start();
					
					Log.i("ConnectedEXpertActivity.onClick",
							"After CountDownThread");
					getNotifThrd = null;

					// Log.i("ConnectedEXpertActivity", "while(!" + bool + ")");

				}
			}
		});

	}
	
	
	//TODO MyFunction Refresh List
	


	@Override
	public boolean onCreateOptionsMenu(Menu menu) {

		// ajout au submenu
	//	menu.add(0, 100, 0, "Send video");
		
		
		menu.add(0,100,0,"Rafraîchir la liste").setIcon(R.drawable.refresh);
		menu.add(0, 102, 0, "Quitter").setIcon(R.drawable.stop32);
		getMenuInflater().inflate(R.menu.activity_main, menu);

	
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {


		switch (item.getItemId()) {

		case 100:{
			
			data.clear();
			listItem.clear();

			data = AppelService.getConnectedExperts();

			for (int i = 0; i < data.size(); i++) {

				HashMap<String, Object> map = new HashMap<String, Object>();
				Object[] expert = data.get(i);

				map.put("Login", expert[0].toString());
				map.put("Name", expert[1].toString());
				map.put("LastName", expert[2].toString());
				map.put("IpAddress", expert[3].toString());
				map.put("State", expert[4]);

				// add an image
				if (Integer.parseInt(expert[4].toString()) == NotificationExists
						|| Integer.parseInt(expert[4].toString()) == NotificationApproved)
					map.put("img", String.valueOf(R.drawable.occupied));
				else
					map.put("img", String.valueOf(R.drawable.free));
				Log.i("IMAGE", String.valueOf(map.get("img")));

				listItem.add(map);
			}
			

		
			mSchedule.notifyDataSetChanged();


			
			break;
		}
		
//		case 100:
//			 //If the help request is approved we launch the video session.
//			 Intent intent = new Intent(context,MarsARActivityExpert.class);
//			 intent.putExtra("login", sLogin);
//			 Toast.makeText(context, sLogin, Toast.LENGTH_SHORT).show();
//			 _this.startActivityForResult(intent, 90);
//			break;
	
		case 102:
			System.exit(0);
			break;
		}
		return super.onOptionsItemSelected(item);
	}
	
	//TODO My EXTRA CODE


	@Override
	protected Dialog onCreateDialog(int id) {

		switch (id) {
		case 0: {
			ProgressDialog dialog = new ProgressDialog(this);
			dialog.setMessage("Etablissement de connexion avec l'expert ...");
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
	protected void onDestroy() {

		try {
			
			
			getNotifThrd.interrupt();
			getNotifThrd.stop();
			getNotifThrd = null;
			
			
			refreshListThread.interrupt();
			refreshListThread.stop();
			refreshListThread = null;
			

			//
			if (waitTimer != null) {
				waitTimer.cancel();
				waitTimer = null;
			}
		} catch (Exception x) {
		}
		super.onDestroy();
	}

	

	public class GetMessageThread extends Thread {
		String login;

		public GetMessageThread(String login) {
			this.login = login;
		}

		@Override
		public void run() {
			Looper.prepare();
			sLogin = login;
			while(true){
			
			if (AppelService.ExistMessage(login)) {
					String msg = AppelService.ReceiveMessage(login);
					Constants.Messages.add(msg);
					Log.i("ConnectedExp", "Message reçu");
					PromptDialog p = new PromptDialog(context, "Les messages",
							"");
					p.SetParams(login);
					p.show();
				}
			}
		}
	}

	public void showToast() {
		ConnectedExpertsActivity.this.runOnUiThread(new Runnable() {

			public void run() {
				// Looper.prepare();
				Log.e("ConnectedExpertsActivity.showToast", "notifState: "
						+ notifState);
				if (notifState == 2)// Notification
				// approved
				{
//					Toast.makeText(context,
//							"Votre demande a été acceptée par l'expert \n Debut de l'envoi.",4000).show();
//					
					
				
					Log.e("ConnexctedExpertsActivity", "Toast should pop up");
					if (waitTimer != null) {
						waitTimer.cancel();
						waitTimer = null;
					}
					
					 Intent intent = new Intent(context,MarsARActivityExpert.class);
					 intent.putExtra("login", sLogin);
				//	 Toast.makeText(context, sLogin, Toast.LENGTH_SHORT).show();
					 _this.startActivityForResult(intent, 90);
					
					 finish();

				} else if (notifState == 3)// Notification canceled
				{
					Toast.makeText(context,
							"Votre demande a été refusée, Expert occupé.",
							Toast.LENGTH_LONG).show();
				} else {
					Log.e("ConnectedExpertsActivity.showToast",
							"Unexpected value of notification.");
				}
			}
		});
	}
	
	public class RefreshListThread extends Thread {

		public RefreshListThread() {
		}

		@Override
		public void run() {
			Looper.prepare();


			while (true) {

				Log.i("THREAD", "THREADLISTEREFRESH");


				data.clear();
				listItem.clear();

				data = AppelService.getConnectedExperts();

				for (int i = 0; i < data.size(); i++) {

					HashMap<String, Object> map = new HashMap<String, Object>();
					Object[] expert = data.get(i);

					map.put("Login", expert[0].toString());
					map.put("Name", expert[1].toString());
					map.put("LastName", expert[2].toString());
					map.put("IpAddress", expert[3].toString());
					map.put("State", expert[4]);

					// add an image
					if (Integer.parseInt(expert[4].toString()) == NotificationExists
							|| Integer.parseInt(expert[4].toString()) == NotificationApproved)
						map.put("img", String.valueOf(R.drawable.occupied));
					else
						map.put("img", String.valueOf(R.drawable.free));
					Log.i("IMAGE", String.valueOf(map.get("img")));

					listItem.add(map);
				}
				

			
				//Puisque on peut pas touché les UI dans un thread
				
				//On fait appelle au handler pour qu'il modifie notre liste

				handler.sendEmptyMessage(0);




				
				try {
					Thread.sleep(5000);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}

			}
		}
	}

	public class GetNotificationThread extends Thread {
		String login;

		public GetNotificationThread(String login) {
			this.login = login;
		}

		@Override
		public void run() {
			Looper.prepare();
			notifState = AppelService.getNotificationState(login);
			while (notifState == 1 || notifState == 0)
			{
				notifState = AppelService.getNotificationState(login);
			}

			Log.i("ConnectedEcpertsActivity.GetNotifStateThread", Thread
					.currentThread().toString());
			Log.i("ConnectedEcpertsActivity.GetNotifStateThread", "ret:"
					+ notifState);
			
			if (AppelService.ExistMessage(login)) {
				String msg = AppelService.ReceiveMessage(login);
				Constants.Messages.add(msg);
				Log.i("ConnectedExp","Message reçu");
				PromptDialog p = new PromptDialog(context, "Les messages", "");
				p.SetParams(login);
				p.show();
			}

			// Close the loding dialog
			try {
				dismissDialog(0);
				removeDialog(0);
			} catch (Exception x) {
				x.printStackTrace();
			}

			Log.i("ConnexctedExpertsActivity.myThread.run",
					"After dismiss, Before test()");
			sLogin = login;
			showToast();
		}
	}

	
	private Handler handler = new Handler() {

		public void handleMessage(android.os.Message msg) {

		if(msg.what == 0) {



			mSchedule.notifyDataSetChanged();

		}

		};

		};
		
		
	@Override
	protected void onResume() {
		// TODO Auto-generated method stub
		
		
	
	
		super.onResume();
	}

	@Override
	protected void onRestart() {
		// TODO Auto-generated method stub
		
	try {
			
			
			getNotifThrd.interrupt();
			getNotifThrd.stop();
			getNotifThrd = null;
			
			
//			refreshListThread.interrupt();
//			refreshListThread.stop();
//			refreshListThread = null;
//			

			//
			if (waitTimer != null) {
				waitTimer.cancel();
				waitTimer = null;
			}
		} catch (Exception x) {
		}
	
	
		super.onRestart();
	}
	
	@Override
	protected void onStop() {
		// TODO Auto-generated method stub
	try {
			
			
			getNotifThrd.interrupt();
			getNotifThrd.stop();
			getNotifThrd = null;
			
			
//			refreshListThread.interrupt();
//			refreshListThread.stop();
//			refreshListThread = null;
//			

			//
			if (waitTimer != null) {
				waitTimer.cancel();
				waitTimer = null;
			}
		} catch (Exception x) {
		}
		super.onStop();
	}
}
