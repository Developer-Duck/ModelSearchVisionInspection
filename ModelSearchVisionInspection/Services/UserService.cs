using ModelSearchVisionInspection.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ModelSearchVisionInspection.Services
{
    public class UserService
    {
        // user 폴더 안에 users.json 저장
        private readonly string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user");
        private readonly string jsonFileName = "users.json";
        private string JsonFilePath => Path.Combine(folderPath, jsonFileName);

        // SHA256 해시 계산 메서드
        private string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(bytes);
            }
        }

        public List<UserInfo> LoadUsersFromJson()
        {
            if (!File.Exists(JsonFilePath))
            {
                return new List<UserInfo>();
            }

            string json = File.ReadAllText(JsonFilePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<UserInfo>();
            }

            return JsonConvert.DeserializeObject<List<UserInfo>>(json) ?? new List<UserInfo>();
        }

        public bool ValidateUser(string username, string password)
        {
            try
            {
                // 기본 admin 계정 체크 (필요하면 해시 적용 가능)
                if (username == "admin" && password == "1234")
                {
                    return true;
                }

                List<UserInfo> users = LoadUsersFromJson();

                string hashedInputPassword = ComputeHash(password);

                return users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                                      u.Password == hashedInputPassword);
            }
            catch
            {
                return false;
            }
        }

        public void SaveUser(UserInfo user)
        {
            // user 폴더가 없으면 생성
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            List<UserInfo> users = LoadUsersFromJson();

            if (users.Any(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("이미 존재하는 계정입니다.");
            }

            user.Password = ComputeHash(user.Password);

            users.Add(user);

            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(JsonFilePath, json);
        }
    }
}
