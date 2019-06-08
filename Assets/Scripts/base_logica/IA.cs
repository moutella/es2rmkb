using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MaoUsuario
{

	public IA(){
		this.pecas = new ArrayList();
        this.primeiraJogada = true;
        this.comprouPeca = false;
	}



/* 
	public ArrayList procuraConjunto(bool primeiraJogada)
	{
		this.arrumaPorCores();
		Peca inicio=null;
		Peca anterior=null;
		int soma = 0;
		Conjunto conjunto = new Conjunto();
		Jogada jogadaAtual = new Jogada();
		//SubJogada novaSubjogada;
		foreach (Peca atual in pecas)
		{
			if (inicio == null)
			{
				inicio = atual;
				anterior = atual;
				jogadaAtual.insereSubJogada(new SubJogada(atual, SubJogada.INS));
				conjunto.inserePeca(atual);
				//soma += atual.getValor();
				
			}
			else
			{
				if(atual.getCodigoCor()==anterior.getCodigoCor()){
					if (atual.getValor() - anterior.getValor() == 1){
						anterior = atual;
					}
				}
				if (atual.getValor() - anterior.getValor() == 1 && atual.getCodigoCor() == anterior.getCodigoCor())
				{
					anterior = atual;
					soma += atual.getValor();
					conjunto.Add(atual);
				}
				else if (conjunto.Count >= 3 && ((primeiraJogada && soma >= 30) || (!primeiraJogada)))
				{
					return conjunto;
				}
				else
				{
					conjunto.Clear();
					soma = conjunto.getValor();
				}
			}
		}
	}

	*/

	public void conjuntosBacktracking(ArrayList grupos, ArrayList respAtual, ArrayList resp, int k){
		for(int i=k;i<grupos.Count;i++){
			if(this.saoDisjuntos((Conjunto)grupos[i],respAtual)){
				respAtual.Add(grupos[i]);
				resp.Add((ArrayList)respAtual.Clone());
				((Conjunto)grupos[i]).printaPecas();
				conjuntosBacktracking(grupos, respAtual, resp, i);
				respAtual.Remove(grupos[i]);
			}	
		}
	}


	public ArrayList retornaTodosOsConjuntosDaMao(){
		ArrayList grupos = retornaTodosOsGrupos();
		//ArrayList sequencias = retornaTodasAsSequencias();

		foreach(Conjunto c in grupos){
			Debug.Log("Printando pecas retornadas no método inicial");
			c.printaPecas();
		}


		ArrayList jogadas = new ArrayList();
		ArrayList jogadaAtual = new ArrayList();
		
		Debug.Log("Conjuntos dentro do BackTracking");
		conjuntosBacktracking(grupos, jogadaAtual, jogadas, 0);

		return jogadas;
		
	}

	public ArrayList retornaTodasAsSequencias(){
		/*Conjunto conj = new Conjunto();
		Peca primeira = null;
		this.arrumaPorCores();
		foreach(Peca p in this.pecas){
			if(!p.ehCoringa()){
				if(primeira==null){
					primeira = p;
					conj.inserePeca(p);
				}
			}
		}*/

		//TODO

		return this.pecas;
	}

	public ArrayList retornaTodosOsGrupos(){
		this.arrumaSequencial();
		Peca coringa = null;
		Peca anterior = null;
		int n=0;
		Conjunto conjunto = new Conjunto();
		ArrayList resp = new ArrayList();
		foreach(Peca p in this.pecas){
			if(!p.ehCoringa()){
				if(anterior==null){
					//jogadaAtual.insereSubJogada(new SubJogada(p, SubJogada.INS));
					conjunto.inserePeca(p);

				}else{
					if(anterior.getValor()==p.getValor()){
						if(anterior.getCodigoCor()!=p.getCodigoCor()){
							//Nunca vai acontecer da cor ser diferente da peça imediatamente anterior mas igual a outra peça do conjunto
							//porque a ordenação sequencial também ordena secundariamente por cor
							conjunto.inserePeca(p);
						}
					}else{
						//Se são de valores diferentes este conjunto já não tem mais como aumentar(a não ser por coringas)
						n = conjunto.getNumPecas();
						if(n<4){
							coringa = achaCoringaForaDoConj(conjunto);
							while(coringa!=null && conjunto.getNumPecas()<4){
								//NO CASO DO GRUPO NÃO HÁ ORDEM, ENTÃO A LINHA ABAIXO DEVE FUNCIONAR CORRETAMENTE
								conjunto.inserePeca(coringa);
								if(conjunto.getValida()) resp.Add(conjunto.cloneConjunto());
								coringa = achaCoringaForaDoConj(conjunto);
							}
						}

						//Tomar cuidado, pois o conjunto continuaria "válido" se eu não iniciasse um novo com a peça atual
						conjunto.limpaConjunto();
						conjunto.inserePeca(p);

							
					}
				}		
			}

			//Verifica se o conjunto atual é válido. Se for, add no array de resposta
			if(conjunto.getValida()) resp.Add(conjunto.cloneConjunto());
			
			anterior = p;
		}

		return resp;
	}


	public Peca achaCoringaForaDoConj(Conjunto c){
		foreach(Peca p in this.pecas){
			if(p.ehCoringa() && !c.getPecas().Contains(p)){
				return p;
			}
		}

		return null;
	}

	public bool saoDisjuntos(Conjunto c, ArrayList al){
		foreach(Conjunto c2 in al){
			if(!c.ehDisjunto(c2)) return false;
		}

		return true;
	}

}