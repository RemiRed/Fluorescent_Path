﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColorSymbolPasswordRandomizer : NetworkBehaviour {

	public Password passwordManager;
	public PasswordClues P1Clues, P2Clues;

	public List<PasswordButton> unsetPasswordButtons;

	public List<Color> symbolColors;
	public List<Material> symbols;

	public int passwordLengt;

	// Use this for initialization
	void Start () {

		StartPuzzle ();
	}

	public void StartPuzzle(){

		passwordManager = GameObject.FindGameObjectWithTag ("PasswordManager").GetComponent<Password> ();
		P1Clues = GameObject.FindGameObjectWithTag ("P1").GetComponent<PasswordClues> ();
		P2Clues = GameObject.FindGameObjectWithTag ("P2").GetComponent<PasswordClues> ();

		if (isServer) {

			int randomButtonRange = unsetPasswordButtons.Count;
			int _randomButton;
			int randomSymbolRange = symbols.Count;
			int _randomSymbol;
			int randomColorRange = symbolColors.Count;
			int _randomColor;

			for (int i = 1; i <= passwordLengt; i++) {

				_randomButton = Random.Range (0, randomButtonRange);	
				_randomSymbol = Random.Range (0, randomSymbolRange);
				_randomColor = Random.Range (0, randomColorRange);

				RpcSetRandomPassword (i,_randomButton,_randomSymbol,_randomColor);
				randomButtonRange--;
				randomSymbolRange--;
				randomColorRange--;
			}

			for (int j = 0; j < randomButtonRange; j++) {

				_randomSymbol = Random.Range (0, randomSymbolRange);
				RpcAddRestofPasswordButtons (j, _randomSymbol);
				randomSymbolRange--;
			}
		}
	}
		
	[ClientRpc]
	void RpcSetRandomPassword(int _index,int _randomButton, int _randomSymbol, int _randomColor){

		passwordManager.passwordButtons.Add (unsetPasswordButtons [_randomButton]);
		unsetPasswordButtons.RemoveAt (_randomButton);

		passwordManager.passwordButtons [_index-1].buttonOrderID = _index;
		passwordManager.passwordButtons [_index-1].GetComponent<Renderer>().material = symbols[_randomSymbol];

		P1Clues.SetClues(symbols[_randomSymbol], symbolColors[_randomColor]);
		P2Clues.SetClues(symbols[_randomSymbol], symbolColors[_randomColor]);

		symbols.RemoveAt (_randomSymbol);
		symbolColors.RemoveAt (_randomColor);

		passwordManager.passwordLength = passwordLengt; //Its unnessesary to have this variable update every time, but it also doesn't do any harm and was easy this way.
	}

	[ClientRpc]
	void RpcAddRestofPasswordButtons(int _index, int _randomSymbol){

		unsetPasswordButtons [_index].buttonOrderID = 0;
		passwordManager.passwordButtons.Add (unsetPasswordButtons [_index]);

		unsetPasswordButtons [_index].GetComponent<Renderer> ().material = symbols [_randomSymbol];
		symbols.RemoveAt (_randomSymbol);
	}
}