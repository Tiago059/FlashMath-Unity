using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
	Este script SENSACIONAL configura e exibe a tela de loading durante
	qualquer transição entre cenas que se queira. 

	Por exemplo, entre a cena do menu do Precisão e entre a cena de 
	carregar o jogo (Arcade ou Time Attack) há um pequeno delay que faz
	o jogador esperar antes de começar a jogar. Para uma melhor experiência,
	uma tela de loading cai muito bem. Basta chamar alguns atributos deste script.

	Para usar, primeiro vá aonde haverá a mudança de cena, basicamente, 
	onde tinha antes SceneManagement.LoadScene(). Para inserir a tela de 
	loading durante o carregamento da cena primeiro "ligue" a tela de loading
	através da instrução "ScenerLoader.startLoadingScene(true)". 
	Após isso, você deve dizer qual cena deverá ser carregada após o loading
	estar completo. Use a intrução 
	"ScenerLoader.setSceneToLoadAfterLoading(cenaASerCarregada)" para isso.
	Com tudo pronto, basta agora chamar a cena de loading, que ela será carregada
	e quando ela terminar ela irá chamar a cena que você quer. O comando é:
	"SceneManager.LoadScene("loadingScreen")".

*/
public class LoadingSceneManager : MonoBehaviour {

	// Flag que indica se a tela de loading foi carregada
	private bool loadScene = false; 
	// Flag estática para ser mudada por outras classes, para saber que a tela de loading será carregada
	private static bool loadLScene = false; 
	// Nome da cena a ser carregada após a tela de loading terminar seu loading
	private static string sceneName;

	// Atributos da barra de progresso
	/* As declarações [SerializeField] neste caso servem para deixar os 
	   atributos privados, mas ainda sendo visíveis no editor do Unity. */
	[SerializeField]
	private RectTransform barFillRectTransform; // Representa a imagem transformada em retângulo

	private Vector3 barFillLocalScale; // Representa a posição e as dimensões, no caso, do retângulo

	[SerializeField]
	private Text percentLoadedText; // O texto que exibe a porcentagem do progresso

	[SerializeField]
	private Text loadingText; // Texo escrito "Loading..." que ficará piscando

	void Awake(){ 
		// Neste momento as dimensões do retângulo são passadas para um Vector3
		barFillLocalScale = barFillRectTransform.localScale;
		// Desligando qualquer música que esteja tocando
		BackgroudMusicManager.Instance.stop();
	}

	void Update(){
		// Se a tela de loading está pronta para ser carregada e se a própria tela já não foi carregada
		if (loadLScene == true && loadScene == false){
			// Podemos fazer o que queremos fazer
			loadScene = true;
			// Colocamos o texto para aparecer
			loadingText.text = "Loading...";
			// Iniciamos uma Coroutine que carregará a cena e teremos acesso ao seu progresso
			/* Adendo: Coroutines são instruções que funcionam paralelamente ao Update(),
			   algo semelhante a uma Thread ou até mesmo como se fosse um outro Update(). */
			StartCoroutine(LoadNewScene());
		}
		// Se a cena foi carregada, fazemos o texto "Loading" piscar...
		if (loadScene == true){
			// Pois é.. é uma linha BEM grande mas fez em uma linha o que eu tinha precisado de um script inteiro para faze
			loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
		}
	}

	// Este método dá início a execução da tela de loading
	public static void startLoadingScene(bool b){ LoadingSceneManager.loadLScene = b; }
	// E aqui dizemos qual cena será carregada após o fim do loading
	public static void setSceneToLoadAfterLoading(string s){ LoadingSceneManager.sceneName = s; }

	// Pesquisar: IEnumerator
	IEnumerator LoadNewScene(){

		/* Não entendi exatamente, mas esta linha de código carrega uma cena 
		   do mesmo jeito usual que fazemos. A diferença é que agora podemos
		   ter acesso a informações como do tipo, o progresso do carregamento,
		   que é o que nos interessa. Tudo isto está na variável "async". 
		   O async guarda informações que são carregadas no background. */
		AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

		/* Outra informação é saber se a cena foi carregada. Enquanto ela não
		for, esta será o tempo que a tela de loading ficará na tela. */
		while (!async.isDone){ 
			Debug.Log("JOOJ");
			// E o progresso exato do carregamento é pegado através do async.progress
			float progress = async.progress;

			// Em testes,a tela carregava em 0.9. Como gostaria que 100% aparecesse na tela...
			if (progress > 0.89f) { progress = 1f; }

			// Aqui redimensionamos a largura do Vector3 de acordo com o progresso
			barFillLocalScale.x = progress;

			// E uma vez com um novo valor, redimensionamos o retângulo. Isto gera o efeito visual de carregamento
			barFillRectTransform.localScale = barFillLocalScale;

			// Para a porcentagem, basta arredondar o valor float do progress e multiplicar por 100
			percentLoadedText.text = Mathf.CeilToInt(progress * 100).ToString() + "%";

			// Pesquisar: para que serve isto?
			yield return null;
			Debug.Log("SAAS"); 
		}

	}
}