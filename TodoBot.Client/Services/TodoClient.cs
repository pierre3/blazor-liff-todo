﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TodoBot.Shared;

namespace TodoBot.Client.Services
{
    public class TodoClient : ITodoClient
    {
        private readonly string requestUrl;
        private readonly HttpClient httpClient;
       
        public TodoClient(HttpClient httpClient, string requestUrl)
        {
            this.requestUrl = requestUrl;
            this.httpClient = httpClient;
        }

        public async Task<IList<Todo>> GetTodoListAsync(string accessToken, string userId)
        {
            var response = await SendAsync(
                accessToken,
                HttpMethod.Get,
                $"{requestUrl}/api/{userId}/todoList");
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IList<Todo>>(body) ?? new List<Todo>();
        }

        public async Task<Todo> GetTodoAsync(string accessToken, string userId, string id)
        {
            var response = await SendAsync(
                accessToken,
                HttpMethod.Get,
                $"{requestUrl}/api/{userId}/todoList/{id}");
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Todo>(body);
        }

        public async Task UpdateTodoAsync(string accessToken, string id, Todo todo)
        {
            var json = JsonConvert.SerializeObject(todo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await SendAsync(
                accessToken,
                HttpMethod.Put,
                $"{requestUrl}/api/todoList/{id}",
                content);
        }

        public async Task CreateTodoAsync(string accessToken, Todo todo)
        {
            var json = JsonConvert.SerializeObject(todo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await SendAsync(
                accessToken,
                HttpMethod.Post,
                $"{requestUrl}/api/todoList",
                content);
        }

        public async Task DeleteTodoAsync(string accessToken, string userId, string id)
        {
            await SendAsync(
                accessToken,
                HttpMethod.Delete,
                $"{requestUrl}/api/{userId}/todoList/{id}");
        }


        private async Task<HttpResponseMessage> SendAsync(string accessToken, HttpMethod method, string url, HttpContent content = null)
        {
            var request = new HttpRequestMessage(method, url);
            request.Headers.Add(ApiServer.AccessTokenHeaderName, accessToken);
            request.Content = content;
            var response = await httpClient.SendAsync(request);
            return response.EnsureSuccessStatusCode();
        }
    }
}
