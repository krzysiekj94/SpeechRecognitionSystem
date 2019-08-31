using SpeechRecognitionThesis.Models.Database;
using SpeechRecognitionThesis.Models.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class AccountRepository : RepositoryBase<User>, IAccountRepository
    {
        RepositoryContext _repositoryContext;

        public AccountRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<User> Authenticate(string usernameString, string passwordString)
        {
            string encodedPassword = UserTools.ConvertInputTextToSha512( passwordString );
            User user = await Task.Run( () => _repositoryContext.Users.SingleOrDefault( x => x.NickName == usernameString && x.Password == encodedPassword) );

            return user;
        }

        public User GetAnonymousUser()
        {
            return FindAll().FirstOrDefault( user => user.NickName == UserTools.ANONYMOUS_USER_NICKNAME );
        }

        public User GetUser( long lUserId )
        {
            return FindAll().FirstOrDefault(user => user.Id == lUserId);
        }

        public bool UpdateLoggedUserData(User loggedUser)
        {
            if( loggedUser == null)
            {
                return false;
            }

            loggedUser.IsLogged = true;
            loggedUser.LastLoggedAccountDate = DateTime.Now.ToString();
            
            Update( loggedUser );

            return true;
        }

        public bool UpdateUserData( long lUserId, User user )
        {
            User userFromDb = GetUser( lUserId );
            string userNewPasswordString = UserTools.ConvertInputTextToSha512(user.Password);
            string userEmailString = user.Email;
            bool bUpdateUserDb = false;

            if( userFromDb != null )
            {
                bUpdateUserDb = true;

                if ( user.Password.Length > 0 
                    && userNewPasswordString != userFromDb.Password )
                {
                    userFromDb.Password = userNewPasswordString;
                    userFromDb.LastUpdateAccountDate = DateTime.Now.ToString();
                }

                if( userEmailString.Length > 0 
                    && userEmailString != userFromDb.Email )
                {
                    userFromDb.Email = userEmailString;
                }

                Update( userFromDb );
            }

            return bUpdateUserDb;
        }
    }
}
