using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;

namespace TodoBot.Shared
{
    /// <summary>
    /// Todo Model
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class Todo
    {
        /// <summary>
        /// レコードID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ユーザーID。LIFFクライアントから取得したLINEに紐づくユーザーID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// ステータス
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// 期日
        /// </summary>
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime DueDate { get; set; }

        public Todo()
        {

        }

    }
}