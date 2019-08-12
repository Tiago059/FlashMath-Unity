using UnityEngine;

public class BackgroudMusicManager : MonoBehaviour {

	/*
		Esta classe é responsável por gerenciar a música de fundo que toca na tela-título
		e nos menus. Tem algumas configurações interessantes nela.
	*/

	public static BackgroudMusicManager Instance; // Instância da classe, para ser usada por outras classes
	private AudioSource music; // Componente AudioSource que tocará a música

	void Start(){

		// Atribuindo a classe para a instância dela chamada Instance. Mano isso é mt lindo *-*
		Instance = this;
		// Checando se der algo errado com essa instance
		if (Instance == null) { Debug.Log("erro no instance background sound manager"); }

		/* Se o gameObject que executa a música não existe, fazemos com que esse gameObject
		   seja criado e ainda, que não seja destruído quando outra cena for carregada. 
		   Mas somente se ele não existir, pois senão, vários outros gameObjects seriam
		   criados quando essa cena fosse chamada e então várias músicas tocariam ao mesmo
		   tempo. */
		if (PlayerSeetings.getBgmObjectExists() == false){ 
			// Cria o gameObject que executa a música e faz com que ela não suma ao ser carregada outra cena
			DontDestroyOnLoad(gameObject); 
			// Nas configurações do jogador, setamos o objeto estático que toca a música para que ela seja usada
			// durante toda a execução do jogo.
			PlayerSeetings.setBgmObject(GetComponent<AudioSource>());
		}

		// Uma vez criado ou setado o objeto da música, atribuimos a uma variável.
		this.music = PlayerSeetings.getBgmObject();
		// Agora playamos a música!
		play();
	}

	// Essa função advinha... TOCA A MÚSICA DE BACKGROUND!
	public void play(){ 
		// Se o objeto da música não existe, ele é criado e mudamos agora para que ele exista
		if (PlayerSeetings.getBgmObjectExists() == false){ PlayerSeetings.setBgmObjectExists(true); }

		// 	Só tocaremos a música se ela já não estiver sendo tocada
		if (PlayerSeetings.getBgmIsPlaying() == false) {
			// Toca a música (método do AudioSource)
			music.Play(); 
			// Mudamos a flag para todos que forem chamar play() saberem que a música já está sendo tocada
			PlayerSeetings.setBgmIsPlaying(true);
		}
	}

	// HMMM... o que será que essa função faz? 
	public void stop(){ 
		// Para a música (também é um método do AudioSource)
		music.Stop(); 
		// Se a música foi parada, então ela não está sendo tocada no momento
		PlayerSeetings.setBgmIsPlaying(false);
	}
}