using RestWithAspNet.Data.Converter;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet.Data.Converters
{
    public class UserConverter : IParser<UserVO, User>, IParser<User, UserVO>
    {
        public User Parse(UserVO origin)
        {
            if (origin != null)
            {
                return new User
                {
                    AccessKey = origin.AccessKey,
                    Login = origin.Login
                };
            }

            return null;
        }

        public UserVO Parse(User origin)
        {
            if (origin != null)
            {
                return new UserVO
                {
                    AccessKey = origin.AccessKey,
                    Login = origin.Login
                };
            }

            return null;
        }

        public List<User> ParseList(List<UserVO> origins)
        {
            if (origins != null && origins.Any())
            {
                return origins.Select(item => Parse(item)).ToList();
            }

            return null;
        }

        public List<UserVO> ParseList(List<User> origins)
        {
            if (origins != null && origins.Any())
            {
                return origins.Select(item => Parse(item)).ToList();
            }

            return null;
        }
    }
}
