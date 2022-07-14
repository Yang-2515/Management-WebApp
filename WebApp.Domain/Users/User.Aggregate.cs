using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebApp.Domain.Boards;

namespace WebApp.Domain.Users
{
    public partial class User
    {
        public void AddBoard()
        {
            AddEvent(new AddUserEvent(this));
        }
        public void Update(string name, string linkImage, int age, string email, string address, string phone, string gender)
        {
            Name = name;
            LinkImage = linkImage;
            Age = age;
            EmailAddress = email;
            HomeAddress = address;
            Phone = phone;
            Gender = gender;
        }
        public List<Board> GetBoards()
        {
            List<Board> boards = new List<Board>();
            foreach (var listTask in Boards)
            {
                boards.Add(listTask);
            }
            return boards;
        }
        public void SetSecurity()
        {
            string pass = Password;
            string subString = pass.Substring(pass.Length - 2);
            FirstSecurityString = subString;
            LastSecurityString = subString;
            Password = GetMD5(pass, subString, subString);
        }
        public static string GetMD5(string str, string firstStr = "", string lastStr = "")
        {
            str = firstStr + str + lastStr;
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(str);
            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);
            foreach (byte b in mang)
            {
                str_md5 += b.ToString("x2");
            }
            return str_md5;
        }
        public void UpdateUsedRefreshToken(int refreshTokenId, bool used)
        {
            var refreshToken = RefreshTokens.FirstOrDefault(c => c.Id == refreshTokenId);
            refreshToken.Used = used;
        }
    }
}
