package com.project.mars;

import java.util.ArrayList;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.view.ViewGroup.LayoutParams;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ScrollView;
import android.widget.TextView;

import com.project.manager.Constants;
import com.project.webservice.AppelService;

public class PromptDialog extends AlertDialog.Builder implements
		OnClickListener {

	final TextView txtVw_Msgs;
	final EditText edtTxt_SendMsg;
	
	 DialogInterface dial;

	String Login;
	// String Msg;
	ArrayList<String> Msgs;

	Context ctxt;

	public PromptDialog(Context context, String title, String message) {

		super(context);
		setTitle(title);
		setMessage(message);

		
		
		ctxt = context;
		txtVw_Msgs = new TextView(context);
		edtTxt_SendMsg = new EditText(context);

		LinearLayout lila1 = new LinearLayout(context);
		

		ScrollView scroll = new ScrollView(context);
		scroll.addView(lila1);
		lila1.setScrollBarStyle(0);
		lila1.setOrientation(LinearLayout.VERTICAL);
		//lila1.setPadding(0, 5, 5, 5);

	//	lila1.setScrollContainer(true);
		

		LinearLayout.LayoutParams TextParams = new LinearLayout.LayoutParams(
				LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT);

		lila1.addView(txtVw_Msgs, TextParams);
		lila1.addView(edtTxt_SendMsg, TextParams);

	//	lila1.addView(txtVw_Msgs, 400, 200);
	//	lila1.addView(edtTxt_SendMsg, 400, 40);

		this.setView(scroll);

		setPositiveButton("Envoyer", this);
		setNegativeButton("Annuler", this);
	}

	public void onCancelClicked(DialogInterface dialog) {
		Constants.dialogOpen = false;
		dialog.dismiss();
	}

	@Override
	public void onClick(DialogInterface dialog, int which) {
		if (which == DialogInterface.BUTTON_POSITIVE) {

			// onOkClicked(Integer.parseInt(i.getText().toString()),
			// Float.parseFloat(inputx.getText().toString()),
			// Float.parseFloat(inputy.getText().toString()),
			// Float.parseFloat(inputz.getText().toString()));
			// Toast.makeText(ctxt, "Working", Toast.LENGTH_SHORT).show();
			AppelService
					.SendMessage(Login, edtTxt_SendMsg.getText().toString());

			Constants.dialogOpen = false;
			dialog.dismiss();
		} else {
			onCancelClicked(dialog);
		}
	}

	public void SetParams(String login) {
		this.Login = login;
		// this.Msg=msg;

		if (Constants.Messages != null) {
			for (int i = 0; i < Constants.Messages.size(); i++)
				txtVw_Msgs.setText(txtVw_Msgs.getText().toString() + "\n"
						+ "Expert:\n" + Constants.Messages.get(i));
		}
	}

	public boolean onOkClicked(int i, float inputx, float inputy, float inputz) {
		// TODO Auto-generated method stub
		return false;
	}

	
}
