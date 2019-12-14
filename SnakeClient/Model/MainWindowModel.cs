using Newtonsoft.Json;
using RestSharp;
using SnakeClient.DataTransferObjects;

namespace Snake
{
    class MainWindowModel
    {
        private RestClient restClient;

        public MainWindowModel(string serverAdress)
        {
            restClient = new RestClient(serverAdress);
        }

        public GameStateResponseBody GetGameState()
        {
            RestRequest request = new RestRequest("/api/player/gameboard", dataFormat: RestSharp.DataFormat.Json);
            IRestResponse<GameStateResponseBody> response = restClient.Execute<GameStateResponseBody>(request);
            return JsonConvert.DeserializeObject<GameStateResponseBody>(response.Content);
        }

        public NameResponseBody GetName(string token)
        {
            RestRequest request = new RestRequest("/api/player/name", dataFormat: RestSharp.DataFormat.Json);
            request.AddParameter("token", token);
            IRestResponse<NameResponseBody> response = restClient.Execute<NameResponseBody>(request);
            return JsonConvert.DeserializeObject<NameResponseBody>(response.Content);
        }

        public void PostDirection(DirectionResponseBody directionRequest)
        {
            RestRequest request = new RestRequest("/api/player/direction", Method.POST, dataFormat: RestSharp.DataFormat.Json);
            request.AddJsonBody(directionRequest);
            restClient.Execute(request);
        }
    }
}
