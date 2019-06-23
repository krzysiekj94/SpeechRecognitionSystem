using SpeechRecognitionThesis.Models.Database;
using SpeechRecognitionThesis.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.DataManager
{
    public class UsersDbManager : IDataRepository<User>
    {
        readonly SpeechRecognitonDbContext _speechRecognitionDbContext;

        public UsersDbManager(SpeechRecognitonDbContext context)
        {
            _speechRecognitionDbContext = context;
        }

        public void Add(User user)
        {
            _speechRecognitionDbContext.Users.Add(user);
            _speechRecognitionDbContext.SaveChanges();
        }

        public void Delete(User user)
        {
            _speechRecognitionDbContext.Users.Remove(user);
            _speechRecognitionDbContext.SaveChanges();
        }

        public User Get(long userId)
        {
            return _speechRecognitionDbContext.Users
                  .FirstOrDefault(user => user.UserId == userId);
        }

        public IEnumerable<User> GetAll()
        {
            return _speechRecognitionDbContext.Users.ToList();
        }

        public void Update(User user, User entity)
        {
            user.UserId = entity.UserId;
            user.NickName = entity.NickName;
            user.PasswordHash = entity.PasswordHash;
            user.CreateAccountDate = entity.CreateAccountDate;
            user.LastUpdateAccountDate = entity.LastUpdateAccountDate;
            user.IsActiveAccount = entity.IsActiveAccount;

            _speechRecognitionDbContext.SaveChanges();
        }
    }
}
