// Importa bibliotecas necess�rias
using UnityEngine; // Para usar funcionalidades do Unity
using Photon.Pun; // Para usar o Photon PUN (Photon Unity Networking)
using Photon.Realtime; // Para usar funcionalidades em tempo real do Photon

// Define a classe NetworkManager que herda de MonoBehaviourPunCallbacks
public class NetworkManager : MonoBehaviourPunCallbacks
{
    #region Singleton

    // Declara uma inst�ncia est�tica da classe NetworkManager
    public static NetworkManager instance;

    // M�todo chamado quando o script � inicializado
    private void Awake()
    {
        // Verifica se a inst�ncia � nula
        if (instance == null)
        {
            instance = this; // Define a inst�ncia para este objeto
            DontDestroyOnLoad(gameObject); // N�o destr�i o objeto ao carregar uma nova cena
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroi o objeto se j� houver uma inst�ncia existente
        }
    }

    #endregion

    
    // M�todo chamado antes do primeiro frame de atualiza��o
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Conecta ao servidor Photon usando configura��es definidas
    }
    
    // M�todo chamado quando conectado ao servidor mestre do Photon
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected Successful"); // Loga uma mensagem no console

        MenuManager.instance.Connected(); // Chama o m�todo Connected do menuManager
    }
    
    // M�todo para entrar em uma sala com um nome de sala e apelido
    public void JoinRoom(string roomName, string nickname)
    {
        PhotonNetwork.NickName = nickname; // Define o apelido do jogador
        PhotonNetwork.JoinRoom(roomName); // Tenta entrar na sala especificada
    }
    
    // M�todo para criar uma sala com um nome de sala e apelido
    public void CreateRoom(string roomName, string nickname)
    {
        PhotonNetwork.NickName = nickname; // Define o apelido do jogador
        PhotonNetwork.CreateRoom(roomName); // Cria uma sala com o nome especificado
    }

    // M�todo para sair da sala atual
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(); // Sai da sala atual
    }
    
    // M�todo chamado quando um jogador entra na sala
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player " + newPlayer.NickName + " joined room"); // Loga uma mensagem no console
        MenuManager.instance.UpdatePlayerList(GetPlayerList()); // Atualiza a lista de jogadores no menuManager
    }

    // M�todo chamado quando um jogador sai da sala
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player " + otherPlayer.NickName + " left room"); // Loga uma mensagem no console
        MenuManager.instance.UpdatePlayerList(GetPlayerList()); // Atualiza a lista de jogadores no menuManager
        MenuManager.instance.SetStartButton(PhotonNetwork.IsMasterClient); // Define o bot�o de iniciar se o jogador for o mestre da sala
    }
    
    // M�todo chamado quando o jogador entra na sala
    public override void OnJoinedRoom()
    {
        Debug.Log("Player " + PhotonNetwork.NickName + " joined room"); // Loga uma mensagem no console
        MenuManager.instance.UpdatePlayerList(GetPlayerList()); // Atualiza a lista de jogadores no menuManager
        MenuManager.instance.SetStartButton(PhotonNetwork.IsMasterClient); // Define o bot�o de iniciar se o jogador for o mestre da sala

        Vector2 spawnPosition = new Vector2( 900, 317); // Define uma posição de spawn aleatória
        PhotonNetwork.Instantiate("Car", spawnPosition, Quaternion.identity, 0); // Instancia o carro

    }
    
    // M�todo para carregar uma cena
    public void LoadScene(string sceneName)
    {
        photonView.RPC("LoadSceneRPC", RpcTarget.All, sceneName); // Chama um RPC para carregar a cena em todos os clientes
    }

    // M�todo RPC para carregar uma cena
    [PunRPC]
    private void LoadSceneRPC(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName); // Carrega a cena especificada
    }

    public GameObject Instantiate(string prefabName, Vector3 position, Quaternion rotation)
    {
        return PhotonNetwork.Instantiate(prefabName, position, rotation);
    }

    // M�todo para obter a lista de jogadores como string
    public string GetPlayerList()
    {
        string list = ""; // Inicializa uma string vazia

        // Itera sobre a lista de jogadores do Photon
        foreach (var player in PhotonNetwork.PlayerList)
        {
            list += player + "\n"; // Adiciona o jogador � string
        }

        return list; // Retorna a lista de jogadores
    }
}
