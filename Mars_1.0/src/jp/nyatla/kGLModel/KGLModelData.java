/* 
 * PROJECT: NyARMqoView
 * --------------------------------------------------------------------------------
 * ã�“ã‚Œã�¯Metaseqãƒ•ã‚¡ã‚¤ãƒ«ï¼ˆ.MQOï¼‰ãƒ•ã‚¡ã‚¤ãƒ«ã‚’ï½Šï½�ï½–ï½�ã�«èª­ã�¿è¾¼ã�¿ï¼†æ��ç”»ã�™ã‚‹ã‚¯ãƒ©ã‚¹ã�§ã�™ã€‚
 * Copyright (C)2008 kei
 * 
 * 
 * ã‚ªãƒªã‚¸ãƒŠãƒ«ãƒ•ã‚¡ã‚¤ãƒ«ã�®è‘—ä½œæ¨©ã�¯keiã�•ã‚“ã�«ã�‚ã‚Šã�¾ã�™ã€‚
 * ã‚ªãƒªã‚¸ãƒŠãƒ«ã�®ãƒ•ã‚¡ã‚¤ãƒ«ã�¯ä»¥ä¸‹ã�®URLã�‹ã‚‰å…¥æ‰‹ã�§ã��ã�¾ã�™ã€‚
 * http://www.sainet.or.jp/~kkoni/OpenGL/reader.html
 * 
 * ã�“ã�®ãƒ•ã‚¡ã‚¤ãƒ«ã�¯ã€�http://www.sainet.or.jp/~kkoni/OpenGL/20080408.zipã�«ã�‚ã‚‹ãƒ•ã‚¡ã‚¤ãƒ«ã‚’
 * ãƒ™ãƒ¼ã‚¹ã�«ã€�NyARMqoViewç”¨ã�«ã‚«ã‚¹ã‚¿ãƒžã‚¤ã‚ºã�—ã�Ÿã‚‚ã�®ã�§ã�™ã€‚
 *
 * For further information please contact.
 *	Aè™Žï¼ nyatla.jp
 *	http://nyatla.jp/nyatoolkit/
 *	<airmail(at)ebony.plala.or.jp>
 * 
 */
package jp.nyatla.kGLModel;
import java.io.* ;
import java.nio.*;

import javax.microedition.khronos.egl.EGL10;
import javax.microedition.khronos.opengles.*;
import android.opengl.GLU;
import android.opengl.GLUtils;

import android.content.res.*;
import android.util.Log;

/**
 * JOGLã‚’ä½¿ç”¨ã�—ã�¦ãƒ•ã‚¡ã‚¤ãƒ«ã�‹ã‚‰ãƒ¢ãƒ‡ãƒ«ãƒ‡ãƒ¼ã‚¿ã�®èª­ã�¿è¾¼ã�¿ã�¨æ��ç”»ã‚’è¡Œã�†<br>
 * ä½¿ç”¨å¾Œã�¯Clear()ã‚’å‘¼ã‚“ã�§ã��ã� ã�•ã�„<br>
 * ï¼¯ï½�ï½…ï½Žï¼§ï¼¬ã�¸ç™»éŒ²ã�—ã�Ÿãƒªã‚½ãƒ¼ã‚¹ã�®è§£æ”¾ã‚’ã�—ã�¾ã�™ã€‚<br>
 * 
 * @author kei
 */
public class KGLModelData {
    /**
     * ãƒ†ã‚¯ã‚¹ãƒ�ãƒ£ç®¡ç�†ã‚¯ãƒ©ã‚¹
     */
    protected KGLTextures texPool  = null ;
    /**
     * ãƒ†ã‚¯ã‚¹ãƒ�ãƒ£ç®¡ç�†ã‚¯ãƒ©ã‚¹ã‚’ã�“ã�®ã‚¯ãƒ©ã‚¹ã�§ä½œæˆ�ã�—ã�Ÿã�‹ã�©ã�†ã�‹
     */
    protected boolean isMakeTexPool = false ;
    /**
     * VBOï¼ˆé ‚ç‚¹é…�åˆ—ãƒ�ãƒƒãƒ•ã‚¡ï¼‰ã‚’ä½¿ç”¨ã�™ã‚‹ã�‹ã�©ã�†ã�‹
     */
    protected boolean isUseVBO = false ; 

    /**
     * ãƒžãƒ†ãƒªã‚¢ãƒ«ã�®æ��ç”»æƒ…å ±
     * @author kkoni
     *
     */
    protected class GLMaterial {
	/**
	 * ãƒžãƒ†ãƒªã‚¢ãƒ«å��
	 */
	String name ;
	/**
	 * æ��ç”»æœ‰ç„¡<br>
	 */
	boolean isVisible = true;
	/**
	 * è‰²æƒ…å ±
	 */
	float[]	color = null ;
	/**
	 * æ‹¡æ•£å…‰
	 */
	float[]	dif = null ;
	/**
	 * ç’°å¢ƒå…‰
	 */
	float[]	amb = null ;
	/**
	 * æ”¾å°„è¼�åº¦
	 */
	float[]	emi = null ;
	/**
	 * é�¡é�¢å��å°„
	 */
	float[]	spc = null ;
	/**
	 * é�¡é�¢å��å°„å¼·åº¦
	 */
	float[] power = null;

	/**
	 * ã‚·ã‚§ãƒ¼ãƒ‡ã‚£ãƒ³ã‚°ãƒ¢ãƒ¼ãƒ‰<br>
	 * GL_SMOOTH or GL_FLAT
	 */
	boolean shadeMode_IsSmooth = true ; //OpenGLã�®ãƒ‡ãƒ•ã‚©ãƒ«ãƒˆã�¯GL_SMOOTH

	/**
	 * é ‚ç‚¹æ•°
	 */
	int vertex_num ;
	/**
	 * ãƒ†ã‚¯ã‚¹ãƒ�ãƒ£ï¼©ï¼¤ï¼ˆæœªä½¿ç”¨ã�®å ´å�ˆï¼�ï¼‰<br>
	 */
	int	texID = 0 ;
	// reload ç”¨
	String texName = null;
	String alphaTexName = null;

	// interleaveFormat ã�¯ç„¡ã�„ã�®ã�§
	// ShortBuffer indexBuffer;
	ByteBuffer vertexBuffer;
	ByteBuffer normalBuffer;
	ByteBuffer uvBuffer = null;
	ByteBuffer colBuffer = null;

	boolean uvValid = false;
	boolean colValid = false;

	// int indexCount;
    }
    /**
     * ãƒ¢ãƒ‡ãƒ«ã�®å�„ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆæƒ…å ±ä¿�æŒ�ã‚¯ãƒ©ã‚¹
     * @author kei
     *
     */
    protected class GLObject {
	/**
	 * ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆå��<br>
	 */
	String name = null ;
	/**
	 * æ��ç”»æœ‰ç„¡<br>
	 */
	boolean isVisible = true;
	/**
	 * ãƒžãƒ†ãƒªã‚¢ãƒ«æ¯Žã�®æ��ç”»æƒ…å ±<br>
	 */
	GLMaterial[] mat = null ;
	/**
	 * ï¼¯ï½�ï½…ï½Žï¼§ï¼¬ã�¸ç™»éŒ²ã�—ã�Ÿé ‚ç‚¹é…�åˆ—ãƒ�ãƒƒãƒ•ã‚¡ï¼©ï¼¤<br>
	 * ï¼ˆé ‚ç‚¹é…�åˆ—ãƒ�ãƒƒãƒ•ã‚¡ã‚’ä½¿ç”¨ã�™ã‚‹å ´å�ˆã�«ã�—ã�‹å€¤ã�¯å…¥ã‚‰ã�ªã�„ï¼‰<br>
	 */
	int[] VBO_ids = null ;
    }
    /**
     * æ��ç”»ç”¨å†…éƒ¨ãƒ‡ãƒ¼ã‚¿
     */
    protected GLObject[] glObj ;
    /**
     * ãƒ•ã‚¡ã‚¤ãƒ«å��ã�®æ‹¡å¼µå­�ã‚’è¦‹ã�¦èª­ã�¿è¾¼ã�¿ã‚¯ãƒ©ã‚¹ã‚’ä½œæˆ�ã�™ã‚‹ã€‚<br>
     * â†’MQOãƒ•ã‚¡ã‚¤ãƒ«ã�—ã�‹ä½œã�£ã�¦ã�ªã�„ã�‘ã�©ã�­ï¼�<br>
     * 
     * @param in_gl		OpenGLã‚³ãƒžãƒ³ãƒ‰ç¾¤ã‚’ã‚«ãƒ—ã‚»ãƒ«åŒ–ã�—ã�Ÿã‚¯ãƒ©ã‚¹
     * @param in_texPool	ãƒ†ã‚¯ã‚¹ãƒ�ãƒ£ç®¡ç�†ã‚¯ãƒ©ã‚¹ï¼ˆnullã�ªã‚‰ã�“ã�®ã‚¯ãƒ©ã‚¹å†…éƒ¨ã�«ä½œæˆ�ï¼‰
     * @param i_file_provider	ãƒ•ã‚¡ã‚¤ãƒ«æ��ä¾›ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆ
     * @param i_moq_name	MOQãƒ•ã‚¡ã‚¤ãƒ«ã‚’è­˜åˆ¥ã�™ã‚‹æ–‡å­—åˆ—
     * @param scale		ãƒ¢ãƒ‡ãƒ«ã�®å€�çŽ‡
     * @param in_isUseVBO	é ‚ç‚¹é…�åˆ—ãƒ�ãƒƒãƒ•ã‚¡ã‚’ä½¿ç”¨ã�™ã‚‹ã�‹ã�©ã�†ã�‹
     * @return	ãƒ¢ãƒ‡ãƒ«ãƒ‡ãƒ¼ã‚¿ã‚¯ãƒ©ã‚¹
     */
    static public KGLModelData createGLModel(GL10 gl, KGLTextures in_texPool, AssetManager am, String msqname, float scale) throws KGLException
    {
	//ãƒ•ã‚¡ã‚¤ãƒ«è§£æž�ã�—ã�¦MOQã�‹åˆ¤åˆ¥ã�—ã�Ÿã�„ã�‘ã�©ã€�ã�¨ã‚Šã�‚ã�ˆã�šMOQã� ã�¨ä¿¡ã�˜ã‚‹ã€‚
        return new KGLMetaseq(gl,in_texPool, am, msqname, scale) ;
 //           throw new KGLException();
    }
    /**
     * ï¼¯ï½�ï½…ï½Žï¼§ï¼¬ã�¸ç™»éŒ²ã�—ã�Ÿãƒªã‚½ãƒ¼ã‚¹ã‚’è§£æ”¾ã�™ã‚‹<br>
     *
     */
    public void Clear(GL10 gl) {
	if( glObj == null ) return ;
	glObj = null ;
	if( isMakeTexPool ) {
	    texPool.Clear(gl) ;
	    texPool = null ;
	}
    }
    /**
     * ã‚³ãƒ³ã‚¹ãƒˆãƒ©ã‚¯ã‚¿
     * createGLModelã‚’ä½¿ç”¨ã�—ã�¦ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹åŒ–ã�™ã‚‹ã�®ã�§ã€�ä½¿ç”¨ã�—ã�ªã�„ã€‚
     * @param in_gl		OpenGLã‚³ãƒžãƒ³ãƒ‰ç¾¤ã‚’ã‚«ãƒ—ã‚»ãƒ«åŒ–ã�—ã�Ÿã‚¯ãƒ©ã‚¹
     * @param in_texPool	ãƒ†ã‚¯ã‚¹ãƒ�ãƒ£ç®¡ç�†ã‚¯ãƒ©ã‚¹ï¼ˆnullã�ªã‚‰ã�“ã�®ã‚¯ãƒ©ã‚¹å†…éƒ¨ã�«ä½œæˆ�ï¼‰
     * @param scale		ãƒ¢ãƒ‡ãƒ«ã�®å€�çŽ‡
     * @param in_isUseVBO		é ‚ç‚¹é…�åˆ—ãƒ�ãƒƒãƒ•ã‚¡ã‚’ä½¿ç”¨ã�™ã‚‹ã�‹ã�©ã�†ã�‹
     */
    protected KGLModelData(KGLTextures in_texPool, AssetManager am, float scale)

//    protected KGLModelData(GL in_gl,KGLTextures in_texPool,float scale,boolean in_isUseVBO)
    {
	texPool = in_texPool ;
	glObj = null ;
	if( texPool == null ) {
	    texPool = new KGLTextures(am) ;
	    isMakeTexPool = true ;
	}
    }
    /**
     * æ��ç”»æœ‰ç„¡ã‚’å¤‰æ›´ã�™ã‚‹<br>
     * @param objectName	ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆå��
     * @param isVisible	æ��ç”»æœ‰ç„¡
     */
    public void objectVisible(String objectName,boolean isVisible) {
	if( glObj == null ) return ;
	for( int o = 0 ; o < glObj.length ; o++ ) {
	    if( objectName.equals(glObj[o].name) ) {
		glObj[o].isVisible = isVisible ;
		break ;
	    }
	}

    }
    /**
     * æ��ç”»æœ‰ç„¡ã‚’å¤‰æ›´ã�™ã‚‹<br>
     * @param materialtName	ãƒžãƒ†ãƒªã‚¢ãƒ«å��
     * @param isVisible	æ��ç”»æœ‰ç„¡
     */
    public void materialVisible(String materialtName,boolean isVisible)
    {
	if( glObj == null ) return ;
	for( int o = 0 ; o < glObj.length ; o++ ) {
	    for( int m = 0 ; m < glObj[o].mat.length ; m++ ) {
		if( materialtName.equals(glObj[o].mat[m].name) ) {
		    glObj[o].mat[m].isVisible = isVisible ;
		    break ;
		}
	    }
	}

    }
    /**
     * æ��ç”»æœ‰ç„¡ã‚’å¤‰æ›´ã�™ã‚‹<br>
     * @param objectName	ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆå��
     * @param materialtName	ãƒžãƒ†ãƒªã‚¢ãƒ«å��
     * @param isVisible	æ��ç”»æœ‰ç„¡
     */
    public void materialVisible(String objectName,String materialtName,boolean isVisible) {
	if( glObj == null ) return ;
	for( int o = 0 ; o < glObj.length ; o++ ) {
	    if( ! objectName.equals(glObj[o].name) ) continue ;
	    for( int m = 0 ; m < glObj[o].mat.length ; m++ ) {
		if( materialtName.equals(glObj[o].mat[m].name) ) {
		    glObj[o].mat[m].isVisible = isVisible ;
		    break ;
		}
	    }
	}
    }
    /**
     * æ��ç”»ã�«å¿…è¦�ã�ªglEnableå‡¦ç�†ã‚’ä¸€æ‹¬ã�—ã�¦è¡Œã�†ã€‚<br>
     * glEnableã�™ã‚‹ã‚‚ã�®ã�¯<br>
     * GL_DEPTH_TEST<br>
     * GL_ALPHA_TEST<br>
     * GL_NORMALIZEï¼ˆscaleã�Œ1.0ä»¥å¤–ã�®å ´å�ˆã�®ã�¿ï¼‰<br>
     * GL_TEXTURE_2D<br>
     * GL_BLEND<br>
     * ã�“ã‚Œã‚‰ã�Œå¿…è¦�ã�ªã�„ã�“ã�¨ã�Œã‚�ã�‹ã�£ã�¦ã�„ã‚‹ã�¨ã��ã�¯æ‰‹å‹•ã�§è¨­å®šã�™ã‚‹ã�»ã�†ã�Œã‚ˆã�„ã�¨æ€�ã�„ã�¾ã�™<br>
     *@param scale æ��ç”»ã�™ã‚‹ã‚µã‚¤ã‚ºï¼ˆï¼‘å€�ä»¥å¤–ã�¯ï¼¯ï½�ï½…ï½Žï¼§ï¼¬ã�«ä½™è¨ˆã�ªå‡¦ç�†ã�Œå…¥ã‚‹ï¼‰
     */
    public void enables(GL10 gl, float scale) {
	gl.glFrontFace(GL10.GL_CW) ;
	gl.glCullFace(GL10.GL_BACK) ;
	gl.glEnable(GL10.GL_CULL_FACE) ;
	gl.glEnable(GL10.GL_DEPTH_TEST) ;
	gl.glEnable(GL10.GL_ALPHA_TEST) ;
	if( scale != 1.0 ) {
	    gl.glScalef(scale,scale,scale) ;
	    //Si vous voulez changer l'Ã©chelle et je ne fais pas pas obtenir un calcul de la normale Ã  OpenGL
	    gl.glEnable(GL10.GL_NORMALIZE) ;//ã‚¹ã‚±ãƒ¼ãƒ«ã‚’å¤‰ã�ˆã‚‹ã�¨ã��ã�¯OpenGLã�«æ³•ç·šã�®è¨ˆç®—ã‚’ã�—ã�¦ã‚‚ã‚‰ã‚�ã�ªã�„ã�¨ã�„ã�‘ã�ªã�„
	}
	gl.glTexParameterx(GL10.GL_TEXTURE_2D, GL10.GL_TEXTURE_WRAP_S, GL10.GL_CLAMP_TO_EDGE);
	gl.glTexParameterx(GL10.GL_TEXTURE_2D, GL10.GL_TEXTURE_WRAP_T, GL10.GL_CLAMP_TO_EDGE);
	gl.glEnable(GL10.GL_TEXTURE_2D) ;
    }
    /**
     * æ��ç”»ã�§ä½¿ã�£ã�Ÿãƒ•ãƒ©ã‚°ï¼ˆenables()ã�§è¨­å®šã�—ã�Ÿã‚‚ã�®ï¼‰ã‚’ã�Šã�¨ã�™<br>
     * glDsableã�™ã‚‹ã‚‚ã�®ã�¯<br>
     * GL_DEPTH_TEST<br>
     * GL_ALPHA_TEST<br>
     * GL_NORMALIZE<br>
     * GL_TEXTURE_2D<br>
     * GL_BLEND<br>
     */
    public void disables(GL10 gl) {
	gl.glDisable(GL10.GL_BLEND) ;
	gl.glDisable(GL10.GL_TEXTURE_2D) ;
	gl.glDisable(GL10.GL_NORMALIZE) ;
	gl.glDisable(GL10.GL_ALPHA_TEST) ;
	gl.glDisable(GL10.GL_DEPTH_TEST) ;
	gl.glDisableClientState(GL10.GL_TEXTURE_COORD_ARRAY) ;
	gl.glDisableClientState(GL10.GL_COLOR_ARRAY) ;
    }
    /**
     * æ��ç”»<br>
     * å†…éƒ¨ã�«æŒ�ã�£ã�¦ã�„ã‚‹ãƒ‡ãƒ¼ã‚¿ã‚’æ��ç”»ã�™ã‚‹
     */
    public void draw(GL10 gl) {

	draw(gl, 1.0f) ;
    }
    /**
     * æ��ç”»<br>
     * å†…éƒ¨ã�«æŒ�ã�£ã�¦ã�„ã‚‹ãƒ‡ãƒ¼ã‚¿ã‚’æ��ç”»ã�™ã‚‹
     *
     *@param alpha	æ��ç”»ã�™ã‚‹é€�æ˜Žåº¦ï¼ˆï¼�ï½žï¼‘ï¼‰
     */
    public void draw(GL10 gl, float alpha) {
	float[] fw = new float[4] ;
	if( glObj == null ) return ;
	gl.glPushMatrix() ;
	/* glEnableï¼�glDisableã�¯å‘¼ã�³å‡ºã�—å�´ã�®éƒ½å�ˆã�«ã‚ˆã�£ã�¦å¿…è¦�ã�ªã�„ï¼ˆã�‹ã‚‚ã�—ã‚Œã�ªã�„ï¼‰
	 * ã�®ã�§ã€�å¤–ã� ã�—(enables(float),disables())ã�«ã�—ã�Ÿã€‚
		gl.glEnable(GL.GL_DEPTH_TEST) ;
		gl.glEnable(GL.GL_ALPHA_TEST) ;
		if( scale != 1.0 ) {
			gl.glScalef(scale,scale,scale) ;
			gl.glEnable(GL.GL_NORMALIZE) ;
		}
	 */
	for( int o = 0 ; o < glObj.length ; o++ ) {
	    GLObject glo = glObj[o] ;
	    if( glo == null ) continue ;
	    if( ! glo.isVisible) continue ;
	    for( int m = 0 ; m < glo.mat.length ; m++ ) {
		GLMaterial mat = glo.mat[m] ;
		if( mat == null ) continue ;
		if( ! mat.isVisible ) continue ;
		boolean useAlpha = false ;
		//OpenGLã�®æ��ç”»ãƒ•ãƒ©ã‚°è¨­å®š
		if( mat.texID != 0 ) {
		    gl.glTexParameterx(GL10.GL_TEXTURE_2D, GL10.GL_TEXTURE_WRAP_S, GL10.GL_CLAMP_TO_EDGE);
		    gl.glTexParameterx(GL10.GL_TEXTURE_2D, GL10.GL_TEXTURE_WRAP_T, GL10.GL_CLAMP_TO_EDGE);
//		    gl.glEnable(GL.GL_TEXTURE_2D) ;
		}

		if( mat.shadeMode_IsSmooth ) {
		    gl.glShadeModel(GL10.GL_SMOOTH) ;
		}
		else {
		    gl.glShadeModel(GL10.GL_FLAT) ;
		}

//		gl.glEnable(GL.GL_BLEND) ;
//		gl.glBlendFunc(GL.GL_SRC_ALPHA,GL.GL_ONE_MINUS_SRC_ALPHA) ;

		//è‰²é–¢ä¿‚ã�®è¨­å®š
		gl.glColor4f(mat.color[0],mat.color[1],mat.color[2],mat.color[3]) ;
		if( mat.dif != null ) {//æ‹¡æ•£å��å°„æˆ�åˆ†ï¼šç‰©ä½“ã�®è‰²
//		    gl.glMaterialfv(GL.GL_FRONT_AND_BACK,GL.GL_DIFFUSE,mat.dif,0) ;
		    System.arraycopy(mat.dif,0,fw,0,mat.dif.length) ;
		    fw[3]*=alpha ;
		    gl.glMaterialfv(GL10.GL_FRONT_AND_BACK,GL10.GL_DIFFUSE,fw,0) ;

		    // @@@
		    useAlpha = fw[3] < 1.0f ;
		}
		if( mat.amb != null ) gl.glMaterialfv(GL10.GL_FRONT_AND_BACK,GL10.GL_AMBIENT,mat.amb,0) ;//ç’°å¢ƒå…‰
		if( mat.spc != null ) {//é�¡é�¢å��å°„æˆ�åˆ† : ã��ã‚‰ã‚�ã��ã�®è‰²
//		    gl.glMaterialfv(GL.GL_FRONT_AND_BACK,GL.GL_SPECULAR,mat.spc,0) ;
		    System.arraycopy(mat.spc,0,fw,0,mat.spc.length) ;
		    fw[3]*=alpha ;
		    gl.glMaterialfv(GL10.GL_FRONT_AND_BACK,GL10.GL_SPECULAR,fw,0) ;

		    // @@@ ã�“ã�£ã�¡ã�¯åˆ¤æ–­ã�«ã�„ã‚Œã�ªã�„
		    // useAlpha = useAlpha || fw[3] < 1.0f ;
		}
		if( mat.emi != null ) gl.glMaterialfv(GL10.GL_FRONT_AND_BACK,GL10.GL_EMISSION,mat.emi,0) ;//æ”¾å°„è¼�åº¦
		if( mat.power != null ) gl.glMaterialf(GL10.GL_FRONT_AND_BACK,GL10.GL_SHININESS,mat.power[0]) ;//é�¡é�¢ä¿‚æ•°

		//ãƒ†ã‚¯ã‚¹ãƒ�ãƒ£ã�®è¨­å®š
		if( mat.texID != 0 ) {
		    gl.glBindTexture(GL10.GL_TEXTURE_2D,mat.texID) ;
		}

		if (useAlpha) {
			gl.glEnable(GL10.GL_BLEND) ;
			gl.glBlendFunc(GL10.GL_SRC_ALPHA, GL10.GL_ONE_MINUS_SRC_ALPHA) ;
		} else {
			gl.glDisable(GL10.GL_BLEND) ;
		}

		//æ��ç”»ãƒ‡ãƒ¼ã‚¿è¨­å®š
		if (mat.uvValid) {
			// Log.i("KGLModelData", "uvValid");

			mat.uvBuffer.position(0);
			gl.glTexCoordPointer(2, GL10.GL_FLOAT, 0, mat.uvBuffer);
			gl.glEnableClientState(GL10.GL_TEXTURE_COORD_ARRAY);
		} else {
			gl.glDisableClientState(GL10.GL_TEXTURE_COORD_ARRAY);
		}
		if (mat.colValid) {
			// Log.i("KGLModelData", "colValid");

			mat.colBuffer.position(0);
			gl.glColorPointer(4, GL10.GL_FLOAT, 0, mat.colBuffer);
			gl.glEnableClientState(GL10.GL_COLOR_ARRAY);
		} else {
			gl.glDisableClientState(GL10.GL_COLOR_ARRAY);
		}
		mat.vertexBuffer.position(0);
		mat.normalBuffer.position(0);
		gl.glVertexPointer(3, GL10.GL_FLOAT, 0, mat.vertexBuffer);
		gl.glNormalPointer(GL10.GL_FLOAT, 0, mat.normalBuffer);
		gl.glEnableClientState(GL10.GL_NORMAL_ARRAY);
		gl.glEnableClientState(GL10.GL_VERTEX_ARRAY);

		//æ��ç”»å®Ÿè¡Œ
		gl.glDrawArrays(GL10.GL_TRIANGLES,0,mat.vertex_num) ;
		//gl.glDrawElements(GL10.GL_TRIANGLES, mat.indexCount, 
		//					GL10.GL_UNSIGNED_SHORT, mat.indexBuffer);

		//è¨­å®šã‚’ã‚¯ãƒªã‚¢ã�™ã‚‹
		if( mat.texID != 0 ) {
		    gl.glBindTexture(GL10.GL_TEXTURE_2D,0) ;
		}
//		gl.glDisable(GL.GL_BLEND) ;
//		if( mat.texID != 0 ) {
//		gl.glDisable(GL.GL_TEXTURE_2D) ;
//		}
	    }
	}
	/*
		if( scale != 1.0 ) {
			gl.glDisable(GL.GL_NORMALIZE) ;
		}
		gl.glDisable(GL.GL_ALPHA_TEST) ;
		gl.glDisable(GL.GL_DEPTH_TEST) ;
	 */
	gl.glPopMatrix() ;
    }

    /**
     * æ–‡å­—åˆ—ï¼†ãƒ�ã‚¤ãƒŠãƒªãƒ‡ãƒ¼ã‚¿æ··å�ˆèª­ã�¿è¾¼ã�¿ã‚¯ãƒ©ã‚¹
     * 
     */
    protected class multiInput {
	/**
	 * èª­ã�¿è¾¼ã�¿ã‚¹ãƒˆãƒªãƒ¼ãƒ 
	 */
	private BufferedInputStream bis = null ;
	private BufferedReader br = null ;
	/**
	 * ã‚³ãƒ³ã‚¹ãƒˆãƒ©ã‚¯ã‚¿<br>
	 * @param is	å…¥åŠ›ã‚¹ãƒˆãƒªãƒ¼ãƒ 
	 */
	public multiInput(InputStream is) {
	    bis = new BufferedInputStream(is) ;
		br = new BufferedReader(new InputStreamReader(is), 8*1024) ;
	}
	/**
	 * ãƒ‡ãƒ¼ã‚¿èª­ã�¿è¾¼ã�¿<br>
	 * ã‚¹ãƒˆãƒªãƒ¼ãƒ ã�‹ã‚‰b.lengthã‚µã‚¤ã‚ºã�®ãƒ‡ãƒ¼ã‚¿ã‚’èª­ã�¿è¾¼ã‚‚ã�†ã�¨ã�™ã‚‹<br>
	 * å®Ÿéš›ã�«èª­ã�¿è¾¼ã‚“ã� ã‚µã‚¤ã‚ºã�¯returnå€¤ã‚’å�‚ç…§<br>
	 * @param b	èª­ã�¿è¾¼ã�¿ãƒ�ãƒƒãƒ•ã‚¡
	 * @return	èª­ã�¿è¾¼ã�¿ã‚µã‚¤ã‚º
	 * @throws IOException
	 */
	public int read(byte[] b) throws IOException {
	    return bis.read(b) ;
	}
	/**
	 * ã‚¹ãƒˆãƒªãƒ¼ãƒ ã‚’ã‚¯ãƒ­ãƒ¼ã‚ºã�™ã‚‹
	 * @throws IOException
	 */
	public void close()throws IOException {
	    bis.close() ;
	}
	/**
	 * ï¼‘è¡Œï¼ˆæ–‡å­—åˆ—ï¼‰èª­ã�¿è¾¼ã�¿<br>
	 * 1 è¡Œã�®çµ‚ç«¯ã�¯ã€�æ”¹è¡Œ (ã€Œ\nã€�) ã�‹ã€�å¾©å¸° (ã€Œ\rã€�)ã€�ã�¾ã�Ÿã�¯å¾©å¸°ã�¨ã��ã‚Œã�«ç¶šã��æ”¹è¡Œ<br>
	 * ã�“ã�®é–¢æ•°ã�®ã�‚ã�¨ã€�èª­ã�¿è¾¼ã�¿ä½�ç½®ã�¯æ”¹è¡Œæ–‡å­—ã�®æ¬¡ã�«é€²ã‚€<br>
	 * @return	ï¼‘è¡Œã�®ãƒ‡ãƒ¼ã‚¿
	 */
	public String readLine() {
	    String ret = null ;
	    try {
		ret = br.readLine() ;
	    }
	    catch(Exception e) {
		e.printStackTrace() ;
	    }

	    return ret ;
	}
    }
    /**
     * 
     */
    public String toString() {
	String ret = null ;
	String retCode = (String)System.getProperties().get("line.separator") ;
	StringBuffer sb = new StringBuffer() ;
	if( glObj == null ) return "ãƒ‡ãƒ¼ã‚¿ã�ªã�—" ;
	sb.append("ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆå��(ãƒžãƒ†ãƒªã‚¢ãƒ«å��,...ï¼‰").append(retCode) ;
	for( int o = 0 ; o < glObj.length ; o++ ) {
	    sb.append(glObj[o].name).append("(") ;
	    for( int m = 0 ; m < glObj[o].mat.length ; m++ ) {
		sb.append(glObj[o].mat[m].name).append(",");
	    }
	    sb.append(")").append(retCode) ;
	}
	ret = sb.toString();
	return ret ;
    }

    
	// Chargement des textures des modèles
	public void reloadTexture(GL10 gl) {
		// Log.i("KGLModelData", "reloadTexture in");

		for (int o = 0; o < glObj.length; o++) {
			GLObject glo = glObj[o];
			if (glo == null)	continue ;
			for (int m = 0; m < glo.mat.length; m++) {
				GLMaterial mat = glo.mat[m];
				if (mat.texName != null) {
					mat.texID = texPool.getGLTexture(gl, mat.texName,
													 mat.alphaTexName, true);
				}
			}
		}
	}

	public void resetTexture() {
		for (int o = 0; o < glObj.length; o++) {
			GLObject glo = glObj[o];
			if (glo == null)	continue ;
			for (int m = 0; m < glo.mat.length; m++) {
				GLMaterial mat = glo.mat[m];
				if (mat.texName != null) {
					texPool.reset(null, mat.texName, mat.alphaTexName);
				}
			}
		}
	}
}
