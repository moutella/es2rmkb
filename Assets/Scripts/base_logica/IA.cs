using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IA : MaoUsuario
{
	public MCTSNo monteCarloTree;
	public bool jogadorAtual;
	public ControladorJogo controlador;

	public IA(){
		this.pecas = new ArrayList();
        this.primeiraJogada = true;
        this.comprouPeca = false;
		controlador = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>();
	}
	public IA(ArrayList pecas,Boolean jogadorAtual){
		this.pecas=pecas;
		this.primeiraJogada=true;
		this.comprouPeca=false;
		this.jogadorAtual=jogadorAtual;
		controlador = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>();

	}
	public IA(MaoUsuario MaoUsuario){
		this.pecas=MaoUsuario.getPecas();
		this.primeiraJogada=MaoUsuario.getPrimeiraJogada();
		this.comprouPeca= MaoUsuario.getComprouPeca();
		this.controlador= GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>();
	}

	//public IA (MaoUsuario)

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

	public ArrayList retornaJogadasPossiveis(){
		ArrayList conjuntosDaMao = retornaTodosOsConjuntosDaMao();
		ArrayList jogadasDaMao = transformaTodosOsArrayListsEmJogadas(conjuntosDaMao);


		//Aqui farei um AddRange das outras antes de retornar
		return jogadasDaMao;
	}


	public ArrayList retornaTodosOsConjuntosDaMao(){
		ArrayList grupos = retornaTodosOsGrupos();
		ArrayList sequencias = retornaTodasAsSequencias();

		grupos.AddRange(sequencias);


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


	public ArrayList retornaInsercoes(Tabuleiro tabuleiro){
		//Tabuleiro tabuleiro = controlador.getTabuleiroAtual();
		Conjunto clone = null;
		ArrayList jogadas = new ArrayList();

		Jogada jogadaAtual = new Jogada();
		foreach(Conjunto c in tabuleiro.getConjuntos()){
			clone = c.cloneConjunto();
			retornaInsercoesBacktrackingFim(clone, jogadaAtual,jogadas, 0, c);
			//Fazer o do inicio
			retornaInsercoesBacktrackingInicio(clone, jogadaAtual,jogadas, this.pecas.Count-1, c);
		}

		return jogadas;
	}

	public void retornaInsercoesBacktrackingFim(Conjunto clone, Jogada jogadaAtual, ArrayList jogadas, int i, Conjunto real){
		//Debug.Log(i +", "+this.pecas.Count);
		if(i>=this.pecas.Count)return;
		
		Peca p = (Peca)this.pecas[i];
		clone.inserePeca(p);
		if(clone.getValida()){
			jogadaAtual.insereSubJogada(new SubJogada(p, SubJogada.INS, real, false));
			jogadas.Add(jogadaAtual.clonaJogada());
			retornaInsercoesBacktrackingFim(clone, jogadaAtual, jogadas, i+1, real);
			jogadaAtual.subjogadas.Clear();
		}
		clone.removePeca(p);
		retornaInsercoesBacktrackingFim(clone, jogadaAtual, jogadas, i+1, real);
	}

	public void retornaInsercoesBacktrackingInicio(Conjunto clone, Jogada jogadaAtual, ArrayList jogadas, int i, Conjunto real){
		if(i<0)return;

		Peca p = (Peca)this.pecas[i];
		clone.inserePecaAntes(p);
		if(clone.getValida()){
			jogadaAtual.insereSubJogada(new SubJogada(p, SubJogada.INS, real, true));
			jogadas.Add(jogadaAtual.clonaJogada());
			retornaInsercoesBacktrackingInicio(clone, jogadaAtual, jogadas, i-1, real);
			jogadaAtual.subjogadas.Clear();
		}
		clone.removePeca(p);
		retornaInsercoesBacktrackingInicio(clone,jogadaAtual,jogadas,i-1, real);
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

	public ArrayList transformaTodosOsArrayListsEmJogadas(ArrayList al){
		ArrayList resp = new ArrayList();
		foreach(ArrayList array in al){
			resp.Add(transformaArrayListEmJogada(array));
		}

		return resp;
	}


	public Jogada transformaArrayListEmJogada(ArrayList al){
		Jogada jogada = new Jogada();

		foreach(Conjunto c in al){
			jogada.insereSubJogada(new SubJogada(null, SubJogada.NOVO, c));
		}

		return jogada;
	}
	public IA clone(){
        IA newUsuario=(IA)new MaoUsuario();//Pq não new IA()?
        newUsuario.primeiraJogada=this.primeiraJogada;
        newUsuario.comprouPeca=this.comprouPeca;
        newUsuario.pecas=(ArrayList)this.pecas.Clone();
		newUsuario.controlador=this.controlador;
		newUsuario.jogadorAtual=this.jogadorAtual;
        return newUsuario;
    }

	public void jogar(Jogada jogada,Tabuleiro tabuleiro){
		foreach(SubJogada sj in jogada.subjogadas){
			int tipo=sj.tipo;
			if (tipo==0){   //Split

			}else if(tipo==1){      //inserção

			}else if(tipo==2){      //Novo
				tabuleiro.insereConjunto(sj.pai);
				foreach(Peca peca in sj.pai.getPecas()){
					this.removePeca(peca);	
				}		
			}else if(tipo==3){      //Move

			}
		}
		//return true;
	}
	
	public Jogada monteCarlo(MaoUsuario jogador,Tabuleiro tabuleiro){
			//Monte carlo possui 4 etapas:Seleção, expansão, simulação e backpropagation
			//Seleção: Escolhe uma jogada do estado atual(não é 100% aleatorio, ele leva em consideraçào as jogadas anteriores)
			//Expansão: Gera todas as jogadas possiveis do estado escolhido
			//Simulação: Escolhe aleatoriamente uma jogada possivel, passando pro nó novo e chamando novamente a expansão
			//Backpropagation: Volta o resultado(vitoria/derrota/empate) para o nó acima até chegar na raiz
			// Pra fazer o monte carlo é preciso criar uma estrutura que vai conter: O pai da jogada atual, quantidade de vitorias, as possiveis jogadas geradas por ele, e a quantidade de vezes que este nó foi visitado 
		return null;
	}

	public void jogarAleatorio(ArrayList jogadas,Tabuleiro tabuleiro){
			int tamArray=jogadas.Count;
			System.Random rnd=new System.Random();
			Jogada escolhido=(Jogada)jogadas[rnd.Next(tamArray)];
			jogar(escolhido,tabuleiro);	
	}

}