using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithAspNet.Model;
using RestWithAspNet.Model.Context;

namespace RestWithAspNet.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private MySqlContext _context;

        public UserRepository(MySqlContext context)
        {
            _context = context;
        }


        public User FindByLogin(string login)
        {
            try
            {
                return _context.Users.SingleOrDefault(x => x.Login.Equals(login));
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
