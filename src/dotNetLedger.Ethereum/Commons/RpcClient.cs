using Microsoft.Extensions.Logging;
using Nethereum.JsonRpc.Client;
using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace dotNetLedger.Ethereum.Commons
{
    public class RpcClient : ClientBase
    {
        private const int NUMBER_OF_SECONDS_TO_RECREATE_HTTP_CLIENT = 60;

        private const int NUMBER_OF_MINUTES_TO_POOL_CONNECTIONS = 10;

        private const int NUMBER_OF_MINUTES_TO_IDLE_CONNECTIONS = 5;

        private readonly AuthenticationHeaderValue _authHeaderValue;

        private readonly Uri _baseUrl;

        private readonly HttpClientHandler _httpClientHandler;

        private readonly ILogger _log;

        private readonly JsonSerializerSettings _jsonSerializerSettings;

        private volatile bool _firstHttpClient;

        private HttpClient _httpClient;

        private HttpClient _httpClient2;

        private bool _rotateHttpClients = true;

        private DateTime _httpClientLastCreatedAt;

        private readonly object _lockObject = new object();

        public static int MaximumConnectionsPerServer { get; set; } = 20;

        public RpcClient(Uri baseUrl, AuthenticationHeaderValue authHeaderValue = null, JsonSerializerSettings jsonSerializerSettings = null, HttpClientHandler httpClientHandler = null, ILogger log = null)
        {
            _baseUrl = baseUrl;
            if (authHeaderValue == null)
            {
                authHeaderValue = BasicAuthenticationHeaderHelper.GetBasicAuthenticationHeaderValueFromUri(baseUrl);
            }

            _authHeaderValue = authHeaderValue;
            if (jsonSerializerSettings == null)
            {
                jsonSerializerSettings = DefaultJsonSerializerSettingsFactory.BuildDefaultJsonSerializerSettings();
            }

            _jsonSerializerSettings = jsonSerializerSettings;
            _httpClientHandler = httpClientHandler;
            _log = log;
            _httpClient = CreateNewHttpClient();
            _rotateHttpClients = false;
        }

        private static HttpMessageHandler GetDefaultHandler()
        {
            try
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(10L),
                    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5L),
                    MaxConnectionsPerServer = MaximumConnectionsPerServer
                };
            }
            catch
            {
                return null;
            }
        }

        public RpcClient(Uri baseUrl, HttpClient httpClient, AuthenticationHeaderValue authHeaderValue = null, JsonSerializerSettings jsonSerializerSettings = null, ILogger log = null)
        {
            _baseUrl = baseUrl;
            if (authHeaderValue == null)
            {
                authHeaderValue = BasicAuthenticationHeaderHelper.GetBasicAuthenticationHeaderValueFromUri(baseUrl);
            }

            _authHeaderValue = authHeaderValue;
            if (jsonSerializerSettings == null)
            {
                jsonSerializerSettings = DefaultJsonSerializerSettingsFactory.BuildDefaultJsonSerializerSettings();
            }

            _jsonSerializerSettings = jsonSerializerSettings;
            _log = log;
            InitialiseHttpClient(httpClient);
            _httpClient = httpClient;
            _rotateHttpClients = false;
        }

        protected override async Task<RpcResponseMessage[]> SendAsync(RpcRequestMessage[] requests)
        {
            RpcLogger logger = new RpcLogger(_log);
            try
            {
                HttpClient orCreateHttpClient = GetOrCreateHttpClient();
                string text = JsonConvert.SerializeObject(requests, _jsonSerializerSettings);
                StringContent content = new StringContent(text, Encoding.UTF8, "application/json");
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(ClientBase.ConnectionTimeout);
                logger.LogRequest(text);
                HttpResponseMessage obj = await orCreateHttpClient.PostAsync(string.Empty, content, cancellationTokenSource.Token).ConfigureAwait(continueOnCapturedContext: false);
                obj.EnsureSuccessStatusCode();
                using StreamReader reader = new StreamReader(await obj.Content.ReadAsStreamAsync().ConfigureAwait(continueOnCapturedContext: false));
                using JsonTextReader reader2 = new JsonTextReader(reader);
                return JsonSerializer.Create(_jsonSerializerSettings).Deserialize<RpcResponseMessage[]>(reader2);
            }
            catch (TaskCanceledException innerException)
            {
                RpcClientTimeoutException ex = new RpcClientTimeoutException($"Rpc timeout after {ClientBase.ConnectionTimeout.TotalMilliseconds} milliseconds", innerException);
                logger.LogException(ex);
                throw ex;
            }
            catch (Exception innerException2)
            {
                RpcClientUnknownException ex2 = new RpcClientUnknownException("Error occurred when trying to send multiple rpc requests(s)", innerException2);
                logger.LogException(ex2);
                throw ex2;
            }
        }

        public override async Task<RpcResponseMessage> SendAsync(RpcRequestMessage request, string route = null)
        {
            RpcLogger logger = new RpcLogger(_log);
            try
            {
                HttpClient orCreateHttpClient = GetOrCreateHttpClient();
                string text = JsonConvert.SerializeObject(request, _jsonSerializerSettings);
                StringContent content = new StringContent(text, Encoding.UTF8, "application/json");
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(ClientBase.ConnectionTimeout);
                logger.LogRequest(text);
                HttpResponseMessage obj = await orCreateHttpClient.PostAsync(route, content, cancellationTokenSource.Token).ConfigureAwait(continueOnCapturedContext: false);
                obj.EnsureSuccessStatusCode();
                using StreamReader reader = new StreamReader(await obj.Content.ReadAsStreamAsync().ConfigureAwait(continueOnCapturedContext: false));
                using JsonTextReader reader2 = new JsonTextReader(reader);
                RpcResponseMessage rpcResponseMessage = JsonSerializer.Create(_jsonSerializerSettings).Deserialize<RpcResponseMessage>(reader2);
                logger.LogResponse(rpcResponseMessage);
                return rpcResponseMessage;
            }
            catch (TaskCanceledException innerException)
            {
                RpcClientTimeoutException ex = new RpcClientTimeoutException($"Rpc timeout after {ClientBase.ConnectionTimeout.TotalMilliseconds} milliseconds", innerException);
                logger.LogException(ex);
                throw ex;
            }
            catch (Exception innerException2)
            {
                RpcClientUnknownException ex2 = new RpcClientUnknownException("Error occurred when trying to send rpc requests(s): " + request.Method, innerException2);
                logger.LogException(ex2);
                throw ex2;
            }
        }

        private HttpClient GetOrCreateHttpClient()
        {
            if (_rotateHttpClients)
            {
                lock (_lockObject)
                {
                    if ((DateTime.UtcNow - _httpClientLastCreatedAt).TotalSeconds > 60.0)
                    {
                        CreateNewRotatedHttpClient();
                    }

                    return GetClient();
                }
            }

            return GetClient();
        }

        private HttpClient GetClient()
        {
            if (_rotateHttpClients)
            {
                lock (_lockObject)
                {
                    return _firstHttpClient ? _httpClient : _httpClient2;
                }
            }

            return _httpClient;
        }

        private void CreateNewRotatedHttpClient()
        {
            HttpClient httpClient = CreateNewHttpClient();
            _httpClientLastCreatedAt = DateTime.UtcNow;
            if (_firstHttpClient)
            {
                lock (_lockObject)
                {
                    _firstHttpClient = false;
                    _httpClient2 = httpClient;
                    return;
                }
            }

            lock (_lockObject)
            {
                _firstHttpClient = true;
                _httpClient = httpClient;
            }
        }

        private HttpClient CreateNewHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            if (_httpClientHandler != null)
            {
                httpClient = new HttpClient(_httpClientHandler);
            }
            else
            {
                HttpMessageHandler defaultHandler = GetDefaultHandler();
                if (defaultHandler != null)
                {
                    httpClient = new HttpClient(defaultHandler);
                }
            }

            InitialiseHttpClient(httpClient);
            return httpClient;
        }

        private void InitialiseHttpClient(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization = _authHeaderValue;
            httpClient.BaseAddress = _baseUrl;
        }
    }
}
