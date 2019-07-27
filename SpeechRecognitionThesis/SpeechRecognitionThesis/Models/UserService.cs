using SpeechRecognitionThesis.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models
{
    public class UserService : IUserService
    {
        private readonly IDataRepository<User> _dataRepository;

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public User Authenticate(string usernameString, string passwordString)
        {
            List<User> listUsers = _dataRepository.GetAll().ToList();

            return listUsers.First();
        }
    }
}
