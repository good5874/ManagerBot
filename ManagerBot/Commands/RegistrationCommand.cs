﻿using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.Entity;
using ManagerBot.DAL.Entity.Enums;
using ManagerBot.Models;

using Telegram.Bot.Args;

namespace ManagerBot.Commands
{
    public class RegistrationCommand : IBaseCommand
    {
        public string Name { get; } = "Регистрация";

        public RequestResultModel Execute(MessageEventArgs message, UserEntity user)
        {
            if (user.CurrentEvent == UserEventsEnum.FirstVisit)
            {
                user.CurrentEvent = UserEventsEnum.Registration;

                return new RequestResultModel()
                {
                    Message = "Введите ваше ФИО:",
                    User = user
                };
            }

            if(user.CurrentEvent == UserEventsEnum.Registration)
            {
                if (string.IsNullOrEmpty(user.FullName))
                {
                    if (string.IsNullOrEmpty(message.Message.Text))
                    {
                        return new RequestResultModel()
                        {
                            Message = "ФИО не может быть пустым",
                            User = user
                        };
                    }

                    user.FullName = message.Message.Text;

                    return new RequestResultModel()
                    {
                        Message = "Введите ваш Email:",
                        User = user
                    };
                }

                if (string.IsNullOrEmpty(user.Email))
                {
                    if (string.IsNullOrEmpty(message.Message.Text))
                    {
                        return new RequestResultModel()
                        {
                            Message = "Email не может быть пустым",
                            User = user
                        };
                    }

                    user.Email = message.Message.Text;
                    user.CurrentEvent = UserEventsEnum.ConfirmEmail;

                    //TODO: Отправить код на почту

                    return new RequestResultModel()
                    {
                        Message = "Вам на почту отправленно сообщение с кодом подтверждения. \n" +
                                  "Введите код:",
                        User = user
                    };
                }
            }

            if(user.CurrentEvent == UserEventsEnum.ConfirmEmail)
            {
                return null;
            }
            return null;
        }
    }
}
