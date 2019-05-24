using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA
{

	class ComparadorPorCor : IComparer
	{
		int IComparer.Compare(Peca x, Peca y)
		{
			if (x.getCodigoCor() < y.getCodigoCor()) { return -1; }
			if (x.getCodigoCor() > y.getCodigoCor()) { return 1; }
			if (x.getValor() < y.getValor()) { return -1; }
			if (x.getValor() >= y.getValor()) { return 1; }
		}
	}


	class ComparadorPorValor : IComparer
	{
		int IComparer.Compare(Peca x, Peca y)
		{
			if (x.getValor() < y.getValor()) { return -1; }
			if (x.getValor() > y.getValor()) { return 1; }
			if (x.getCodigoCor() < y.getCodigoCor()) { return -1; }
			if (x.getCodigoCor() >= y.getCodigoCor()) { return 1; }
		}
	}


	private MaoUsuario mao;

	public IA(MaoUsuario m) { this.mao = m; }

	public MaoUsuario getMao() { return this.mao; }
	public void setMao(MaoUsuario m) { this.mao = m; }


	public ArrayList procuraConjunto(bool primeiraJogada)
	{
		ArrayList pecas = this.mao.getPecas();
		pecas.Sort(new ComparadorPorCor());
		Peca inicio;
		Peca anterior;
		int soma = 0;
		ArrayList conjunto = new ArrayList();
		foreach (Peca atual in pecas)
		{
			if (inicio = null)
			{
				inicio = atual;
				anterior = atual;
				soma += atual.getValor();
				conjunto.Add(atual);
			}
			else
			{
				if (atual.getValor() - anterior.getValor() = 1 && atual.getCodigoCor() = anterior.getCodigoCor())
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


	public void jogada()
	{
		if (this.mao.getPrimeiraJogada())
		{
			/*
			procura Conjunto >= 30 pontos
			if (Conjunto existe)
			{
				realiza jogada
				this.primeiraJogada = false;
			}
			else
			{
				saca do Deck
			}
			*/
		}
		else
		{
			/*
			procura jogada possivel
			if (jogada existe)
			{
				realiza jogada
			}
			else
			{
				saca do Deck
			}
			*/
		}
	}

}