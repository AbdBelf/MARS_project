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
 *  Atsuo Igarashi
 */

package com.project.expert;

import java.io.InputStream;
import java.util.ArrayList;

import jp.androidgroup.nyartoolkit.model.VoicePlayer;
import jp.nyatla.nyartoolkit.NyARException;
import jp.nyatla.nyartoolkit.core.NyARCode;
import jp.nyatla.nyartoolkit.core.param.NyARParam;
import jp.nyatla.nyartoolkit.core.raster.rgb.NyARRgbRaster_RGB;
import jp.nyatla.nyartoolkit.core.transmat.NyARTransMatResult;
import jp.nyatla.nyartoolkit.core.types.NyARBufferType;
import jp.nyatla.nyartoolkit.detector.NyARDetectMarker;
import jp.nyatla.nyartoolkit.jogl.utils.NyARGLUtil;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.SystemClock;
import android.util.Log;

import com.project.manager.Constants;

/**
 * ARToolKit Drawer
 *  ãƒžãƒ¼ã‚«ãƒ¼èª?è­˜éƒ¨åˆ†ã?ªã?©ã?Œå?«ã?¾ã‚Œã?Ÿã‚¯ãƒ©ã‚¹
 */
public class ARToolkitDrawer
{
	/**
	 * ãƒžãƒ¼ã‚«ãƒ¼ã?®æœ€å¤§ãƒ‘ã‚¿ãƒ¼ãƒ³æ•°
	 */
	private static final int PATT_MAX = Constants.PATT_MAX;
	
	/**
	 * ä¸€å›žã?®å‡¦ç?†ã?§æ¤œå‡ºã?§ã??ã‚‹æœ€å¤§ã?®ãƒžãƒ¼ã‚«ãƒ¼æ•°ã€‚èª¤èª?è­˜åˆ†ã‚‚å?«ã?¾ã‚Œã‚‹ã?®ã?§å°‘ã?ªã?™ã?Žã?¦ã‚‚é§„ç›®ã€?å¤šã?™ã?Žã‚‹ã?¨å‡¦ç?†è² è?·å¢—
	 */
	private static final int MARKER_MAX = Constants.MARKER_MAX;
	
	/**
	 * @see jp.nyatla.nyartoolkit.detector.NyARDetectMarker
	 */
	private NyARDetectMarker nya = null;
	
	/**
	 * @see jp.nyatla.nyartoolkit.core.raster.rgb.NyARRgbRaster_RGB
	 */
	private NyARRgbRaster_RGB raster = null;
	
	/**
	 * @see jp.nyatla.nyartoolkit.jogl.utils.NyARGLUtil
	 */
	private NyARGLUtil ar_util = null;
	
	/**
	 * ã‚«ãƒ¡ãƒ©ãƒ‘ãƒ©ãƒ¡ãƒ¼ã‚¿ã‚’ä¿?æŒ?ã?™ã‚‹ã‚¯ãƒ©ã‚¹
	 * @see jp.nyatla.nyartoolkit.core.param.NyARParam
	 */
	private NyARParam ar_param = null;
	
	/**
	 * ãƒžãƒ¼ã‚«ãƒ¼ã?®ãƒ‘ã‚¿ãƒ¼ãƒ³æƒ…å ±ã‚’ç®¡ç?†ã?™ã‚‹ã‚¯ãƒ©ã‚¹
	 * @see jp.nyatla.nyartoolkit.core.param.NyARParam
	 */
	private NyARCode[] ar_code = new NyARCode[PATT_MAX];
	
	/**
	 * æ¤œå‡ºã?—ã?Ÿãƒžãƒ¼ã‚«ãƒ¼ã?®åº§æ¨™å¤‰æ?›è¡Œåˆ—
	 * @see jp.nyatla.nyartoolkit.core.transmat.NyARTransMatResult
	 */
	private NyARTransMatResult ar_transmat_result = new NyARTransMatResult();

	
	private ModelRenderer mRenderer = null;
	
	private boolean mTranslucentBackground;
	
	private boolean isYuv420spPreviewFormat;
	
	private VoicePlayer mVoiceSound = null;
	
	static {
		System.loadLibrary("yuv420sp2rgb");
	}
	public static native void decodeYUV420SP(int[] rgb, byte[] yuv420sp, int width, int height, int type);
	public static native void decodeYUV420SP(byte[] rgb, byte[] yuv420sp, int width, int height, int type);
	
	/**
	 * Constructor
	 * 
	 * @param camePara
	 * @param patt
	 * @param mRenderer
	 * @param mTranslucentBackground
	 * @param isYuv420spPreviewFormat
	 */
	public ARToolkitDrawer(InputStream camePara, ArrayList<InputStream> patt, ModelRenderer mRenderer, boolean mTranslucentBackground, boolean isYuv420spPreviewFormat) {
		this.mRenderer = mRenderer;
		this.mTranslucentBackground = mTranslucentBackground;
		this.isYuv420spPreviewFormat = isYuv420spPreviewFormat;
		
		this.initialization(camePara, patt);
	}
	
	/**
	 * åˆ?æœŸåŒ–å‡¦ç?†
	 * ãƒžãƒ¼ã‚«ãƒ¼ãƒ‘ã‚¿ãƒ¼ãƒ³ã€?ã‚«ãƒ¡ãƒ©ãƒ‘ãƒ©ãƒ¡ãƒ¼ã‚¿ã?¯ã?“ã?“ã?§èª­ã?¿è¾¼ã‚€
	 */
	private void initialization(InputStream camePara, ArrayList<InputStream> patt) {
		try {
			for (int i = 0; i < PATT_MAX; i++) {
				// ãƒžãƒ¼ã‚«ãƒ¼ã?®åˆ†å‰²æ•°
				ar_code[i] = new NyARCode(16, 16);
				ar_code[i].loadARPatt(patt.get(i));
			}
			
			ar_param = new NyARParam();
			ar_param.loadARParam(camePara);
		} catch (Exception e) {
			Log.e("nyar", "resource loading failed", e);
		}
	}
	
	/**
	 * NyARToolKitã?®åˆ?æœŸåŒ–
	 *  ã‚«ãƒ¡ãƒ©ãƒ‘ãƒ©ãƒ¡ãƒ¼ã‚¿ã?®èª­ã?¿è¾¼ã?¿
	 * Lire les paramÃ¨tres d'initialisation de la camÃ©ra
	 * @param w ã‚¹ã‚¯ãƒªãƒ¼ãƒ³ã‚µã‚¤ã‚º(width)
	 * @param h ã‚¹ã‚¯ãƒªãƒ¼ãƒ³ã‚µã‚¤ã‚º(height)
	 */
	private void createNyARTool(int w, int h) {
		// NyARToolkit setting.
		try {
			if (nya == null) {
				
				//parametre de la cam et de la matrice de projection
				
				ar_util = new NyARGLUtil();
				ar_param.changeScreenSize(w, h);
				
				double[] width = new double[PATT_MAX];
				for (int i = 0; i < PATT_MAX; i++) {
					width[i] = 80.0;
				}
				// ãƒžãƒ¼ã‚«ãƒ¼ã?®æž ç·šå¹…ã‚’å¤‰ã?ˆã‚‹ã?“ã?¨ã?¯å?¯èƒ½ã€‚ Possible de modifier la largeur de la bordure du marqueur.
				// NyARDetectMarker å†…ã?«ã‚³ãƒ¡ãƒ³ãƒˆã‚’è¿½åŠ ã?—ã?Ÿã?®ã?§ã??ã?®éƒ¨åˆ†ã‚’å?‚ç…§ã?®ã?“ã?¨
				nya = new NyARDetectMarker(ar_param, ar_code, width, PATT_MAX, NyARBufferType.BYTE1D_B8G8R8_24);
				nya.setContinueMode(true);
			}
			Log.d("nyar", "resources have been loaded");
		} catch (Exception e) {
			Log.e("nyar", "resource loading failed", e);
		}

	}

	/**
	 * 
	 * @param mVoiceSound
	 */
	public void setVoicePlayer(VoicePlayer mVoiceSound) {
		this.mVoiceSound = mVoiceSound;
	}
	
	/**
	 * æ??ç”»å‡¦ç?†éƒ¨åˆ†
	 *  ãƒ¡ã‚¤ãƒ³ãƒ«ãƒ¼ãƒ—å‡¦ç?†ã?¨èª­ã?¿æ›¿ã?ˆã?¦ã‚‚è‰¯ã?„
	 * @param data
	 */
	public void draw(byte[] data /*, int w , int h*/) {
		
		if(data == null) {
			Log.d("AR draw", "data= null");
			return;
		}
		Log.d("AR draw", "data.length= " + data.length);
		
//		int width = 320;
//		int height = 240;
		
		int width = Constants.parameters.getPreviewSize().width;

		int height = Constants.parameters.getPreviewSize().height;

		Bitmap bitmap = null;
		if (!isYuv420spPreviewFormat) {
			BitmapFactory.Options options = new BitmapFactory.Options();
			options.inSampleSize = 4;
			bitmap = BitmapFactory.decodeByteArray(data, 0, data.length, options);
			if (bitmap == null) {
				Log.d("AR draw", "data is not BitMap data.");
				return;
			}

			if(bitmap.getHeight() < 240) {
				bitmap = BitmapFactory.decodeByteArray(data, 0, data.length);
				if (bitmap == null) {
					Log.d("AR draw", "data is not BitMap data.");
					return;
				}
			}

//			width = bitmap.getWidth();
//			height = bitmap.getHeight();
			
			width = Constants.parameters.getPreviewSize().width;

			height = Constants.parameters.getPreviewSize().height;
			
			Log.d("AR draw", "bitmap width * height()= " + width + " * " + height);

		//Mettre l'image actuelle comme background de l'objet augmentÃ©
//			mRenderer.setBgBitmap(bitmap);

		} else if (!mTranslucentBackground) {
			// assume YUV420SP
			int[] rgb = new int[(width * height)];
			try {
				bitmap = Bitmap.createBitmap(width, height, Bitmap.Config.ARGB_8888);
			} catch (Exception e) {
				Log.d("AR draw", "bitmap create error.");
				return;
			}		
			long time1 = SystemClock.uptimeMillis();
			// convert YUV420SP to ARGB
			decodeYUV420SP(rgb, data, width, height, 2);
			long time2 = SystemClock.uptimeMillis();
			Log.d("ARToolkitDrawer", "ARGB decode time: " + (time2 - time1) + "ms");
			bitmap.setPixels(rgb, 0, width, 0, 0, width, height);

//			mRenderer.setBgBitmap(bitmap);
		}

		// start coordinates calculation.
		byte[] buf = new byte[width * height * 3];

		if (!isYuv420spPreviewFormat) {
			int[] rgb = new int[(width * height)];

			bitmap.getPixels(rgb, 0, width, 0, 0, width, height);

			// convert ARGB to RGB24
			for (int i = 0; i < rgb.length; i++) {
				byte r = (byte) (rgb[i] & 0x00FF0000 >> 16);
				byte g = (byte) (rgb[i] & 0x0000FF00 >> 8);
				byte b = (byte) (rgb[i] & 0x000000FF);
				buf[i * 3] = r;
				buf[i * 3 + 1] = g;
				buf[i * 3 + 2] = b;
			}
		} else {
			// assume YUV420SP
			long time1 = SystemClock.uptimeMillis();
			// convert YUV420SP to RGB24
			
			// !!!!!!!!!!!!   Image convertis de YUV vers BitMap a fin de faire les traitement dessu !!
			
			
		//	Log.d("AR draw", "bitmap width * height()= " + w + " * " + h);

			
			width = Constants.parameters.getPreviewSize().width;

			height = Constants.parameters.getPreviewSize().height;
			
			Log.d("AR draw", "bitmap width * height()= " + width + " * " + height);
			
			decodeYUV420SP(buf, data, width, height, 1);
			
						
			long time2 = SystemClock.uptimeMillis();
			Log.d("ARToolkitDrawer", "RGB decode time: " + (time2 - time1) + "ms");
		}
		
	
		
		float[][] resultf = new float[MARKER_MAX][16];

		int found_markers;
		int ar_code_index[] = new int[MARKER_MAX];
		
		createNyARTool(width, height);
		// Marker detection 
		try {
			Log.d("AR draw", "Marker detection.");
			raster = new NyARRgbRaster_RGB(width, height);
			raster.wrapBuffer(buf);
			
			// binarisÃ© ! raster 
			// !!!!!!! Valeur de BINARISATION 100  - !!!!
			
			found_markers = nya.detectMarkerLite(raster, 128);
		} catch (NyARException e) {
			Log.e("AR draw", "marker detection failed", e);
			return;
		}
		
		// An OpenGL object will be drawn if matched.
		if (found_markers > 0) {
			Log.d("AR draw", "!!!!!!!!!!!exist marker." + found_markers + "!!!!!!!!!!!");
            
			// Projection transformation.
			float[] cameraRHf = new float[16];
			ar_util.toCameraFrustumRHf(ar_param, cameraRHf);

			if (found_markers > MARKER_MAX)
				found_markers = MARKER_MAX;
			for (int i = 0; i < found_markers; i++) {
				if (nya.getConfidence(i) < 0.60f)
					continue;
				try {
					
					//Je retourne l'index de la ARCode marqueur.
					ar_code_index[i] = nya.getARCodeIndex(i);
					
					NyARTransMatResult transmat_result = ar_transmat_result;
					nya.getTransmationMatrix(i, transmat_result);
					
					//Remplir la matrice de transformation
					
					ar_util.toCameraViewRHf(transmat_result, resultf[i]);
					
				
				} catch (NyARException e) {
					Log.e("AR draw", "getCameraViewRH failed", e);
					return;
				}
			}
			
			/**
			 * Donner au render les matrices de transformation des marqueurs detecté
			 */
			
		
			
	
			double bestConfidence = 0;
			int bestMarkerIndex = -1;
			
			for (int i = 0; i < found_markers; i++) {
				
				
				double best = nya.getConfidence(i);
				if(nya.getConfidence(i)>bestConfidence){
					
					bestConfidence = best;
					bestMarkerIndex = i;
				}
				
				

				
			}
			
			

			Log.d("ModelRenderer", "BestConfidence : " + bestConfidence + ", Marker : "+ ar_code_index[bestMarkerIndex]);
				
			Log.d("Changement de la matrice dans le web service", "Web Service Matrice !!");
		
			if(mRenderer != null )
			mRenderer.objectPointChanged(found_markers, ar_code_index, resultf, cameraRHf , nya);
		
		} 
		else 
		{
			Log.d("AR draw", "not exist marker.");
		
			mRenderer.objectClear();
		}

	}
	
	public void changerRender(ModelRenderer render)
	{
		this.mRenderer = render;
	}
}
