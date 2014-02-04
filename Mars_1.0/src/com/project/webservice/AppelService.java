package com.project.webservice;

import java.nio.ByteBuffer;
import java.nio.FloatBuffer;
import java.util.Vector;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.Marshal;
import org.ksoap2.serialization.MarshalBase64;
import org.ksoap2.serialization.MarshalFloat;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.AndroidHttpTransport;
import org.ksoap2.transport.HttpTransportSE;

import android.util.Log;

import com.project.etapes.Objet;
import com.project.manager.Constants;
import com.project.manager.Position;
import com.project.manager.Rotation;
import com.project.manager.Scale;

public class AppelService {

	/**
	 * 
	 */

	/**
	 * 
	 */

	private AppelService() {
	}

	private static final String NAMESPACE = "http://tempuri.org/";

	/**
	 * On met 10.0.2.2 car la localhost ou 127.0.0.1 est pri par l'AVD
	 * (emulateur ) On change a 10.0.2.2 pour le router vers la machine sur
	 * lequel il s'exécute Source Google Developper
	 */

	public static String URL = Constants.IP + "service1.asmx";

	public static void AppelServ(byte[] imageByte, String login) {

		try {

			SoapObject request = new SoapObject(NAMESPACE, "setImage");

			request.addProperty("value", imageByte);
			request.addProperty("t", imageByte.length);
			request.addProperty("login", login);

			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			Marshal marshal = new MarshalBase64();
			marshal.register(envelope);

			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);

			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call("http://tempuri.org/setImage", envelope);

		} catch (Exception e)

		{
			Log.e("simpleMethod", "", e);
		}

		// return st;

	}

	public static byte[] floatToByteArray(float f) {
		byte[] ret = null;

		ByteBuffer byteBuf = ByteBuffer.allocate(4);
		FloatBuffer floatBuf = byteBuf.asFloatBuffer();
		floatBuf.put(f);
		ret = byteBuf.array();

		return ret;
	}

	static public Objet vectorToObjet(Vector<Object> vect) {

		Objet ret = new Objet((Integer) vect.get(0), (String) vect.get(1),

		(String) vect.get(2), new Position(strToFloat(vect.get(3).toString()),
				strToFloat(vect.get(4).toString()), strToFloat(vect.get(5)
						.toString())), new Rotation(strToFloat(vect.get(9)
				.toString()), strToFloat(vect.get(6).toString()),
				strToFloat(vect.get(7).toString()), strToFloat(vect.get(8)
						.toString())), new Scale(strToFloat(vect.get(10)
				.toString()), strToFloat(vect.get(11).toString()),
				strToFloat(vect.get(12).toString())));

		return ret;
	}

	static Float strToFloat(String str) {
		return Float.parseFloat(str);
	}

	// Gets a list of all the connected Experts.
	public static Vector<Object[]> getConnectedExperts() {
		Vector<Object[]> vect = new Vector<Object[]>();
		try {

			Log.e("AppelService.getConnectedExperts", "getConnectedExperts.try");
			SoapObject request = new SoapObject(NAMESPACE,
					"getConnectedExperts");

			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call("http://tempuri.org/getConnectedExperts",
					envelope);

			SoapObject objetSOAP = (SoapObject) envelope.getResponse();

			// Log.e("AppelService.getConnectedExperts","getConnectedExperts.try.preForLoop");
			for (int i = 0; i < objetSOAP.getPropertyCount(); i++) {
				vect.add(getExpertInfo(objetSOAP.getProperty(i).toString()));

				Log.e("AppelService.getConnectedExperts.getProperty",
						objetSOAP.getProperty(i) + "");

				// for(String str :
				// getExpertInfo(objetSOAP.getProperty(i).toString()))
				// Log.e("AppelService.getConnectedExperts", "str: " + str+ "");
			}
			Log.i("AppelService.getConnectedExperts", vect.size() + "");

		} catch (Exception e)

		{
			Log.e("AppelService.getConnectedExperts", "", e);
		}
		return vect;
	}

	// Send notification to the expert.
	public static boolean SendNotification(String login, String techName,
			String techLastName, String breakDown, String breakDownType,
			String diagnostic) {
		try {

			Log.e("AppelService.SendNotification", "SendNotification.try");
			SoapObject request = new SoapObject(NAMESPACE, "SendNotification");

			request.addProperty("login", login);
			request.addProperty("iInt", 1);
			request.addProperty("techName", techName);
			request.addProperty("techLastName", techLastName);
			request.addProperty("breakdownType", breakDown);
			request.addProperty("breakdown", breakDownType);
			request.addProperty("diagnostic", diagnostic);

			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call("http://tempuri.org/SendNotification",
					envelope);
			// SoapObject Pour les type de fichier Byte
			Object objetSOAP = envelope.getResponse();

			if (objetSOAP.toString().equals("true")) {
				Log.i("AppelService.SendNotification", "Notification sent.");
				return true;

			}

		} catch (Exception e)

		{
			Log.e("AppelService.SendNotification", "", e);
		}
		return false;
	}

	// Gets the state of a notification of a specified expert.
	public static int getNotificationState(String login) {
		int notifState = 1;
		try {

			Log.i("AppelService.getNotificationState",
					"getNotificationState.try");
			SoapObject request = new SoapObject(NAMESPACE,
					"getNotificationState");

			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			request.addProperty("login", login);
			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call(
					"http://tempuri.org/getNotificationState", envelope);

			Object objetSOAP = envelope.getResponse();

			// Log.e("AppelService.getConnectedExperts","getConnectedExperts.try.preForLoop");
			// notifState =
			// Integer.parseInt(objetSOAP.getProperty(0).toString());
			notifState = Integer.parseInt(objetSOAP.toString());
			Log.i("AppelService.getNotificationState.getProperty", objetSOAP
					+ "");

		} catch (Exception e) {
			notifState = 1;
			Log.e("AppelService.getConnectedExperts", "", e);
		}
		return notifState;
	}

	// Send notification to the expert.
	public static boolean StartVideoFlow(String login) {
		try {

			Log.i("AppelService.StartVideoFlow", "StartVideoFlow.try");
			SoapObject request = new SoapObject(NAMESPACE, "StartVideoFlow");
			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			request.addProperty("login", login);
			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call("http://tempuri.org/StartVideoFlow",
					envelope);
			// SoapObject Pour les type de fichier Byte
			Object objetSOAP = envelope.getResponse();

			if (objetSOAP.toString().equals("true")) {
				Log.i("AppelService.StartVideoFlow", "StartVideoFlow done.");
				return true;

			}

		} catch (Exception e)

		{
			Log.e("AppelService.StartVideoFlow", "", e);
		}
		return false;
	}

	public static Object[] getExpertInfo(String data) {
		Object[] ret = new Object[5];
		int count = 0;
		String word = "";
		int begin = 0;

		if (data == "" || data == null)
			Log.e("AppelService.getConnectedExperts.getExpertInfo",
					"there is no expert connected.");

		for (int i = 0; i < data.length(); i++) {

			if (data.charAt(i) == '=') {
				begin = i + 1;
			}
			if (data.charAt(i) == ';') {
				word = data.substring(begin, i);
				ret[count] = word;
				count++;
			}
		}
		Log.i("AppelSErvice.getExpertInfo", "Finished parsing.");
		return ret;
	}

	public static boolean AppelServLoginTech(String login, String pass) {

		try {

			SoapObject request = new SoapObject(NAMESPACE, "loginTechnician");

			request.addProperty("login", login);
			request.addProperty("password", pass);

			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call("http://tempuri.org/loginTechnician",
					envelope);

			// SoapObject Pour les type de fichier Byte
			Object objetSOAP = envelope.getResponse();

			if (objetSOAP.toString().equals("true"))
				return true;
			else
				return false;

		} catch (Exception e)

		{
			Log.e("simpleMethod", "", e);
		}

		return false;

	}

	public static String AppelServNomTech() {

		try {

			SoapObject request = new SoapObject(NAMESPACE,
					"getFullNameTechnician");

			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call(
					"http://tempuri.org/getFullNameTechnician", envelope);

			Object objetSOAP = envelope.getResponse();

			return objetSOAP.toString();
		} catch (Exception e)

		{
			Log.e("simpleMethod", "", e);
		}

		return "";
	}

	public static int getProcedure(String loginExpert) {

		try {

			SoapObject request = new SoapObject(NAMESPACE, "ReceiveProcedure");

			request.addProperty("login", loginExpert);

			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call("http://tempuri.org/ReceiveProcedure",
					envelope);

			Object objetSOAP = envelope.getResponse();

			return Integer.parseInt(objetSOAP.toString());
		} catch (Exception e)

		{
			Log.e("simpleMethod", "", e);
		}

		return -1;
	}
	
	public static void setProcedure(String loginExpert) {

		try {

			SoapObject request = new SoapObject(NAMESPACE, "SendProcedure");

			request.addProperty("login", loginExpert);
			request.addProperty("ProcID", -1);

			

			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call("http://tempuri.org/SendProcedure",
					envelope);


		} catch (Exception e)

		{
			Log.e("simpleMethod", "", e);
		}

	}
	
	public static void DisconnectTechnician(String loginExpert) {

		try {

			SoapObject request = new SoapObject(NAMESPACE, "DisconnectTechnician");

			request.addProperty("login", loginExpert);

			

			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call("http://tempuri.org/DisconnectTechnician",
					envelope);


		} catch (Exception e)

		{
			Log.e("simpleMethod", "", e);
		}

	}
	public static int CompressionRate(String loginExpert) {

		try {

			SoapObject request = new SoapObject(NAMESPACE, "ReceiveCompressionRate");

			request.addProperty("login", loginExpert);

			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call("http://tempuri.org/ReceiveCompressionRate",
					envelope);

			Object objetSOAP = envelope.getResponse();

			return Integer.parseInt(objetSOAP.toString());
		} catch (Exception e)

		{
			Log.e("simpleMethod", "", e);
		}

		return 75;
	}
	
	public static boolean SendMessage(String login, String message){
		try {

			Log.e("AppelService.SendNotification", "SendMsgToExpert.try");
			SoapObject request = new SoapObject(NAMESPACE, "SendMsgToExpert");

			request.addProperty("login", login);
			request.addProperty("msg", message);

			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call("http://tempuri.org/SendMsgToExpert",
					envelope);
			// SoapObject Pour les type de fichier Byte
//			Object objetSOAP = envelope.getResponse();
//
//			if (objetSOAP.toString().equals("true")) {
//				Log.i("AppelService.SendMsgToExpert", "SendMsgToExpert sent.");
				return true;
//			}

		} catch (Exception e)

		{
			Log.e("AppelService.SendMsgToExpert", "", e);
		}
		return false;
	}
	
	public static String ReceiveMessage(String loginExpert) {

		try {

			SoapObject request = new SoapObject(NAMESPACE, "ReceiveMsgFromExpert");

			request.addProperty("login", loginExpert);

			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call("http://tempuri.org/ReceiveMsgFromExpert",
					envelope);

			Object objetSOAP = envelope.getResponse();

			return objetSOAP.toString();
		} catch (Exception e)

		{
			Log.e("simpleMethod", "", e);
		}

		return "";
	}
	
	public static boolean ExistMessage(String loginExpert) {

		try {

			SoapObject request = new SoapObject(NAMESPACE, "ExisteMsgFromExpert");

			request.addProperty("login", loginExpert);

			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
					SoapEnvelope.VER11);

			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
			androidHttpTransport.call("http://tempuri.org/ExisteMsgFromExpert",
					envelope);

			Object objetSOAP = envelope.getResponse();

			return objetSOAP.toString().equals("true");
		} catch (Exception e)

		{
			Log.e("simpleMethod", "", e);
		}

		return false;
	}
	

}