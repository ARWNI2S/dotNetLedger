using Microsoft.Extensions.Logging;
using Solnet.Rpc.Converters;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Messages;
using Solnet.Rpc.Utilities;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace dotNetLedger.Solana.Commons
{
    internal abstract class JsonRpcClient
    {
        private readonly JsonSerializerOptions _serializerOptions;

        private readonly HttpClient _httpClient;

        private readonly ILogger _logger;

        private IRateLimiter _rateLimiter;

        public Uri NodeAddress { get; }

        protected JsonRpcClient(string url, ILogger logger = null, HttpClient httpClient = null, IRateLimiter rateLimiter = null)
        {
            _logger = logger;
            NodeAddress = new Uri(url);
            _httpClient = httpClient ?? new HttpClient
            {
                BaseAddress = NodeAddress
            };
            _rateLimiter = rateLimiter;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
            {
                (JsonConverter)new EncodingConverter(),
                (JsonConverter)new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
            };
        }

        protected async Task<RequestResult<T>> SendRequest<T>(JsonRpcRequest req)
        {
            string requestJson = JsonSerializer.Serialize(req, _serializerOptions);
            try
            {
                _rateLimiter?.Fire();
                _logger?.LogInformation(new EventId(req.Id, req.Method), "Sending request: " + requestJson);
                byte[] bytes = Encoding.UTF8.GetBytes(requestJson);
                using HttpRequestMessage httpReq = new HttpRequestMessage(HttpMethod.Post, (string?)null)
                {
                    Content = new ByteArrayContent(bytes)
                    {
                        Headers = { { "Content-Type", "application/json" } }
                    }
                };
                using HttpResponseMessage response = await _httpClient.SendAsync(httpReq).ConfigureAwait(continueOnCapturedContext: false);
                RequestResult<T> obj = await HandleResult<T>(req, response).ConfigureAwait(continueOnCapturedContext: false);
                obj.RawRpcRequest = requestJson;
                return obj;
            }
            catch (HttpRequestException ex)
            {
                RequestResult<T> result = new RequestResult<T>(ex.StatusCode ?? HttpStatusCode.BadRequest, ex.Message)
                {
                    RawRpcRequest = requestJson
                };
                _logger?.LogDebug(new EventId(req.Id, req.Method), "Caught exception: " + ex.Message);
                return result;
            }
            catch (Exception ex2)
            {
                RequestResult<T> result2 = new RequestResult<T>(HttpStatusCode.BadRequest, ex2.Message)
                {
                    RawRpcRequest = requestJson
                };
                _logger?.LogDebug(new EventId(req.Id, req.Method), "Caught exception: " + ex2.Message);
                return result2;
            }
        }

        private async Task<RequestResult<T>> HandleResult<T>(JsonRpcRequest req, HttpResponseMessage response)
        {
            RequestResult<T> result = new RequestResult<T>(response);
            try
            {
                RequestResult<T> requestResult = result;
                requestResult.RawRpcResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                _logger?.LogInformation(new EventId(req.Id, req.Method), "Result: " + result.RawRpcResponse);
                JsonRpcResponse<T> jsonRpcResponse = JsonSerializer.Deserialize<JsonRpcResponse<T>>(result.RawRpcResponse, _serializerOptions);
                if (jsonRpcResponse.Result != null)
                {
                    result.Result = jsonRpcResponse.Result;
                    result.WasRequestSuccessfullyHandled = true;
                }
                else
                {
                    JsonRpcErrorResponse jsonRpcErrorResponse = JsonSerializer.Deserialize<JsonRpcErrorResponse>(result.RawRpcResponse, _serializerOptions);
                    if (jsonRpcErrorResponse != null && jsonRpcErrorResponse.Error != null)
                    {
                        result.Reason = jsonRpcErrorResponse.Error.Message;
                        result.ServerErrorCode = jsonRpcErrorResponse.Error.Code;
                        result.ErrorData = jsonRpcErrorResponse.Error.Data;
                    }
                    else if (jsonRpcErrorResponse != null && jsonRpcErrorResponse.ErrorMessage != null)
                    {
                        result.Reason = jsonRpcErrorResponse.ErrorMessage;
                    }
                    else
                    {
                        result.Reason = "Something wrong happened.";
                    }
                }
            }
            catch (JsonException ex)
            {
                _logger?.LogDebug(new EventId(req.Id, req.Method), "Caught exception: " + ex.Message);
                result.WasRequestSuccessfullyHandled = false;
                result.Reason = "Unable to parse json.";
            }

            return result;
        }

        public async Task<RequestResult<JsonRpcBatchResponse>> SendBatchRequestAsync(JsonRpcBatchRequest reqs)
        {
            if (reqs == null)
            {
                throw new ArgumentNullException("reqs");
            }

            if (reqs.Count == 0)
            {
                throw new ArgumentException("Empty batch");
            }

            int id_for_log = reqs.Min((JsonRpcRequest x) => x.Id);
            string requestsJson = JsonSerializer.Serialize(reqs, _serializerOptions);
            try
            {
                _rateLimiter?.Fire();
                _logger?.LogInformation(new EventId(id_for_log, $"[batch of {reqs.Count}]"), "Sending request: " + requestsJson);
                byte[] bytes = Encoding.UTF8.GetBytes(requestsJson);
                using HttpRequestMessage httpReq = new HttpRequestMessage(HttpMethod.Post, (string?)null)
                {
                    Content = new ByteArrayContent(bytes)
                    {
                        Headers = { { "Content-Type", "application/json" } }
                    }
                };
                using HttpResponseMessage response = await _httpClient.SendAsync(httpReq).ConfigureAwait(continueOnCapturedContext: false);
                RequestResult<JsonRpcBatchResponse> obj = await HandleBatchResult(reqs, response).ConfigureAwait(continueOnCapturedContext: false);
                obj.RawRpcRequest = requestsJson;
                return obj;
            }
            catch (HttpRequestException ex)
            {
                RequestResult<JsonRpcBatchResponse> result = new RequestResult<JsonRpcBatchResponse>(ex.StatusCode ?? HttpStatusCode.BadRequest, ex.Message)
                {
                    RawRpcRequest = requestsJson
                };
                _logger?.LogDebug(new EventId(id_for_log, $"[batch of {reqs.Count}]"), "Caught exception: " + ex.Message);
                return result;
            }
            catch (Exception ex2)
            {
                RequestResult<JsonRpcBatchResponse> result2 = new RequestResult<JsonRpcBatchResponse>(HttpStatusCode.BadRequest, ex2.Message)
                {
                    RawRpcRequest = requestsJson
                };
                _logger?.LogDebug(new EventId(id_for_log, $"[batch of {reqs.Count}]"), "Caught exception: " + ex2.Message);
                return result2;
            }
        }

        private async Task<RequestResult<JsonRpcBatchResponse>> HandleBatchResult(JsonRpcBatchRequest reqs, HttpResponseMessage response)
        {
            int id_for_log = reqs.Min((JsonRpcRequest x) => x.Id);
            RequestResult<JsonRpcBatchResponse> result = new RequestResult<JsonRpcBatchResponse>(response);
            try
            {
                RequestResult<JsonRpcBatchResponse> requestResult = result;
                requestResult.RawRpcResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                _logger?.LogInformation(new EventId(id_for_log, $"[batch of {reqs.Count}]"), "Result: " + result.RawRpcResponse);
                JsonRpcBatchResponse jsonRpcBatchResponse = JsonSerializer.Deserialize<JsonRpcBatchResponse>(result.RawRpcResponse, _serializerOptions);
                if (jsonRpcBatchResponse != null)
                {
                    result.Result = jsonRpcBatchResponse;
                    result.WasRequestSuccessfullyHandled = true;
                }
                else
                {
                    JsonRpcErrorResponse jsonRpcErrorResponse = JsonSerializer.Deserialize<JsonRpcErrorResponse>(result.RawRpcResponse, _serializerOptions);
                    if (jsonRpcErrorResponse != null && jsonRpcErrorResponse.Error != null)
                    {
                        result.Reason = jsonRpcErrorResponse.Error.Message;
                        result.ServerErrorCode = jsonRpcErrorResponse.Error.Code;
                        result.ErrorData = jsonRpcErrorResponse.Error.Data;
                    }
                    else if (jsonRpcErrorResponse != null && jsonRpcErrorResponse.ErrorMessage != null)
                    {
                        result.Reason = jsonRpcErrorResponse.ErrorMessage;
                    }
                    else
                    {
                        result.Reason = "Something wrong happened.";
                    }
                }
            }
            catch (JsonException ex)
            {
                _logger?.LogDebug(new EventId(id_for_log, $"[batch of {reqs.Count}]"), "Caught exception: " + ex.Message);
                result.WasRequestSuccessfullyHandled = false;
                result.Reason = "Unable to parse json.";
            }

            return result;
        }
    }
}
