﻿using PhotoShare.Data;
using PhotoShare.Models;
using System;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    public class RegisterUserCommand
    {
        // RegisterUser <username> <password> <repeat-password> <email>
        public string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string email = data[3];

            if (password != repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

            User user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now
            };

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (context.Users.FirstOrDefault(u => u.Username == user.Username) != null)
                {
                    throw new InvalidOperationException($"Username {user.Username} is already taken!");
                }

                context.Users.Add(user);
                context.SaveChanges();
            }

            return "User " + user.Username + " was registered successfully!";
        }
    }
}
