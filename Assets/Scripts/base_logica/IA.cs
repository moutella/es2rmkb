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
				conjuntosBacktracking(grupos, respAtual, resp, i);
				respAtual.Remove(grupos[i]);
			}	
		}
	}


	public ArrayList retornaTodosOsConjuntosDaMao(){
		ArrayList grupos = retornaTodosOsGrupos();
		//ArrayList sequencias = retornaTodasAsSequencias();


		ArrayList jogadas = new ArrayList();
		ArrayList jogadaAtual = new ArrayList();
		
		conjuntosBacktracking(grupos, jogadaAtual, jogadas, 0);

		return jogadas;
		
	}

	public ArrayList retornaTodasAsSequencias(){
		Conjunto conjunto = new Conjunto();
		ArrayList resp = new ArrayList();
		int pecasNoConjunto = 0;
		int ultimaCor = -1;
		this.arrumaPorCores();

		int i, j;
		for(i = 0; i < this.pecas.Count; i++) {
			Peca pivo = (Peca) this.pecas[i];
			conjunto.inserePeca(pivo);
			ultimaCor = pivo.getCodigoCor();
			pecasNoConjunto = 1;

			for(j = i+1; j < this.pecas.Count; j++) {
				Peca auxiliar = (Peca) this.pecas[j];
				if(ultimaCor != auxiliar.getCodigoCor()) break; //Caso onde eu já to olhando uma cor diferente da primeira peça da sequencia
				
				conjunto.inserePeca(auxiliar); //Insiro a possivel proxima peça valida
				conjunto.printaPecas();
				pecasNoConjunto += 1; //Atribuo 1 ao número de elementos no conjunto
				if(conjunto.getValida()) resp.Add(conjunto.cloneConjunto()); // Se o conjunto estiver válido com esse elemento adicionado, adiciono na resposta
				else if (pecasNoConjunto >= 3) { // Caso onde o conjunto já ficou inválido, e já tem mais de 2 peças no conjunto, ou seja ele nao ficar mais valido se eu adicionar mais peças
					conjunto.limpaConjunto();
					break;
				}
			}
		}

		return resp;
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
				//Verifica se o conjunto atual é válido. Se for, add no array de resposta
				if(conjunto.getValida()) resp.Add(conjunto.cloneConjunto());		
			}

			
			
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


	public Jogada transformaArrayListEmJogada(ArrayList al){
		Jogada jogada = new Jogada();

		foreach(Conjunto c in al){
			jogada.insereSubJogada(new SubJogada(null, SubJogada.NOVO, c));
		}

		return jogada;
	}

}