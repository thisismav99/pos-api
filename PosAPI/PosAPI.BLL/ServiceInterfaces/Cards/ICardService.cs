﻿using Microsoft.EntityFrameworkCore;
using PosAPI.DAL.Models.Cards;

namespace PosAPI.BLL.ServiceInterfaces.Cards
{
    public interface ICardService<TContext> where TContext : DbContext
    {
        Task<Dictionary<bool, string>> AddCard(CardModel cardModel);
        Task<Dictionary<bool, string>> DeleteCard(Guid id);
        Task<Dictionary<bool, string>> UpdateCard(CardModel cardModel);
        Task<CardModel?> GetCard(Guid id);
        Task<List<CardModel>?> GetCards();
    }
}
