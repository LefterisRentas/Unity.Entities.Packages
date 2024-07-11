using MOBA.Client;
using MOBA.Common;
using TMPro;
using Unity.Entities;
using Unity.NetCode;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace LefterisRentas.Dev.Helpers.Netcode.ConnectionManager
{
    public partial class ClientConnectionManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _addressField;
        [SerializeField] private TMP_InputField _portField;
        [SerializeField] private TMP_Dropdown _connectionModeDropdown;
        [SerializeField] protected TMP_Dropdown _teamDropdown;
        [SerializeField] private Button _connectButton;

        private ushort Port => ushort.Parse(_portField.text);
        private string Address => _addressField.text;

        private void OnEnable()
        {
            _connectionModeDropdown.onValueChanged.AddListener(OnConnectionModeChanged);
            _connectButton.onClick.AddListener(OnButtonConnect);
            OnConnectionModeChanged(_connectionModeDropdown.value);
        }

        private void OnDisable()
        {
            _connectionModeDropdown.onValueChanged.RemoveAllListeners();
            _connectButton.onClick.RemoveAllListeners();
        }

        private void OnConnectionModeChanged(int connectionMode)
        {
            string buttonLabel;
            _connectButton.enabled = true;

            switch (connectionMode)
            {
                case 0:
                    buttonLabel = "Start Host";
                    break;
                case 1:
                    buttonLabel = "Start Server";
                    break;
                case 2:
                    buttonLabel = "Start Client";
                    break;
                default:
                    buttonLabel = "<ERROR> Unknown Connection Mode";
                    _connectButton.enabled = false;
                    break;
            }

            var buttonText = _connectButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = buttonLabel;
        }

        private void OnButtonConnect()
        {
            DestroyLocalSimulationWorld();
            SceneManager.LoadScene(1);

            switch (_connectionModeDropdown.value)
            {
                case 0:
                    StartServerWithHooks();
                    StartClientWithHooks();
                    break;
                case 1:
                    StartServerWithHooks();
                    break;
                case 2:
                    StartClientWithHooks();
                    break;
                default:
                    Debug.LogError("High Severity ERROR: Unknown Connection Mode", gameObject);
                    break;
            }
        }

        private static void DestroyLocalSimulationWorld()
        {
            foreach (var world in World.All)
            {
                if (world.Flags == WorldFlags.Game)
                {
                    world.Dispose();
                    break;
                }
            }
        }

        private World StartServer()
        {
            var serverWorld = ClientServerBootstrap.CreateServerWorld("Game Server World");

            var serverEndpoint = NetworkEndpoint.AnyIpv4.WithPort(Port);

            using var networkDriverQuery = serverWorld.EntityManager.CreateEntityQuery(ComponentType.ReadWrite<NetworkStreamDriver>());
            networkDriverQuery.GetSingletonRW<NetworkStreamDriver>().ValueRW.Listen(serverEndpoint);
            return serverWorld;
        }

        private World StartClient()
        {
            var clientWorld = ClientServerBootstrap.CreateClientWorld("Game Client World");
            
            var connectionEndpoint = NetworkEndpoint.Parse(Address, Port);

            using var networkDriverQuery = clientWorld.EntityManager.CreateEntityQuery(ComponentType.ReadWrite<NetworkStreamDriver>());
            networkDriverQuery.GetSingletonRW<NetworkStreamDriver>().ValueRW.Connect(clientWorld.EntityManager, connectionEndpoint);
            
            World.DefaultGameObjectInjectionWorld = clientWorld;
            return clientWorld;
        }
    }
}